using System;
using System.Windows.Forms;

namespace System_Programming_Examination_Assignment;

public partial class EnterNewWordForm : Form
{
    public EnterNewWordForm()
    {
        InitializeComponent();
    }

    private void bAddNewWord_Click(object sender, EventArgs e)
    {
        if (tbEnterWord.Text.Length > 0) Close();
    }
}