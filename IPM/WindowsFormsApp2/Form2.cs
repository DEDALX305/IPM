using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace WindowsFormsApp2
{
    public partial class Form2 : Form
    {
        public static Bitmap Bitmapfurie;


        public Form2()
        {
            InitializeComponent();
        }
        


        public class DiscreteFourierTransform
        {

            ListView list, list2, list3;

            public DiscreteFourierTransform(ListView list, ListView list2, ListView list3)
            {
                this.list = list;
                this.list2 = list2;
                this.list3 = list3;
            }

            public Bitmap funck1(Bitmap barChart)
            {



                Rectangle rect = new Rectangle(0, 0, barChart.Width, barChart.Height);
                System.Drawing.Imaging.BitmapData bmpData = barChart.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, barChart.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = barChart.Width * barChart.Height;// общее количество пикселей в изображении
                byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
                int[] R = new int[256]; //
                byte[] N = new byte[256];
                byte[] left = new byte[256];
                byte[] right = new byte[256];
                int Width = bmpData.Stride;
                int Height = barChart.Height;
                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                double[] grayValuesFourD = new double[bytes];
                double[] grayValuesFourM = new double[bytes];
                double[] grayValuesFour = new double[bytes];
                byte[] grayValuesFourbyt = new byte[bytes];

                for (int k = 0; k < 32; k++)
                {
                    for (int n = 0; n < 32; n++)
                    {
                        grayValuesFourD[k] += grayValues[n] * (Math.Cos(Math.PI * 2 * k * n / 32));
                        grayValuesFourM[k] += grayValues[n] * (Math.Sin(Math.PI * 2 * k * n / 32));

                    }
                    grayValuesFour[k] = Math.Sqrt(grayValuesFourD[k] * grayValuesFourD[k] + grayValuesFourM[k] * grayValuesFourM[k]);
                    grayValuesFour[k] = (int)grayValuesFour[k];
                    if (grayValuesFour[k] > 255)
                        grayValuesFourbyt[k] = 255;
                    else
                        grayValuesFourbyt[k] = (byte)grayValuesFour[k];
                }




                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
                barChart.UnlockBits(bmpData);


                return barChart;

            } //мусор

            public Bitmap testc(Bitmap TempBitmap)
            {
                Rectangle rect = new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = TempBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
                    TempBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;

                int bytes = bmpData.Stride * TempBitmap.Height;
                int Width = bmpData.Stride;
                byte[] grayValues = new byte[bytes];
                byte[] NewgrayValues = new byte[bytes];
                int end;

                byte[] buf = new byte[32];

                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                for (int i = 0; i < 32; i++)
                {

                    buf[i] = grayValues[i];
                }

                System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);

                Bitmap outputBitmap = null;

                TempBitmap.UnlockBits(bmpData);

                outputBitmap = funck1(TempBitmap);




                return outputBitmap;
            } //мусор

            public Bitmap ImgIncr(Bitmap bit)
            {
                Bitmap bitbox = new Bitmap(320, 10);

                int i, j = 0, l, m, k = 0;

                for (i = 0; i < bit.Width; i++)
                {
                    j = i * 10;

                    Color clr = bit.GetPixel(i, 0);

                    for (l = j; l < (j + 10); l++)
                    {
                        for (m = k; m < (k + 10); m++)
                        {
                            bitbox.SetPixel(l, m, clr);
                        }
                    }
                }

                return bitbox;
            } // Преобразования изображения с 32 пикселями в 320 пикселей

            public Bitmap Bit32(Bitmap bmp)
            {

                Bitmap bmp32 = new Bitmap(bmp);
                Rectangle cropArea = new Rectangle(bmp.Width / 2, bmp.Height / 2, 32, 1);   
                Bitmap bmp32out = bmp32.Clone(cropArea, bmp32.PixelFormat);

                list2.Items.Clear(); // Костыль
                list2.Items.Add("Высота: " + bmp32out.Height);
                list2.Items.Add("Ширина: " + bmp32out.Width);
                list2.Items.Add("Количество яркостей: " + infoImage(bmp32out));

                return bmp32out;

            } // Обрезка изоюбражения под 32 пикселя

            public Bitmap Fourier(Bitmap bmp)//обратное дискретное преобразование Фурье
            {

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = bmp.Width * bmp.Height;// общее количество пикселей в изображении
                byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
                int[] R = new int[256]; //
                byte[] N = new byte[256];
                byte[] left = new byte[256];
                byte[] right = new byte[256];
                int Width = bmpData.Stride;
                int Height = bmp.Height;
                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                double[] grayValuesFourD = new double[bytes];
                double[] grayValuesFourM = new double[bytes];
                double[] grayValuesFour = new double[bytes];
                byte[] grayValuesFourbyt = new byte[bytes];

                for (int k = 0; k < 32; k++)
                {
                    for (int n = 0; n < 32; n++)
                    {
                        grayValuesFourD[k] += grayValues[n] * (Math.Cos(Math.PI * 2 * k * n / 32));
                        grayValuesFourM[k] += grayValues[n] * (Math.Sin(Math.PI * 2 * k * n / 32));

                    }
                    grayValuesFour[k] = Math.Sqrt(grayValuesFourD[k] * grayValuesFourD[k] + grayValuesFourM[k] * grayValuesFourM[k]);
                    grayValuesFour[k] = (int)grayValuesFour[k];
                    if (grayValuesFour[k] > 255)
                        grayValuesFourbyt[k] = 255;
                    else
                        grayValuesFourbyt[k] = (byte)grayValuesFour[k];
                }

                //// Complex complex;
                // Complex grayValuesCom;
                // Complex grayValuesFourCom;
                // for (int k =0; k<32;k++)
                // {
                //     for (int n = 0; n< 32; n++)
                //     {
                //         grayValuesCom = new Complex(grayValues[n], 0);
                //         grayValuesFourCom=new Complex(grayValuesFour[k], 0);
                //         complex = new Complex(-2 * Math.PI * k * n / 32, 0);

                //         grayValuesFourCom = grayValuesCom *complex;

                //         //вывести на экран модуль (квадрат действит+ квадрат мнимой и корень из этого)
                //     }
                // }

                //List<byte> listNoise=new List<byte>();
                //List<double> listSSHF = new List<double>();
                //for (int k = 0; k < 32; k++)
                //{
                //    listNoise.Add(grayValues[k]);
                //}


                //    Complex ImOne = new Complex(0, 1);
                //Complex complex;
                //Complex sum;
                //Complex sk;
                //for (int n = 0; n <32; n++)
                //{
                //    sum = new Complex(0, 0);
                //    for (int k = 0; k < 32; k++)
                //    {
                //        sk = new Complex(listNoise[k], 0);
                //        complex = new Complex(-2 * Math.PI * k * n / 32, 0);
                //        complex = Complex.Multiply(ImOne, complex);
                //        complex = Complex.Exp(complex);
                //        complex = Complex.Multiply(sk, complex);
                //        sum = Complex.Add(sum, complex);
                //    }
                //    listSSHF.Add(Complex.Abs(sum));

                System.Runtime.InteropServices.Marshal.Copy(grayValuesFourbyt, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);

                list3.Items.Clear();
                list3.Items.Add("Высота: " + bmp.Height);
                list3.Items.Add("Ширина: " + bmp.Width);
                list3.Items.Add("Количество яркостей: " + infoImage(bmp));

                return bmp;
            }

            public Bitmap FourierTransformnoise(Bitmap bmp)
            {

                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = bmp.Width * bmp.Height;// общее количество пикселей в изображении
                byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
                int[] R = new int[256]; //
                byte[] N = new byte[256];
                byte[] left = new byte[256];
                byte[] right = new byte[256];
                int Width = bmpData.Stride;
                int Height = bmp.Height;
                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                double[] grayValuesFourD = new double[bytes];
                double[] grayValuesFourM = new double[bytes];
                double[] grayValuesFour = new double[bytes];
                byte[] grayValuesFourbyt = new byte[bytes];

                for (int k = 0; k < 32; k++)
                {
                    for (int n = 0; n < 32; n++)
                    {
                        grayValuesFourD[k] += grayValues[n] * (Math.Cos(Math.PI * 2 * k * n / 32));
                        grayValuesFourM[k] += grayValues[n] * (Math.Sin(Math.PI * 2 * k * n / 32));

                    }
                    grayValuesFour[k] = Math.Sqrt(grayValuesFourD[k] * grayValuesFourD[k] + grayValuesFourM[k] * grayValuesFourM[k]);
                    grayValuesFour[k] = (int)grayValuesFour[k];
                    if (grayValuesFour[k] > 255)
                        grayValuesFourbyt[k] = 255;
                    else
                        grayValuesFourbyt[k] = (byte)grayValuesFour[k];
                }



                //// Complex complex;
                // Complex grayValuesCom;
                // Complex grayValuesFourCom;
                // for (int k =0; k<32;k++)
                // {
                //     for (int n = 0; n< 32; n++)
                //     {
                //         grayValuesCom = new Complex(grayValues[n], 0);
                //         grayValuesFourCom=new Complex(grayValuesFour[k], 0);
                //         complex = new Complex(-2 * Math.PI * k * n / 32, 0);

                //         grayValuesFourCom = grayValuesCom *complex;

                //         //вывести на экран модуль (квадрат действит+ квадрат мнимой и корень из этого)
                //     }
                // }

                //List<byte> listNoise=new List<byte>();
                //List<double> listSSHF = new List<double>();
                //for (int k = 0; k < 32; k++)
                //{
                //    listNoise.Add(grayValues[k]);
                //}


                //    Complex ImOne = new Complex(0, 1);
                //Complex complex;
                //Complex sum;
                //Complex sk;
                //for (int n = 0; n <32; n++)
                //{
                //    sum = new Complex(0, 0);
                //    for (int k = 0; k < 32; k++)
                //    {
                //        sk = new Complex(listNoise[k], 0);
                //        complex = new Complex(-2 * Math.PI * k * n / 32, 0);
                //        complex = Complex.Multiply(ImOne, complex);
                //        complex = Complex.Exp(complex);
                //        complex = Complex.Multiply(sk, complex);
                //        sum = Complex.Add(sum, complex);
                //    }
                //    listSSHF.Add(Complex.Abs(sum));
                Bitmap _bmp;
                bmp.UnlockBits(bmpData);
                System.Runtime.InteropServices.Marshal.Copy(grayValuesFourbyt, 0, ptr, bytes);
                _bmp = bmp;
                return _bmp;

            } //мусор

            public int infoImage(Bitmap bmp) // Информация об изображении
            {

                Rectangle RectSource1 = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(RectSource1, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr Ptr = bmpData.Scan0;

                int bytes = bmpData.Stride * bmp.Height;
                byte[] grayValues = new byte[bytes];
     
                System.Runtime.InteropServices.Marshal.Copy(Ptr, grayValues, 0, bytes);

                bmp.UnlockBits(bmpData);

                return grayValues.Length;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Дискретное преобразование Фурье

            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;

            gistogram g = new gistogram();
            DiscreteFourierTransform DFT = new DiscreteFourierTransform(listView1, listView2, listView3);
            
            Bitmap DFTBit32Bitmapfurie = DFT.Bit32(Bitmapfurie);
            Bitmap DFTFourierDFTBit32Bitmapfurie = DFT.Fourier(DFT.Bit32(Bitmapfurie));

            pictureBox2.Image = (Image)DFT.ImgIncr(DFTBit32Bitmapfurie);
            pictureBox3.Image = (Image)DFT.ImgIncr(DFTFourierDFTBit32Bitmapfurie);
            pictureBox4.Image = (Image)g.GistogramNew(DFTBit32Bitmapfurie);
            pictureBox5.Image = (Image)g.GistogramNew(DFTFourierDFTBit32Bitmapfurie);

            //pictureBox2.Image = (Image)DFT.ImgIncr(DFT.Bit32(Bitmapfurie));
            //pictureBox3.Image = (Image)DFT.ImgIncr(DFT.Fourier(DFT.Bit32(Bitmapfurie)));
            //pictureBox4.Image = (Image)g.GistogramNew(DFT.Bit32(Bitmapfurie));
            //pictureBox5.Image = (Image)g.GistogramNew(DFT.Fourier(DFT.Bit32(Bitmapfurie)));

            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button2_Click(object sender, EventArgs e)
        {

            pictureBox1.Image = null; // оригинал
            pictureBox2.Image = null; // до обработки
            pictureBox3.Image = null; // после обработки
            pictureBox4.Image = null; // до обработки
            pictureBox5.Image = null; // после обработки
            pictureBox6.Image = null; // оригинал

            listView1.Items.Clear();

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Bitmap Image|*.bmp|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.Title = "Open Image File";
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    Bitmapfurie = new Bitmap(openFileDialog1.FileName);
                }
            }

            gistogram _gistogram = new gistogram();
            DiscreteFourierTransform class_ = new DiscreteFourierTransform(listView1, listView2, listView3);

            pictureBox1.Image = (Image)Bitmapfurie;
            pictureBox6.Image = (Image)_gistogram.gistogramM(Bitmapfurie);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;

            class_.infoImage(Bitmapfurie);

            listView1.Items.Add("Высота: " + Bitmapfurie.Height);
            listView1.Items.Add("Ширина: " + Bitmapfurie.Width);
            listView1.Items.Add("Количество яркостей: " + class_.infoImage(Bitmapfurie));

        }
    }
}
