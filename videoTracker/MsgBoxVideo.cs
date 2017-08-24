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
    public partial class MsgBoxVideo : Form
    {
        public MsgBoxVideo()
        {
            InitializeComponent();
        }
        static MsgBoxVideo CustomMsgBoxVideo; static DialogResult result = DialogResult.No;
        public static DialogResult Show(string Text, string Caption, string btnOK)
        {
            CustomMsgBoxVideo = new MsgBoxVideo();
            CustomMsgBoxVideo.label1.Text = Text;
            CustomMsgBoxVideo.button1.Text = btnOK;
            CustomMsgBoxVideo.Text = Caption;
            result = DialogResult.No;
            CustomMsgBoxVideo.ShowDialog();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes; CustomMsgBoxVideo.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            result = DialogResult.Yes; CustomMsgBoxVideo.Close();
        }
    }
}