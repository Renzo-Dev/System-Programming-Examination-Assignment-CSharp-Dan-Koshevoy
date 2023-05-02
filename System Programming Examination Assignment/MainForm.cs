using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreadState = System.Threading.ThreadState;

namespace System_Programming_Examination_Assignment
{

    public partial class MainForm : Form
    {
        private Mutex mutex = new Mutex();
        private int _countFiles;
        private bool _threadsAborteds; // если все потоки ( поиск файлов / поиск слов ) закончили свою работу
        private readonly List<Drive> _drives = new List<Drive>();
        private readonly List<string> _listWords = new List<string>();
        private readonly List<Thread> _threads = new List<Thread>();

        public MainForm()
        {
            InitializeComponent();
            Size = new Size(257, 522);
        }

        // Удаляем запретное слово из listbox
        private void RemoveWord(object sender, EventArgs e)
        {
            if (lbBannedWords.SelectedIndex != -1)
            {
                _listWords.Remove(lbBannedWords.SelectedItem.ToString());
                lbBannedWords.Items.Remove(lbBannedWords.SelectedItem);
                lCountWords.Text = $@"Слов: {_listWords.Count}";
                if (_listWords.Count == 0)
                {
                    bStart.Enabled = false;
                } 
            }
        }

        private async void bFileLoadWords_Click(object sender, EventArgs e)
        {
            try
            {
                if (ofdLoadWords.ShowDialog() == DialogResult.OK)
                    using (var buffer = new StreamReader(ofdLoadWords.FileName))
                    {
                        var words = await buffer.ReadToEndAsync();
                        foreach (var word in words.Split(' ', '(', ')', '/', '\\', ',', ';', ':', '[', ']', '}', '{',
                                     '\'',
                                     '\"', '!', '?', '.', '-', '\n', '\t'))
                            await Task.Run(() =>
                            {
                                foreach (var str in _listWords)
                                    if (word.TrimEnd('\r') == str) // если у нас уже есть слово в списке
                                        return;

                                if (lbBannedWords.InvokeRequired)
                                    BeginInvoke(new Action(() =>
                                    {
                                        lbBannedWords.Items.Add(word);
                                        _listWords.Add(word);
                                    }));
                            });
                        lCountWords.Text = $@"Слов: {lbBannedWords.Items.Count}";
                    }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Error , Loading words from file" +
                                $@"{ex.Message}");
            }
        }

