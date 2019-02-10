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

            public static Complex[,] RedComp = new Complex[Bitmapfurie.Width, Bitmapfurie.Height];

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

                    for (int i = 0; i < bmpG.Width * 20; i += 20)
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

            public Bitmap FourierComplex(Bitmap image)
            {
                //double Re;// Реальная часть
                //double Im; // Мнимая
                //double Amplitude; // Амплитуда для АЧХ
                //double Faza; // Фаза для ФЧХ
                //double Frecuensy; // Частота гармоники
                //
                //
                //Bitmap bmp = new Bitmap(image);
                //Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                //System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
                //IntPtr ptr = bmpData.Scan0;
                //
                //
                //int[] R = new int[256];
                ////byte[] N = new byte[256];
                //byte[] left = new byte[256];
                //byte[] right = new byte[256];
                //
                //int width = Math.Abs(bmpData.Stride);
                //int bytes = width * bmp.Height; // общее количество пикселей в изображении
                //byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
                //
                //
                //double[] grayValuesFourD = new double[bytes];
                //double[] grayValuesFourM = new double[bytes];
                //double[] grayValuesFour = new double[bytes];
                //byte[] grayValuesFourbyt = new byte[bytes];
                //
                //System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);







                // for (int k = 0; k < N; k++)
                // {
                //     //Furie[k] = new Complex();
                //     Complex suma = new Complex();
                //
                //     for (int n = 0; n < Inner.Length; n++)
                //     {
                //         Arg = 2 * Math.PI * k * n / N;
                //         Furie[k].Re += Inner[n] * Math.Cos(Arg);
                //         Furie[k].Im -= Inner[n] * Math.Sin(Arg);
                //     }
                //
                //
                //     Furie[k].Amplitude = (Math.Sqrt(Math.Pow(Furie[k].Re, 2) + Math.Pow(Furie[k].Im, 2))) / N;
                //
                //     Furie[k].Faza = Math.Atan(Furie[k].Im / Furie[k].Re / Math.PI * 180);
                //     Furie[k].Frecuensy = ((N - 1) * (k));
                //
                // }



                //Color colorOne;
                //int[,] colorOld = new int[32, 32];
                //
                //for (int i = 0; i < bmp.Width; i++)
                //{
                //    for (int j = 0; j < bmp.Height; j++)
                //    {
                //        colorOne = bmp.GetPixel(i, j);
                //        colorOld[i, j] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                //    }
                //}


                //double K;
                //K = grayValues.Length;
                //int N = grayValues.Length;
                //double Arg;
                //int[,] colorNew = new int[32, 32];
                ////Цикл вычисления коэффициентов 
                //for (int u = 0; u < 32; u++)
                //{
                //    //цикл суммы 
                //    Complex summa = new Complex();
                //    for (int k = 0; k < 1; k++)
                //    {
                //        Complex S = new Complex(colorOld[u,k], 0);
                //        double koeff = -2 * Math.PI * u * k / K;
                //        Complex e = new Complex(Math.Cos(koeff), Math.Sin(koeff));
                //        summa += (S * e);
                //    }
                //    colorNew = (summa / K).;
                //}

                Bitmap bmp = new Bitmap(image);
                Bitmap bmpNew = null;
                bmpNew = new Bitmap(bmp.Width, bmp.Height);

                Color colorOne;
                double[] colorOld = new double[32];

                for (int i = 0; i < 32; i++)
                {
                    colorOne = bmp.GetPixel(i, 0);
                    colorOld[i] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                }

                int[] colorNew = new int[32];
                for (int i = 0; i < 32; i++)
                {

                    Complex sum = new Complex();

                    for (int j = 0; j < 32; j++)
                    {
                        //Complex S = new Complex(colorOld[j], 0);
                        //
                        double K = -2 * Math.PI * i * j / 32;
                        Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                        sum += (colorOld[j] * e) * Math.Pow(-1, j); // 
                    }

                    //sum = sum / 32;
                    colorNew[i] = (int)((Math.Sqrt(sum.Real * sum.Real + sum.Imaginary * sum.Imaginary)));

                    if (colorNew[i] > 255)
                    {
                        colorNew[i] = 254;
                    }
                }
                for (int i = 0; i < 32; i++)
                {
                    bmpNew.SetPixel(i, 0, Color.FromArgb(colorNew[i], colorNew[i], colorNew[i]));
                }

                return bmpNew;
                
            }

            public class Data 
            {

                public int h;
                public int k;
                public int Height;
                public int Width;
                public int w;
                //public int n;
                public double[,] RedIsh;
                public Complex[,] RedComp;

                public Data(int _h, int _k, int _Height, int _Width, int _w, double[,] _RedIsh, Complex[,] _RedComp)
                {

                    this.h = _h;
                    this.k = _k;
                    this.Height = _Height;
                    this.Width = _Width;
                    this.w = _w;
                    //this.n = _n;
                    this.RedIsh = _RedIsh;
                    this.RedComp = _RedComp;
               }


                public void ThreadFunction2() //int h, int k, int Height, int Width, int w, int n, double[,] RedIsh, Complex[,] RedComp
                {
                    double hu;

                    for (int n = 0; n < Width; n++) // N Строка
                    {

                        hu = 2 * Math.PI * (((double)h * k / Height) + ((double)w * n / Width)); // Расчет чисто степень

                        Complex a = new Complex(RedIsh[k, n], 0); // ковенртация цветов в комплексное Xmn

                        Complex b = new Complex(Math.Cos(hu), -Math.Sin(hu)); // действительная и мнимая часть Е в степени 

                        Complex c = Complex.Multiply(a, b); // E  * Xmn

                        RedComp[h, w] = Complex.Add(RedComp[h, w], c * Math.Pow(-1, n + k)); // проверить если полосы // сложение обоих сумм

                    }

                   


                }



            }

            public Bitmap FourierThread3(Bitmap bit)
            {
                Bitmap bitbox = new Bitmap(bit);
                // int size = bitbox.Width * bitbox.Height;
                int Width = bitbox.Width, Height = bitbox.Height;

                double[,] RedIsh = new double[bitbox.Width, bitbox.Height]; // информация о яркости каждого пикселя
                Color[,] ColIsh = new Color[bitbox.Width, bitbox.Height]; //исходные цвета пикселей


                double[,] RedFur = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS
                double[,] RedFur2 = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS

                Color[,] ColFour = new Color[bitbox.Width, bitbox.Height];
                //Complex[,] RedComp = new Complex[bitbox.Width, bitbox.Height]; // комлексные числа 
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
                        
                        int[] nums = Enumerable.Range(0, Height).ToArray();
                        long total = 0;


                        Parallel.ForEach<int, long>(nums, // source collection
                                     () => 0, // method to initialize the local variable
                                     (j, loop, subtotal) => // method invoked by the loop on each iteration
                                     {

                                         for (int k = 0; k < Height; k++) // M Столбец
                                         {

                                             for (int n = 0; n < Width; n++) // N Строка
                                             {

                                                 Data data = new Data(h, k, Height, Width, w, RedIsh, RedComp);
                                                 data.ThreadFunction2();

                                                 //hu = 2 * Math.PI * (((double)h * k / Height) + ((double)w * n / Width)); // Расчет чисто степень
                                                 //
                                                 //Complex a = new Complex(RedIsh[k, n], 0); // ковенртация цветов в комплексное Xmn
                                                 //
                                                 //Complex b = new Complex(Math.Cos(hu), -Math.Sin(hu)); // действительная и мнимая часть Е в степени 
                                                 //
                                                 //Complex c = Complex.Multiply(a, b); // E  * Xmn
                                                 //
                                                 //RedComp[h, w] = Complex.Add(RedComp[h, w], c * Math.Pow(-1, n + k)); // проверить если полосы // сложение обоих сумм

                                             }



                                         }


                                         subtotal += j; //modify local variable
                                         return subtotal; // value to be passed to next iteration


                                     },
                                     // Method to be executed when each partition has completed.
                                     // finalResult is the final value of subtotal for a particular partition.
                                     (finalResult) => Interlocked.Add(ref total, finalResult)
                                     );

                        

                           
                      

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
            }

            public Bitmap FourierThread2(Bitmap bit)
            {
                Bitmap bitbox = new Bitmap(bit);
                // int size = bitbox.Width * bitbox.Height;
                int Width = bitbox.Width, Height = bitbox.Height;

                double[,] RedIsh = new double[bitbox.Width, bitbox.Height]; // информация о яркости каждого пикселя
                Color[,] ColIsh = new Color[bitbox.Width, bitbox.Height]; //исходные цвета пикселей


                double[,] RedFur = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS
                double[,] RedFur2 = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS

                Color[,] ColFour = new Color[bitbox.Width, bitbox.Height];
                //Complex[,] RedComp = new Complex[bitbox.Width, bitbox.Height]; // комлексные числа 
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

                        int size = Width;
                        int numProcs = Environment.ProcessorCount;
                        int range = size / numProcs;

                        // разбиваем данные, запускаем все потоки и ждём завершения
                        var threads = new List<Thread>(numProcs);
                        for (int p = 0; p < numProcs; p++)
                        {
                            int start = p * range + 0;
                            int end = (p == numProcs - 1) ?
                            Width : start + range;
                            threads.Add(new Thread(() => 
                            {
                                //for (int i = start; i < end; i++)  // Тело
                                    for (int k = 0; k < Height; k++) // M Столбец
                                    {
                               
                                        for (int n = 0; n < Width; n++) // N Строка
                                        {

                                            hu = 2 * Math.PI * (((double)h * k / Height) + ((double)w * n / Width)); // Расчет чисто степень

                                            Complex a = new Complex(RedIsh[k, n], 0); // ковенртация цветов в комплексное Xmn

                                            Complex b = new Complex(Math.Cos(hu), -Math.Sin(hu)); // действительная и мнимая часть Е в степени 

                                            Complex c = Complex.Multiply(a, b); // E  * Xmn

                                            RedComp[h, w] = Complex.Add(RedComp[h, w], c * Math.Pow(-1, n + k)); // проверить если полосы // сложение обоих сумм

                                        }



                                    }

                            }));
                        }
                        foreach (var thread in threads) thread.Start();
                        foreach (var thread in threads) thread.Join();
 
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
            }

            public Bitmap FourierThread(Bitmap bit)
            {
                Bitmap bitbox = new Bitmap(bit);
                // int size = bitbox.Width * bitbox.Height;
                int Width = bitbox.Width, Height = bitbox.Height;

                double[,] RedIsh = new double[bitbox.Width, bitbox.Height]; // информация о яркости каждого пикселя
                Color[,] ColIsh = new Color[bitbox.Width, bitbox.Height]; //исходные цвета пикселей


                double[,] RedFur = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS
                double[,] RedFur2 = new double[bitbox.Width, bitbox.Height];  // итоговый результат после ABS

                Color[,] ColFour = new Color[bitbox.Width, bitbox.Height];
                //Complex[,] RedComp = new Complex[bitbox.Width, bitbox.Height]; // комлексные числа 
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
                            //Parallel.For(0, Width, new ParallelOptions { MaxDegreeOfParallelism = 8 },
                            //      i =>
                            //      {
                                        //for (int n = 0; n < Width; n++) // N Строка
                                        //{
                                        
                                            Data data = new Data(h, k, Height, Width, w, RedIsh, RedComp);
                                            //Thread thread = new Thread(new ParameterizedThreadStart(data.ThreadFunction2));
                                            Thread thread = new Thread(new ThreadStart(data.ThreadFunction2));
                                            thread.Start();
                                            thread.Join();
                                        //}
                                //  });
                            //Parallel.ForEach<int>(8, (0) =>
                            //      
                            //      {
                            //          for (int n = 0; n < Width; n++) // N Строка
                            //          {
                            //
                            //              Data data = new Data(h, k, Height, Width, w, n, RedIsh, RedComp);
                            //              //Thread thread = new Thread(new ParameterizedThreadStart(data.ThreadFunction2));
                            //              Thread thread = new Thread(new ThreadStart(data.ThreadFunction2));
                            //              thread.Start();
                            //
                            //          }
                            //      });



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
            }

            //static void ThreadFunction(object data) //int h, int k, int Height, int Width, int w, int n, double[,] RedIsh, Complex[,] RedComp
            //{
            //    double hu;
            //
            //    
            //
            //    hu = 2 * Math.PI * (((double)h * k / Height) + ((double)w * n / Width)); // Расчет чисто степень
            //
            //    Complex a = new Complex(RedIsh[k, n], 0); // ковенртация цветов в комплексное Xmn
            //
            //    Complex b = new Complex(Math.Cos(hu), -Math.Sin(hu)); // действительная и мнимая часть Е в степени 
            //
            //    Complex c = Complex.Multiply(a, b); // E  * Xmn
            //
            //    RedComp[h, w] = Complex.Add(RedComp[h, w], c * Math.Pow(-1, n + k)); // проверить если полосы // сложение обоих сумм
            //
            //
            //}

            public Bitmap FourierComplex32x32(Bitmap image)
            {
               

                Bitmap bmp = new Bitmap(image);
                Bitmap bmpNew = null;
                bmpNew = new Bitmap(bmp.Width, bmp.Height);



                Bitmap bmpNew2 = null;
                bmpNew2 = new Bitmap(bmpNew.Width, bmpNew.Height);


                Color colorOne;
                

                for (int stroki = 0; stroki < 32; stroki++)
                {
                    double[] colorOld = new double[32];

                    for (int i = 0; i < 32; i++)
                    {
                        colorOne = bmp.GetPixel(i, stroki);
                        colorOld[i] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                    }

                    int[] colorNew = new int[32];
                    for (int i = 0; i < 32; i++)
                    {

                        Complex sum = new Complex();

                        for (int j = 0; j < 32; j++)
                        {
                            //Complex S = new Complex(colorOld[j], 0);
                            double K = -2 * Math.PI * i * j / 32;
                            Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                            sum += (colorOld[j] * e) * Math.Pow(-1, j+i);
                        }

                        //sum = sum / 32;
                        colorNew[i] = (int)((Math.Sqrt(sum.Real * sum.Real + sum.Imaginary * sum.Imaginary)));

                        if (colorNew[i] > 255)
                        {
                            colorNew[i] = 254;
                        }
                    }

                    for (int k = 0; k < 32; k++)
                    {
                        bmpNew.SetPixel(k, stroki, Color.FromArgb(colorNew[k], colorNew[k], colorNew[k]));
                    }

                }


                // stolbci
                for (int stolbci = 0; stolbci < 32; stolbci++)
                {
                    //for (int stolbciDown = 0; stolbciDown < 1; stolbciDown++)
                    //{

                        double[] colorOld = new double[32];

                        for (int i = 0; i < 32; i++)
                        {
                            colorOne = bmpNew.GetPixel(stolbci, i);
                            colorOld[i] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                        }

                        int[] colorNew2 = new int[32];
                        for (int i = 0; i < 32; i++)
                        {

                            Complex sum = new Complex();

                            for (int j = 0; j < 32; j++)
                            {
                                //Complex S = new Complex(colorOld[j], 0);
                                double K = -2 * Math.PI * i * j / 32; 
                                Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                                sum += (colorOld[j] * e) * -Math.Pow(-1, j);
                            }

                            //sum = sum / 32;
                            colorNew2[i] = (int)((Math.Sqrt(sum.Real * sum.Real + sum.Imaginary * sum.Imaginary)));

                            if (colorNew2[i] > 255)
                            {
                                colorNew2[i] = 254;
                            }
                        }

                        for (int k = 0; k < 32; k++)
                        {
                            bmpNew2.SetPixel(stolbci, k, Color.FromArgb(colorNew2[k], colorNew2[k], colorNew2[k]));
                        }
                    //}
                    

                }






                //int[] colorNew2 = new int[32];
                //
                //for (int x = 0; x < 32; x++)
                //{
                //    Complex sum2 = new Complex();
                //
                //    for (int d = 0; d < 32; d++)
                //    {
                //        for (int m = 0; m < 1; m++)
                //        {
                //
                //            //Complex S = new Complex(colorOld[j], 0);
                //            double K = -2 * Math.PI * d * m / 32;
                //            Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                //            sum2 += (colorNew[m] * e) * Math.Pow(-1, m);
                //
                //        }
                //
                //        colorNew2[d] = (int)((Math.Sqrt(sum2.Real * sum2.Real + sum2.Imaginary * sum2.Imaginary)));
                //
                //        if (colorNew2[d] > 255)
                //        {
                //            colorNew2[d] = 254;
                //        }
                //    }
                //}
                //for (int i = 0; i < 32; i++)
                //{
                //    for (int j = 0; j < 32; j++)
                //    {
                //        for (int k = 0; k < 1; k++)
                //        {
                //            bmpNew.SetPixel(j, 0, Color.FromArgb(colorNew2[j], colorNew2[j], colorNew2[j]));
                //        }
                //    }
                //        
                //}
                //   

                // int[] colorNew2 = new int[32];
                // for (int i = 0; i < 32; i++)
                // {
                //
                //     Complex sum2 = new Complex();
                //
                //     for (int j = 0; j < 32; j++)
                //     {
                //         //Complex S = new Complex(colorOld[j], 0);
                //         double K = -2 * Math.PI * i * j / 32;
                //         Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                //         sum2 += (colorNew[j] * e) * Math.Pow(-1, j);
                //     }
                //
                //     //sum = sum / 32;
                //     colorNew2[i] = (int)((Math.Sqrt(sum2.Real * sum2.Real + sum2.Imaginary * sum2.Imaginary)));
                //
                //     if (colorNew2[i] > 255)
                //     {
                //         colorNew2[i] = 254;
                //     }
                // }






                return bmpNew2;

            }

            public Bitmap FourierComplex320x320(Bitmap image)
            {

                Bitmap bmp = new Bitmap(image);
                Bitmap bmpNew = null;
                bmpNew = new Bitmap(bmp.Width, bmp.Height);



                Bitmap bmpNew2 = null;
                bmpNew2 = new Bitmap(bmpNew.Width, bmpNew.Height);


                Color colorOne;


                for (int stroki = 0; stroki < bmpNew.Height; stroki++)
                {
                    double[] colorOld = new double[bmpNew.Width];
                
                    for (int i = 0; i < bmpNew.Width; i++)
                    {
                        colorOne = bmp.GetPixel(i, stroki);
                        colorOld[i] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                    }
                
                    int[] colorNew = new int[bmpNew.Width];
                    for (int i = 0; i < bmpNew.Height; i++)
                    {
                
                        Complex sum = new Complex();
                
                        for (int j = 0; j < bmpNew.Width; j++)
                        {
                            //colorOld[i] = colorOld[i] * Math.Pow(-1, i + j);
                            double K = -2 * Math.PI * i * j / bmpNew.Width;    //hu = 2 * Math.PI * (((double)h * k / he) + ((double)w * n / wh));  double K = -2 * Math.PI * i * j / bmpNew.Width;
                            Complex e = new Complex(Math.Sin(K), -Math.Cos(K));
                            sum += (colorOld[j] * e) * Math.Pow(-1, j); //
                        }
                
                       
                        colorNew[i] = (int)((Math.Sqrt(sum.Real * sum.Real + sum.Imaginary * sum.Imaginary)));
                
                        if (colorNew[i] > 255)
                        {
                            colorNew[i] = 254;
                        }
                    }
                
                    for (int k = 0; k < bmpNew.Width; k++)
                    {
                        bmpNew.SetPixel(k, stroki, Color.FromArgb(colorNew[k], colorNew[k], colorNew[k]));
                    }
                
                }


                //for (int stolbci = 0; stolbci < bmpNew.Width-1; stolbci++)
                //{
                //    double[] colorOld = new double[bmpNew.Height];
                //
                //    for (int i = 0; i < bmpNew.Height; i++)
                //    {
                //        colorOne = bmpNew.GetPixel(stolbci, i);
                //        colorOld[i] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                //    }
                //
                //    int[] colorNew = new int[bmpNew.Height];
                //    for (int i = 0; i < bmpNew.Width-1; i++)
                //    {
                //
                //        Complex sum = new Complex();
                //
                //        for (int j = 0; j < bmpNew.Width; j++)
                //        {
                //            
                //            double K = -2 * Math.PI * i * j / bmpNew.Width;
                //            Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                //            sum += (colorOld[j] * e) * Math.Pow(-1, j);
                //        }
                //
                //       
                //        colorNew[i] = (int)((Math.Sqrt(sum.Real * sum.Real + sum.Imaginary * sum.Imaginary)));
                //
                //        if (colorNew[i] > 255)
                //        {
                //            colorNew[i] = 254;
                //        }
                //    }
                //
                //    for (int k = 0; k < bmpNew.Width; k++)
                //    {
                //        bmpNew2.SetPixel(stolbci, k, Color.FromArgb(colorNew[k], colorNew[k], colorNew[k]));
                //    }
                //
                //}

                return bmpNew;
                //for (int stolbci = 0; stolbci < bmp.Width; stolbci++)
                //{
                //    double[] colorOld = new double[bmp.Height];
                //
                //    for (int i = 0; i < bmp.Height; i++)
                //    {
                //        colorOne = bmp.GetPixel(stolbci, i);
                //        colorOld[i] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                //    }
                //
                //    int[] colorNew = new int[bmp.Height];
                //    for (int i = 0; i < bmp.Width; i++)
                //    {
                //
                //        Complex sum = new Complex();
                //
                //        for (int j = 0; j < bmp.Width - 1; j++)
                //        {
                //            //Complex S = new Complex(colorOld[j], 0);
                //            double K = -2 * Math.PI * i * j / bmp.Width;
                //            Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                //            sum += (colorOld[j] * e) * Math.Pow(-1, j);
                //        }
                //
                //        //sum = sum / 32;
                //        colorNew[i] = (int)((Math.Sqrt(sum.Real * sum.Real + sum.Imaginary * sum.Imaginary)));
                //
                //        if (colorNew[i] > 255)
                //        {
                //            colorNew[i] = 254;
                //        }
                //    }
                //
                //    for (int k = 0; k < bmp.Width; k++)
                //    {
                //        bmpNew2.SetPixel(stolbci, k, Color.FromArgb(colorNew[k], colorNew[k], colorNew[k]));
                //    }
                //
                //}


                //// stolbci
                //for (int stolbci = 0; stolbci < 32; stolbci++)
                //{
                //    //for (int stolbciDown = 0; stolbciDown < 1; stolbciDown++)
                //    //{
                //
                //    double[] colorOld = new double[32];
                //
                //    for (int i = 0; i < 32; i++)
                //    {
                //        colorOne = bmpNew.GetPixel(stolbci, i);
                //        colorOld[i] = (colorOne.R + colorOne.G + colorOne.B) / 3;
                //    }
                //
                //    int[] colorNew2 = new int[32];
                //    for (int i = 0; i < 32; i++)
                //    {
                //
                //        Complex sum = new Complex();
                //
                //        for (int j = 0; j < 32; j++)
                //        {
                //            //Complex S = new Complex(colorOld[j], 0);
                //            double K = -2 * Math.PI * i * j / 32;
                //            Complex e = new Complex(Math.Cos(K), Math.Sin(K));
                //            sum += (colorOld[j] * e) * Math.Pow(-1, j);
                //        }
                //
                //        //sum = sum / 32;
                //        colorNew2[i] = (int)((Math.Sqrt(sum.Real * sum.Real + sum.Imaginary * sum.Imaginary)));
                //
                //        if (colorNew2[i] > 255)
                //        {
                //            colorNew2[i] = 254;
                //        }
                //    }
                //
                //    for (int k = 0; k < 32; k++)
                //    {
                //        bmpNew2.SetPixel(stolbci, k, Color.FromArgb(colorNew2[k], colorNew2[k], colorNew2[k]));
                //    }
                //    //}
                //
                //
                //}





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
                int sumOldI, sumOld, sumOldJD, sumOldJM;

                for (int iNew = 0; iNew < 32; iNew++)
                {
                    for (int jNew = 0; jNew < 32; jNew++)
                    {

                        sumOldI = 0;
                        sumOld = 0;

                        for (int iOld = 0; iOld < 32; iOld++)
                        {

                            sumOldJD = 0;
                            sumOldJM = 0;

                            for (int jOld = 0; jOld < 32; jOld++)
                            {
                                sumOldJD += (int)(colorOld[iOld, jOld] * Math.Pow(-1, jOld) * (Math.Cos(Math.PI * 2 * ((jOld * iNew / 32) + (iOld * iNew / 32)))));
                                sumOldJM += (int)(colorOld[iOld, jOld] * Math.Pow(-1, jOld) * (Math.Sin(Math.PI * 2 * ((jOld * iNew / 32) + (iOld * iNew / 32)))));
                            }

                            sumOld += (int)(Math.Sqrt(sumOldJD * sumOldJD + sumOldJM * sumOldJM));
                   
                        }

                        colorNew[iNew, jNew] = (1 / (32 * 32)) * sumOld;
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
        { // Дискретное преобразование Фурье

            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox7.Image = null;
            pictureBox8.Image = null;

            gistogram g = new gistogram();
            DiscreteFourierTransform DFT = new DiscreteFourierTransform(listView1, listView2, listView3);
            
            Bitmap DFTBit32Bitmapfurie = DFT.Bit32(Bitmapfurie);
            Bitmap DFTFourierDFTBit32Bitmapfurie = DFT.FourierComplex(DFT.Bit32(Bitmapfurie));
            
            pictureBox2.Image = (Image)DFT.ImgIncr32(DFTBit32Bitmapfurie);
            pictureBox3.Image = (Image)DFT.ImgIncr32(DFTFourierDFTBit32Bitmapfurie);
            pictureBox7.Image = (Image)DFT.gpaphicLight(DFTBit32Bitmapfurie);
            pictureBox8.Image = (Image)DFT.gpaphicLight(DFTFourierDFTBit32Bitmapfurie);

        } // Дискретное преобразование Фурье

        private void button2_Click(object sender, EventArgs e)
        { // Загрузка изображения


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

        } // Загрузка изображения

        private void button3_Click(object sender, EventArgs e)
        { // Двумерное преообразование 32x32
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox7.Image = null;
            pictureBox8.Image = null;

            gistogram g = new gistogram();
            DiscreteFourierTransform DFT = new DiscreteFourierTransform(listView1, listView2, listView3);

            Bitmap DFTBit32Bitmapfurie = DFT.Bit32x32(Bitmapfurie);
            Bitmap DFTFourierDFTBit32Bitmapfurie = DFT.FourierComplex32x32(DFT.Bit32x32(Bitmapfurie));

            pictureBox2.Image = (Image)DFT.ImgIncr32x32(DFTBit32Bitmapfurie);
            pictureBox3.Image = (Image)DFT.ImgIncr32x32(DFTFourierDFTBit32Bitmapfurie);
        } // Двумерное преообразование 32x32
    }
}
