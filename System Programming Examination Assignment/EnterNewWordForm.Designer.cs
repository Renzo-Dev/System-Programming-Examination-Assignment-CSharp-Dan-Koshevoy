﻿namespace System_Programming_Examination_Assignment
{
    partial class EnterNewWordForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tbEnterWord = new System.Windows.Forms.TextBox();
            this.bAddNewWord = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbEnterWord
            // 
            this.tbEnterWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(50)))));
            this.tbEnterWord.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbEnterWord.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbEnterWord.ForeColor = System.Drawing.Color.White;
            this.tbEnterWord.Location = new System.Drawing.Point(12, 33);
            this.tbEnterWord.Name = "tbEnterWord";
            this.tbEnterWord.Size = new System.Drawing.Size(174, 19);
            this.tbEnterWord.TabIndex = 0;
            // 
            // bAddNewWord
            // 
            this.bAddNewWord.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(20)))), ((int)(((byte)(20)))));
            this.bAddNewWord.FlatAppearance.BorderSize = 0;
            this.bAddNewWord.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.bAddNewWord.ForeColor = System.Drawing.Color.White;
            this.bAddNewWord.Location = new System.Drawing.Point(12, 70);
            this.bAddNewWord.Name = "bAddNewWord";
            this.bAddNewWord.Size = new System.Drawing.Size(174, 44);
            this.bAddNewWord.TabIndex = 8;
            this.bAddNewWord.Text = "Ввести";
            this.bAddNewWord.UseVisualStyleBackColor = false;
            this.bAddNewWord.Click += new System.EventHandler(this.bAddNewWord_Click);
            // 
            // EnterNewWordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.ClientSize = new System.Drawing.Size(197, 126);
            this.Controls.Add(this.bAddNewWord);
            this.Controls.Add(this.tbEnterWord);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(213, 165);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(213, 165);
            this.Name = "EnterNewWordForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Запретное слово";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tbEnterWord;
        private System.Windows.Forms.Button bAddNewWord;
    }
}