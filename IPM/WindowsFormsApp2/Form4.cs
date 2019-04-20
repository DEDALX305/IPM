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
        public static Bitmap bitNew;


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
                Bitmap processed_bitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
                int bytes = TempBitmap.Width * TempBitmap.Height;
                byte[] color_image = new byte[bytes];

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

                int Width = TempBitmap.Width; // Ширина члена
                int Height = TempBitmap.Height; // дли... высота члена

                for ( int i = Width + 1; i < color_image.Length - Width - 1; i++) // Бегаем по массиву 1 и 0 изображения
                {
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

                    // Создаем новое изображения после первого прохода
                    if ((condition_a + condition_b + condition_v + condition_g) == 4)
                    {   
                        color_image[i] = 0;
                    }
                }

                for (int i = Width + 1; i < color_image.Length - Width - 1; i++) // Бегаем по массиву 1 и 0 изображения
                {
                    
                    /* Условия для первого шага
                      (а) 2 ≤ N(p1) ≤ 6
                      (б) T(p1) = 1
                      (в) p2 * p4 * p8 = 0
                      (г) p2 * p6 * p8 = 0
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

                    // Создаем новое изображения после первого прохода
                    if ((condition_a + condition_b + condition_v + condition_g) == 4)
                    {   
                        color_image[i] = 0;
                    }
                }

                // Создаем изображдение из обработанного массива
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
            }

            ~skeletization()
            {

            }
        } // Сглаживание

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