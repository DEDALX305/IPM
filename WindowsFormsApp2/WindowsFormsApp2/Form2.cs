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
        


        class furie
        {

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

            }

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
            }

            public Bitmap ImgIncr(Bitmap bit)
            {
                Bitmap bitbox = new Bitmap(320, 10);

                /*Rectangle rect = new Rectangle(0, 0, bit.Width, bit.Height);
                System.Drawing.Imaging.BitmapData bmpData = bit.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bit.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                int bytes = bmpData.Stride * bit.Height;// общее количество пикселей в изображении

                Rectangle rectPR = new Rectangle(0, 0, bitbox.Width, bitbox.Height);
                System.Drawing.Imaging.BitmapData bmpDataPR = bitbox.LockBits(rectPR, System.Drawing.Imaging.ImageLockMode.ReadWrite, bitbox.PixelFormat);
                IntPtr ptrPR = bmpDataPR.Scan0;
                int bytesPR = bmpDataPR.Stride * bitbox.Height;// общее количество пикселей в изображении

                byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
                byte[] grayValuesPR = new byte[bytesPR];
                int[] R = new int[256]; //

                byte[] N = new byte[256];  //кривая отображения
                byte[] left = new byte[256];
                byte[] right = new byte[256];
                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);  // записываем значения яркостей пикселей в grayValues 
                System.Runtime.InteropServices.Marshal.Copy(ptrPR, grayValuesPR, 0, bytesPR);*/

                /*Rectangle rect = new Rectangle(0, 0, bit.Width, bit.Height);
                System.Drawing.Imaging.BitmapData bmpData = bit.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bit.PixelFormat);

                Rectangle rect = new Rectangle(0, 0, bit.Width, bit.Height);
                System.Drawing.Imaging.BitmapData bmpData = bit.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bit.PixelFormat);

                IntPtr ptr = bmpData.Scan0;
                int bytes = bit.Width * bit.Height;// общее количество пикселей в изображении
                byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
                byte[] grayValuesNew = new byte[bytes*10];

                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);*/

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

                /*System.Runtime.InteropServices.Marshal.Copy(grayValuesPR, 0, ptrPR, bytesPR);
                System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);

                bit.UnlockBits(bmpData);
                bitbox.UnlockBits(bmpDataPR);*/

                /*System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
                bit.UnlockBits(bmpData);*/

                return bitbox;
            }

            public Bitmap Bit32(Bitmap bmp)
            {
                Bitmap bmp32 = new Bitmap(bmp);
                Rectangle cropArea = new Rectangle(bmp.Width / 2, bmp.Height / 2, 32, 1);

                /*var bmp8bpp = Grayscale.CommonAlgorithms.BT709.Apply(bmp);

                Bitmap newbmp = new Bitmap(bmp32.Width, bmp32.Height, PixelFormat.Format8bppIndexed);
                Graphics gr = Graphics.FromImage(newbmp);
                gr.PageUnit = GraphicsUnit.Pixel;
                gr.DrawImageUnscaled(bmp32, 0, 0);

                ComplexImage complexImage1 = ComplexImage.FromBitmap(newbmp);
                complexImage1.ForwardFourierTransform();
                Bitmap fourierImage = complexImage1.ToBitmap();

                return fourierImage;*/

                return bmp32.Clone(cropArea, bmp32.PixelFormat);
            }

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

                return bmp;
            }

            public Bitmap FourierTransformnoise(Bitmap bmp)//обратное дискретное преобразование Фурье
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

            }

        }



        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            ////Bitmap bmpPR = new Bitmap(Bitmapfurie);
            //Rectangle rect = new Rectangle(0, 0, Bitmapfurie.Width, Bitmapfurie.Height);
            //System.Drawing.Imaging.BitmapData bmpData = Bitmapfurie.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite,
            //    Bitmapfurie.PixelFormat);
            //IntPtr ptr = bmpData.Scan0;
            //int bytes = bmpData.Stride * Bitmapfurie.Height;// общее количество пикселей в изображении3
            //int Width = bmpData.Stride;
            //int Height = Bitmapfurie.Height;
            //byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
            //int end;

            //Bitmap bmp32 = new Bitmap(Bitmapfurie);
            //
            //Rectangle rectPR = new Rectangle(0, 0, 1, 32);
            //
            //Bitmapfurie.Clone(rectPR, Bitmapfurie.PixelFormat);

            //Bitmapfurie.UnlockBits();

            gistogram g = new gistogram();

            //2 и 4 до обработки
              //  3 и 5 после
                //1 и 6 оригинал


            
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;

            furie fu = new furie();

            pictureBox2.Image = (Image)fu.ImgIncr(fu.Bit32(Bitmapfurie));
            pictureBox4.Image = (Image)g.GistogramNew(fu.Bit32(Bitmapfurie));




            pictureBox3.Image = (Image)fu.ImgIncr(fu.Fourier(fu.Bit32(Bitmapfurie)));
            //furie f = new furie();
            //pictureBox2.Image = (Image)f.funck1(Bitmapfurie);
            //pictureBox2.Image = (Image)fu.testc(Bitmapfurie);;



            //pictureBox4.Image = (Image)g.gistogramM(Fourier(Bit32(Bitmapfurie)));
            pictureBox5.Image = (Image)g.GistogramNew(fu.Fourier(fu.Bit32(Bitmapfurie)));
            //pictureBox4.Image = (Image)g.gistogramM(f.funck1(Bitmapfurie));

            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox5.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Загурзить изображение

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            pictureBox5.Image = null;
            pictureBox6.Image = null;

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


            pictureBox1.Image = (Image)Bitmapfurie;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            gistogram _gistogram = new gistogram();


            pictureBox6.Image = (Image)_gistogram.gistogramM(Bitmapfurie);
            pictureBox6.SizeMode = PictureBoxSizeMode.Zoom;
        }
    }
}
