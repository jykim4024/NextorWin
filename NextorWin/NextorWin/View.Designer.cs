namespace NextorWin
{
    partial class View
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(View));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtRatio = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.heightTextBox = new System.Windows.Forms.TextBox();
            this.widthTextBox = new System.Windows.Forms.TextBox();
            this.label_Y = new System.Windows.Forms.TextBox();
            this.label_X = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ctrl_ScrollPictureBox1 = new ScrollPictureBox.Ctrl_ScrollPictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnSave = new System.Windows.Forms.PictureBox();
            this.btnOpen = new System.Windows.Forms.PictureBox();
            this.btnZoomout = new System.Windows.Forms.PictureBox();
            this.btnZoomin = new System.Windows.Forms.PictureBox();
            this.btnAutofit = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox2.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnZoomout)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnZoomin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAutofit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox5);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.pictureBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 58);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 772);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnSave);
            this.groupBox5.Controls.Add(this.btnOpen);
            this.groupBox5.Location = new System.Drawing.Point(6, 55);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(188, 90);
            this.groupBox5.TabIndex = 18;
            this.groupBox5.TabStop = false;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtRatio);
            this.groupBox4.Controls.Add(this.btnZoomout);
            this.groupBox4.Controls.Add(this.btnZoomin);
            this.groupBox4.Controls.Add(this.btnAutofit);
            this.groupBox4.Font = new System.Drawing.Font("Copperplate Gothic Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox4.Location = new System.Drawing.Point(6, 312);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(188, 164);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Zoom";
            // 
            // txtRatio
            // 
            this.txtRatio.Location = new System.Drawing.Point(6, 131);
            this.txtRatio.Name = "txtRatio";
            this.txtRatio.ReadOnly = true;
            this.txtRatio.Size = new System.Drawing.Size(47, 21);
            this.txtRatio.TabIndex = 8;
            this.txtRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.heightTextBox);
            this.groupBox3.Controls.Add(this.widthTextBox);
            this.groupBox3.Controls.Add(this.label_Y);
            this.groupBox3.Controls.Add(this.label_X);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Font = new System.Drawing.Font("Copperplate Gothic Light", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Location = new System.Drawing.Point(6, 151);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(188, 155);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Mouse Location";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.Control;
            this.label2.Location = new System.Drawing.Point(11, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 15);
            this.label2.TabIndex = 24;
            this.label2.Text = "▶ square X  :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.Control;
            this.label4.Location = new System.Drawing.Point(11, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 15);
            this.label4.TabIndex = 23;
            this.label4.Text = "▶ square X  :";
            // 
            // heightTextBox
            // 
            this.heightTextBox.Location = new System.Drawing.Point(118, 53);
            this.heightTextBox.Name = "heightTextBox";
            this.heightTextBox.ReadOnly = true;
            this.heightTextBox.Size = new System.Drawing.Size(57, 21);
            this.heightTextBox.TabIndex = 22;
            this.heightTextBox.Text = "200";
            this.heightTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // widthTextBox
            // 
            this.widthTextBox.Location = new System.Drawing.Point(118, 26);
            this.widthTextBox.Name = "widthTextBox";
            this.widthTextBox.ReadOnly = true;
            this.widthTextBox.Size = new System.Drawing.Size(57, 21);
            this.widthTextBox.TabIndex = 21;
            this.widthTextBox.Text = "200";
            this.widthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_Y
            // 
            this.label_Y.Location = new System.Drawing.Point(109, 120);
            this.label_Y.Name = "label_Y";
            this.label_Y.ReadOnly = true;
            this.label_Y.Size = new System.Drawing.Size(57, 21);
            this.label_Y.TabIndex = 20;
            this.label_Y.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_X
            // 
            this.label_X.Location = new System.Drawing.Point(109, 93);
            this.label_X.Name = "label_X";
            this.label_X.ReadOnly = true;
            this.label_X.Size = new System.Drawing.Size(57, 21);
            this.label_X.TabIndex = 19;
            this.label_X.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.Control;
            this.label7.Location = new System.Drawing.Point(23, 120);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(86, 15);
            this.label7.TabIndex = 17;
            this.label7.Text = "▶ Y      : ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Consolas", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.Control;
            this.label3.Location = new System.Drawing.Point(23, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(86, 15);
            this.label3.TabIndex = 15;
            this.label3.Text = "▶ X      : ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Copperplate Gothic Bold", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.Control;
            this.label1.Location = new System.Drawing.Point(9, 364);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 14);
            this.label1.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 836);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1240, 137);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // ctrl_ScrollPictureBox1
            // 
            this.ctrl_ScrollPictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ctrl_ScrollPictureBox1.Location = new System.Drawing.Point(218, 15);
            this.ctrl_ScrollPictureBox1.Name = "ctrl_ScrollPictureBox1";
            this.ctrl_ScrollPictureBox1.Size = new System.Drawing.Size(1034, 815);
            this.ctrl_ScrollPictureBox1.TabIndex = 10;
            this.ctrl_ScrollPictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.ctrl_ScrollPictureBox1_Paint);
            this.ctrl_ScrollPictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ctrl_ScrollPictureBox1_MouseDown);
            this.ctrl_ScrollPictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ctrl_ScrollPictureBox1_MouseMove);
            this.ctrl_ScrollPictureBox1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ctrl_ScrollPictureBox1_MouseUp);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog1";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Picture Files|*.bmp;*.jpg;*.gif;*.png;*.tif|All Files|*.*";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackgroundImage = global::NextorWin.Properties.Resources.viewTitle1;
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Location = new System.Drawing.Point(12, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(200, 40);
            this.pictureBox2.TabIndex = 11;
            this.pictureBox2.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.BackgroundImage = global::NextorWin.Properties.Resources.check_24849_640;
            this.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnSave.Location = new System.Drawing.Point(109, 20);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(54, 54);
            this.btnSave.TabIndex = 1;
            this.btnSave.TabStop = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.BackgroundImage = global::NextorWin.Properties.Resources.plus_297823_640;
            this.btnOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnOpen.Location = new System.Drawing.Point(26, 20);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(54, 54);
            this.btnOpen.TabIndex = 0;
            this.btnOpen.TabStop = false;
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnZoomout
            // 
            this.btnZoomout.BackgroundImage = global::NextorWin.Properties.Resources.btnZoomout;
            this.btnZoomout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZoomout.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZoomout.Location = new System.Drawing.Point(6, 94);
            this.btnZoomout.Name = "btnZoomout";
            this.btnZoomout.Size = new System.Drawing.Size(176, 31);
            this.btnZoomout.TabIndex = 2;
            this.btnZoomout.TabStop = false;
            this.btnZoomout.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnZoomout_MouseDown);
            this.btnZoomout.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnZoomout_MouseUp);
            // 
            // btnZoomin
            // 
            this.btnZoomin.BackgroundImage = global::NextorWin.Properties.Resources.btnZoomin;
            this.btnZoomin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnZoomin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnZoomin.Location = new System.Drawing.Point(6, 57);
            this.btnZoomin.Name = "btnZoomin";
            this.btnZoomin.Size = new System.Drawing.Size(176, 31);
            this.btnZoomin.TabIndex = 1;
            this.btnZoomin.TabStop = false;
            this.btnZoomin.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnZoomin_MouseDown);
            this.btnZoomin.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnZoomin_MouseUp);
            // 
            // btnAutofit
            // 
            this.btnAutofit.BackgroundImage = global::NextorWin.Properties.Resources.btnAutoFit;
            this.btnAutofit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnAutofit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAutofit.Location = new System.Drawing.Point(6, 20);
            this.btnAutofit.Name = "btnAutofit";
            this.btnAutofit.Size = new System.Drawing.Size(176, 31);
            this.btnAutofit.TabIndex = 0;
            this.btnAutofit.TabStop = false;
            this.btnAutofit.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btnAutofit_MouseDown);
            this.btnAutofit.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btnAutofit_MouseUp);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::NextorWin.Properties.Resources.subTitle1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(22, 20);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(152, 29);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // View
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(34)))), ((int)(((byte)(34)))));
            this.ClientSize = new System.Drawing.Size(1264, 985);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.ctrl_ScrollPictureBox1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "View";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nextor Detail Result";
            this.Load += new System.EventHandler(this.View_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnZoomout)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnZoomin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAutofit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private ScrollPictureBox.Ctrl_ScrollPictureBox ctrl_ScrollPictureBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.PictureBox btnAutofit;
        private System.Windows.Forms.PictureBox btnZoomout;
        private System.Windows.Forms.PictureBox btnZoomin;
        private System.Windows.Forms.TextBox txtRatio;
        private System.Windows.Forms.TextBox label_Y;
        private System.Windows.Forms.TextBox label_X;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox heightTextBox;
        private System.Windows.Forms.TextBox widthTextBox;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.PictureBox btnOpen;
        private System.Windows.Forms.PictureBox btnSave;
    }
}