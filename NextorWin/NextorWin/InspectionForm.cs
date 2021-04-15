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
    public partial class InspectionForm : Form
    {
        private bool bView;
        private Timer timer1;
        private Timer timer2;
        private Timer timer3;

        private bool bPass;
        private bool bFail;

        public InspectionForm()
        {
            InitializeComponent();

            timer1 = new Timer();
            timer1.Interval = 500;
            timer1.Tick += new EventHandler(timer1_Tick);
            bView = false;

            timer2 = new Timer();
            timer2.Interval = 500;
            timer2.Tick += new EventHandler(timer2_Tick);
            bPass = false;

            timer3 = new Timer();
            timer3.Interval = 500;
            timer3.Tick += new EventHandler(timer3_Tick);
            bFail = false;
        }

        private void InspectionForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult result = MessageBox.Show("Closing This Form?", "System Info", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                try
                {
                    Application.ExitThread();
                    Environment.Exit(0);
                }
                catch
                {
                    Console.WriteLine("Exception!");
                }
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void btnStart_MouseDown(object sender, MouseEventArgs e)
        {
            btnStart.BackgroundImage = NextorWin.Properties.Resources.button_220_57_start_d;
        }

        private void btnStart_MouseUp(object sender, MouseEventArgs e)
        {
            btnStart.BackgroundImage = NextorWin.Properties.Resources.button_220_57_start;
            timer1.Start();

            btnStop.Enabled = true;
            btnStop.BackgroundImage = NextorWin.Properties.Resources.button_220_57_stop;
        }

        private void InspectionForm_Load(object sender, EventArgs e)
        {
            StringBuilder stringBuilder1 = new StringBuilder();

            string filePath = "../../../results/";
            string fileName = "image1.jpg";

            stringBuilder1.Append(filePath);
            stringBuilder1.Append(fileName);

            pictureBox1.Load(stringBuilder1.ToString());
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            btnStop.Enabled = false;
            btnDetail.Enabled = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bView == true)
            {
                bView = false;
                btnStart.BackgroundImage = NextorWin.Properties.Resources.button_220_57_start;
            }
            else
            {
                bView = true;
                btnStart.BackgroundImage = NextorWin.Properties.Resources.button_220_57_start_d;
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (bPass == true)
            {
                bPass = false;
                resultBox.BackgroundImage = NextorWin.Properties.Resources.text_pass_d;
            }
            else
            {
                bPass = true;
                resultBox.BackgroundImage = NextorWin.Properties.Resources.text_pass_a;
            }
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            if (bFail == true)
            {
                bFail = false;
                resultBox.BackgroundImage = NextorWin.Properties.Resources.text_fail_d;
            }
            else
            {
                bFail = true;
                resultBox.BackgroundImage = NextorWin.Properties.Resources.text_fail_a;
            }
        }

        private void btnStop_MouseDown(object sender, MouseEventArgs e)
        {
            btnStop.BackgroundImage = NextorWin.Properties.Resources.button_220_57_stop_d;
        }

        private void btnStop_MouseUp(object sender, MouseEventArgs e)
        {
            btnStop.BackgroundImage = NextorWin.Properties.Resources.button_220_57_stop;

            timer1.Stop();
            btnStart.BackgroundImage = NextorWin.Properties.Resources.button_220_57_start;

            timer2.Stop();
            timer3.Stop();
            resultBox.BackgroundImage = NextorWin.Properties.Resources.text_default;

            btnDetail.Enabled = true;
            btnDetail.BackgroundImage = NextorWin.Properties.Resources.button_220_57_detail;
        }

        private void btnDetail_MouseDown(object sender, MouseEventArgs e)
        {
            btnDetail.BackgroundImage = NextorWin.Properties.Resources.button_220_57_detail_d;
        }

        private void btnDetail_MouseUp(object sender, MouseEventArgs e)
        {
            btnDetail.BackgroundImage = NextorWin.Properties.Resources.button_220_57_detail;

            //View viewM = new View();
            Detail_View viewM = new Detail_View();
            viewM.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            resultBox.BackgroundImage = NextorWin.Properties.Resources.text_pass_a;
            timer3.Stop();
            timer2.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            resultBox.BackgroundImage = NextorWin.Properties.Resources.text_fail_a;
            timer2.Stop();
            timer3.Start();
        }
    }
}
