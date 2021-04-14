using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

namespace NextorWin
{
    public partial class View : Form
    {
        private int mag = 0;

        /// 좌상단 포인트
        private Point leftTopPoint = new Point(0, 0);

        /// 드래그 여부
        private bool isDragging = false;

        /// 마지막 포인트
        private Point lastPoint;

        /// 소스 비트맵
        private Bitmap sourceBitmap = null;

        /// 타겟 비트맵
        private Bitmap targetBitmap = null;

        /// 이미지 스케일
        private float imageScale = 1f;

        public View()
        {
            InitializeComponent();

            this.widthTextBox.TextChanged += sizeTextBox_TextChanged;
            this.heightTextBox.TextChanged += sizeTextBox_TextChanged;
        }

        private void View_Load(object sender, EventArgs e)
        {
            timer1.Start();

            ctrl_ScrollPictureBox1.zoomRate = 0.1;
            ctrl_ScrollPictureBox1.zoom = 1.0;
            ctrl_ScrollPictureBox1.LoadImage("../../../results/Image.png");
            ctrl_ScrollPictureBox1.ZoomFit();
        }

        private void sizeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.ctrl_ScrollPictureBox1.Refresh();
        }

        private Rectangle GetSelectionRectangle(bool scaled)
        {
            int x;
            int y;
            int width;
            int height;

            if (!int.TryParse(this.widthTextBox.Text, out width))
            {
                return new Rectangle();
            }

            if (!int.TryParse(this.heightTextBox.Text, out height))
            {
                return new Rectangle();
            }

            x = this.leftTopPoint.X;
            y = this.leftTopPoint.Y;

            if (scaled)
            {
                x = (int)(x * this.imageScale);
                y = (int)(y * this.imageScale);

                width = (int)(width * this.imageScale);
                height = (int)(height * this.imageScale);
            }

            return new Rectangle(x, y, width, height);
        }

        private void btnAutofit_MouseDown(object sender, MouseEventArgs e)
        {
            btnAutofit.BackgroundImage = NextorWin.Properties.Resources.btnAutoFit_d;
        }

        private void btnAutofit_MouseUp(object sender, MouseEventArgs e)
        {
            btnAutofit.BackgroundImage = NextorWin.Properties.Resources.btnAutoFit;

            ctrl_ScrollPictureBox1.ZoomFit();
            txtRatio.Text = "";
        }

        private void btnZoomin_MouseDown(object sender, MouseEventArgs e)
        {
            btnZoomin.BackgroundImage = NextorWin.Properties.Resources.btnZoomin_d;
        }

        private void btnZoomin_MouseUp(object sender, MouseEventArgs e)
        {
            btnZoomin.BackgroundImage = NextorWin.Properties.Resources.btnZoomin;

            if (ctrl_ScrollPictureBox1.zoom <= 3)
            {
                ctrl_ScrollPictureBox1.ZoomIn();
            }

            txtRatio.Text = ctrl_ScrollPictureBox1.zoom.ToString();
        }

        private void btnZoomout_MouseDown(object sender, MouseEventArgs e)
        {
            btnZoomout.BackgroundImage = NextorWin.Properties.Resources.btnZoomout_d;
        }

        private void btnZoomout_MouseUp(object sender, MouseEventArgs e)
        {
            btnZoomout.BackgroundImage = NextorWin.Properties.Resources.btnZoomout;
            ctrl_ScrollPictureBox1.ZoomOut();
            txtRatio.Text = ctrl_ScrollPictureBox1.zoom.ToString();
        }

        private void ctrl_ScrollPictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!this.isDragging)
            {
                return;
            }

            this.leftTopPoint.X += (int)((e.Location.X - this.lastPoint.X) / this.imageScale);
            this.leftTopPoint.Y += (int)((e.Location.Y - this.lastPoint.Y) / this.imageScale);

            this.lastPoint = e.Location;

            this.ctrl_ScrollPictureBox1.Refresh();
        }

        private void ctrl_ScrollPictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            Rectangle rectangle = GetSelectionRectangle(true);

            if (!rectangle.Contains(e.Location))
            {
                return;
            }

            this.lastPoint = e.Location;

            this.isDragging = true;
        }

        private void ctrl_ScrollPictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            this.isDragging = false;
        }

        private void ctrl_ScrollPictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Color c = Color.FromArgb(191, 255, 0);
                using (Pen pen = new Pen(c, 2))
                {
                    Rectangle rectangle = GetSelectionRectangle(true);

                    e.Graphics.DrawRectangle(pen, rectangle);
                }
            }
            catch
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_X.Text = (Cursor.Position.X - (this.Location.X + ctrl_ScrollPictureBox1.Location.X + 8)).ToString();
            label_Y.Text = (Cursor.Position.Y - (this.Location.Y + ctrl_ScrollPictureBox1.Location.Y + 30)).ToString();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (this.openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.sourceBitmap = LoadBitmapUnlocked(this.openFileDialog.FileName);

                    ShowTargetBitmap();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private Bitmap LoadBitmapUnlocked(string filePath)
        {
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                return new Bitmap(bitmap);
            }
        }

        private void ShowTargetBitmap()
        {
            if (this.sourceBitmap == null)
            {
                return;
            }

            int targetWidth = (int)(this.sourceBitmap.Width * this.imageScale);
            int targetHeight = (int)(this.sourceBitmap.Height * this.imageScale);

            this.targetBitmap = new Bitmap(targetWidth, targetHeight);

            using (Graphics graphics = Graphics.FromImage(this.targetBitmap))
            {
                Point[] targetPointArray =
                {
                    new Point(0              ,                0),
                    new Point(targetWidth - 1,                0),
                    new Point(0              , targetHeight - 1)
                };

                Rectangle sourceRectangle = new Rectangle
                (
                    0,
                    0,
                    this.sourceBitmap.Width - 1,
                    this.sourceBitmap.Height - 1
                );

                graphics.DrawImage(this.sourceBitmap, targetPointArray, sourceRectangle, GraphicsUnit.Pixel);
            }

            this.ctrl_ScrollPictureBox1.LoadImage(this.targetBitmap);
            this.ctrl_ScrollPictureBox1.Visible = true;
            this.ctrl_ScrollPictureBox1.Refresh();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Rectangle rectangle = GetSelectionRectangle(false);

                    Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);

                    using (Graphics graphics = Graphics.FromImage(bitmap))
                    {
                        graphics.DrawImage(this.sourceBitmap, 0, 0, rectangle, GraphicsUnit.Pixel);
                    }

                    SaveImage(bitmap, this.saveFileDialog.FileName);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        public void SaveImage(Image image, string filePath)
        {
            string extension = Path.GetExtension(filePath);

            switch (extension.ToLower())
            {
                case ".bmp": image.Save(filePath, ImageFormat.Bmp); break;
                case ".exif": image.Save(filePath, ImageFormat.Exif); break;
                case ".gif": image.Save(filePath, ImageFormat.Gif); break;
                case ".jpg":
                case ".jpeg": image.Save(filePath, ImageFormat.Jpeg); break;
                case ".png": image.Save(filePath, ImageFormat.Png); break;
                case ".tif":
                case ".tiff": image.Save(filePath, ImageFormat.Tiff); break;
                default: throw new NotSupportedException("Unknown file extension " + extension);
            }
        }
    }
}
