namespace NextorWin
{
    partial class MainCover
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainCover));
            this.label1 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnInspection = new System.Windows.Forms.PictureBox();
            this.btnTraining = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnInspection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTraining)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Copperplate Gothic Bold", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(29, 38);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(346, 26);
            this.label1.TabIndex = 3;
            this.label1.Text = "Nextor Vision System 1.0";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 424);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(776, 14);
            this.progressBar1.TabIndex = 4;
            // 
            // btnInspection
            // 
            this.btnInspection.BackgroundImage = global::NextorWin.Properties.Resources.inspectionMode_d;
            this.btnInspection.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnInspection.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInspection.Location = new System.Drawing.Point(403, 302);
            this.btnInspection.Name = "btnInspection";
            this.btnInspection.Size = new System.Drawing.Size(191, 49);
            this.btnInspection.TabIndex = 7;
            this.btnInspection.TabStop = false;
            this.btnInspection.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnInspection_MouseDown);
            this.btnInspection.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnInspection_MouseUp);
            // 
            // btnTraining
            // 
            this.btnTraining.BackgroundImage = global::NextorWin.Properties.Resources.trainingMode_d;
            this.btnTraining.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnTraining.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTraining.Location = new System.Drawing.Point(196, 302);
            this.btnTraining.Name = "btnTraining";
            this.btnTraining.Size = new System.Drawing.Size(191, 49);
            this.btnTraining.TabIndex = 6;
            this.btnTraining.TabStop = false;
            this.btnTraining.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnTraining_MouseDown);
            this.btnTraining.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnTraining_MouseUp);
            // 
            // MainCover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnInspection);
            this.Controls.Add(this.btnTraining);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainCover";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nextor Cover";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainCover_FormClosing);
            this.Load += new System.EventHandler(this.MainCover_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnInspection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnTraining)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.PictureBox btnInspection;
        private System.Windows.Forms.PictureBox btnTraining;
    }
}

