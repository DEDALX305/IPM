using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;
using System.Threading;
using System.Collections.Concurrent;
using System.Diagnostics;



namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        public static Bitmap Bitmapfurie;
        public static Complex[,] publicColor = new Complex[1000, 1000];


        public Form3()
        {
            InitializeComponent();
        }


        public class TwoDimensionalFourierTransform
        {

            public Bitmap TwoDimensional(Bitmap bit) 
            {
                Bitmap bitbox = new Bitmap(bit);
                // int size = bitbox.Width * bitbox.Height;
                int Width = bitbox.Width, Height = bitbox.Height;

                double[,] RedIsh = new double[bitbox.Width, bitbox.Height]; // информация о яркости каждого пикселя
                Color[,] ColIsh = new Color[bitbox.Width, bitbox.Height]; //исходные цвета пикселей


                double[,] RedFur = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS
                double[,] RedFur2 = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS

                Color[,] ColFour = new Color[bitbox.Width, bitbox.Height];
                Complex[,] RedComp = new Complex[bitbox.Width, bitbox.Height]; // комлексные числа 
                                                                               // Complex RedCal;
                                                                               //Complex RedIshCom;
                double max = 0;
                double hu;



                /// <summary> 
                /// Получение исходго цвета пикселей
                /// </summary> 
                for (int k = 0; k < Width; k++)
                {
                    for (int n = 0; n < Height; n++)
                    {
                        ColIsh[k, n] = bitbox.GetPixel(k, n);
                        RedIsh[k, n] = (ColIsh[k, n].G + ColIsh[k, n].R + ColIsh[k, n].B) / 3;
                    }
                }

                // Width - Ширина
                // Height - Высота

                for (int h = 0; h < Height; h++) // u
                    for (int w = 0; w < Width; w++) // w
                    {
                        for (int k = 0; k < Height; k++) // M Столбец
                        {
                            for (int n = 0; n < Width; n++) // N Строка
                            {
                                //RedIsh[k, n] = RedIsh[k, n] * Math.Pow(-1, n + k); // создает рябь черную умножает на -1 чтобы центровать -1^m+n
                                hu = 2 * Math.PI * (((double)h * k / Height) + ((double)w * n / Width)); // Расчет чисто степень

                                Complex a = new Complex(RedIsh[k, n], 0); // ковенртация цветов в комплексное Xmn

                                Complex b = new Complex(Math.Cos(hu), -Math.Sin(hu)); // действительная и мнимая часть Е в степени 

                                Complex c = Complex.Multiply(a, b); // E  * Xmn

                                RedComp[h, w] = Complex.Add(RedComp[h, w], c * Math.Pow(-1, n + k)); // проверить если полосы // сложение обоих сумм
                            }
                        }
                        RedFur[h, w] = Complex.Abs(RedComp[h, w]); // 
                        RedFur[h, w] = RedFur[h, w] / ((double)Height * Width); // перевод в обычное число
                    }


                max = 0;
                for (int h = 0; h < Height; h++)
                    for (int w = 0; w < Width; w++)
                    {
                        {
                            RedFur[h, w] = 0.5d * Math.Log(1 + RedFur[h, w]);

                            if (max < RedFur[h, w])
                            {
                                max = RedFur[h, w];
                            }

                        }
                    }

                for (int h = 0; h < Height; h++)
                    for (int w = 0; w < Width; w++)
                    {
                        {
                            RedFur2[h, w] = ((RedFur[h, w] * 255d) / max);
                            ColFour[h, w] = Color.FromArgb((int)RedFur2[h, w], (int)RedFur2[h, w], (int)RedFur2[h, w]);
                            bitbox.SetPixel(h, w, ColFour[h, w]);
                        }
                    }

                return bitbox;
            } // 

            public Bitmap FourierBackward(Bitmap image) 
            {
                Bitmap bmp = new Bitmap(image);
                Bitmap bmpNew = null;
                bmpNew = new Bitmap(bmp.Width, bmp.Height);
                //Bitmap bmpNew1 = null;
                //bmpNew1 = new Bitmap(bmp.Width, bmp.Height);
                //

                double[,] colorNew = new double[bmp.Height, bmp.Width];

                for (int iNew = 0; iNew < bmp.Height; iNew++)
                {
                    for (int jNew = 0; jNew < bmp.Width; jNew++)
                    {

                        Complex sum2 = new Complex();

                        for (int iOld = 0; iOld < bmp.Height; iOld++)
                        {

                            Complex sum1 = new Complex();

                            for (int jOld = 0; jOld < bmp.Width; jOld++)
                            {

                                double K = 2.0 * Math.PI * (((double)iOld * iNew / bmp.Height) + ((double)jOld * jNew / bmp.Width));

                                //Complex x = new Complex(colorOld[iOld, jOld], 0);
                                Complex end = new Complex((Math.Cos(K) * publicColor[iOld, jOld].Real - Math.Sin(K) * publicColor[iOld, jOld].Imaginary),
                                    (Math.Cos(K) * publicColor[iOld, jOld].Imaginary + Math.Sin(K) * publicColor[iOld, jOld].Real));
                                //end.Real = Math.Cos(K) * publicColor[iOld, jOld].Real - Math.Sin(K) * publicColor[iOld, jOld].Imaginary;
                                //end.Imaginary = Math.Cos(K) * publicColor[iOld, jOld].Imaginary + Math.Sin(K) * publicColor[iOld, jOld].Real;
                                //Complex end = Complex.Multiply(publicColor[iNew, jNew], e);
                                //end = Complex.Multiply(end, Math.Pow(-1, iOld + jOld));

                                sum1 = Complex.Add(sum1, end);
                            }

                            sum2 = Complex.Add(sum2, sum1);
                        }

                        colorNew[iNew, jNew] = Complex.Abs(sum2);
                        //colorNew[iNew, jNew] = colorNew[iNew, jNew];

                        if (colorNew[iNew, jNew] > 255)
                        {
                            colorNew[iNew, jNew] = 254;
                        }

                    }
                }

                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        bmpNew.SetPixel(j, i, Color.FromArgb((int)colorNew[i, j], (int)colorNew[i, j], (int)colorNew[i, j]));
                    }
                }

                return bmpNew;
            } // 
            
            public Bitmap TwoDimensionalV2(Bitmap image) // 
            { // 

                Bitmap bmp = new Bitmap(image);
                Bitmap bmpNew = null;
                bmpNew = new Bitmap(bmp.Width, bmp.Height);
                //Bitmap bmpNew1 = null;
                //bmpNew1 = new Bitmap(bmp.Width, bmp.Height);

                Color colorOne;

                double[,] colorOld = new double[bmp.Height, bmp.Width];

                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        colorOne = bmp.GetPixel(j, i);
                        colorOld[i, j] = (double)(colorOne.R + colorOne.G + colorOne.B) / 3;
                    }
                }

                double[,] colorNew = new double[bmp.Height, bmp.Width];

                for (int iNew = 0; iNew < bmp.Height; iNew++) // u
                {
                    for (int jNew = 0; jNew < bmp.Width; jNew++) // w
                    {

                        Complex sum2 = new Complex();

                        for (int iOld = 0; iOld < bmp.Height; iOld++) // M Столбец
                        {

                            Complex sum1 = new Complex();
                            //int W = bmp.Width;

                            for (int jOld = 0; jOld < bmp.Width; jOld++) // N Строка
                            {
                                double K = 2.0 * Math.PI * (((double)iOld * iNew / bmp.Height) + ((double)jOld * jNew / bmp.Width)); // Расчет степени

                                Complex x = new Complex(colorOld[iOld, jOld], 0); // Конвертация цветов в комплексные Xmn
                                Complex e = new Complex(Math.Cos(K), -Math.Sin(K)); // е в степени K
                                Complex end = Complex.Multiply(x, e); // e * Xmn

                                end = Complex.Multiply(end, Math.Pow(-1, iOld + jOld)); // Центрирование ярскостей

                                sum1 = Complex.Add(sum1, end);
                            }

                            sum2 = Complex.Add(sum2, sum1);
                        }

                        publicColor[iNew, jNew] = Complex.Add(publicColor[iNew, jNew], sum2);
                        publicColor[iNew, jNew] = Complex.Divide(publicColor[iNew, jNew], (bmp.Height * bmp.Width)); // без этого преобразования белый квадрат на выходе

                        colorNew[iNew, jNew] = Complex.Abs(sum2);
                        colorNew[iNew, jNew] = 30 * colorNew[iNew, jNew] / (double)(bmp.Height * bmp.Width); // 
                          
                        
                        if (colorNew[iNew, jNew] > 255)
                        {
                            colorNew[iNew, jNew] = 254;
                        }

                    }
                }

                //double max = 0;
                //max = 0;
                //Color[,] ColFour = new Color[bmp.Width, bmp.Height];
                //double[,] RedFur2 = new double[bmp.Width, bmp.Height];
                //
                //for (int h = 0; h < bmp.Height; h++)
                //    for (int w = 0; w < bmp.Width; w++)
                //    {
                //        {
                //            colorNew[h, w] = 0.5d * Math.Log(1 + colorNew[h, w]);
                //
                //            if (max < colorNew[h, w])
                //            {
                //                max = colorNew[h, w];
                //            }
                //
                //        }
                //    }
                //
                //for (int h = 0; h < bmp.Height; h++)
                //    for (int w = 0; w < bmp.Width; w++)
                //    {
                //        {
                //            RedFur2[h, w] = ((colorNew[h, w] * 255d) / max);
                //            ColFour[h, w] = Color.FromArgb((int)RedFur2[h, w], (int)RedFur2[h, w], (int)RedFur2[h, w]);
                //            bmpNew.SetPixel(h, w, ColFour[h, w]);
                //        }
                //    }


                for (int i = 0; i < bmp.Height; i++)
                {
                    for (int j = 0; j < bmp.Width; j++)
                    {
                        bmpNew.SetPixel(j, i, Color.FromArgb((int)colorNew[i, j], (int)colorNew[i, j], (int)colorNew[i, j]));
                    }
                }

                return bmpNew;
            }

        } // TwoDimensionalFourierTransform


        private void button2_Click(object sender, EventArgs e)
        { // Загрузка изображения

            pictureBox1.Image = null; // Оригинал
            pictureBox2.Image = null; // Двумерное преобразование
            pictureBox3.Image = null; // Обратное преобразование

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

        } // Загрузка изображения


        private void button3_Click(object sender, EventArgs e)
        { // Двумерное и обратное преообразование

            pictureBox2.Image = null;
            pictureBox3.Image = null;

            TwoDimensionalFourierTransform TDFT = new TwoDimensionalFourierTransform();

            Bitmap TDFTFourierTransformnoisePix = TDFT.TwoDimensionalV2(Bitmapfurie);
            Bitmap TDFTFourierBackward = TDFT.FourierBackward(TDFTFourierTransformnoisePix);

            pictureBox2.Image = (Image)TDFTFourierTransformnoisePix;
            pictureBox3.Image = (Image)TDFTFourierBackward;

        } // Двумерное и обратное преообразование
    }
}
