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
    public partial class MainCover : Form
    {
        private Timer timer;
        private int timercount = 0;

        public MainCover()
        {
            InitializeComponent();

            btnTraining.Enabled = false;
            btnInspection.Enabled = false;

            timer = new Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer_Tick);
        }

        private void MainCover_Load(object sender, EventArgs e)
        {
            // 최대, 최소, 간격을 임의로 조정
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 5;
            progressBar1.Value = 0;

            // 테스트를 위해 타이머 시작
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            progressBar1.PerformStep();

            if (++timercount == 20)
            {
                timer.Stop();

                btnTraining.BackgroundImage = NextorWin.Properties.Resources.trainingMode_a;
                btnTraining.Enabled = true;
                btnInspection.BackgroundImage = NextorWin.Properties.Resources.inspectionMode_a;
                btnInspection.Enabled = true;
            }
        }

        private void btnTraining_MouseDown(object sender, MouseEventArgs e)
        {
            btnTraining.BackgroundImage = NextorWin.Properties.Resources.trainingMode_d;
        }

        private void btnTraining_MouseUp(object sender, MouseEventArgs e)
        {
            btnTraining.BackgroundImage = NextorWin.Properties.Resources.trainingMode_a;

            DialogResult result = MessageBox.Show("Training Solution Start.", "System Info", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                TrainForm trainForm = new TrainForm();
                trainForm.Owner = this;
                trainForm.Show();

                this.Hide();
            }
        }

        private void btnInspection_MouseDown(object sender, MouseEventArgs e)
        {
            btnInspection.BackgroundImage = NextorWin.Properties.Resources.inspectionMode_d;
        }

        private void btnInspection_MouseUp(object sender, MouseEventArgs e)
        {
            btnInspection.BackgroundImage = NextorWin.Properties.Resources.inspectionMode_a;

            DialogResult result = MessageBox.Show("Inspection Solution Start.", "System Info", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                InspectionForm inspectionForm = new InspectionForm();
                inspectionForm.Owner = this;
                inspectionForm.Show();

                this.Hide();
            }
        }

        private void MainCover_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Closing This Form?", "System Info", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
            {
                e.Cancel = true;
            }
            else
            {
                Application.ExitThread();
                Environment.Exit(0);
            }
        }
    }
}
