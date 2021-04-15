using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Diagnostics;

namespace NextorWin
{
    public partial class Detail_View : Form
    {
        // mouse zoom & image move
        private Point LastPoint;
        private Bitmap img;
        /// <summary>
        /// 확대, 축소 비율 Handling
        /// </summary>
        private double zoomRatio = 1.0F;
        /// <summary>
        /// 확대, 축소 이미지를 Handling
        /// </summary>
        private Rectangle imgRect;
        /// <summary>
        /// 마우스 포인트를 저장
        /// </summary>
        private Point imgPoint;
        private Point clickPoint;

        // image ROI & save
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

        public Detail_View()
        {
            InitializeComponent();

            detailImageBox.MouseWheel += new MouseEventHandler(detailImage_MouseWheel);

            // 임시
            img = new Bitmap(@"C:\Users\Joon Young\Documents\카카오톡 받은 파일\cutyTY.jpg");
            detailImageBox.Image = img;
            imgPoint = new Point(detailImageBox.Width / 2, detailImageBox.Height / 2);
            imgRect = new Rectangle(0, 0, detailImageBox.Width, detailImageBox.Height);
            zoomRatio = 1.0;
            clickPoint = imgPoint;

            detailImageBox.Invalidate();

            this.widthTextBox.TextChanged += sizeTextBox_TextChanged;
            this.heightTextBox.TextChanged += sizeTextBox_TextChanged;
        }

        private void detailImage_MouseWheel(object sender, MouseEventArgs e)
        {
            int lines = e.Delta * SystemInformation.MouseWheelScrollLines / 120;
            PictureBox pb = (PictureBox)sender;

            if (lines > 0)
            {
                zoomRatio *= 1.1F;
                if (zoomRatio > 100.0) zoomRatio = 100.0f;

                imgRect.Width = (int)Math.Round(detailImageBox.Width * zoomRatio);
                imgRect.Height = (int)Math.Round(detailImageBox.Height * zoomRatio);
                imgRect.X = -(int)Math.Round(1.1F * (imgPoint.X - imgRect.X) - imgPoint.X);
                imgRect.Y = -(int)Math.Round(1.1F * (imgPoint.Y - imgRect.Y) - imgPoint.Y);
            }
            else if (lines < 0)
            {
                zoomRatio *= 0.9F;
                if (zoomRatio < 1) zoomRatio = 1;

                imgRect.Width = (int)Math.Round(detailImageBox.Width * zoomRatio);
                imgRect.Height = (int)Math.Round(detailImageBox.Height * zoomRatio);
                imgRect.X = -(int)Math.Round(0.9F * (imgPoint.X - imgRect.X) - imgPoint.X);
                imgRect.Y = -(int)Math.Round(0.9F * (imgPoint.Y - imgRect.Y) - imgPoint.Y);
            }

            if (imgRect.X > 0) imgRect.X = 0;
            if (imgRect.Y > 0) imgRect.Y = 0;
            if (imgRect.X + imgRect.Width < detailImageBox.Width) imgRect.X = detailImageBox.Width - imgRect.Width;
            if (imgRect.Y + imgRect.Height < detailImageBox.Height) imgRect.Y = detailImageBox.Height - imgRect.Height;
            detailImageBox.Invalidate();
        }

        private void detailImageBox_Paint(object sender, PaintEventArgs e)
        {
            if (detailImageBox.Image != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.Default;

                e.Graphics.DrawImage(detailImageBox.Image, imgRect);
                detailImageBox.Focus();
            }

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
            label_X.Text = ((Cursor.Position.X - (this.Location.X + detailImageBox.Location.X + 8)).ToString());
            label_Y.Text = ((Cursor.Position.Y - (this.Location.Y + detailImageBox.Location.Y + 30)).ToString());
        }

