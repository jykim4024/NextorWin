using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

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
            btnDetail.Enabled = false;
            btnDetail.BackgroundImage = NextorWin.Properties.Resources.button_220_57_detail_d;
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
    }
}
