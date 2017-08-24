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
    public partial class MsgBoxQuit : Form
    {
        public MsgBoxQuit()
        {
            InitializeComponent();
        }
        static MsgBoxQuit CustomMsgBoxQuit; static DialogResult result = DialogResult.No;
        public static DialogResult Show(string Text, string Caption, string btnOK, string btnNO)
        {
            CustomMsgBoxQuit = new MsgBoxQuit();
            CustomMsgBoxQuit.label1.Text = Text;
            CustomMsgBoxQuit.button1.Text = btnOK;
            CustomMsgBoxQuit.button2.Text = btnNO;
            CustomMsgBoxQuit.Text = Caption;
            result = DialogResult.No;
            CustomMsgBoxQuit.ShowDialog();
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.ExitThread();
        }

         private void button2_Click(object sender, EventArgs e)
        {
            CustomMsgBoxQuit.Close();           
        }

    }
}