        private void detailImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (zoomRatio != 1.0)
            {
                //Zoom
                if (e.Button == MouseButtons.Left)
                {
                    clickPoint = new Point(e.X, e.Y);
                }
                detailImageBox.Invalidate();
            }
            else
            {
                // ROI
                Rectangle rectangle = GetSelectionRectangle(true);
                if (!rectangle.Contains(e.Location))
                {
                    return;
                }
                this.lastPoint = e.Location;
                this.isDragging = true;
            }
        }

        private void detailImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (zoomRatio != 1.0)
            {
                // Zoom
                if (e.Button == MouseButtons.Left)
                {
                    imgRect.X = imgRect.X + (int)Math.Round((double)(e.X - clickPoint.X) / 5);
                    if (imgRect.X >= 0) imgRect.X = 0;
                    if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - detailImageBox.Width)) imgRect.X = -(imgRect.Width - detailImageBox.Width);
                    imgRect.Y = imgRect.Y + (int)Math.Round((double)(e.Y - clickPoint.Y) / 5);
                    if (imgRect.Y >= 0) imgRect.Y = 0;
                    if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - detailImageBox.Height)) imgRect.Y = -(imgRect.Height - detailImageBox.Height);
                }
                else
                {
                    LastPoint = e.Location;
                }
                detailImageBox.Invalidate();
            }
            else
            {
                // ROI
                if (!this.isDragging)
                {
                    return;
                }
                this.leftTopPoint.X += (int)((e.Location.X - this.lastPoint.X) / this.imageScale);
                this.leftTopPoint.Y += (int)((e.Location.Y - this.lastPoint.Y) / this.imageScale);
                this.lastPoint = e.Location;
                this.detailImageBox.Refresh();
            }
        }

        private void Detail_View_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void sizeTextBox_TextChanged(object sender, EventArgs e)
        {
            this.detailImageBox.Refresh();
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

        private void detailImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            this.isDragging = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            int w = Int32.Parse(widthTextBox.Text);
            int h = Int32.Parse(heightTextBox.Text);

            if (w==50 && h>50)
            {
                h = h - 10;
            }
            else if (w>50 && h==50)
            {
                w = w - 10;
            }
            else if (w>50 && h>50)
            {
                w = w - 10;
                h = h - 10;
            }
            if (w < 50 || h < 50)
            {

            }
            else
            {
                widthTextBox.Text = w.ToString();
                heightTextBox.Text = h.ToString();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            int w = Int32.Parse(widthTextBox.Text);
            int h = Int32.Parse(heightTextBox.Text);

            if (w==500 && h<500)
            {
                h = h + 10;
            }
            else if (w<500 && h==500)
            {
                w = w + 10;
            }
            else if (w<500 && h<500)
            {
                w = w + 10;
                h = h + 10;
            }
            if (w > 500 || h > 500)
            {

            }
            else
            {
                widthTextBox.Text = w.ToString();
                heightTextBox.Text = h.ToString();
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            int w = Int32.Parse(widthTextBox.Text);
            int h = Int32.Parse(heightTextBox.Text);

            if (w == 50 && h > 50)
            {
                h = h - 50;
            }
            else if (w > 50 && h == 50)
            {
                w = w - 50;
            }
            else if (w > 50 && h > 50)
            {
                w = w - 50;
                h = h - 50;
            }
            if (w < 50 || h < 50)
            {

            }
            else
            {
                widthTextBox.Text = w.ToString();
                heightTextBox.Text = h.ToString();
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            int w = Int32.Parse(widthTextBox.Text);
            int h = Int32.Parse(heightTextBox.Text);

            if (w == 500 && h < 500)
            {
                h = h + 50;
            }
            else if (w < 500 && h == 500)
            {
                w = w + 50;
            }
            else if (w < 500 && h < 500)
            {
                w = w + 50;
                h = h + 50;
            }

            if (w>500 || h>500)
            {

            }
            else
            {
                widthTextBox.Text = w.ToString();
                heightTextBox.Text = h.ToString();
            }
        }
    }
}
