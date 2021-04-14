using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NextorWin
{
    public partial class TrainForm : Form
    {
        public TrainForm()
        {
            InitializeComponent();
        }

        private void TrainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Closing This Form?", "System Info", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                Application.ExitThread();
                Environment.Exit(0);
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}
