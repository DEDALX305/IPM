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
    public partial class Form1 : Form //MetroFramework.Forms.MetroForm
    {

        public static Bitmap BitmapPicture1;
        public static Bitmap BitmapBinarization;
        public static Bitmap BitmapEcvalize;
        public static Bitmap BitmapCast;
        public static Bitmap BitmapSmoothing;

        public Form1()
        {
            InitializeComponent();
        }

        

        class equalization
        {

            private Bitmap transformation(Bitmap TempBitap)
            {
                Rectangle rect = new Rectangle(0, 0, TempBitap.Width, TempBitap.Height); // прямоугольник

                System.Drawing.Imaging.BitmapData // Создается обьект класса BitmapData  - Задает атрибуты точечного рисунка
                                                 bmpData = TempBitap.LockBits(rect, // Структура Rectangle, которая задает часть объекта Bitmap для блокировки.
                                                                                       System.Drawing.Imaging.ImageLockMode.ReadWrite, // Задает уровень доступа - чтение
                                                                                       TempBitap.PixelFormat); //Указывает формат данных о цвете для каждого пикселя изображения.
                IntPtr ptr = bmpData.Scan0; // Возвращает или задает адрес данных первого пикселя в точечном рисунке. Также под этим можно понимать первую строку развертки в точечном рисунке.
                int bytes // 
                         = bmpData.Stride //Возвращает или задает ширину шага по индексу (также называемую шириной развертки)  количество байт в одной строке.
                                          * TempBitap.Height;// высота 
                byte[] grayValues = new byte[bytes]; // количество байт в одной строке * высота изображения // массив нулей 

                int[] R = new int[256]; // массив на 256 целсых чисел


                System.Runtime.InteropServices.Marshal.// разметка массива grayValues - количество цветов в одном пикселе ? 
                    Copy(ptr, // Указатель памяти, из которого выполняется копирование.
                                                                grayValues, // Массив для копирования данных.
                                                                0, //Отсчитываемый от нуля индекс в массиве назначения, с которого начинается копирование.
                                                                bytes // Число копируемых элементов массива.
                                                                ); // вроде динамической памяти

                for (int i = 0; i < grayValues.Length; i++)
                {
                    ++R[grayValues[i]]; // На первом шаге i = 0, R = 1, grayValues = 255
                    // в масив R записывается информация по количеству цветов  типо grayValues[0] =  255 то элемент массива R с индексом 255 увелчится на 1
                }
                byte z = 0; // ?
                int Hint = 0; // оттенок
                byte[] N = new byte[256];// массив на 256 целсых чисел
                byte[] left = new byte[256];// массив на 256 целсых чисел
                byte[] right = new byte[256];// массив на 256 целсых чисел
                int Havg = grayValues.Length / R.Length; // ? 268800/256=1050
                for (int i = 0; i < N.Length - 1; i++) // почему   N.Length ?
                {
                    N[i] = 0; // разметка массива N 0  ?? нафига 
                }
                for (int j = 0; j < R.Length; j++) // почему   R.Length ?
                {
                    left[j] = z;
                    Hint = Hint + R[j];
                    while (Hint > Havg)
                    {
                        Hint = Hint - Havg;
                        z++;
                    }
                    right[j] = z;
                    N[j] = (byte)((left[j] + right[j]) / 2);
                }
                for (int i = 0; i < grayValues.Length; i++)
                {
                    if (left[grayValues[i]] == right[grayValues[i]]) grayValues[i] = (byte)left[grayValues[i]];
                    else grayValues[i] = (byte)N[grayValues[i]];
                }

                System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);

                TempBitap.UnlockBits(bmpData); // разблокировка 


                return TempBitap;
            }

            public Bitmap equaliz(Bitmap TempBitap)
            {
                Bitmap _TempBitap = transformation(TempBitap);
                return _TempBitap;
            }

            ~equalization()
            {

            }
        }

        class binarization
        {
            public Bitmap transformation(Bitmap BitBin)
            {
                System.Drawing.Imaging.BitmapData data = BitBin.LockBits(new Rectangle(0, 0, BitBin.Width, BitBin.Height), System.Drawing.Imaging.ImageLockMode.WriteOnly, BitBin.PixelFormat);

                Byte[] bytes = new byte[data.Height * data.Stride];

                System.Runtime.InteropServices.Marshal.Copy(data.Scan0, bytes, 0, bytes.Length);

                for (int i = 0; i < data.Height * data.Stride; i++)
                {
                    if (bytes[i] >= 135)
                        bytes[i] = 255;
                    else
                        bytes[i] = 0;
                }

                System.Runtime.InteropServices.Marshal.Copy(bytes, 0, data.Scan0, bytes.Length);
                BitBin.UnlockBits(data);

                return BitBin;
            }

            ~binarization()
            {

            }
        }

        class cast
        {
            public Bitmap transformation(Bitmap source, Bitmap target)
            {

                //source

                Rectangle RectSource1 = new Rectangle(0, 0, source.Width, source.Height);
                System.Drawing.Imaging.BitmapData bmpData = source.LockBits(RectSource1, System.Drawing.Imaging.ImageLockMode.ReadWrite, source.PixelFormat);
                IntPtr Ptr1 = bmpData.Scan0;

                int bytes1 = bmpData.Stride * source.Height;
                byte[] grayValues1 = new byte[bytes1];
                int[] barChart1 = new int[256];

                System.Runtime.InteropServices.Marshal.Copy(Ptr1, grayValues1, 0, bytes1);

                for (int i = 0; i < grayValues1.Length; i++)
                {
                    ++barChart1[grayValues1[i]];
                }

                //Target

                Rectangle RectSource2 = new Rectangle(0, 0, target.Width, target.Height);
                System.Drawing.Imaging.BitmapData bmpData2 = target.LockBits(RectSource2, System.Drawing.Imaging.ImageLockMode.ReadWrite, target.PixelFormat);
                IntPtr Ptr2 = bmpData.Scan0;

                int bytes2 = bmpData.Stride * target.Height;
                byte[] grayValues2 = new byte[bytes2];
                int[] barChart2 = new int[256];

                System.Runtime.InteropServices.Marshal.Copy(Ptr2, grayValues2, 0, bytes2);

                for (int i = 0; i < grayValues2.Length; i++)
                {
                    ++barChart2[grayValues2[i]];
                }

                //Buffer

                byte[] BufferT = new byte[256];

                //от 0 до 255 и по второй кривой берешь ее значения вот это значение для 3 кривой
                //будет ее индексом  а индекс второй будет значением 3 

                for (int j = 0; j < barChart2.Length; j++)
                {
                    BufferT[grayValues2[j]] = (byte)j;
                }

                for (int i = 0; i < grayValues1.Length; i++)
                {
                    grayValues1[i] = BufferT[grayValues2[i]];
                }

                System.Runtime.InteropServices.Marshal.Copy(grayValues1, 0, Ptr1, bytes1);

                Bitmap transBitmap = source;

                source.UnlockBits(bmpData);
                target.UnlockBits(bmpData2);

                return transBitmap;

            }

            ~cast()
            {

            }

        }

        class smoothing
        {
            public Bitmap transformation(Bitmap TempBitmap)
            {
                Rectangle rect = new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = TempBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, TempBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;

                int bytes = bmpData.Stride * TempBitmap.Height;
                int Width = bmpData.Stride;
                byte[] grayValues = new byte[bytes];  
                byte[] NewgrayValues = new byte[bytes];
                int end;

                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                for (int i = 1; i < Width; i++)
                {
                    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width] + grayValues[i + Width + 1] + grayValues[i + Width - 1]) / 6);
                }

                for (int i = Width + 1; i < grayValues.Length - Width; i++)
                {
                    end = i - Width + 1;
                    if (end % Width == 0)
                    {
                        NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + Width] + grayValues[i + Width - 1] + grayValues[i - Width] + grayValues[i - Width - 1]) / 6);
                        continue;
                    }
                    else if (end - 1 % Width == 0)
                    {
                        NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i + 1] + grayValues[i + Width] + grayValues[i + Width + 1] + grayValues[i - Width] + grayValues[i - Width + 1]) / 6);
                        continue;
                    }
                    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width] + grayValues[i + Width + 1] + grayValues[i + Width - 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - Width - 1]) / 9);
                }

                for (int i = grayValues.Length - Width + 1; i < grayValues.Length - 1; i++)
                {
                    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - Width - 1]) / 6);
                }

                System.Runtime.InteropServices.Marshal.Copy(NewgrayValues, 0, ptr, bytes);

                TempBitmap.UnlockBits(bmpData);

                return TempBitmap;

            }

            ~smoothing()
            {

            }
        }

       

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        private void button1_Click(object sender, EventArgs e)
        {
            // Бинаризация

            pictureBox2.Image = null;

            pictureBox4.Image = null;

            binarization binar = new binarization();

            pictureBox2.Image = (Image)binar.transformation(BitmapBinarization);

            gistogram g = new gistogram();

            pictureBox4.Image = (Image)g.gistogramM(binar.transformation(BitmapBinarization));

            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

        } // Бинаризация

        private void button2_Click(object sender, EventArgs e)
        {
            // Сглаживание

            Bitmap _smoothing2picture = null;

            pictureBox2.Image = null;
            
            pictureBox4.Image = null;

            
            smoothing _smoothing = new smoothing();
            gistogram _gistogram = new gistogram();

            _smoothing2picture = _smoothing.transformation(BitmapSmoothing);

            pictureBox2.Image = (Image)_smoothing2picture;
            
            pictureBox4.Image = (Image)_gistogram.gistogramM(_smoothing2picture);

            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        } // Сглаживание

        private void button3_Click(object sender, EventArgs e)
        {
            // Загурзить изображение

            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;

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
                    BitmapEcvalize = new Bitmap(openFileDialog1.FileName);
                    BitmapBinarization = new Bitmap(openFileDialog1.FileName);
                    BitmapSmoothing = new Bitmap(openFileDialog1.FileName);
                    //Bitmapfurie = new Bitmap(openFileDialog1.FileName);
                }
            }


            pictureBox1.Image = (Image)BitmapPicture1;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            gistogram _gistogram = new gistogram();


            pictureBox3.Image = (Image)_gistogram.gistogramM(BitmapPicture1);
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;

        } // Загурзить изображение

        private void button4_Click(object sender, EventArgs e)
        {
            // Приведение
           
            pictureBox2.Image = null;
            
            pictureBox4.Image = null;
            
            cast _cast = new cast();
            gistogram _gistogram = new gistogram();
            
            using (OpenFileDialog openFileDialog1 = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Bitmap Image|*.bmp|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.Title = "Open Image File";
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    BitmapCast = new Bitmap(openFileDialog1.FileName);

                }
            }


            pictureBox2.Image = (Image)_cast.transformation(BitmapPicture1, BitmapCast);
            
            pictureBox4.Image = (Image)_gistogram.gistogramM(_cast.transformation(BitmapPicture1, BitmapCast));
       
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            
            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        } // Приведение

        private void button5_Click(object sender, EventArgs e)
        {
            //Эквализация

            Bitmap _equalizationBitmap;

            pictureBox2.Image = null;

            pictureBox4.Image = null;

            equalization _equalization = new equalization();
            gistogram _gistogram = new gistogram();

            _equalizationBitmap = _equalization.equaliz(BitmapEcvalize);

            Bitmap gistogram2 = _gistogram.gistogramM(_equalizationBitmap);


            pictureBox2.Image = (Image)_equalizationBitmap;

            pictureBox4.Image = (Image)gistogram2;

            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

            pictureBox4.SizeMode = PictureBoxSizeMode.Zoom;
        } // Эквализация

        private void button6_Click(object sender, EventArgs e)
        {
            // Фурье - открытие новой формы

            Form2 f2 = new Form2();
            f2.ShowDialog();

        } // Фурье
    }
}
