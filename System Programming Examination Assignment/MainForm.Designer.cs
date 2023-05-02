using System.Windows.Forms;

namespace System_Programming_Examination_Assignment
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gbParamFound = new System.Windows.Forms.GroupBox();
            this.lCountWords = new System.Windows.Forms.Label();
            this.bAddNewWord = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.bStart = new System.Windows.Forms.Button();
            this.lbBannedWords = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bFileLoadWords = new System.Windows.Forms.Button();
            this.ofdLoadWords = new System.Windows.Forms.OpenFileDialog();
            this.bCancel = new System.Windows.Forms.Button();
            this.bPauseResume = new System.Windows.Forms.Button();
            this.pgbFindWords = new System.Windows.Forms.ProgressBar();
            this.panelInformation = new System.Windows.Forms.Panel();
            this.lCountFiles = new System.Windows.Forms.Label();
            this.lStatusScanning = new System.Windows.Forms.Label();
            this.lable = new System.Windows.Forms.Label();
            this.lable2 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.gbParamFound.SuspendLayout();
            this.panelInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbParamFound
            // 
            this.gbParamFound.Controls.Add(this.lCountWords);
            this.gbParamFound.Controls.Add(this.bAddNewWord);
            this.gbParamFound.Controls.Add(this.label1);
            this.gbParamFound.Controls.Add(this.bStart);
            this.gbParamFound.Controls.Add(this.lbBannedWords);
            this.gbParamFound.Controls.Add(this.label2);
            this.gbParamFound.Controls.Add(this.bFileLoadWords);
            this.gbParamFound.Font = new System.Drawing.Font("Verdana", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.gbParamFound.ForeColor = System.Drawing.Color.White;
            this.gbParamFound.Location = new System.Drawing.Point(9, 25);
            this.gbParamFound.Margin = new System.Windows.Forms.Padding(2);
            this.gbParamFound.Name = "gbParamFound";
            this.gbParamFound.Padding = new System.Windows.Forms.Padding(2);
            this.gbParamFound.Size = new System.Drawing.Size(220, 448);
            this.gbParamFound.TabIndex = 15;
            this.gbParamFound.TabStop = false;
            // 
            // lCountWords
            // 
            this.lCountWords.AutoSize = true;
            this.lCountWords.Location = new System.Drawing.Point(14, 115);
            this.lCountWords.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lCountWords.Name = "lCountWords";
            this.lCountWords.Size = new System.Drawing.Size(64, 17);
            this.lCountWords.TabIndex = 10;
            this.lCountWords.Text = "Слов: 0";
            // 
            // bAddNewWord
            // 
            this.bAddNewWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.bAddNewWord.FlatAppearance.BorderSize = 0;
            this.bAddNewWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddNewWord.ForeColor = System.Drawing.Color.White;
            this.bAddNewWord.Location = new System.Drawing.Point(45, 68);
            this.bAddNewWord.Margin = new System.Windows.Forms.Padding(2);
            this.bAddNewWord.Name = "bAddNewWord";
            this.bAddNewWord.Size = new System.Drawing.Size(130, 36);
            this.bAddNewWord.TabIndex = 7;
            this.bAddNewWord.Text = "Добавить";
            this.toolTip1.SetToolTip(this.bAddNewWord, "Добавить запретное слово в список");
            this.bAddNewWord.UseVisualStyleBackColor = false;
            this.bAddNewWord.Click += new System.EventHandler(this.bAddNewWord_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(14, 0);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Поиск запрещенных слов";
            // 
            // bStart
            // 
            this.bStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(15)))), ((int)(((byte)(15)))));
            this.bStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bStart.Enabled = false;
            this.bStart.FlatAppearance.BorderSize = 0;
            this.bStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bStart.Font = new System.Drawing.Font("Bahnschrift SemiBold", 13F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bStart.Location = new System.Drawing.Point(45, 390);
            this.bStart.Margin = new System.Windows.Forms.Padding(2);
            this.bStart.Name = "bStart";
            this.bStart.Size = new System.Drawing.Size(130, 36);
            this.bStart.TabIndex = 6;
            this.bStart.Text = "Старт";
            this.bStart.UseVisualStyleBackColor = false;
            this.bStart.Click += new System.EventHandler(this.bStart_Click);
            // 
            // lbBannedWords
            // 
            this.lbBannedWords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.lbBannedWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lbBannedWords.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lbBannedWords.ForeColor = System.Drawing.Color.White;
            this.lbBannedWords.FormattingEnabled = true;
            this.lbBannedWords.ItemHeight = 17;
            this.lbBannedWords.Location = new System.Drawing.Point(5, 192);
            this.lbBannedWords.Margin = new System.Windows.Forms.Padding(2);
            this.lbBannedWords.Name = "lbBannedWords";
            this.lbBannedWords.Size = new System.Drawing.Size(210, 170);
            this.lbBannedWords.TabIndex = 5;
            this.lbBannedWords.TabStop = false;
            this.lbBannedWords.DoubleClick += new System.EventHandler(this.RemoveWord);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.label2.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
            this.label2.Location = new System.Drawing.Point(34, 165);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(153, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Запрещенные слова";
            // 
            // bFileLoadWords
            // 
            this.bFileLoadWords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.bFileLoadWords.FlatAppearance.BorderSize = 0;
            this.bFileLoadWords.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bFileLoadWords.ForeColor = System.Drawing.Color.White;
            this.bFileLoadWords.Location = new System.Drawing.Point(45, 28);
            this.bFileLoadWords.Margin = new System.Windows.Forms.Padding(2);
            this.bFileLoadWords.Name = "bFileLoadWords";
            this.bFileLoadWords.Size = new System.Drawing.Size(130, 36);
            this.bFileLoadWords.TabIndex = 13;
            this.bFileLoadWords.Text = "Загрузить";
            this.toolTip1.SetToolTip(this.bFileLoadWords, "Загрузить запретные слова из файла");
            this.bFileLoadWords.UseVisualStyleBackColor = false;
            this.bFileLoadWords.Click += new System.EventHandler(this.bFileLoadWords_Click);
            // 
            // ofdLoadWords
            // 
            this.ofdLoadWords.Filter = "Text files (*.txt;*.ini)|*.txt;*.ini";
            // 
            // bCancel
            // 
            this.bCancel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.bCancel.FlatAppearance.BorderSize = 0;
            this.bCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bCancel.ForeColor = System.Drawing.Color.White;
            this.bCancel.Location = new System.Drawing.Point(344, 76);
            this.bCancel.Margin = new System.Windows.Forms.Padding(2);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(130, 36);
            this.bCancel.TabIndex = 8;
            this.bCancel.Text = "Отмена";
            this.bCancel.UseVisualStyleBackColor = false;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // bPauseResume
            // 
            this.bPauseResume.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.bPauseResume.FlatAppearance.BorderSize = 0;
            this.bPauseResume.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bPauseResume.ForeColor = System.Drawing.Color.White;
            this.bPauseResume.Location = new System.Drawing.Point(344, 26);
            this.bPauseResume.Margin = new System.Windows.Forms.Padding(2);
            this.bPauseResume.Name = "bPauseResume";
            this.bPauseResume.Size = new System.Drawing.Size(130, 36);
            this.bPauseResume.TabIndex = 8;
            this.bPauseResume.Text = "Пауза";
            this.bPauseResume.UseVisualStyleBackColor = false;
            this.bPauseResume.Click += new System.EventHandler(this.BPauseResume_click);
            // 
            // pgbFindWords
            // 
            this.pgbFindWords.Location = new System.Drawing.Point(14, 93);
            this.pgbFindWords.Margin = new System.Windows.Forms.Padding(2);
            this.pgbFindWords.MarqueeAnimationSpeed = 30;
            this.pgbFindWords.Name = "pgbFindWords";
            this.pgbFindWords.Size = new System.Drawing.Size(294, 19);
            this.pgbFindWords.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.pgbFindWords.TabIndex = 9;
            // 
            // panelInformation
            // 
            this.panelInformation.Controls.Add(this.lCountFiles);
            this.panelInformation.Controls.Add(this.lStatusScanning);
            this.panelInformation.Controls.Add(this.lable);
            this.panelInformation.Controls.Add(this.lable2);
            this.panelInformation.Controls.Add(this.bPauseResume);
            this.panelInformation.Controls.Add(this.pgbFindWords);
            this.panelInformation.Controls.Add(this.bCancel);
            this.panelInformation.Enabled = false;
            this.panelInformation.Font = new System.Drawing.Font("Verdana", 10F);
            this.panelInformation.Location = new System.Drawing.Point(233, 36);
            this.panelInformation.Margin = new System.Windows.Forms.Padding(2);
            this.panelInformation.Name = "panelInformation";
            this.panelInformation.Size = new System.Drawing.Size(481, 136);
            this.panelInformation.TabIndex = 10;
            this.panelInformation.Visible = false;
            // 
            // lCountFiles
            // 
            this.lCountFiles.AutoSize = true;
            this.lCountFiles.ForeColor = System.Drawing.Color.White;
            this.lCountFiles.Location = new System.Drawing.Point(150, 45);
            this.lCountFiles.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lCountFiles.Name = "lCountFiles";
            this.lCountFiles.Size = new System.Drawing.Size(17, 17);
            this.lCountFiles.TabIndex = 15;
            this.lCountFiles.Text = "0";
            // 
            // lStatusScanning
            // 
            this.lStatusScanning.AutoSize = true;
            this.lStatusScanning.ForeColor = System.Drawing.Color.White;
            this.lStatusScanning.Location = new System.Drawing.Point(77, 17);
            this.lStatusScanning.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lStatusScanning.Name = "lStatusScanning";
            this.lStatusScanning.Size = new System.Drawing.Size(0, 17);
            this.lStatusScanning.TabIndex = 14;
            // 
            // lable
            // 
            this.lable.AutoSize = true;
            this.lable.ForeColor = System.Drawing.Color.White;
            this.lable.Location = new System.Drawing.Point(11, 17);
            this.lable.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lable.Name = "lable";
            this.lable.Size = new System.Drawing.Size(62, 17);
            this.lable.TabIndex = 13;
            this.lable.Text = "Статус:";
            // 
            // lable2
            // 
            this.lable2.AutoSize = true;
            this.lable2.ForeColor = System.Drawing.Color.White;
            this.lable2.Location = new System.Drawing.Point(11, 45);
            this.lable2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lable2.Name = "lable2";
            this.lable2.Size = new System.Drawing.Size(135, 17);
            this.lable2.TabIndex = 12;
            this.lable2.Text = "Найдено файлов:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(720, 483);
            this.Controls.Add(this.panelInformation);
            this.Controls.Add(this.gbParamFound);
            this.DoubleBuffered = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(736, 522);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(257, 522);
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Поиск запретных слов";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.gbParamFound.ResumeLayout(false);
            this.gbParamFound.PerformLayout();
            this.panelInformation.ResumeLayout(false);
            this.panelInformation.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbParamFound;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bFileLoadWords;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lbBannedWords;
        private System.Windows.Forms.Button bStart;
        private System.Windows.Forms.Button bAddNewWord;
        private System.Windows.Forms.OpenFileDialog ofdLoadWords;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.Button bPauseResume;
        private System.Windows.Forms.ProgressBar pgbFindWords;
        private System.Windows.Forms.Panel panelInformation;
        private Label lCountWords;
        private Label lable2;
        private Label lable;
        private Label lStatusScanning;
        private Label lCountFiles;
        private ToolTip toolTip1;
    }
}

