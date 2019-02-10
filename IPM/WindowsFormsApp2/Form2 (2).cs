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

            public Bitmap gpaphicLight(Bitmap image)
            {

                Bitmap barChart = null;
                if (image != null)
                {
                    int width = 640, height = 464;
                    //int width = 410, height = 268;
                    Bitmap bmpG = new Bitmap(image);
                    barChart = new Bitmap(width, height);
                 

                    Color cColor;
                    int[] iColor = new int[32];

                    for (int i = 0; i < bmpG.Width; i++)
                        for (int j = 0; j < bmpG.Height; j++)
                        {
                            cColor = bmpG.GetPixel(i, j);
                            iColor[i] = (cColor.R + cColor.G + cColor.B) / 3;
                        }

                    for (int i = 0; i < bmpG.Width *20; i += 20)
                    {
                        for (int j = height - 1; j > height - iColor[i / 20]; --j)
                            for (int m = 0; m < 10; m++)
                                barChart.SetPixel(i + m, j, Color.Black);
                        for (int j = 0; j < height; j++)
                            for (int m = 1; m < 10; m++)
                                barChart.SetPixel(i + 10 + m, j, Color.White);
                    }
                   
                }
                else
                    barChart = new Bitmap(1, 1);

                return barChart;

            } // График яркостей пикселей

            public Bitmap ImgIncr32(Bitmap bit)
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

            public Bitmap Bit32x32(Bitmap bmp)
            {
                Bitmap bmp32 = new Bitmap(bmp);
                Rectangle cropArea = new Rectangle(bmp.Width / 2, bmp.Height / 2, 32, 32);

                return bmp32.Clone(cropArea, bmp32.PixelFormat);
            }

            public Bitmap ImgIncr32x32(Bitmap bit)
            {

                //Bitmap barChart = null;
                //if (image != null)
                //{
                //    int width = 1100, height = 464;
                //    Bitmap bmpG = new Bitmap(image);
                //    barChart = new Bitmap(width, height);
                //
                //    Color cColor;
                //    int[] iColor = new int[bmpG.Width * bmpG.Height];
                //
                //    for (int i = 0; i < bmpG.Width; i++)
                //        for (int j = 0; j < bmpG.Height; j++)
                //        {
                //            cColor = bmpG.GetPixel(i, j);
                //            iColor[i * 32 + j] = (cColor.R + cColor.G + cColor.B) / 3;
                //        }
                //
                //    for (int i = 0; i < 32; i++)
                //    {
                //        for (int j = 0; j < 32; j++)
                //        {
                //            for (int l = height - 1; l > iColor[(i * 32) + j]; --l)
                //            {
                //                barChart.SetPixel((i * 32) + j, l, Color.Black);
                //            }
                //        }
                //
                //        /*for (int j = height - 1; j > height - iColor[i / 20]; --j)
                //            for (int m = 0; m < 10; m++)
                //                barChart.SetPixel(i + m, j, Color.Black);
                //        for (int j = 0; j < height; j++)
                //            for (int m = 1; m < 10; m++)
                //                barChart.SetPixel(i + 10 + m, j, Color.White);*/
                //    }
                //
                //}
                //else
                //    barChart = new Bitmap(1, 1);

                Bitmap bitbox = new Bitmap(320, 320);

                //int j1 = 0;

                for (int i = 0; i < bit.Width; i++)
                {
                    for (int j = 0; j < bit.Height; j++)
                    {
                        //j1 = j * 10;

                        Color clr = bit.GetPixel(i, j);

                        for (int k = i * 10; k < (i + 1) * 10; k++)
                        {
                            for (int m = j * 10; m < (j + 1) * 10; m++)
                            {
                                bitbox.SetPixel(k, m, clr);
                            }
                        }
                    }
                }

                return bitbox;
            }

            public Bitmap Fourier(Bitmap image)//обратное дискретное преобразование Фурье
            {

                Bitmap bmp = new Bitmap(image);
                Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                IntPtr ptr = bmpData.Scan0;
                
                
                int[] R = new int[256]; 
                byte[] N = new byte[256];
                byte[] left = new byte[256];
                byte[] right = new byte[256];

                int width = Math.Abs(bmpData.Stride);
                int bytes = width * bmp.Height; // общее количество пикселей в изображении
                byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
              

                double[] grayValuesFourD = new double[bytes];
                double[] grayValuesFourM = new double[bytes];
                double[] grayValuesFour = new double[bytes];
                byte[] grayValuesFourbyt = new byte[bytes];

                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);
           
                for (int k = 0; k < bytes; k += 4)
                {
                    for (int n = 0; n < bytes; n += 4)
                    {
                        grayValuesFourD[k] += grayValues[n] * Math.Pow(-1, n / 4) * (Math.Cos(Math.PI * 2 * k / 4 * n / 4 / 32));
                        grayValuesFourM[k] += grayValues[n] * Math.Pow(-1, n / 4) * (Math.Sin(Math.PI * 2 * k / 4 * n / 4 / 32));
                    }
                    grayValuesFour[k] = Math.Sqrt(grayValuesFourD[k] * grayValuesFourD[k] + grayValuesFourM[k] * grayValuesFourM[k]);
                    //grayValuesFour[k] = Math.Sqrt(grayValuesFourD[k] + grayValuesFourM[k]);
                    if (grayValuesFour[k] > 255)
                        grayValuesFourbyt[k] = 254;  //если 255, то на гистограмме не видно
                    else
                        grayValuesFourbyt[k] = (byte)grayValuesFour[k];

                    grayValuesFourbyt[k + 3] = 255;

                    for (int v = k + 1; v < k + 3; v += 1)
                    {
                        grayValuesFourbyt[v] = grayValuesFourbyt[k];
                    }
                }


                System.Runtime.InteropServices.Marshal.Copy(grayValuesFourbyt, 0, ptr, bytes);
                bmp.UnlockBits(bmpData);

                list3.Items.Clear();
                list3.Items.Add("Высота: " + bmp.Height);
                list3.Items.Add("Ширина: " + bmp.Width);
                list3.Items.Add("Количество яркостей: " + infoImage(bmp));

                return bmp;
            }     

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

            public Bitmap Fourier32x32(Bitmap image)
            {

                Bitmap bmp = new Bitmap(image);
                Bitmap bmpNew = null;
                bmpNew = new Bitmap(bmp.Width, bmp.Height);

                Color colorOne;
                int[,] colorOld = new int[32, 32];

                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        colorOne = bmp.GetPixel(i, j);
                        colorOld[i, j] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                    }
                }

                int[,] colorNew = new int[32, 32];
                double sumOldI, sumOld, sumOldJD, sumOldJM, sum2, sumI;
                sumOldI = 0;
                sumOld = 0;
                sum2 = 0;
                sumOldJD = 0;
                sumOldJM = 0;

                for (int iNew = 0; iNew < 32; iNew++)
                {
                    for (int jNew = 0; jNew < 32; jNew++)
                    {

                        

                        for (int iOld = 0; iOld < 32; iOld++)
                        {

                            

                            for (int jOld = 0; jOld < 32; jOld++)
                            {
                                sumOldJD += colorOld[iOld, jOld] * Math.Pow(-1, jOld) * (Math.Cos(Math.PI * 2 * (jOld + iNew)) / 32);
                                sumOldJM += colorOld[iOld, jOld] * Math.Pow(-1, jOld) * (Math.Sin(Math.PI * 2 * (jOld + iNew)) / 32);
                            }

                            sumOld += Math.Sqrt(sumOldJD * sumOldJD + sumOldJM * sumOldJM);
                   
                        }

                        //sum2 += sumOld;
                        sumI = (1 / (32 * 32)) * sumOld;

                        //sumI = sum2 / (32 * 32);
                        colorNew[iNew, jNew] = (int)sumI;
                    }
                }

                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < 32; j++)
                    {
                        //colorOne = Color.FromArgb(colorNew[i, j], colorNew[i, j], colorNew[i, j]);
                        //bmpNew.SetPixel(i, j, colorOne);
                        bmpNew.SetPixel(i, j, Color.FromArgb(colorNew[i, j], colorNew[i, j], colorNew[i, j]));
                    }
                }

     

                return bmpNew;
            }

            public Bitmap Fourier32x1Test(Bitmap image)
            {

                Bitmap bmp = new Bitmap(image);
                Bitmap bmpNew = null;
                bmpNew = new Bitmap(bmp.Width, bmp.Height);

                Color colorOne;
                int[,] colorOld = new int[32, 1];

                for (int i = 0; i < bmp.Width; i++)
                {
                    for (int j = 0; j < bmp.Height; j++)
                    {
                        colorOne = bmp.GetPixel(i, j);
                        colorOld[i, j] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                    }
                }

                int[,] colorNew = new int[32, 1];
                int sumOldI, sumOld, sumOldJD, sumOldJM;

                for (int iNew = 0; iNew < 32; iNew++)
                {
                    for (int jNew = 0; jNew < 1; jNew++)
                    {

                        sumOldI = 0;
                        sumOld = 0;

                        for (int iOld = 0; iOld < 32; iOld++)
                        {

                            sumOldJD = 0;
                            sumOldJM = 0;

                            for (int jOld = 0; jOld < 1; jOld++)
                            {
                                sumOldJD += (int)(colorOld[iOld, jOld] * Math.Pow(-1, jOld) * (Math.Cos(Math.PI * 2 * ((jOld * iNew / 32) + (iOld * iNew / 32)))));
                                sumOldJM += (int)(colorOld[iOld, jOld] * Math.Pow(-1, jOld) * (Math.Sin(Math.PI * 2 * ((jOld * iNew / 32) + (iOld * iNew / 32)))));
                            }

                            sumOld += (int)(Math.Sqrt(sumOldJD * sumOldJD + sumOldJM * sumOldJM));

                        }
                        //colorNew[iNew, jNew] = sumOld / (32 * 32);
                        colorNew[iNew, jNew] = (1 / (32 * 1)) * sumOld;
                    }
                }

                for (int i = 0; i < 32; i++)
                {
                    for (int j = 0; j < 1; j++)
                    {
                        //colorOne = Color.FromArgb(colorNew[i, j], colorNew[i, j], colorNew[i, j]);
                        //bmpNew.SetPixel(i, j, colorOne);
                        bmpNew.SetPixel(i, j, Color.FromArgb(colorNew[i, j], colorNew[i, j], colorNew[i, j]));
                    }
                }



                return bmpNew;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Дискретное преобразование Фурье

            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox7.Image = null;
            pictureBox8.Image = null;

            gistogram g = new gistogram();
            DiscreteFourierTransform DFT = new DiscreteFourierTransform(listView1, listView2, listView3);
            
            Bitmap DFTBit32Bitmapfurie = DFT.Bit32(Bitmapfurie);
            //Bitmap DFTFourierDFTBit32Bitmapfurie = DFT.Fourier(DFT.Bit32(Bitmapfurie));
            Bitmap DFTFourierDFTBit32Bitmapfurie = DFT.Fourier32x1Test(DFT.Bit32(Bitmapfurie));


            pictureBox2.Image = (Image)DFT.ImgIncr32(DFTBit32Bitmapfurie);
            pictureBox3.Image = (Image)DFT.ImgIncr32(DFTFourierDFTBit32Bitmapfurie);
            pictureBox7.Image = (Image)DFT.gpaphicLight(DFTBit32Bitmapfurie);
            pictureBox8.Image = (Image)DFT.gpaphicLight(DFTFourierDFTBit32Bitmapfurie);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Загрузка изображения

            pictureBox1.Image = null; // оригинал
            pictureBox2.Image = null; // до обработки
            pictureBox3.Image = null; // после обработки
            pictureBox6.Image = null; // оригинал
            pictureBox7.Image = null; // график яркостей до обработки
            pictureBox8.Image = null; // график яркостей после обработки

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

            class_.infoImage(Bitmapfurie);

            listView1.Items.Add("Высота: " + Bitmapfurie.Height);
            listView1.Items.Add("Ширина: " + Bitmapfurie.Width);
            listView1.Items.Add("Количество яркостей: " + class_.infoImage(Bitmapfurie));

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox7.Image = null;
            pictureBox8.Image = null;


            gistogram g = new gistogram();
            DiscreteFourierTransform DFT = new DiscreteFourierTransform(listView1, listView2, listView3);

            Bitmap DFTBit32Bitmapfurie = DFT.Bit32x32(Bitmapfurie);
            Bitmap DFTFourierDFTBit32Bitmapfurie = DFT.Fourier32x32(DFT.Bit32x32(Bitmapfurie));

            pictureBox2.Image = (Image)DFT.ImgIncr32x32(DFTBit32Bitmapfurie);
            pictureBox3.Image = (Image)DFT.ImgIncr32x32(DFTFourierDFTBit32Bitmapfurie);
            pictureBox7.Image = (Image)DFT.gpaphicLight(DFTBit32Bitmapfurie);
            pictureBox8.Image = (Image)DFT.gpaphicLight(DFTFourierDFTBit32Bitmapfurie);

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