        private void bAddNewWord_Click(object sender, EventArgs e)
        {
            using (var form = new EnterNewWordForm())
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    string[] words = form.tbEnterWord.Text.Split(new char[]
                    {
                        ' ', '(', ')', '/', '\\', ',', ';', ':', '[', ']', '}', '{',
                        '\'',
                        '\"', '!', '?', '.', '-', '\n', '\t'
                    });
                    foreach (var word in words)
                    {
                        if (word != string.Empty)
                            try
                            {
                                foreach (var str in _listWords)
                                    if (word == str) // если у нас уже есть слово в списке
                                    {
                                        MessageBox.Show($@"Слово [ {word} ] , уже есть в списке");
                                        return;
                                    }

                                bStart.Enabled = true;
                                lbBannedWords.Items.Add(word);
                                lCountWords.Text = $@"Слов: {lbBannedWords.Items.Count}";
                                _listWords.Add(word);

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(@"Error , adding word to Listbox & List" +
                                                $@"{ex.Message}");
                            }
                    }
                }

            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // проверка на существующий экземпляр программы
                if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
                {
                    MessageBox.Show(@"Программа уже запущена", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Close();
                }

                // узнаем количество дисков и инф про них
               // foreach (var drive in DriveInfo.GetDrives()) _drives.Add(new Drive(drive.Name));
               _drives.Add(new Drive("D:\\"));
            }
            catch
            {
                //
            }
        }

        private void GetCountFiles()
        {
            _threadsAborteds = false;
            for (var i = 0; i < _drives.Count; i++)
            {
                // создаем поток для поиска текстовых файлов в каждом диске ( 1 поток = 1 диску )
                var i1 = i;
                _threads.Add(new Thread(() => GetFilesFromDirectories(_drives[i1].Name, i1)));
                _threads[i].Start();
            }

            // ждем все потоки ( сканирование дисков )
            try
            {
                foreach (var thread in _threads)
                    thread.Join();
            }
            catch
            {
                /* */
            }

            // продолжить выполнение кода, если потоки завершили поиск , и при этом не было нажато кнопка отмены
            if (_threadsAborteds == false)
            {
                // меняем статус на [поиск запретных слов]
                if (lStatusScanning.InvokeRequired)
                    lStatusScanning.BeginInvoke((MethodInvoker)delegate
                    {
                        lStatusScanning.ForeColor = Color.OrangeRed;
                        lStatusScanning.Text = @"Поиск запретных слов в файлах";
                    });

                if (pgbFindWords.InvokeRequired)
                    pgbFindWords.BeginInvoke((MethodInvoker)delegate
                    {
                        pgbFindWords.Maximum = _countFiles;
                        pgbFindWords.Style = ProgressBarStyle.Blocks;
                    });

                int cThreads = _threads.Count;
                try
                {
                    // начинаем поиск запретных слов ( по одному потоку на каждый диск )
                    for (var i = 0; i < _drives.Count; i++)
                    {
                        // создаем поток для поиска текстовых файлов в каждом диске ( 1 поток = 1 диску )
                        var i1 = i;
                        _threads[i] = new Thread(() => Test(_drives[i1]));
                        _threads[i].Start();
                    }

                    for (int i = 0; i < _drives.Count; i++)
                    {
                        _threads[i].Join();
                    }
                }
                catch
                {
                    //
                }
            }
        }

        private void Test(Drive drive)
        {
            mutex.WaitOne();
            int cThreads = _threads.Count;
            // разделяем количество файлов на Томе ( на 3 , для поиска )
            int first = drive.FilesPaths.Count / 3;
            int middle = drive.FilesPaths.Count - (first + first);
            int last = drive.FilesPaths.Count - (first + middle);
            // создаем 3 потока в них выполняем поиск

            int counter = 1;
            for (int j = cThreads; j < cThreads + 3; j++)
            {
                if (counter == 1)
                {
                    _threads.Add(new Thread(() =>
                    {
                        FileInfo fileInfo;
                        for (int ind = 0; ind < first; ind++)
                        {
                            fileInfo = new FileInfo(drive.FilesPaths[ind]);

                            // если 
                            if (fileInfo.Length > 1000000)
                            {
                                var index1 = ind;
                                // создаем поток в объекте drive ( тома )
                                drive._threads.Add(new Thread(() =>
                                    FindWord(drive.FilesPaths[index1])));
                                drive._threads.Last().Start();
                            }
                            else
                            {
                                FindWord(drive.FilesPaths[ind]);
                            }
                        }
                    }));
                    counter++;
                    _threads[j].Start();
                }
                else if (counter == 2)
                {
                    _threads.Add(new Thread(() =>
                    {
                        FileInfo fileInfo;
                        for (int ind = first; ind < middle; ind++)
                        {
                            fileInfo = new FileInfo(drive.FilesPaths[ind]);

                            // если 
                            if (fileInfo.Length > 1000000)
                            {
                                MessageBox.Show(fileInfo.DirectoryName);
                                var index1 = ind;
                                // создаем поток в объекте drive ( тома )
                                drive._threads.Add(new Thread(() =>
                                    FindWord(drive.FilesPaths[index1])));
                                drive._threads.Last().Start();
                            }
                            else
                            {
                                FindWord(drive.FilesPaths[ind]);
                            }
                        }
                    }));
                    counter++;
                    _threads[j].Start();
                }
                else if (counter == 3)
                {
                    _threads.Add(new Thread(() =>
                    {
                        for (int ind = middle; ind < last; ind++)
                        {
                            var fileInfo = new FileInfo(drive.FilesPaths[ind]);

                            // если 
                            if (fileInfo.Length > 1000000)
                            {
                                MessageBox.Show(fileInfo.DirectoryName);
                                var index1 = ind;
                                // создаем поток в объекте drive ( тома )
                                drive._threads.Add(new Thread(() =>
                                    FindWord(drive.FilesPaths[index1])));
                                drive._threads.Last().Start();
                            }
                            else
                            {
                                FindWord(drive.FilesPaths[ind]);
                            }
                        }
                    }));
                    _threads[j].Start();
                }
            }
            mutex.ReleaseMutex();
        }

        private void FindWord(string filePath)
        {
            bool state = false;
            // поиск слова в файле

            // список всех слов из файла
            string[] words = null;
            string fileContent;
            using (StreamReader reader = new StreamReader(filePath))
            {
                fileContent = reader.ReadToEnd();
                words = fileContent.Split(new char[]
                {
                    ' ', '(', ')', '/', '\\', ',', ';', ':', '[', ']', '}', '{',
                    '\'',
                    '\"', '!', '?', '.', '-', '\n', '\t'
                });
            }

            // проходим по списку запрещенных слов
            foreach (string bWord in lbBannedWords.Items)
            {
                // проходим по каждому слову из файла
                foreach (var word in words)
                {
                    // если запретно слово есть в файле
                    if (bWord == word)
                    {
                        if (state != true)
                        {
                            // создаем копию файла без замены текста

                        }
                        FileInfo fileInfo = new FileInfo(filePath);
                        // меняем запрещенное слово на звездочки
                        fileContent = fileContent.Replace(bWord, "*******");

                        state = true;
                    }
                }
            }

            // если в файле было запрещенное слово
            if (state)
            {
                // создаем копию файла с заменой текста


            }

            if (pgbFindWords.InvokeRequired)
            {
                pgbFindWords.BeginInvoke(new MethodInvoker(() => { pgbFindWords.PerformStep(); }));
            }
        }



        private async void bStart_Click(object sender, EventArgs e)
        {
            if (_listWords.Count > 0)
            {
                pgbFindWords.Style = ProgressBarStyle.Marquee;
                bStart.Enabled = false;
                bAddNewWord.Enabled = false;
                bFileLoadWords.Enabled = false;

                int kef = 7;
                // меняем размер окна
                for (; Width < MaximumSize.Width;)
                {
                    // задержка отрисовки 1 милисекунды
                    await Task.Delay(1);
                    // растягивание окна
                    // максимальная ширена окна отнимаем текущую ширину делим на коефициент 
                    Width += (MaximumSize.Width - Width) / kef + 1;
                    // выравнивание окна по центру при растягивании
                    Location = new Point(Location.X - ((MaximumSize.Width - Width) / kef + 3) / 2, Location.Y);
                }

                panelInformation.Visible = true;
                panelInformation.Enabled = true;

                lStatusScanning.ForeColor = Color.Goldenrod;
                lStatusScanning.Text = @"Поиск текстовых файлов";

                MinimumSize = new Size(736, 522);

                await Task.Run(GetCountFiles);
            }
        }

        private void GetFilesFromDirectories(string targetDirectory, int index)
        {
            try
            {
                // получаем список txt файлов в каталоге
                var fileEntries = Directory.GetFiles(targetDirectory, "*.txt");
                foreach (var filePath in fileEntries)
                {
                    _drives[index].FilesPaths.Add(filePath); // передаем индекс диска ( в нашем листе )
                    _countFiles++;
                }

                // получаем список ini файлов в каталоге
                fileEntries = Directory.GetFiles(targetDirectory, "*.ini");
                foreach (var filePath in fileEntries)
                {
                    _drives[index].FilesPaths.Add(filePath); // передаем индекс диска ( в нашем листе )
                    _countFiles++;
                }

                if (lCountFiles.InvokeRequired)
                    lCountFiles.BeginInvoke((MethodInvoker)delegate { lCountFiles.Text = _countFiles.ToString(); });

                // получаем подкаталоги этого каталога
                var subDirectoryEntries = Directory.GetDirectories(targetDirectory);
                // если есть подкаталог , ищем файлы и подкаталоги в подкаталогах ( рекурсия )
                foreach (var subDirectory in subDirectoryEntries) GetFilesFromDirectories(subDirectory, index);

            }
            catch
            {
                //
            }
        }

        [Obsolete("Obsolete")]
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (var thread in _threads)
                    if (thread != null)
                        if (thread.IsAlive)
                        {
                            if (thread.ThreadState == ThreadState.Suspended)
                            {
                                thread.Resume();
                                thread.Abort();
                            }
                            else
                            {
                                thread.Abort();
                            }
                        }
                _threads.Clear();
                foreach (var drive in _drives)
                {
                    foreach (var thread in drive._threads)
                    {
                        if (thread != null)
                            if (thread.IsAlive)
                            {
                                if (thread.ThreadState == ThreadState.Suspended)
                                {
                                    thread.Resume();
                                    thread.Abort();
                                }
                                else
                                {
                                    thread.Abort();
                                }
                            }
                    }
                    drive._threads.Clear();
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        [Obsolete("Obsolete")]
        private async void BPauseResume_click(object sender, EventArgs e)
        {
            bPauseResume.Text = bPauseResume.Text.Equals("Пауза") ? @"Возобновить" : @"Пауза";

            await Task.Run(() =>
            {
                foreach (var thread in _threads)
                    if (thread.IsAlive)
                    {
                        if (thread.ThreadState != ThreadState.Suspended)
                        {
                            thread.Suspend();
                            if (pgbFindWords.InvokeRequired)
                                pgbFindWords.BeginInvoke((MethodInvoker)delegate
                                {
                                    pgbFindWords.Style = ProgressBarStyle.Blocks;
                                });
                        }
                        else
                        {
                            thread.Resume();
                            if (pgbFindWords.InvokeRequired)
                                pgbFindWords.BeginInvoke((MethodInvoker)delegate
                                {
                                    pgbFindWords.Style = ProgressBarStyle.Marquee;
                                });
                        }
                    }
            });
        }

        [Obsolete("Obsolete")]
        private async void bCancel_Click(object sender, EventArgs e)
        {
            // останавливаем работу всех потоков ( поиск файлов/слов )
            await Task.Run(() =>
            {
                try
                {
                    foreach (var thread in _threads)
                        if (thread.IsAlive)
                        {
                            if (thread.ThreadState == ThreadState.Suspended)
                            {
                                thread.Resume();
                                thread.Abort();
                            }
                            else
                            {
                                thread.Abort();
                            }
                        }

                    foreach (var drive in _drives)
                    {
                        drive.FilesPaths.Clear();
                        foreach (var thread in drive._threads)
                        {
                            if (thread.IsAlive)
                            {
                                if (thread.ThreadState == ThreadState.Suspended)
                                {
                                    thread.Resume();
                                    thread.Abort();
                                }
                                else
                                {
                                    thread.Abort();
                                }
                            }
                        }
                        drive._threads.Clear();
                    }
                    _threads.Clear();

                    _threadsAborteds = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });

            _countFiles = 0;
            lCountFiles.Text = _countFiles.ToString();

            lStatusScanning.ForeColor = Color.Goldenrod;
            lStatusScanning.Text = @"Поиск текстовых файлов";

            MinimumSize = new Size(257, 522);

            int kef = 7;
            // меняем размер окна
            for (; Width > MinimumSize.Width;)
            {
                await Task.Delay(1);
                Width -= (MaximumSize.Width - Width) / kef + 1;
                Location = new Point(Location.X + ((MaximumSize.Width - Width) / kef - 3) / 2, Location.Y);
            }

            if (bPauseResume.Text.Equals("Возобновить")) bPauseResume.Text = @"Пауза";
            bStart.Enabled = true;
            bAddNewWord.Enabled = true;
            bFileLoadWords.Enabled = true;
            panelInformation.Visible = false;
            panelInformation.Enabled = false;
        }
    }

    /// <summary>
    ///     Класс хранит информацию о количестве текстовых файлов и количестве логических разделов
    /// </summary>
    internal class Drive
    {
        public List<Thread> _threads = new List<Thread>(); // потоки для поиска слов
        public List<string> FilesPaths = new List<string>();

        public Drive(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }
}