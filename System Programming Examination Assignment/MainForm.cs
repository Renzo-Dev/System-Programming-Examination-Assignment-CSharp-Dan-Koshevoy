using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThreadState = System.Threading.ThreadState;

namespace System_Programming_Examination_Assignment;

public partial class MainForm : Form
{
    private int _countFiles;
    private readonly List<Drive> _drives = new();
    private readonly List<string> _listWords = new();
    private Thread[] _threads;

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
                    foreach (var word in words.Split(' ', '(', ')', '/', '\\', ',', ';', ':', '[', ']', '}', '{', '\'',
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
        string word;
        using (var form = new EnterNewWordForm())
        {
            form.ShowDialog();
            word = form.tbEnterWord.Text;
        }

        if (word != string.Empty)
            try
            {
                foreach (var str in _listWords)
                    if (word == str) // если у нас уже есть слово в списке
                    {
                        MessageBox.Show($@"Слово [ {word} ] , уже есть в списке");
                        return;
                    }

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

    private void MainForm_Load(object sender, EventArgs e)
    {
        // проверка на уже запущенную программу
        if (Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName).Length > 1)
        {
            MessageBox.Show(@"Программа уже запущена", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Close();
        }

        // узнаем количество дисков и инф про них
        foreach (var drive in DriveInfo.GetDrives()) _drives.Add(new Drive(drive.Name));

        _threads = new Thread[_drives.Count];
    }

    private void GetCountFiles()
    {
        for (var i = 0; i < _drives.Count; i++)
        {
            // создаем поток для поиска текстовых файлов в каждом диске ( 1 поток = 1 диску )
            var i1 = i;
            _threads[i] = new Thread(() => GetFilesFromDirectories(_drives[i1].Name, i1));
            _threads[i].Start();
        }

        // ждем все потоки ( сканирование дисков )
        foreach (var thread in _threads) thread.Join();

        // продолжить выполнение кода, если потоки завершили поиск , и при этом не было нажато кнопка отмены
        if (_threads[0].ThreadState == ThreadState.Stopped)
        {
            if (lStatusScanning.InvokeRequired)
                lStatusScanning.BeginInvoke((MethodInvoker)delegate
                {
                    lStatusScanning.ForeColor = Color.OrangeRed;
                    lStatusScanning.Text = @"Поиск запретных слов в файлах";
                });

            // доделать поиск запретынх слов в файле
            if (pgbFindWords.InvokeRequired)
                pgbFindWords.BeginInvoke((MethodInvoker)delegate
                {
                    pgbFindWords.Maximum = _countFiles;
                    pgbFindWords.Style = ProgressBarStyle.Blocks;
                });
        }
    }

    private async void bStart_Click(object sender, EventArgs e)
    {
        if (_listWords.Count > 0)
        {
            bStart.Enabled = false;
            panel1.Visible = true;
            panel1.Enabled = true;

            var kef = 10;
            for (; Width < MaximumSize.Width;)
            {
                await Task.Delay(1);
                Width += (MaximumSize.Width - Width) / kef + 1;
                Location = new Point(Location.X - ((MaximumSize.Width - Width) / kef + 1) / 2, Location.Y);
            }

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
                _drives[index]._filesPaths.Add(filePath); // передаем индекс диска ( в нашем листе )
                _countFiles++;
            }

            // получаем список ini файлов в каталоге
            fileEntries = Directory.GetFiles(targetDirectory, "*.ini");
            foreach (var filePath in fileEntries)
            {
                _drives[index]._filesPaths.Add(filePath); // передаем индекс диска ( в нашем листе )
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
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    [Obsolete("Obsolete")]
    private async void BPauseResume_click(object sender, EventArgs e)
    {
        if (bPauseResume.Text.Equals("Пауза"))
            bPauseResume.Text = @"Возобновить";
        else
            bPauseResume.Text = @"Пауза";

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

                foreach (var drive in _drives) drive._filesPaths.Clear();
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

        var kef = 10;

        for (; Width > MinimumSize.Width;)
        {
            await Task.Delay(1);
            Width -= (MaximumSize.Width - Width) / kef + 1;
            Location = new Point(Location.X + ((MaximumSize.Width - Width) / kef + 1) / 2, Location.Y);
        }

        bStart.Enabled = true;
        panel1.Visible = false;
        panel1.Enabled = false;
    }
}

/// <summary>
///     Класс хранит информацию о количестве текстовых файлов и количестве логических разделов
/// </summary>
internal class Drive
{
    public List<string> _filesPaths = new();

    public Drive(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
}