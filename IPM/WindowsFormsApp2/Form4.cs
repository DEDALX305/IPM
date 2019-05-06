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
                    int condition_a = 0, condition_b = 0, condition_v = 0, condition_g = 0; // Обьявление пустых переменных для условий, одно из них твой мозг
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

                    // Сергей Дружко маг? конечно нет
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

                    // Создаем новое изображения после первого прохода, главное не создать Скайнет.
                    if ((condition_a + condition_b + condition_v + condition_g) == 4)
                    {   
                        color_image[i] = 0;
                    }
                }

                for (int i = Width + 1; i < color_image.Length - Width - 1; i++) // Бегаем по массиву 1 и 0 изображения
                {
                    
                    /* Условия для первого шага, быть богатым, и тогда она сама тебе на хуй прыгнет
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

                byte[] Rules = new byte[255];

                // Вы очевидно очень гордитесь вашими клонами
                
                Rules[0] = 1;
                Rules[1] = 0;
                Rules[2] = 0;
                Rules[3] = 3;
                Rules[4] = 0;
                Rules[5] = 1;
                Rules[6] = 1;
                Rules[7] = 1;
                Rules[8] = 0;
                Rules[9] = 1;
                Rules[10] = 0;
                Rules[11] = 0;
                Rules[12] = 1;
                Rules[13] = 1;
                Rules[14] = 1;
                Rules[15] = 1;
                Rules[16] = 0;
                Rules[17] = 0;
                Rules[18] = 1;
                Rules[19] = 0;
                Rules[20] = 1;
                Rules[21] = 1;
                Rules[22] = 1;
                Rules[23] = 1;
                Rules[24] = 0;
                Rules[25] = 0;
                Rules[26] = 0;
                Rules[27] = 0;
                Rules[28] = 1;
                Rules[29] = 1;
                Rules[30] = 1;
                Rules[31] = 1;
                Rules[32] = 0;
                Rules[33] = 5;
                Rules[34] = 0;
                Rules[35] = 0;
                Rules[36] = 0;
                Rules[37] = 0;
                Rules[38] = 0;
                Rules[39] = 5;
                Rules[40] = 1;
                Rules[41] = 1;
                Rules[42] = 0;
                Rules[43] = 3;
                Rules[44] = 1;
                Rules[45] = 1;
                Rules[46] = 1;
                Rules[47] = 1;
                Rules[48] = 0;
                Rules[49] = 0;
                Rules[50] = 0;
                Rules[51] = 0;
                Rules[52] = 2;
                Rules[53] = 5;
                Rules[54] = 2;
                Rules[55] = 0;
                Rules[56] = 0;
                Rules[57] = 0;
                Rules[58] = 0;
                Rules[59] = 0;
                Rules[60] = 1;
                Rules[61] = 1;
                Rules[62] = 0;
                Rules[63] = 1;
                Rules[64] = 4;
                Rules[65] = 0;
                Rules[66] = 0;
                Rules[67] = 0;
                Rules[68] = 0;
                Rules[69] = 0;
                Rules[70] = 0;
                Rules[71] = 0;
                Rules[72] = 1;
                Rules[73] = 1;
                Rules[74] = 4;
                Rules[75] = 3;
                Rules[76] = 1;
                Rules[77] = 1;
                Rules[78] = 1;
                Rules[79] = 1;
                Rules[80] = 1;
                Rules[81] = 5;
                Rules[82] = 1;
                Rules[83] = 3;
                Rules[84] = 1;
                Rules[85] = 1;
                Rules[86] = 1;
                Rules[87] = 1;
                Rules[88] = 1;
                Rules[89] = 1;
                Rules[90] = 1;
                Rules[91] = 1;
                Rules[92] = 1;
                Rules[93] = 0;
                Rules[94] = 0;
                Rules[95] = 0;
                Rules[96] = 1;
                Rules[97] = 0;
                Rules[98] = 0;
                Rules[99] = 0;
                Rules[100] = 0;
                Rules[101] = 0;
                Rules[102] = 0;
                Rules[103] = 0;
                Rules[104] = 1;
                Rules[105] = 1;
                Rules[106] = 4;
                Rules[107] = 0;
                Rules[108] = 1;
                Rules[109] = 1;
                Rules[110] = 0;
                Rules[111] = 1;
                Rules[112] = 1;
                Rules[113] = 5;
                Rules[114] = 1;
                Rules[115] = 0;
                Rules[116] = 1;
                Rules[117] = 1;
                Rules[118] = 0;
                Rules[119] = 0;
                Rules[120] = 1;
                Rules[121] = 1;
                Rules[122] = 0;
                Rules[123] = 0;
                Rules[124] = 0;
                Rules[125] = 0;
                Rules[126] = 0;
                Rules[127] = 0;
                Rules[128] = 3;
                Rules[129] = 0;
                Rules[130] = 4;
                Rules[131] = 0;
                Rules[132] = 0;
                Rules[133] = 0;
                Rules[134] = 0;
                Rules[135] = 4;
                Rules[136] = 0;
                Rules[137] = 0;
                Rules[138] = 0;
                Rules[139] = 0;
                Rules[140] = 2;
                Rules[141] = 2;
                Rules[142] = 4;
                Rules[143] = 4;
                Rules[144] = 1;
                Rules[145] = 0;
                Rules[146] = 1;
                Rules[147] = 0;
                Rules[148] = 1;
                Rules[149] = 1;
                Rules[150] = 1;
                Rules[151] = 1;
                Rules[152] = 0;
                Rules[153] = 0;
                Rules[154] = 0;
                Rules[155] = 0;
                Rules[156] = 1;
                Rules[157] = 0;
                Rules[158] = 1;
                Rules[159] = 1;
                Rules[160] = 2;
                Rules[161] = 0;
                Rules[162] = 0;
                Rules[163] = 0;
                Rules[164] = 0;
                Rules[165] = 0;
                Rules[166] = 0;
                Rules[167] = 0;
                Rules[168] = 0;
                Rules[169] = 2;
                Rules[170] = 0;
                Rules[171] = 0;
                Rules[172] = 2;
                Rules[173] = 2;
                Rules[174] = 0;
                Rules[175] = 0;
                Rules[176] = 0;
                Rules[177] = 0;
                Rules[178] = 2;
                Rules[179] = 0;
                Rules[180] = 2;
                Rules[181] = 0;
                Rules[182] = 2;
                Rules[183] = 0;
                Rules[184] = 0;
                Rules[185] = 0;
                Rules[186] = 0;
                Rules[187] = 0;
                Rules[188] = 1;
                Rules[189] = 0;
                Rules[190] = 0;
                Rules[191] = 0;
                Rules[192] = 1;
                Rules[193] = 0;
                Rules[194] = 0;
                Rules[195] = 0;
                Rules[196] = 0;
                Rules[197] = 0;
                Rules[198] = 0;
                Rules[199] = 0;
                Rules[200] = 1;
                Rules[201] = 1;
                Rules[202] = 4;
                Rules[203] = 0;
                Rules[204] = 1;
                Rules[205] = 0;
                Rules[206] = 1;
                Rules[207] = 0;
                Rules[208] = 1;
                Rules[209] = 5;
                Rules[210] = 1;
                Rules[211] = 0;
                Rules[212] = 1;
                Rules[213] = 0;
                Rules[214] = 1;
                Rules[215] = 1;
                Rules[216] = 1;
                Rules[217] = 0;
                Rules[218] = 1;
                Rules[219] = 0;
                Rules[220] = 0;
                Rules[221] = 0;
                Rules[222] = 0;
                Rules[223] = 0;
                Rules[224] = 1;
                Rules[225] = 5;
                Rules[226] = 0;
                Rules[227] = 0;
                Rules[228] = 0;
                Rules[229] = 0;
                Rules[230] = 0;
                Rules[231] = 0;
                Rules[232] = 1;
                Rules[233] = 1;
                Rules[234] = 4;
                Rules[235] = 0;
                Rules[236] = 1;
                Rules[237] = 1;
                Rules[238] = 0;
                Rules[239] = 0;
                Rules[240] = 1;
                Rules[241] = 0;
                Rules[242] = 1;
                Rules[243] = 0;
                Rules[244] = 1;
                Rules[245] = 0;
                Rules[246] = 1;
                Rules[247] = 0;
                Rules[248] = 1;
                Rules[249] = 1;
                Rules[250] = 1;
                Rules[251] = 0;
                Rules[252] = 0;
                Rules[253] = 0;
                Rules[254] = 0;
                Rules[255] = 0;

                // ЕБАЛ В РОТ ЭТУ ТАБЛИЦУ БЛЯ

                return processed_bitmap;
            }

            ~skeletization()
            {

            }
        } // Скелитизация

        class chain // Цепной код
        { // Скелетизация! даже в заднице есть выход ¯ \ _ (ツ) _ / ¯ 
            public List<int> transformation(Bitmap TempBitmap)
            {
                Bitmap processed_bitmap = new Bitmap(TempBitmap.Width, TempBitmap.Height);
                int bytes = TempBitmap.Width * TempBitmap.Height;
                byte[] color_image = new byte[bytes];
                byte[,] color_image_matr = new byte[TempBitmap.Width, TempBitmap.Width]; // содание матрицы изображения

                // Создание матрицы 1 и 0 по бинаризованному изображению

                for (int i = 0; i < TempBitmap.Width; i++)
                {
                    for (int j = 0; j < TempBitmap.Height; j++)
                    {
                        if (((TempBitmap.GetPixel(i, j).R + TempBitmap.GetPixel(i, j).G + TempBitmap.GetPixel(i, j).B) / 3) == 0)   // чёрн... т.е афроамериканский.
                        {
                            color_image_matr[j, i] = 1; 
                            //c1++;
                        }
                        else if (((TempBitmap.GetPixel(i, j).R + TempBitmap.GetPixel(i, j).G + TempBitmap.GetPixel(i, j).B) / 3) == 255)    // белый
                        {
                            color_image_matr[j, i] = 0;
                            //c2++;
                        }
                    }
                }

                // Проверка правильности записи изображния в матрицу
                // Проверка твоей ориентации... Спойлер: Ты пидор
                
                for (int i = 0; i < TempBitmap.Height; i++)
                {
                    for (int j = 0; j < TempBitmap.Width; j++)
                    {
                        Console.Write(color_image_matr[i, j]);
                    }
                    Console.Write("\n");
                }
                

                int Width = TempBitmap.Width; // Ширина члена
                int Height = TempBitmap.Height; // дли... высота члена

                /* Алгоритм... могли неверно истолковать. (с) Магистр ДжедайГУ Йоба
                  1.
                    Пусть задана двоичная область (или ее граница) R. Алгоритм прослеживания границы R состоит из следующих шагов.
                    В качестве начальной точки 1. b0 выбирается самая левая верхняя точка изображения, имеющая значение 1. 
                    Обозначим c0 левого соседа b0, как показано на рис. 11.1(б); ясно, что c0 всегда является точкой фона. 
                    Рассмотрим восьмерку соседей b0, начиная с c0 и двигаясь по часовой стрелке. Пусть b1 — первая встретившаяся точка со значением 1, а c1 — точка фона, непосредственно ей предшествующая 
                    в указанном порядке обхода. Запоминаем положения точек b0 и b1 для использования на шаге 5.   
                */
                //  1. Ищем самую верхнюю левую точку, которая имеет значение 1 и получаем её координаты в качестве b0

                int b0i = 0, b0j = 0;
                int minWidth = Width, minHeight = Height; // Начальное значение самой левой и верхней точки, которое равно конечной точке твоего раздолбленного очка

                for (int i = 0; i < Height; i++) // Бегаем по массиву, как твоя мамаша по хуям, в поисках самой верхней левой точки со значением 1
                {
                    for (int j = 0; j< Width; j++)
                    {
                        if (color_image_matr[i,j] == 1) // Проверка условия, если точка равна единице, то проверяем её на левость и верхность
                        {
                            //Проверка условия, если точка левее или равна самой левой точке и точка выше или равна самой высокой точке, то тогда сохраняем её координаты, и новые значения минимальных точек задаём
                            if (j < minWidth) 
                            {
                                b0i = i;
                                b0j = j;
                                minWidth = j;
                                minHeight = i;
                            }else if (j == minWidth)
                            {
                                if (i < minHeight)
                                {
                                    b0i = i;
                                    b0j = j;
                                    minWidth = j;
                                    minHeight = i;
                                }
                            }
                        }
                    }
                }

                // Совет от бати: Если тебе передёрнет девушка, говорящая на языке жестов, то это будет считаться за оральный секс.
                // Укор от бати: Только у тебя нет девушки задрот хуев. Иди онеме смотри.

                // 1. Обозначаем левого соседа b0 как c0
                int c0i = b0i, c0j = b0j - 1;
                int bi = 0, bj = 0;
                int b1i = 0, b1j = 0;
                int c1i = 0, c1j = 0;

                // 1. Запоминаем положения точек b0 и b1 для использования на шаге 5. 
                int b0iRemember = b0i, b0jRemember = b0j;
                int b1iRemember = b1i, b1jRemember = b1j;

                // Не беда, пол кода ещё осталось
                // 1. Ищем b1 и c1
                byte[] array_1_and_0_new = new byte[8];
                array_1_and_0_new[0] = color_image_matr[c0i,c0j];  // левая точка c0 от b0 
                array_1_and_0_new[1] = color_image_matr[c0i - 1,c0j];  // точка в левом верхнем углу от b0
                array_1_and_0_new[2] = color_image_matr[c0i - 1,c0j + 1];  // точка вверху от b0
                array_1_and_0_new[3] = color_image_matr[c0i - 1,c0j + 2];  // точка в правом верхнем углу от b0
                array_1_and_0_new[4] = color_image_matr[c0i, c0j + 2];  // точка справа от b0
                array_1_and_0_new[5] = color_image_matr[c0i + 1, c0j + 2];  // точка справа внизу от b0
                array_1_and_0_new[6] = color_image_matr[c0i + 1, c0j + 1];  // точка внизу от b0
                array_1_and_0_new[7] = color_image_matr[c0i + 1, c0j];  // точка слева внизу от b0

                // Нумерация направлений дорог к спальне твоей мамаши
                List<int> movement = new List<int>();
                // В завиисимости от направления меняет массив точек вокруг.... точки. Вверх = 1, Вправо = 2, Вниз = 3, Влево = 4, а в твою мамашу бесплатно, для всего остального есть мастер кард
                int WhyAreUGay = 0; 

                for (int i = 0; i < 8; i++)
                {
                    if (array_1_and_0_new[i] == 1)
                    {
                        switch (i)
                        {
                            case 0:
                                {
                                    b1i = c0i;
                                    b1j = c0j;
                                    c1i = c0i + 1;
                                    c1j = c0j;
                                    break;
                                }
                            case 1:
                                {
                                    b1i = c0i - 1;
                                    b1j = c0j;
                                    c1i = c0i;
                                    c1j = c0j;
                                    break;
                                }
                            case 2:
                                {
                                    b1i = c0i - 1;
                                    b1j = c0j + 1;
                                    c1i = c0i - 1;
                                    c1j = c0j;
                                    break;
                                }
                            case 3:
                                {
                                    b1i = c0i - 1;
                                    b1j = c0j + 2;
                                    c1i = c0i - 1;
                                    c1j = c0j + 1;
                                    break;
                                }
                            case 4:
                                {
                                    b1i = c0i;
                                    b1j = c0j + 2;
                                    c1i = c0i - 1;
                                    c1j = c0j + 2;
                                    break;
                                }
                            case 5:
                                {
                                    b1i = c0i + 1;
                                    b1j = c0j + 2;
                                    c1i = c0i;
                                    c1j = c0j + 2;
                                    break;
                                }
                            case 6:
                                {
                                    b1i = c0i + 1;
                                    b1j = c0j + 1;
                                    c1i = c0i + 1;
                                    c1j = c0j + 2;
                                    break;
                                }
                            case 7:
                                {
                                    b1i = c0i + 1;
                                    b1j = c0j;
                                    c1i = c0i + 1;
                                    c1j = c0j + 1;
                                    break;
                                }
                        }

                        break;
                    }
                }

                // Если точка идёт наверх, и точка c1 находится теперь слева
                if (((b1i + 1) == b0i) && (b1j == b0j))
                {
                    WhyAreUGay = 1;
                    c1i = b1i;
                    c1j = b1j - 1;
                    array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                    array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                    array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                    array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                    array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                }
                // Если точка идёт наверх направо, и точка c1 находится теперь слева
                if (((b1i + 1) == b0i) && ((b1j - 1) == b0j))
                {
                    WhyAreUGay = 1;
                    c1i = b1i;
                    c1j = b1j - 1;
                    array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                    array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                    array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                    array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                    array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                }

                // Если точка идёт направо, и точка c1 находится теперь вверху
                if ((b1i == b0i) && ((b1j - 1) == b0j))
                {
                    WhyAreUGay = 2;
                    c1i = b1i - 1;
                    c1j = b1j;
                    array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                    array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                    array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                    array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                    array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                }
                // Если точка идёт направо вниз, и точка c1 находится теперь вверху
                if (((b1i - 1) == b0i) && ((b1j - 1) == b0j))
                {
                    WhyAreUGay = 2;
                    c1i = b1i - 1;
                    c1j = b1j;
                    array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                    array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                    array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                    array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                    array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                }

                // Если точка идёт вниз, и точка c1 находится теперь справа
                if (((b1i - 1) == b0i) && (b1j == b0j))
                {
                    WhyAreUGay = 3;
                    c1i = b1i;
                    c1j = b1j + 1;
                    array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                    array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                    array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                    array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                    array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                }
                // Если точка идёт вниз налево, и точка c1 находится теперь справа
                if (((b1i - 1) == b0i) && ((b1j + 1) == b0j))
                {
                    WhyAreUGay = 3;
                    c1i = b1i;
                    c1j = b1j + 1;
                    array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                    array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                    array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                    array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                    array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                }

                // Если точка идёт налево, то она шалава и точка c1 находится теперь внизу
                if ((b1i == b0i) && ((b1j + 1) == b0j))
                {
                    WhyAreUGay = 4;
                    c1i = b1i + 1;
                    c1j = b1j;
                    array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                    array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                    array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                    array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                    array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                }
                // Если точка идёт налево вверх, и точка c1 находится теперь внизу
                if (((b1i + 1) == b0i) && ((b1j + 1) == b0j))
                {
                    WhyAreUGay = 4;
                    c1i = b1i + 1;
                    c1j = b1j;
                    array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                    array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                    array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                    array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                    array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                    array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                    array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                    array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                }

                // Умение использовать if ещё не признак интеллекта
                // Если точка идёт наверх 
                if (((b1i + 1) == b0i) && (b1j == b0j))
                {
                    movement.Add(2);
                }
                // Если точка идёт наверх направо
                if (((b1i + 1)  == b0i) && ((b1j - 1) == b0j))
                {
                    movement.Add(1);
                }
                // Если точка идёт направо
                if ((b1i == b0i) && ((b1j - 1) == b0j))
                {
                    movement.Add(0);
                }
                // Если точка идёт вниз направо
                if (((b1i - 1) == b0i) && ((b1j - 1) == b0j))
                {
                    movement.Add(7);
                }
                // Если точка идёт вниз
                if (((b1i - 1) == b0i) && (b1j == b0j))
                {
                    movement.Add(6);
                    // Будешь проходить мимо.... проходи мимо
                }
                // Если точка идёт вниз налево
                if (((b1i - 1) == b0i) && ((b1j + 1) == b0j))
                {
                    movement.Add(5);
                }
                // Если точка идёт налево, то она шалава
                if ((b1i == b0i) && ((b1j + 1) == b0j))
                {
                    movement.Add(4);
                }
                // Если точка идёт налево наверх
                if (((b1i + 1) == b0i) && ((b1j + 1) == b0j))
                {
                    movement.Add(3);
                    // Твоему направлению пришщёл конец!
                }

                // 2. Пусть b = b1 и c = c1.
                bi = b1i;
                bj = b1j;
                int ci = c1i, cj = c1j;

                /*
                  5.
                    Повторяем шаги 3 и 4 до тех пор, пока не получим, что 5. b = b0 и следующая найденная точка границы — b1  
                */

                // Ержан, просыпайся бля!
                do
                {
                    /*
                      1.
                        Ясно, что c0 всегда является точкой фона. 
                        Рассмотрим восьмерку соседей b0, начиная с c0 и двигаясь по часовой стрелке. Пусть b1 — первая встретившаяся точка со значением 1, 
                        а c1 — точка фона, непосредственно ей предшествующая 
                        в указанном порядке обхода. Запоминаем положения точек b0 и b1 для использования на шаге 5.
                      2. 
                        Пусть b = b1 и c = c1.
                      3.
                        Начиная с точки c и двигаясь по часовой стрелке, обозначим восьмерку соседей точки b через n1, n2,..., n8. Находим первую точку nk, имеющую значение 1.
                      4.
                        Кладем 4. b = nk  и c = nk–1.
                    */
                    // Создание массива вокруг точки b0 для обхода по точкам вокруг неё по часовой стрелке
                    // Ты недооцениваешь мой код!

                    // Если точка идёт наверх, и точка c1 находится теперь слева
                    if (WhyAreUGay == 1)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (array_1_and_0_new[i] == 1)
                            {
                                switch (i)
                                {
                                    case 0:
                                        {
                                            b1i = bi;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 1:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj - 1;
                                            c1i = bi;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 2:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj;
                                            c1i = bi - 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 3:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj;
                                            break;
                                        }
                                    case 4:
                                        {
                                            b1i = bi;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 5:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj + 1;
                                            c1i = bi;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 6:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj;
                                            c1i = bi + 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 7:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj;
                                            break;
                                        }
                                }

                                break;
                            }
                        }

                        //Смотрим куда пошла точка... Спойлер: К твоей мамаше.
                        // Если точка идёт наверх, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && (b1j == bj))
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }
                        // Если точка идёт наверх направо, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }

                        // Если точка идёт направо, и точка c1 находится теперь вверху
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }
                        // Если точка идёт направо вниз, и точка c1 находится теперь вверху
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }

                        // Если точка идёт вниз, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }
                        // Если точка идёт вниз налево, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }

                        // Если точка идёт налево, то она шалава и точка c1 находится теперь внизу
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }
                        // Если точка идёт налево вверх, и точка c1 находится теперь внизу
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }

                        // Добавляем в массив путей значение.
                        // Если точка идёт наверх 
                        if (((b1i + 1) == bi) && (b1j == bj))
                        {
                            movement.Add(2);
                        }
                        // Если точка идёт наверх направо
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(1);
                        }
                        // Если точка идёт направо
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(0);
                        }
                        // Если точка идёт вниз направо
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(7);
                        }
                        // Если точка идёт вниз
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            movement.Add(6);
                            // Будешь проходить мимо.... проходи мимо
                        }
                        // Если точка идёт вниз налево
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(5);
                        }
                        // Если точка идёт налево, то она шалава
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(4);
                        }
                        // Если точка идёт налево наверх
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(3);
                            // Твоему направлению пришщёл конец!
                        }

                        bi = b1i;
                        bj = b1j;
                        ci = c1i;
                        cj = c1j;
                    } else if (WhyAreUGay == 2)    // Если точка идёт направо, и точка c1 находится теперь вверху
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (array_1_and_0_new[i] == 1)
                            {
                                switch (i)
                                {
                                    case 0:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj;
                                            c1i = bi - 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 1:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj;
                                            break;
                                        }
                                    case 2:
                                        {
                                            b1i = bi;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 3:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj + 1;
                                            c1i = bi;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 4:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj;
                                            c1i = bi + 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 5:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj;
                                            break;
                                        }
                                    case 6:
                                        {
                                            b1i = bi;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 7:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj - 1;
                                            c1i = bi;
                                            c1j = bj - 1;
                                            break;
                                        }
                                }

                                break;
                            }
                        }

                        // Смотрим куда пошла точка... Спойлер: К твоей мамаше.
                        // Если точка идёт наверх, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && (b1j == bj))
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }
                        // Если точка идёт наверх направо, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }

                        // Если точка идёт направо, и точка c1 находится теперь вверху
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }
                        // Если точка идёт направо вниз, и точка c1 находится теперь вверху
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }

                        // Если точка идёт вниз, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }
                        // Если точка идёт вниз налево, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }

                        // Если точка идёт налево, то она шалава и точка c1 находится теперь внизу
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }
                        // Если точка идёт налево вверх, и точка c1 находится теперь внизу
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }

                        // Добавляем в массив путей значение.
                        // Если точка идёт наверх 
                        if (((b1i + 1) == bi) && (b1j == bj))
                        {
                            movement.Add(2);
                        }
                        // Если точка идёт наверх направо
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(1);
                        }
                        // Если точка идёт направо
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(0);
                        }
                        // Если точка идёт вниз направо
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(7);
                        }
                        // Если точка идёт вниз
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            movement.Add(6);
                            // Будешь проходить мимо.... проходи мимо
                        }
                        // Если точка идёт вниз налево
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(5);
                        }
                        // Если точка идёт налево, то она шалава
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(4);
                        }
                        // Если точка идёт налево наверх
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(3);
                            // Твоему направлению пришщёл конец!
                        }

                        bi = b1i;
                        bj = b1j;
                        ci = c1i;
                        cj = c1j;
                    } else if (WhyAreUGay == 3)    // Если точка идёт вниз, и точка c1 находится теперь справа
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (array_1_and_0_new[i] == 1)
                            {
                                switch (i)
                                {
                                    case 0:
                                        {
                                            b1i = bi;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 1:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj + 1;
                                            c1i = bi;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 2:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj;
                                            c1i = bi + 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 3:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj;
                                            break;
                                        }
                                    case 4:
                                        {
                                            b1i = bi;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 5:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj - 1;
                                            c1i = bi;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 6:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj;
                                            c1i = bi - 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 7:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj;
                                            break;
                                        }
                                }

                                break;
                            }
                        }

                        //Смотрим куда пошла точка... Спойлер: К твоей мамаше.
                        // Если точка идёт наверх, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && (b1j == bj))   //Смотрим куда пошла точка... Спойлер: К твоей мамаше.  // Если точка идёт наверх, и точка c1 находится теперь слева
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }
                        // Если точка идёт наверх направо, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }

                        // Если точка идёт направо, и точка c1 находится теперь вверху
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }
                        // Если точка идёт направо вниз, и точка c1 находится теперь вверху
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }

                        // Если точка идёт вниз, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }
                        // Если точка идёт вниз налево, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }

                        // Если точка идёт налево, то она шалава и точка c1 находится теперь внизу
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }
                        // Если точка идёт налево вверх, и точка c1 находится теперь внизу
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }

                        // Добавляем в массив путей значение.
                        // Если точка идёт наверх 
                        if (((b1i + 1) == bi) && (b1j == bj))
                        {
                            movement.Add(2);
                        }
                        // Если точка идёт наверх направо
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(1);
                        }
                        // Если точка идёт направо
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(0);
                        }
                        // Если точка идёт вниз направо
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(7);
                        }
                        // Если точка идёт вниз
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            movement.Add(6);
                            // Будешь проходить мимо.... проходи мимо
                        }
                        // Если точка идёт вниз налево
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(5);
                        }
                        // Если точка идёт налево, то она шалава
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(4);
                        }
                        // Если точка идёт налево наверх
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(3);
                            // Твоему направлению пришщёл конец!
                        }

                        bi = b1i;
                        bj = b1j;
                        ci = c1i;
                        cj = c1j;
                    } else if (WhyAreUGay == 4)    // Если точка идёт налево, то она шалава и точка c1 находится теперь внизу
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            if (array_1_and_0_new[i] == 1)
                            {
                                switch (i)
                                {
                                    case 0:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj;
                                            c1i = bi + 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 1:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj;
                                            break;
                                        }
                                    case 2:
                                        {
                                            b1i = bi;
                                            b1j = bj - 1;
                                            c1i = bi + 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 3:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj - 1;
                                            c1i = bi;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 4:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj;
                                            c1i = bi - 1;
                                            c1j = bj - 1;
                                            break;
                                        }
                                    case 5:
                                        {
                                            b1i = bi - 1;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj;
                                            break;
                                        }
                                    case 6:
                                        {
                                            b1i = bi;
                                            b1j = bj + 1;
                                            c1i = bi - 1;
                                            c1j = bj + 1;
                                            break;
                                        }
                                    case 7:
                                        {
                                            b1i = bi + 1;
                                            b1j = bj + 1;
                                            c1i = bi;
                                            c1j = bj + 1;
                                            break;
                                        }
                                }

                                break;
                            }
                        }

                        //Смотрим куда пошла точка... Спойлер: К твоей мамаше.
                        // Если точка идёт наверх, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && (b1j == bj))
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }
                        // Если точка идёт наверх направо, и точка c1 находится теперь слева
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 1;
                            c1i = b1i;
                            c1j = b1j - 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j - 1];  // левая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                        }

                        // Если точка идёт направо, и точка c1 находится теперь вверху
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }
                        // Если точка идёт направо вниз, и точка c1 находится теперь вверху
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            WhyAreUGay = 2;
                            c1i = b1i - 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i - 1, b1j];  // верхняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i - 1, b1j + 1];  // точка в правом верхнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[5] = color_image_matr[b1i + 1, b1j - 1];  // точка слева внизу от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                        }

                        // Если точка идёт вниз, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }
                        // Если точка идёт вниз налево, и точка c1 находится теперь справа
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 3;
                            c1i = b1i;
                            c1j = b1j + 1;
                            array_1_and_0_new[0] = color_image_matr[b1i, b1j + 1];  // правая точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j + 1];  // точка в правом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i + 1, b1j];  // точка внизу от b1
                            array_1_and_0_new[3] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j - 1];  // точка слева вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[7] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                        }

                        // Если точка идёт налево, то она шалава и точка c1 находится теперь внизу
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }
                        // Если точка идёт налево вверх, и точка c1 находится теперь внизу
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            WhyAreUGay = 4;
                            c1i = b1i + 1;
                            c1j = b1j;
                            array_1_and_0_new[0] = color_image_matr[b1i + 1, b1j];  // нижняя точка c1 от b1 
                            array_1_and_0_new[1] = color_image_matr[b1i + 1, b1j - 1];  // точка в левом нижнем углу от b1
                            array_1_and_0_new[2] = color_image_matr[b1i, b1j - 1];  // точка слева от b1
                            array_1_and_0_new[3] = color_image_matr[b1i - 1, b1j - 1];  // точка в левом верхнем углу от b1
                            array_1_and_0_new[4] = color_image_matr[b1i - 1, b1j];  // точка вверху от b1
                            array_1_and_0_new[5] = color_image_matr[b1i - 1, b1j + 1];  // точка справа вверху от b1
                            array_1_and_0_new[6] = color_image_matr[b1i, b1j + 1];  // точка справа от b1
                            array_1_and_0_new[7] = color_image_matr[b1i + 1, b1j + 1];  // точка справа внизу от b1
                        }

                        // Добавляем в массив путей значение.
                        // Если точка идёт наверх 
                        if (((b1i + 1) == bi) && (b1j == bj))
                        {
                            movement.Add(2);
                        }
                        // Если точка идёт наверх направо
                        if (((b1i + 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(1);
                        }
                        // Если точка идёт направо
                        if ((b1i == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(0);
                        }
                        // Если точка идёт вниз направо
                        if (((b1i - 1) == bi) && ((b1j - 1) == bj))
                        {
                            movement.Add(7);
                        }
                        // Если точка идёт вниз
                        if (((b1i - 1) == bi) && (b1j == bj))
                        {
                            movement.Add(6);
                            // Будешь проходить мимо.... проходи мимо
                        }
                        // Если точка идёт вниз налево
                        if (((b1i - 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(5);
                        }
                        // Если точка идёт налево, то она шалава
                        if ((b1i == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(4);
                        }
                        // Если точка идёт налево наверх
                        if (((b1i + 1) == bi) && ((b1j + 1) == bj))
                        {
                            movement.Add(3);
                            // Твоему направлению пришёл конец!
                        }

                        bi = b1i;
                        bj = b1j;
                        ci = c1i;
                        cj = c1j;
                    }

                    int k = 5;

                } while ((bi != b0iRemember) || (bj != b0jRemember));

                int l = 10;
                return movement;
            }

            public Bitmap Detransformation(List<int> NumChain)
            {
                Bitmap bitmap = new Bitmap(50, 50);


                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {
                        bitmap.SetPixel(i, j, Color.White);
                    }
                }

                int x = 2, y = 10;
                foreach (int Num in NumChain)
                {
                    switch (Num)
                    {
                        case 1:
                            x++;
                            y--;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        case 2:
                            y--;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        case 3:
                            x--;
                            y--;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        case 4:
                            x--;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        case 5:
                            x--;
                            y++;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        case 6:
                            y++;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        case 7:
                            x++;
                            y++;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        case 0:
                            x++;
                            bitmap.SetPixel(x, y, Color.Black);
                            break;
                        default:
                            Console.WriteLine("Default case");
                            break;
                    }
                }

                Bitmap bitbox = new Bitmap(500, 500);

                for (int i = 0; i < bitmap.Width; i++)
                {
                    for (int j = 0; j < bitmap.Height; j++)
                    {

                        Color clr = bitmap.GetPixel(i, j);

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

            // Его смерть была лишь необходимой жертвой
            ~chain()
            {

            }
        }

        class binarization
        {
            public Bitmap BitmapToBlackWhite2(Bitmap src)
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
            public Bitmap bigBit(Bitmap src)
            {

                Bitmap bitbox = new Bitmap(500, 500);

                for (int i = 0; i < src.Width; i++)
                {
                    for (int j = 0; j < src.Height; j++)
                    {

                        Color clr = src.GetPixel(i, j);

                        for (int k = i * 20; k < (i + 1) * 20; k++)
                        {
                            for (int m = j * 20; m < (j + 1) * 20; m++)
                            {
                                bitbox.SetPixel(k, m, clr);
                            }
                        }
                    }
                }
                return bitbox;
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

        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            richTextBox1.Text = null;
            pictureBox1.Image = null;

            chain obj_chain = new chain();
            binarization binar = new binarization();

            pictureBox1.Image = (Image)binar.bigBit(BitmapPicture1);

            bitNew = binar.BitmapToBlackWhite2(BitmapBinarization);
            List<int> List_chain = obj_chain.transformation(bitNew);

            for (int i = 0; i < List_chain.Count; i++)
            {
                richTextBox1.Text += List_chain[i].ToString(); // + Environment.NewLine;  + "\r\n"
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox3.Image = null;
            richTextBox1.Text = null;

            chain obj_chain = new chain();
            binarization binar = new binarization();

            bitNew = binar.BitmapToBlackWhite2(BitmapBinarization);
            List<int> List_chain = obj_chain.transformation(bitNew);

            for (int i = 0; i < List_chain.Count; i++)
            {
                richTextBox1.Text += List_chain[i].ToString(); // + Environment.NewLine;  + "\r\n"
            }
            pictureBox3.Image = obj_chain.Detransformation(List_chain);
        }
    }
}