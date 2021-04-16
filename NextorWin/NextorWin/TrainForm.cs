using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using OpenCvSharp;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NextorWin
{
    public partial class TrainForm : Form
    {
        //Mat image = new Mat();

        private Timer timer;
        private Timer timer1;
        private Timer coverTimer;

        private bool bView;
        private int timercount = 0;
        private int mainIndex = 0;

        private int coverIndex = 0;
        private int mode = 0;  // training mode

        // config
        private string UserName;
        private string Password;
        private string HostName;
        private int Port;
        private string VirtualHost;
        private string cam1_ex;
        private string cam1_rk;
        private string cam2_ex;
        private string cam2_rk;
        private string cam3_ex;
        private string cam3_rk;
        private string cam4_ex;
        private string cam4_rk;

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

            // config 정보 가져오기
            IniFile config = new IniFile();
            config.Load("../../../config/config.ini");
            UserName = config["MQ"]["UserName"].ToString();
            Password = config["MQ"]["Password"].ToString();
            HostName = config["MQ"]["HostName"].ToString();
            Port = config["MQ"]["Port"].ToInt();
            VirtualHost = config["MQ"]["VirtualHost"].ToString();
            cam1_ex = config["MQ"]["cam1_ex"].ToString();
            cam1_rk = config["MQ"]["cam1_rk"].ToString();
            cam2_ex = config["MQ"]["cam2_ex"].ToString();
            cam2_rk = config["MQ"]["cam2_rk"].ToString();
            cam3_ex = config["MQ"]["cam3_ex"].ToString();
            cam3_rk = config["MQ"]["cam3_rk"].ToString();
            cam4_ex = config["MQ"]["cam4_ex"].ToString();
            cam4_rk = config["MQ"]["cam4_rk"].ToString();
        }

        /// <summary>
        /// MQ Send function
        /// </summary>
        /// <param name="Cam">카메라번호</param>
        /// <param name="body">메시지</param>
        private void mqSend(string Cam, string body)
        {
            var connectionFactory = new RabbitMQ.Client.ConnectionFactory()
            {
                UserName = UserName,
                Password = Password,
                HostName = HostName,
                Port = Port,
                VirtualHost = VirtualHost
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();

            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            if (Cam == "C1")
            {
                byte[] messagebuffer = Encoding.Default.GetBytes(body);
                model.BasicPublish(cam1_ex, cam1_rk, properties, messagebuffer);
            }
            else if (Cam == "C2")
            {
                byte[] messagebuffer = Encoding.Default.GetBytes(body);
                model.BasicPublish(cam2_ex, cam2_rk, properties, messagebuffer);
            }
            else if (Cam == "C3")
            {
                byte[] messagebuffer = Encoding.Default.GetBytes(body);
                model.BasicPublish(cam3_ex, cam3_rk, properties, messagebuffer);
            }
            else if (Cam == "C4")
            {
                byte[] messagebuffer = Encoding.Default.GetBytes(body);
                model.BasicPublish(cam4_ex, cam4_rk, properties, messagebuffer);
            }
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

        /// <summary>
        /// mq send sample - cam1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCam1_Click(object sender, EventArgs e)
        {
            string body = "c1_0000001.bin";
            mqSend("C1", body);
        }

        /// <summary>
        /// mq send sample - cam2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCam2_Click(object sender, EventArgs e)
        {
            string body = "c2_0000001.bin";
            mqSend("C2", body);
        }

        /// <summary>
        /// mq send sample - cam3
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCam3_Click(object sender, EventArgs e)
        {
            string body = "c3_0000001.bin";
            mqSend("C3", body);
        }

        /// <summary>
        /// mq send sample - cam4
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCam4_Click(object sender, EventArgs e)
        {
            string body = "c4_0000001.bin";
            mqSend("C4", body);
        }

        private void btnCollection_MouseDown(object sender, MouseEventArgs e)
        {
            btnCollection.BackgroundImage = NextorWin.Properties.Resources.button_220_57_collection_d_;
        }

        private void btnCollection_MouseUp(object sender, MouseEventArgs e)
        {
            mode = 1;

            btnCollection.BackgroundImage = NextorWin.Properties.Resources.button_220_57_collection;
            mainIndex = 1;

            coverTimer.Stop();
            picCover.Visible = true;
            coverTimer.Start();
        }

        private void btnTraining_MouseDown(object sender, MouseEventArgs e)
        {
            btnTraining.BackgroundImage = NextorWin.Properties.Resources.button_220_57_training_d;
        }

        private void btnTraining_MouseUp(object sender, MouseEventArgs e)
        {
            mode = 2;

            btnTraining.BackgroundImage = NextorWin.Properties.Resources.button_220_57_training;
            mainIndex = 2;

            coverTimer.Stop();
            picCover.Visible = true;
            coverTimer.Start();
        }
    }
}
