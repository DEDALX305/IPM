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

        class smoothing
        { // 
            public Bitmap transformation(Bitmap TempBitmap)
            {
                //Rectangle rect = new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height);
                //System.Drawing.Imaging.BitmapData bmpData = TempBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, TempBitmap.PixelFormat);
                //IntPtr ptr = bmpData.Scan0;

                Bitmap newBit1 = new Bitmap(TempBitmap.Width, TempBitmap.Height);
                Bitmap newBit2 = new Bitmap(TempBitmap.Width, TempBitmap.Height);
                int bytes = TempBitmap.Width*TempBitmap.Height;
                byte[] grayValues = new byte[bytes];
                byte[] NewgrayValues1 = new byte[bytes];
                //byte[] NewgrayValues2 = new byte[bytes];
                //int end;

                Color pixel;

                int c0 = 0;
                int c1 = 0;
                int c2 = 0;
                for ( int i = 0; i < TempBitmap.Height; i++)
                {
                    for ( int j = 0; j < TempBitmap.Width; j++)
                    {
                        if (((TempBitmap.GetPixel(j, i).R + TempBitmap.GetPixel(j, i).G + TempBitmap.GetPixel(j, i).B) / 3) == 0)
                        {
                            grayValues[i * (TempBitmap.Width - 1) + j] = 1;
                            c1++;
                        } else if (((TempBitmap.GetPixel(j, i).R + TempBitmap.GetPixel(j, i).G + TempBitmap.GetPixel(j, i).B) / 3) == 255)
                        {
                            grayValues[i * (TempBitmap.Width - 1) + j] = 0;
                            c2++;
                        }

                        //if (pixel.R == 0) c0++;
                        //if (pixel.R == 1) c1++;
                        //if (pixel.R == 2) c2++;
                    }
                }

                int Width = TempBitmap.Width;
                int Height = TempBitmap.Height;

                int elem = 0;
                int m1 = 0;
                int m2 = 0;
                int m3 = 0;
                int Cycle = 0;

                for ( int i = Width + 1; i < grayValues.Length - Width - 1; i++)
                {

                    if ((grayValues[i] + grayValues[i - Width - 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width - 1] + grayValues[i + Width] + grayValues[i + Width + 1]) < 8)
                    {
                        Cycle++;
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
                        {
                            if (grayValues[k] == 1)
                            {
                                newBit1.SetPixel((k / Height), (k % Height), Color.Black);
                                m1++;
                            }
                            if (grayValues[k] == 0)
                            {
                                newBit1.SetPixel((k / Height), (k % Height), Color.White);
                                m2++;
                            }

                            m3++;
                        }

                        if ((fa + fb + fc + fd) == 4)
                        {
                            newBit1.SetPixel((i / Height), (i % Height), Color.White);
                        }
                        else
                        {
                            newBit1.SetPixel((i / Height), (i % Height), Color.Black);
                        }
                        elem = i + 1;

                    }
       
                }

                for (int k = elem; k < grayValues.Length; k++)
                {
                    if (grayValues[k] == 1)
                    {
                        newBit1.SetPixel((k / Height), (k % Height), Color.Black);
                    }
                    if (grayValues[k] == 0)
                    {
                        newBit1.SetPixel((k / Height), (k % Height), Color.White);
                    }
                }

                for (int i = 0; i < newBit1.Height; i++)
                {
                    for (int j = 0; j < newBit1.Width; j++)
                    {
                        if (((newBit1.GetPixel(j, i).R + newBit1.GetPixel(j, i).G + newBit1.GetPixel(j, i).B) / 3) == 0)
                        {
                            NewgrayValues1[i * (newBit1.Width - 1) + j] = 1;
                        }
                        else if (((newBit1.GetPixel(j, i).R + newBit1.GetPixel(j, i).G + newBit1.GetPixel(j, i).B) / 3) == 255)
                        {
                            NewgrayValues1[i * (newBit1.Width - 1) + j] = 0;
                        }
                    }
                }

                Width = newBit1.Width;
                Height = newBit1.Height;

                int elem1 = 0;
                m1 = 0;
                m2 = 0;
                m3 = 0;

                for (int i = Width + 1; i < grayValues.Length - Width - 1; i++)
                {

                    if ((NewgrayValues1[i] + NewgrayValues1[i - Width - 1] + NewgrayValues1[i - Width] + NewgrayValues1[i - Width + 1] + NewgrayValues1[i - 1] + NewgrayValues1[i + 1] + NewgrayValues1[i + Width - 1] + NewgrayValues1[i + Width] + NewgrayValues1[i + Width + 1]) < 8)
                    {
                        int testc = (NewgrayValues1[i] + NewgrayValues1[i - Width - 1] + NewgrayValues1[i - Width] + NewgrayValues1[i - Width + 1] + NewgrayValues1[i - 1] + NewgrayValues1[i + 1] + NewgrayValues1[i + Width - 1] + NewgrayValues1[i + Width] + NewgrayValues1[i + Width + 1]);
                        int fa = 0, fb = 0, fc = 0, fd = 0;
                        if ((testc >= 2) && (testc <= 6)) fa = 1;
                        int Tp = 0;

                        byte[] values = new byte[8];
                        values[0] = NewgrayValues1[i - Width];      // P2 
                        values[1] = NewgrayValues1[i - Width + 1];  // P3
                        values[2] = NewgrayValues1[i + 1];          // P4
                        values[3] = NewgrayValues1[i + Width + 1];  // P5
                        values[4] = NewgrayValues1[i + Width];      // P6
                        values[5] = NewgrayValues1[i + Width - 1];  // P7
                        values[6] = NewgrayValues1[i - 1];          // P8
                        values[7] = NewgrayValues1[i - Width - 1];  // P9

                        for (int j = 0; j < 7; j++)
                            if ((values[j] == 0) && (values[j + 1] == 1)) Tp++;
                        if ((values[7] == 0) && (values[0] == 1)) Tp++;
                        if (Tp == 1) fb = 1;

                        if ((NewgrayValues1[i - Width] * NewgrayValues1[i + 1] * NewgrayValues1[i + Width]) == 0) fc = 1;

                        if ((NewgrayValues1[i + 1] * NewgrayValues1[i + Width] * NewgrayValues1[i - 1]) == 0) fd = 1;

                        for (int k = elem1; k < i; k++)
                        {
                            if (NewgrayValues1[k] == 1)
                            {
                                newBit2.SetPixel((k / Height), (k % Height), Color.Black);
                                m1++;
                            }
                            if (NewgrayValues1[k] == 0)
                            {
                                newBit2.SetPixel((k / Height), (k % Height), Color.White);
                                m2++;
                            }
                            m3++;
                        }

                        if ((fa + fb + fc + fd) == 4)
                        {
                            newBit2.SetPixel((i / Height), (i % Height), Color.White);
                        }
                        else
                        {
                            newBit2.SetPixel((i / Height), (i % Height), Color.Black);
                        }
                        elem1 = i + 1;

                    }

                    int oprtyerg = 0;
                }

                return newBit2;
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

            ~smoothing()
            {

            }
        } // Сглаживание

        class binarization
        { // Бинаризация
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
        } // Бинаризация

        class binarization2
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

            binarization2 binar = new binarization2();

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
            smoothing test = new smoothing();

            binarization2 binar = new binarization2();
            bitNew = binar.BitmapToBlackWhite2(BitmapBinarization);

            pictureBox3.Image = (Image)test.transformation(bitNew);
        }
    }
    }

