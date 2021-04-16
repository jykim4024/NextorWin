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

        // 캡쳐관련
        private Bitmap btMain;
        private bool roiView = false;

        private string warterText;

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

            this.sourceBitmap = new Bitmap(detailImageBox.Image);
            ShowTargetBitmap();

            this.widthTextBox.TextChanged += sizeTextBox_TextChanged;
            this.heightTextBox.TextChanged += sizeTextBox_TextChanged;

            IniFile config = new IniFile();
            config.Load("../../../config/config.ini");
            warterText = config["Wartermark"]["warterText"].ToString();

            //getConfig();
        }
        /*
        private void getConfig()
        {
            if (File.Exists("../../../config/config.ini"))
            {
                StreamReader sr = new StreamReader("../../../config/config.ini");
                config = sr.ReadLine();
                sr.Close();
            }
        }
        */
        private Bitmap LoadBitmapUnlocked(string filePath)
        {
            using (Bitmap bitmap = new Bitmap(filePath))
            {
                return new Bitmap(bitmap);
            }
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

            txtZoomRatio.Text = "x " + Math.Round(zoomRatio, 3, MidpointRounding.AwayFromZero).ToString();
            //txtZoomRatio.Text = zoomRatio.ToString();
            this.sourceBitmap = new Bitmap(detailImageBox.Image);
            //ShowTargetBitmap();

            detailImageBox.Invalidate();
        }

        private void ShowTargetBitmap()
        {
            if (this.sourceBitmap == null)
            {
                return;
            }

            //int targetWidth = (int)(this.sourceBitmap.Width * this.imageScale);
            //int targetHeight = (int)(this.sourceBitmap.Height * this.imageScale);

            int targetWidth = (int)(this.sourceBitmap.Width / this.zoomRatio);
            int targetHeight = (int)(this.sourceBitmap.Height / this.zoomRatio);

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

            this.detailImageBox.Image = this.targetBitmap;

            this.detailImageBox.Visible = true;

            this.detailImageBox.Refresh();
        }

        private void detailImageBox_Paint(object sender, PaintEventArgs e)
        {
            if (detailImageBox.Image != null)
            {
                e.Graphics.InterpolationMode = InterpolationMode.Default;

                e.Graphics.DrawImage(detailImageBox.Image, imgRect);
                detailImageBox.Focus();
            }

            if (roiView == true)
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
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label_X.Text = ((Cursor.Position.X - (this.Location.X + detailImageBox.Location.X + 8)).ToString());
            label_Y.Text = ((Cursor.Position.Y - (this.Location.Y + detailImageBox.Location.Y + 30)).ToString());
        }

        private void detailImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            //Zoom
            if (e.Button == MouseButtons.Left)
            {
                clickPoint = new Point(e.X, e.Y);
            }
            else if (e.Button == MouseButtons.Right)
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
            detailImageBox.Invalidate();
            /*
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
            */
        }

        private void detailImageBox_MouseMove(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                imgRect.X = imgRect.X + (int)Math.Round((double)(e.X - clickPoint.X) / 5);
                if (imgRect.X >= 0) imgRect.X = 0;
                if (Math.Abs(imgRect.X) >= Math.Abs(imgRect.Width - detailImageBox.Width)) imgRect.X = -(imgRect.Width - detailImageBox.Width);
                imgRect.Y = imgRect.Y + (int)Math.Round((double)(e.Y - clickPoint.Y) / 5);
                if (imgRect.Y >= 0) imgRect.Y = 0;
                if (Math.Abs(imgRect.Y) >= Math.Abs(imgRect.Height - detailImageBox.Height)) imgRect.Y = -(imgRect.Height - detailImageBox.Height);
            }
            else if (e.Button == MouseButtons.Right)
            {
                LastPoint = e.Location;

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
            detailImageBox.Invalidate();

            /*
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
            */
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
            Font font = new Font("Copperplate Gothic Light", 20, FontStyle.Bold);

            if (this.saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Rectangle rectangle = GetSelectionRectangle(false);
                    if (roiView == true)
                    {
                        Bitmap bitmap = new Bitmap(rectangle.Width, rectangle.Height);
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.DrawImage(this.sourceBitmap, 0, 0, rectangle, GraphicsUnit.Pixel);
                        }
                        SaveImage(bitmap, this.saveFileDialog.FileName);
                    }
                    else
                    {
                        Bitmap bitmap = new Bitmap(detailImageBox.Image);
                        using (Graphics graphics = Graphics.FromImage(bitmap))
                        {
                            graphics.DrawImage(this.sourceBitmap, 0, 0, new Rectangle(0,0, detailImageBox.Width, detailImageBox.Height), GraphicsUnit.Pixel);
                            graphics.DrawString(warterText, font, Brushes.LightGreen, 10, 10);
                        }
                        SaveImage(bitmap, this.saveFileDialog.FileName);
                    }
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

        public static Image resizeImage(Image image, int x, int y,int width, int height)
        {
            if (image != null)
            {
                Bitmap croppedBitmap = new Bitmap(image);
                // Rectangle(이미지의 x시작 위치값, 이미지의 y 시작 위치값, 이미지에 새로적용할 width, 이미지에 새로 적용할 height)
                // 예) size(450,450) > 상단 100, 하단 100 자를때...>> Rectangle(0,100,450,(450-200))
                croppedBitmap = croppedBitmap.Clone(new Rectangle(x, y, image.Width - width, image.Height - height), System.Drawing.Imaging.PixelFormat.DontCare);
                return croppedBitmap;
            }
            else
            {
                return image;
            }

            /*
            if (image != null)
            {
                Bitmap croppedBitmap = new Bitmap(image);
                // Rectangle(이미지의 x시작 위치값, 이미지의 y 시작 위치값, 이미지에 새로적용할 width, 이미지에 새로 적용할 height)
                // 예) size(450,450) > 상단 100, 하단 100 자를때...>> Rectangle(0,100,450,(450-200))
                croppedBitmap = croppedBitmap.Clone(new Rectangle(0, 100, image.Width, image.Height - 200), System.Drawing.Imaging.PixelFormat.DontCare);
                return croppedBitmap;
            }
            else
            {
                return image;
            }
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            btMain = new Bitmap(detailImageBox.Width, detailImageBox.Height);
            Graphics graphics = Graphics.FromImage(btMain);
            graphics.CopyFromScreen(PointToScreen(new Point(this.detailImageBox.Location.X, this.detailImageBox.Location.Y)), new Point(0, 0), this.detailImageBox.Size);
            btMain.Save("temp.png", ImageFormat.Png);
            detailImageBox.ImageLocation = "temp.png";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            detailImageBox.Image = img;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int w = Int32.Parse(widthTextBox.Text);
            int h = Int32.Parse(heightTextBox.Text);
            w = w + 10;
            widthTextBox.Text = w.ToString();
            heightTextBox.Text = h.ToString();
            roiView = true;
            w = w - 10;
            widthTextBox.Text = w.ToString();

            button4.Enabled = false;
            button5.Enabled = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int w = Int32.Parse(widthTextBox.Text);
            int h = Int32.Parse(heightTextBox.Text);
            w = w + 10;
            widthTextBox.Text = w.ToString();
            heightTextBox.Text = h.ToString();
            roiView = false;
            w = w - 10;
            widthTextBox.Text = w.ToString();

            button4.Enabled = true;
            button5.Enabled = false;
        }

        /// <summary>
        /// 이미지에 투명 박스 및 Ractangle 그리기
        /// </summary>
        /// <param name="image"></param>
        /// <param name="FileFullPath"></param>
        /// <returns></returns>
        public static Image DrawRect(Image image, string FileFullPath)
        {
            int sx = 25;
            int sy = 25;
            int ex = 575;
            int ey = 375;

            int sx2 = 50;
            int sy2 = 150;
            int ex2 = 527;
            int ey2 = 344;

            Rectangle raBig;
            Rectangle raSmall;

            try
            {
                raBig = new Rectangle(sx, sy, ex - sx, ey - sy);
                raSmall = new Rectangle(sx2, sy2, ex2 - sx2, ey2 - sy2);

                using (Graphics grp = Graphics.FromImage(image))
                {
                    Brush bigBrush = new SolidBrush(Color.FromArgb(16, 0, 0, 255));
                    grp.FillRectangle(bigBrush, raBig);

                    Pen pSmail = new Pen(Color.FromArgb(128, 0, 255, 0), 4);
                    pSmail.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                    grp.DrawRectangle(pSmail, raSmall);
                }
                return image;
                /*
                string realFileFullPath = Path.GetFileName(FileFullPath);
                string[] sRect = realFileFullPath.Split('_');

                if (sRect.Length >= 5)
                {
                    raBig = new Rectangle(sx, sy, ex - sx, ey - sy);
                    raSmall = new Rectangle(sx2, sy2, ex2 - sx2, ey2 - sy2);

                    using (Graphics grp = Graphics.FromImage(image))
                    {
                        Brush bigBrush = new SolidBrush(Color.FromArgb(16, 0, 0, 255));
                        grp.FillRectangle(bigBrush, raBig);

                        Pen pSmail = new Pen(Color.FromArgb(128, 0, 255, 0), 4);
                        pSmail.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
                        grp.DrawRectangle(pSmail, raSmall);
                    }
                    return image;
                }
                */
            }
            catch (Exception e)
            {

            }
            return image;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            detailImageBox.Image = DrawRect(detailImageBox.Image, @"C:\Users\Joon Young\Documents\카카오톡 받은 파일\cutyTY.jpg");
        }

        /// <summary>
        /// Get Watermark Image
        /// </summary>
        /// <param name="sourceImageFileParh">소스 이미지 파일 경로</param>
        /// <param name="watermarkFont">워터마크 폰트</param>
        /// <param name="watermarkTextOpacity">워터마크 텍스트 투명도</param>
        /// <param name="watermarkTextBrush">워터마크 텍스트 브러시</param>
        /// <param name="watermarkText">워터마크 텍스트</param>
        /// <returns></returns>
        public static Image GetWatermarkImage(string sourceImageFileParh, Font watermarkFont, float watermarkTextOpacity, Brush watermarkTextBrush, string watermarkText)
        {
            Image sourceImage = Image.FromFile(sourceImageFileParh);
            Graphics sourceImageGraphics = Graphics.FromImage(sourceImage);
            SizeF watermarkTextSize = sourceImageGraphics.MeasureString(watermarkText, watermarkFont);
            Bitmap targetImage = new Bitmap(sourceImage.Width, sourceImage.Height);
            Bitmap watermarkImage = new Bitmap((int)watermarkTextSize.Width, (int)watermarkTextSize.Height);
            watermarkImage.SetResolution(sourceImage.HorizontalResolution, sourceImage.VerticalResolution);
            Graphics watermarkImageGraphics = Graphics.FromImage(watermarkImage);
            watermarkImageGraphics.PageUnit = GraphicsUnit.Point;
            watermarkImageGraphics.Clear(Color.Empty);
            watermarkImageGraphics.DrawString(watermarkText, watermarkFont, watermarkTextBrush, new RectangleF(0, 0, watermarkTextSize.Width, watermarkTextSize.Height), StringFormat.GenericDefault);
            Graphics targetImageGraphics = Graphics.FromImage(targetImage);
            float[][] colorMatrixArray = {
                new float[] { 1, 0, 0, 0, 0 },
                new float[] { 0, 1, 0, 0, 0 },
                new float[] { 0, 0, 1, 0, 0 },
                new float[] { 0, 0, 0, watermarkTextOpacity / 100f, 0 },
                new float[] { 0, 0, 0, 0, 1 }
            };
            ColorMatrix colorMatrix = new ColorMatrix(colorMatrixArray);
            ImageAttributes imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            targetImageGraphics.DrawImage(sourceImage, 0, 0, sourceImage.Width, sourceImage.Height);
            targetImageGraphics.DrawImage(
                watermarkImage, 
                new Rectangle(
                    sourceImage.Width - watermarkImage.Width - 5, 
                    sourceImage.Height - watermarkImage.Height - 5, 
                    watermarkImage.Width, watermarkImage.Height), 
                0f, 
                0f, 
                watermarkImage.Width, 
                watermarkImage.Height, 
                GraphicsUnit.Pixel, 
                imageAttributes);

            return targetImage;
        }
    }
}
