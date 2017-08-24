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
    public partial class MsgBoxTrack : Form
    {
        public MsgBoxTrack()
        {
            InitializeComponent();
        }
        static MsgBoxTrack CustomMsgBoxTrack; static DialogResult result = DialogResult.No;
        public static DialogResult Show(string Text, string Caption, string btnOK)
        {
            CustomMsgBoxTrack = new MsgBoxTrack();
            CustomMsgBoxTrack.label1.Text = Text;
            CustomMsgBoxTrack.button1.Text = btnOK;
            CustomMsgBoxTrack.Text = Caption;
            result = DialogResult.No;
            CustomMsgBoxTrack.ShowDialog();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes; CustomMsgBoxTrack.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            result = DialogResult.Yes; CustomMsgBoxTrack.Close();
        }
    }
}