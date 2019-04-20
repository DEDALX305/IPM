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


namespace IMPSpace
{
    public partial class Form4 : Form
    {
        public static Bitmap BitmapPicture1;
        public static Bitmap BitmapBinarization;
        //public static Bitmap bitNew;


        public Form4()
        {
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        class skeletization
        { // Скелетизация! даже в заднице есть выход ¯ \ _ (ツ) _ / ¯ 
            public Bitmap transformation(Bitmap TempBitmap)
            {
                //Rectangle rect = new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height);
                //System.Drawing.Imaging.BitmapData bmpData = TempBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, TempBitmap.PixelFormat);
                //IntPtr ptr = bmpData.Scan0;

                //Bitmap newBit1 = new Bitmap(TempBitmap.Width, TempBitmap.Height);
                Bitmap processed_bitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
                int bytes = TempBitmap.Width*TempBitmap.Height;
                byte[] color_image = new byte[bytes];
                //byte[] NewgrayValues1 = new byte[bytes];
                //byte[] NewgrayValues2 = new byte[bytes];
                //int end;

                Color pixel;

                //int c0 = 0;
                //int c1 = 0;
                //int c2 = 0;

                // Создание массива 1 и 0 по бинаризованному изображению
                for ( int i = 0; i < TempBitmap.Width; i++)
                {
                    for ( int j = 0; j < TempBitmap.Height; j++)
                    {
                        if (((TempBitmap.GetPixel(i, j).R + TempBitmap.GetPixel(i, j).G + TempBitmap.GetPixel(i, j).B) / 3) == 0)
                        {
                            color_image[i + j * TempBitmap.Width] = 1;
                            //c1++;
                        } else if (((TempBitmap.GetPixel(i, j).R + TempBitmap.GetPixel(i, j).G + TempBitmap.GetPixel(i, j).B) / 3) == 255)
                        {
                            color_image[i + j * TempBitmap.Width] = 0;
                            //c2++;
                        }
                    }
                }

                // эта ннннада?
                /*
                for (int i = 0; i < bytes; i++)
                {
                    Console.Write(grayValues[i]);

                    if ((i % TempBitmap.Width) == 0) Console.Write('\n');
                }
                */

                int Width = TempBitmap.Width; // Ширина члена
                int Height = TempBitmap.Height; // дли... высота члена

                //int elem = 0; // Нахуя ?
                //int m1 = 0; // для дебага
                //int m2 = 0; // для дебага
                //int m3 = 0; // для дебага
                //int Cycle = 0; // для дебага
                //int pr = 5000; // Чаво бля?

                // для дебага
                /*
                Console.Write('\n' + grayValues[pr - Width - 1]);
                Console.Write(grayValues[pr - Width]);
                Console.Write(grayValues[pr - Width + 1]);

                Console.Write('\n' + grayValues[pr - 1]);
                Console.Write(grayValues[pr]);
                Console.Write(grayValues[pr + 1]);

                Console.Write('\n' + grayValues[pr + Width - 1]);
                Console.Write(grayValues[pr + Width]);
                Console.Write(grayValues[pr + Width + 1]);
                */

                for ( int i = Width + 1; i < color_image.Length - Width - 1; i++) // Бегаем по массиву 1 и 0 изображения
                {
                    //Нахуя он нужен?
                    //if ((
                    //    grayValues[i - Width - 1] + 
                    //    grayValues[i - Width] + 
                    //    grayValues[i - Width + 1] + 
                    //    grayValues[i - 1] + 
                    //    grayValues[i + 1] + 
                    //    grayValues[i + Width - 1] + 
                    //    grayValues[i + Width] + 
                    //    grayValues[i + Width + 1]) < 8)
                   // {

                    //Cycle++; // для дебага

                    /* Условия для первого шага
                      (а) 2 ≤ N(p1) ≤ 6
                      (б) T(p1) = 1
                      (в) p2 * p4 * p6 = 0
                      (г) p4 * p6 * p8 = 0
                    */
                    int condition_a = 0, condition_b = 0, condition_v = 0, condition_g = 0; // Обьявление пустых переменных для условий
                    int T_p = 0; // Обьявление пустой переменной для условия (б)
                    int N_p1 = ( // Обьявление переменной для условия (а)
                        color_image[i - Width] +     // P2
                        color_image[i - Width + 1] + // P3
                        color_image[i + 1] +         // P4
                        color_image[i + Width + 1] + // P5
                        color_image[i + Width] +     // P6
                        color_image[i + Width - 1] + // P7
                        color_image[i - 1] +         // P8
                        color_image[i - Width - 1]); // P9  

                    // Задание массива для проверки условий (в) и (г)
                    byte[] array_1_and_0 = new byte[8];
                    array_1_and_0[0] = color_image[i - Width];      // P2 
                    array_1_and_0[1] = color_image[i - Width + 1];  // P3
                    array_1_and_0[2] = color_image[i + 1];          // P4
                    array_1_and_0[3] = color_image[i + Width + 1];  // P5
                    array_1_and_0[4] = color_image[i + Width];      // P6
                    array_1_and_0[5] = color_image[i + Width - 1];  // P7
                    array_1_and_0[6] = color_image[i - 1];          // P8
                    array_1_and_0[7] = color_image[i - Width - 1];  // P9

                    // Сергей жружко маг? конечно нет
                    // Проверка условия (а) N(p1) - Число ненулевых соседей элемента p1 N(p1)=p2 + p3 + .... p8 + p9
                    if ((N_p1 >= 2) && (N_p1 <= 6)) condition_a = 1;

                    // Проверка условия (б) T(p1) = 1 T(p1) — число переходов 0—1 в упорядоченной последовательности p2, p3, ... , p8, p9, p2
                    for (int j = 0; j < 7; j++) // Проверка перехода P2...P8
                    {
                        if ((array_1_and_0[j] == 0) && (array_1_and_0[j + 1] == 1))
                        {
                            T_p++;
                        }
                    }
                    if ((array_1_and_0[7] == 0) && (array_1_and_0[0] == 1))
                    {
                        T_p++; // Проверка перехода P9 и P2
                    }
                    if (T_p == 1)
                    {
                        condition_b = 1;
                    }

                    // Как долго я ждал этого момента мой маленький зеленый дружок
                    // Проверка условия (в) p2 * p4 * p6 = 0
                    if ((color_image[i - Width] * // P2
                        color_image[i + 1] * // P4
                        color_image[i + Width]) == 0) condition_v = 1; // P6

                    // Проверка условия (г) p4 · p6 · p8 = 0
                    if ((color_image[i + 1] * // P4
                        color_image[i + Width] * // P6
                        color_image[i - 1]) == 0) condition_g = 1; // P8

                    // А это нахуя? Ковальский, анализ

                    //for (int k = elem; k < i; k++)
                    //{
                    //    if (grayValues[k] == 1)
                    //    {
                    //        newBit1.SetPixel((k % Width), (k / Width), Color.White);
                    //        m1++;
                    //    }
                    //    if (grayValues[k] == 0)
                    //    {
                    //        newBit1.SetPixel((k % Width), (k / Width), Color.Black );
                    //        m2++;
                    //    }
                    //
                    //    m3++;
                    //}
                    
                    // Я вот думаю нам 2 массива точно нада? может в этом вся проблема ?
                    // Создаем новое изображения после первого прохода
                    if ((condition_a + condition_b + condition_v + condition_g) == 4)
                    {   // досих пор не пойму как это считается Your bunny wrote
                        //newBit1.SetPixel((i % Width), (i / Width), Color.White);
                        color_image[i] = 0;
                    }
                    //else
                    //{
                    //    //newBit1.SetPixel((i % Width), (i / Width), Color.White);
                    //    int xddd = 0;
                    //}
                    //elem = i + 1;
                    //}
                }

                //return newBit1;

                // А это нахуя? Нихуя не понятно ..... ЧИ ДА??!

                //for (int k = elem; k < grayValues.Length; k++)
                //{
                //    if (grayValues[k] == 1)
                //    {
                //        newBit1.SetPixel((k % Width), (k / Width), Color.White);
                //    }
                //    if (grayValues[k] == 0)
                //    {
                //        newBit1.SetPixel((k % Width), (k / Width), Color.Black );
                //    }
                //}
                //

                // Создаем новый массив 1 и 0
                //for (int i = 0; i < newBit1.Width; i++)
                //{
                //    for (int j = 0; j < newBit1.Height; j++)
                //    {
                //        if (((newBit1.GetPixel(i, j).R + newBit1.GetPixel(i, j).G + newBit1.GetPixel(i, j).B) / 3) == 0)
                //        {
                //            NewgrayValues1[i + j * newBit1.Width] = 1;
                //        }
                //        else if (((newBit1.GetPixel(i, j).R + newBit1.GetPixel(i, j).G + newBit1.GetPixel(i, j).B) / 3) == 255)
                //        {
                //            NewgrayValues1[i + j * newBit1.Width] = 0;
                //        }
                //    }
                //}

                //Width = newBit1.Width; // Ширина как у нега
                //Height = newBit1.Height; // Длина как у нега
                //
                //int elem1 = 0;
                //m1 = 0;
                //m2 = 0;
                //m3 = 0;

                //for (int i = Width + 1; i < color_image.Length - Width - 1; i++)
                //{
                //
                //    //if ((
                //    //    NewgrayValues1[i - Width - 1] + 
                //    //    NewgrayValues1[i - Width] + 
                //    //    NewgrayValues1[i - Width + 1] + 
                //    //    NewgrayValues1[i - 1] + 
                //    //    NewgrayValues1[i + 1] + 
                //    //    NewgrayValues1[i + Width - 1] + 
                //    //    NewgrayValues1[i + Width] + 
                //    //    NewgrayValues1[i + Width + 1]) < 8)
                //    //{
                //        int testc = (
                //            color_image[i - Width - 1] +
                //            color_image[i - Width] +
                //            color_image[i - Width + 1] +
                //            color_image[i - 1] +
                //            color_image[i + 1] +
                //            color_image[i + Width - 1] +
                //            color_image[i + Width] +
                //            color_image[i + Width + 1]);
                //        int fa = 0, fb = 0, fc = 0, fd = 0;
                //        if ((testc >= 2) && (testc <= 6)) fa = 1;
                //        int Tp = 0;
                //
                //        byte[] values = new byte[8];
                //        values[0] = color_image[i - Width];      // P2 
                //        values[1] = color_image[i - Width + 1];  // P3
                //        values[2] = color_image[i + 1];          // P4
                //        values[3] = color_image[i + Width + 1];  // P5
                //        values[4] = color_image[i + Width];      // P6
                //        values[5] = color_image[i + Width - 1];  // P7
                //        values[6] = color_image[i - 1];          // P8
                //        values[7] = color_image[i - Width - 1];  // P9
                //
                //        for (int j = 0; j < 7; j++)
                //            if ((values[j] == 0) && (values[j + 1] == 1)) Tp++;
                //        if ((values[7] == 0) && (values[0] == 1)) Tp++;
                //        if (Tp == 1) fb = 1;
                //
                //        if ((color_image[i - Width] * color_image[i + 1] * color_image[i - 1]) == 0) fc = 1; // P2 P4 P8
                //        //if ((NewgrayValues1[i - Width] * NewgrayValues1[i + 1] * NewgrayValues1[i + Width]) == 0) fc = 1;
                //        if ((color_image[i - Width] * color_image[i + Width] * color_image[i - 1]) == 0) fd = 1; // P2 P6 P8
                //        //if ((NewgrayValues1[i + 1] * NewgrayValues1[i + Width] * NewgrayValues1[i - 1]) == 0) fd = 1;
                //        //or (int k = elem1; k < i; k++)
                //        //
                //        //   if (NewgrayValues1[k] == 1)
                //        //   {
                //        //       newBit2.SetPixel((k % Width), (k / Width), Color.White );
                //        //       m1++;
                //        //   }
                //        //   if (NewgrayValues1[k] == 0)
                //        //   {
                //        //       newBit2.SetPixel((k % Width), (k / Width), Color.Black);
                //        //       m2++;
                //        //   }
                //        //   m3++;
                //        //
                //
                //        if ((fa + fb + fc + fd) == 4)
                //        {
                //            //newBit2.SetPixel((i % Width), (i / Width), Color.Black);
                //            color_image[i] = 0;
                //    }
                //        else
                //        {
                //        //newBit2.SetPixel((i % Width), (i / Width), Color.White); Black
                //        int xddd = 0;
                //    }
                //        //elem1 = i + 1;
                //
                //    //}
                //
                //    int oprtyerg = 0;
                //}

                //for (int j = 0; j < 7; j++)
                //{
                //    newBit2.SetPixel((i % Width), (i / Width), Color.Black);
                //}
                for (int i = Width + 1; i < color_image.Length - Width - 1; i++) // Бегаем по массиву 1 и 0 изображения
                {
                    //Нахуя он нужен?
                    //if ((
                    //    grayValues[i - Width - 1] + 
                    //    grayValues[i - Width] + 
                    //    grayValues[i - Width + 1] + 
                    //    grayValues[i - 1] + 
                    //    grayValues[i + 1] + 
                    //    grayValues[i + Width - 1] + 
                    //    grayValues[i + Width] + 
                    //    grayValues[i + Width + 1]) < 8)
                    // {

                    //Cycle++; // для дебага

                    /* Условия для первого шага
                      (а) 2 ≤ N(p1) ≤ 6
                      (б) T(p1) = 1
                      (в) p2 * p4 * p6 = 0
                      (г) p4 * p6 * p8 = 0
                    */
                    int condition_a = 0, condition_b = 0, condition_v = 0, condition_g = 0; // Обьявление пустых переменных для условий
                    int T_p = 0; // Обьявление пустой переменной для условия (б)
                    int N_p1 = ( // Обьявление переменной для условия (а)
                        color_image[i - Width] +     // P2
                        color_image[i - Width + 1] + // P3
                        color_image[i + 1] +         // P4
                        color_image[i + Width + 1] + // P5
                        color_image[i + Width] +     // P6
                        color_image[i + Width - 1] + // P7
                        color_image[i - 1] +         // P8
                        color_image[i - Width - 1]); // P9  

                    // Задание массива для проверки условий (в) и (г)
                    byte[] array_1_and_0 = new byte[8];
                    array_1_and_0[0] = color_image[i - Width];      // P2 
                    array_1_and_0[1] = color_image[i - Width + 1];  // P3
                    array_1_and_0[2] = color_image[i + 1];          // P4
                    array_1_and_0[3] = color_image[i + Width + 1];  // P5
                    array_1_and_0[4] = color_image[i + Width];      // P6
                    array_1_and_0[5] = color_image[i + Width - 1];  // P7
                    array_1_and_0[6] = color_image[i - 1];          // P8
                    array_1_and_0[7] = color_image[i - Width - 1];  // P9

                    // Сергей жружко маг? конечно нет
                    // Проверка условия (а) N(p1) - Число ненулевых соседей элемента p1 N(p1)=p2 + p3 + .... p8 + p9
                    if ((N_p1 >= 2) && (N_p1 <= 6)) condition_a = 1;

                    // Проверка условия (б) T(p1) = 1 T(p1) — число переходов 0—1 в упорядоченной последовательности p2, p3, ... , p8, p9, p2
                    for (int j = 0; j < 7; j++) // Проверка перехода P2...P8
                    {
                        if ((array_1_and_0[j] == 0) && (array_1_and_0[j + 1] == 1))
                        {
                            T_p++;
                        }
                    }
                    if ((array_1_and_0[7] == 0) && (array_1_and_0[0] == 1))
                    {
                        T_p++; // Проверка перехода P9 и P2
                    }
                    if (T_p == 1)
                    {
                        condition_b = 1;
                    }

                    // Как долго я ждал этого момента мой маленький зеленый дружок
                    // Проверка условия (д) p2 * p4 * p8 = 0
                    if ((color_image[i - Width] * // P2
                        color_image[i + 1] * // P4
                        color_image[i - 1]) == 0) condition_v = 1; // P8

                    // Проверка условия (Е) p2 · p6 · p8 = 0
                    if ((color_image[i - Width] * // P2
                        color_image[i + Width] * // P6
                        color_image[i - 1]) == 0) condition_g = 1; // P8

                    // А это нахуя? Ковальский, анализ

                    //for (int k = elem; k < i; k++)
                    //{
                    //    if (grayValues[k] == 1)
                    //    {
                    //        newBit1.SetPixel((k % Width), (k / Width), Color.White);
                    //        m1++;
                    //    }
                    //    if (grayValues[k] == 0)
                    //    {
                    //        newBit1.SetPixel((k % Width), (k / Width), Color.Black );
                    //        m2++;
                    //    }
                    //
                    //    m3++;
                    //}

                    // Я вот думаю нам 2 массива точно нада? может в этом вся проблема ?
                    // Создаем новое изображения после первого прохода
                    if ((condition_a + condition_b + condition_v + condition_g) == 4)
                    {   // досих пор не пойму как это считается Your bunny wrote
                        //newBit1.SetPixel((i % Width), (i / Width), Color.White);
                        color_image[i] = 0;
                    }
                    //else
                    //{
                    //    //newBit1.SetPixel((i % Width), (i / Width), Color.White);
                    //    int xddd = 0;
                    //}
                    //elem = i + 1;
                    //}
                }


                for (int i = 0; i < color_image.Length; i++)
                {
                    if (color_image[i] == 1)
                    {
                        processed_bitmap.SetPixel((i % Width), (i / Width), Color.Black);
                    }
                    if (color_image[i] == 0)
                    {
                        processed_bitmap.SetPixel((i % Width), (i / Width), Color.White);
                    }
                }
                

                return processed_bitmap;
                
                //System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                //for (int i = 1; i < Width; i++)
                //{
                //    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width] + grayValues[i + Width + 1] + grayValues[i + Width - 1]) / 6);
                //}

                /*
                int elem = 0;
                int m1 = 0;
                for (int i = Width + 1; i < grayValues.Length - Width -1; i++)
                {

                    if ((grayValues[i] + grayValues[i - Width - 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width - 1] + grayValues[i + Width] + grayValues[i + Width + 1]) < 8)
                    {
                        int testc = (grayValues[i] + grayValues[i - Width - 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width - 1] + grayValues[i + Width] + grayValues[i + Width + 1]);
                        int fa = 0, fb = 0, fc = 0, fd = 0;
                        if ((testc >= 2) && (testc <= 6)) fa = 1;
                        int Tp = 0;

                        byte[] values = new byte[8];
                        values[0] = grayValues[i - Width];      // P2 
                        values[1] = grayValues[i - Width + 1];  // P3
                        values[2] = grayValues[i + 1];          // P4
                        values[3] = grayValues[i + Width + 1];  // P5
                        values[4] = grayValues[i + Width];      // P6
                        values[5] = grayValues[i + Width - 1];  // P7
                        values[6] = grayValues[i - 1];          // P8
                        values[7] = grayValues[i - Width - 1];  // P9

                        for (int j = 0; j < 7; j++)
                            if ((values[j] == 0) && (values[j + 1] == 1)) Tp++;
                        if ((values[7] == 0) && (values[0] == 1)) Tp++;
                        if (Tp == 1) fb = 1;

                        if ((grayValues[i - Width] * grayValues[i + 1] * grayValues[i + Width]) == 0) fc = 1;

                        if ((grayValues[i + 1] * grayValues[i + Width] * grayValues[i - 1]) == 0) fd = 1;

                        for (int k = elem; k < i; k++)
                            NewgrayValues1[k] = grayValues[k];

                        if ((fa + fb + fc + fd) == 4)
                        {
                            if (grayValues[i] != 0)
                            {
                                m1++;
                            }
                            NewgrayValues1[i] = 0;
                        }
                        else
                        {
                            NewgrayValues1[i] = grayValues[i];
                        }
                        elem = i + 1;

                    }


                    //end = i - Width + 1;
                    //if (end % Width == 0)
                    //{
                    //    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + Width] + grayValues[i + Width - 1] + grayValues[i - Width] + grayValues[i - Width - 1]) / 6);
                    //    continue;
                    //}
                    //else if (end - 1 % Width == 0)
                    //{
                    //    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i + 1] + grayValues[i + Width] + grayValues[i + Width + 1] + grayValues[i - Width] + grayValues[i - Width + 1]) / 6);
                    //    continue;
                    //}
                    //NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width] + grayValues[i + Width + 1] + grayValues[i + Width - 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - Width - 1]) / 9);
                }

                elem = 0;
                int m2 = 0;

                for (int i = Width + 1; i < NewgrayValues1.Length - Width - 1; i++)
                {

                    if ((NewgrayValues1[i] + NewgrayValues1[i - Width - 1] + NewgrayValues1[i - Width] + NewgrayValues1[i - Width + 1] + NewgrayValues1[i - 1] + NewgrayValues1[i + 1] + NewgrayValues1[i + Width - 1] + NewgrayValues1[i + Width] + NewgrayValues1[i + Width + 1]) < 8)
                    {
                        int testc = (NewgrayValues1[i] + NewgrayValues1[i - Width - 1] + NewgrayValues1[i - Width] + NewgrayValues1[i - Width + 1] + NewgrayValues1[i - 1] + NewgrayValues1[i + 1] + NewgrayValues1[i + Width - 1] + NewgrayValues1[i + Width] + NewgrayValues1[i + Width + 1]);
                        int fa = 0, fb = 0, fc = 0, fd = 0;
                        if ((testc >= 2) && (testc < 6)) fa = 1;
                        int Tp = 0;

                        byte[] values = new byte[8];
                        values[0] = grayValues[i - Width];
                        values[1] = grayValues[i - Width + 1];
                        values[2] = grayValues[i + 1];
                        values[3] = grayValues[i + Width + 1];
                        values[4] = grayValues[i + Width];
                        values[5] = grayValues[i + Width - 1];
                        values[6] = grayValues[i - 1];
                        values[7] = grayValues[i - Width - 1];
                        //byte[] mask = new byte[8];
                        //mask[0] = 64;
                        //mask[1] = 32;
                        //mask[2] = 8;
                        //mask[3] = 1;
                        //mask[4] = 2;
                        //mask[5] = 4;
                        //mask[6] = 16;
                        //mask[7] = 128;
                        //
                        //byte[] values = new byte[8];
                        //values[0] = grayValues[i - Width];
                        //values[1] = grayValues[i - Width + 1];
                        //values[2] = grayValues[i + 1];
                        //values[3] = grayValues[i + Width + 1];
                        //values[4] = grayValues[i + Width];
                        //values[5] = grayValues[i + Width - 1];
                        //values[6] = grayValues[i - 1];
                        //values[7] = grayValues[i - Width - 1];

                        for (int j = 0; j < 7; j++)
                            if ((values[j] == 0) && (values[j + 1] == 1)) Tp++;
                        if ((values[7] == 0) && (values[0] == 1)) Tp++;
                        if (Tp == 1) fb = 1;

                        if ((NewgrayValues1[i - Width] * NewgrayValues1[i + 1] * NewgrayValues1[i - 1]) == 0) fc = 1;

                        if ((NewgrayValues1[i - Width] * NewgrayValues1[i + Width] * NewgrayValues1[i - 1]) == 0) fd = 1;

                        for (int k = elem; k < i; k++)
                            NewgrayValues2[k] = grayValues[k];

                        if ((fa + fb + fc + fd) == 4)
                        {
                            if (NewgrayValues1[i] != 0)
                            {
                                m2++;
                            }
                            NewgrayValues2[i] = 0;
                        }
                        else
                        {
                            NewgrayValues2[i] = grayValues[i];
                        }
                        elem = i + 1;

                    }

                    //NewgrayValues2[i] = 0;
                }

                //for (int i = grayValues.Length - Width + 1; i < grayValues.Length - 1; i++)
                //{
                //    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - Width - 1]) / 6);
                //}

                System.Runtime.InteropServices.Marshal.Copy(NewgrayValues2, 0, ptr, bytes);

                TempBitmap.UnlockBits(bmpData);

                */

                //return TempBitmap;

            }

            ~skeletization()
            {

            }
        } // Сглаживание

        //class binarization
        //{ // Бинаризация
        //    public Bitmap transformation(Bitmap BitBin)
        //    {
        //        System.Drawing.Imaging.BitmapData data = BitBin.LockBits(new Rectangle(0, 0, BitBin.Width, BitBin.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, BitBin.PixelFormat);
        //
        //        Byte[] bytes = new byte[data.Height * data.Stride];
        //
        //        System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);
        //
        //        for (int i = 0; i < data.Height * data.Stride; i++)
        //        {
        //            if (bytes[i] >= 135)
        //                bytes[i] = 255;
        //            else
        //                bytes[i] = 0;
        //        }
        //
        //        System.Runtime.InteropServices.Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
        //        BitBin.UnlockBits(data);
        //
        //        return BitBin;
        //    }
        //
        //    ~binarization()
        //    {
        //
        //    }
        //} // Бинаризация

        class binarization
        {
            public System.Drawing.Bitmap BitmapToBlackWhite2(System.Drawing.Bitmap src)
            {
                // 1.
                double treshold = 0.6;

                // 2.
                //int treshold = 150;

                Bitmap dst = new Bitmap(src.Width, src.Height);

                for (int i = 0; i < src.Width; i++)
                {
                    for (int j = 0; j < src.Height; j++)
                    {
                        // 1.
                        dst.SetPixel(i, j, src.GetPixel(i, j).GetBrightness() < treshold ? System.Drawing.Color.Black : System.Drawing.Color.White);

                        // 2 (пактически тоже, что 1).
                        //System.Drawing.Color color = src.GetPixel(i, j);
                        //int average = (int)(color.R + color.B + color.G) / 3;
                        //dst.SetPixel(i, j, average < treshold ? System.Drawing.Color.Black : System.Drawing.Color.White);
                    }
                }

                return dst;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        { // Бинаризация

           
            pictureBox2.Image = null;

            binarization binar = new binarization();

            pictureBox2.Image = (Image)binar.BitmapToBlackWhite2(BitmapBinarization);

        } // Бинаризация

        private void button2_Click(object sender, EventArgs e)
        { // Загурзить изображение
            pictureBox1.Image = null;
            pictureBox2.Image = null;
           

            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Bitmap Image|*.bmp|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.Title = "Open Image File";
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    BitmapPicture1 = new Bitmap(openFileDialog1.FileName);
                   
                    BitmapBinarization = new Bitmap(openFileDialog1.FileName);
         
                }
            }    
            pictureBox1.Image = (Image)BitmapPicture1;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            skeletization test = new skeletization();

            binarization binar = new binarization();
            bitNew = binar.BitmapToBlackWhite2(BitmapBinarization);

            pictureBox3.Image = (Image)test.transformation(bitNew);
        }
    }
}