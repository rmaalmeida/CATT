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
    public partial class MsgBoxTrackEmpty : Form
    {
        public MsgBoxTrackEmpty()
        {
            InitializeComponent();
        }
        static MsgBoxTrackEmpty CustomMsgBoxTrackEmpty; static DialogResult result = DialogResult.No;
        public static DialogResult Show(string Text, string Caption, string btnOK)
        {
            CustomMsgBoxTrackEmpty = new MsgBoxTrackEmpty();
            CustomMsgBoxTrackEmpty.label1.Text = Text;
            CustomMsgBoxTrackEmpty.button1.Text = btnOK;
            CustomMsgBoxTrackEmpty.Text = Caption;
            result = DialogResult.No;
            CustomMsgBoxTrackEmpty.ShowDialog();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            result = DialogResult.Yes; CustomMsgBoxTrackEmpty.Close();
        }


    }
}