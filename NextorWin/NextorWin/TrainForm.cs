using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenCvSharp;

namespace NextorWin
{
    public partial class TrainForm : Form
    {
        Mat image = new Mat();

        private Timer timer;
        private Timer timer1;
        private Timer coverTimer;

        private bool bView;
        private int timercount = 0;
        private int mainIndex = 0;

        private int coverIndex = 0;
        private int mode = 0;  // training mode

        public TrainForm()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);

            timer1 = new Timer();
            timer1.Interval = 500;
            timer1.Tick += new EventHandler(timer1_Tick);
            bView = false;

            coverTimer = new Timer();
            coverTimer.Interval = 500;
            coverTimer.Tick += new EventHandler(coverTimer_Tick);
        }

        private void TrainForm_FormClosing(object sender, FormClosingEventArgs e)
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

        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox11.BackgroundImage = NextorWin.Properties.Resources.button_220_57_collection_d_;
        }

        private void pictureBox11_MouseUp(object sender, MouseEventArgs e)
        {
            mode = 1;

            pictureBox11.BackgroundImage = NextorWin.Properties.Resources.button_220_57_collection;
            mainIndex = 1;

            coverTimer.Stop();
            picCover.Visible = true;
            coverTimer.Start();
        }

        private void TrainForm_Load(object sender, EventArgs e)
        {
            StringBuilder stringBuilder1 = new StringBuilder();

            string filePath = "../../../results/";
            string fileName = "Image_1.png";

            stringBuilder1.Append(filePath);
            stringBuilder1.Append(fileName);

            pictureBox1.Load(stringBuilder1.ToString());
            pictureBox2.Load(stringBuilder1.ToString());
            pictureBox3.Load(stringBuilder1.ToString());
            pictureBox4.Load(stringBuilder1.ToString());

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;

            timer.Start();
            timer1.Start();
        }

        private void TrainForm_Resize(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void pictureBox12_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox12.BackgroundImage = NextorWin.Properties.Resources.button_220_57_training_d;
        }

        private void pictureBox12_MouseUp(object sender, MouseEventArgs e)
        {
            mode = 2;

            pictureBox12.BackgroundImage = NextorWin.Properties.Resources.button_220_57_training;
            mainIndex = 2;

            coverTimer.Stop();
            picCover.Visible = true;
            coverTimer.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            picCover.Visible = false;
            coverIndex = 0;
            coverTimer.Stop();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (++timercount == 20)
            {
                pictureBox2.Load("../../../results/Image_1.png");
            }
            else if (++timercount == 40)
            {
                pictureBox3.Load("../../../results/Image_1.png");
            }
            else if (++timercount == 80)
            {
                pictureBox4.Load("../../../results/Image_1.png");
            }
            else if (++timercount == 100)
            {
                timer.Stop();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (bView == true)
            {
                bView = false;
                if (mainIndex == 1)
                {
                    ingBox.BackgroundImage = NextorWin.Properties.Resources.text_a;
                }
                else if (mainIndex == 2)
                {
                    ingBox.BackgroundImage = NextorWin.Properties.Resources.text_training_a;
                }
                else
                {
                    ingBox.BackgroundImage = NextorWin.Properties.Resources.text_default;
                }
            }
            else
            {
                bView = true;
                if (mainIndex == 1)
                {
                    ingBox.BackgroundImage = NextorWin.Properties.Resources.text_d;
                }
                else if (mainIndex == 2)
                {
                    ingBox.BackgroundImage = NextorWin.Properties.Resources.text_training_d;
                }
                else
                {
                    ingBox.BackgroundImage = NextorWin.Properties.Resources.text_default;
                }
            }
        }

        private void coverTimer_Tick(object sender, EventArgs e)
        {
            if (coverIndex == 0)
            {
                coverIndex = 1;
                if (mode == 1)
                {
                    picCover.BackgroundImage = NextorWin.Properties.Resources.cover_c_1;
                }
                else if (mode == 2)
                {
                    picCover.BackgroundImage = NextorWin.Properties.Resources.cover_t_1;
                }
            }
            else if (coverIndex == 1)
            {
                coverIndex = 2;
                if (mode == 1)
                {
                    picCover.BackgroundImage = NextorWin.Properties.Resources.cover_c_2;
                }
                else if (mode == 2)
                {
                    picCover.BackgroundImage = NextorWin.Properties.Resources.cover_t_2;
                }
            }
            else if (coverIndex == 2)
            {
                coverIndex = 0;
                if (mode == 1)
                {
                    picCover.BackgroundImage = NextorWin.Properties.Resources.cover_c_3;
                }
                else if (mode == 2)
                {
                    picCover.BackgroundImage = NextorWin.Properties.Resources.cover_t_3;
                }
            }
        }
    }
}
