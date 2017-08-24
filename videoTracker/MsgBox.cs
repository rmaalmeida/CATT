using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace videoTracker
{
    public partial class MsgBox : Form
    {
        public MsgBox()
        {
            InitializeComponent();
        }
        static MsgBox CustomMsgBox; static DialogResult result = DialogResult.No;
        public static DialogResult Show(string Text, string Caption, string btnOK)
        {
            CustomMsgBox = new MsgBox();
            CustomMsgBox.label1.Text = Text;
            CustomMsgBox.button1.Text = btnOK;
            CustomMsgBox.Text = Caption;
            result = DialogResult.No;
            CustomMsgBox.ShowDialog();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes; CustomMsgBox.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            result = DialogResult.Yes; CustomMsgBox.Close();
        }
    }
}