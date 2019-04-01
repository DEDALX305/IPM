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
                Rectangle rect = new Rectangle(0, 0, TempBitmap.Width, TempBitmap.Height);
                System.Drawing.Imaging.BitmapData bmpData = TempBitmap.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, TempBitmap.PixelFormat);
                IntPtr ptr = bmpData.Scan0;

                int bytes = bmpData.Stride * TempBitmap.Height;
                int Width = bmpData.Stride;
                byte[] grayValues = new byte[bytes];
                byte[] NewgrayValues1 = new byte[bytes];
                byte[] NewgrayValues2 = new byte[bytes];
                int end;

                System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

                //for (int i = 1; i < Width; i++)
                //{
                //    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width] + grayValues[i + Width + 1] + grayValues[i + Width - 1]) / 6);
                //}

                int elem = 0;
                int m1 = 0;
                for (int i = Width + 1; i < grayValues.Length - Width -1; i++)
                {

                    if ((grayValues[i] + grayValues[i - Width - 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width - 1] + grayValues[i + Width] + grayValues[i + Width + 1]) >= 255)
                    {
                        int testc = (grayValues[i] + grayValues[i - Width - 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i + Width - 1] + grayValues[i + Width] + grayValues[i + Width + 1]);
                        int fa = 0, fb = 0, fc = 0, fd = 0;
                        if ((testc < 510) || (testc > 1530)) fa = 1;
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

                        for (int j = 0; j < 7; j++)
                            if ((values[j] == 255) && (values[j + 1] == 0)) Tp++;
                        if (Tp == 1) fb = 1;

                        if ((grayValues[i - Width] + grayValues[i + 1] + grayValues[i + Width]) >= 255) fc = 1;

                        if ((grayValues[i + 1] + grayValues[i + Width] + grayValues[i - 1]) >= 255) fd = 1;

                        for (int k = elem; k < i; k++)
                            NewgrayValues1[k] = grayValues[k];

                        if ((fa + fb + fc + fd) == 4)
                        {
                            if (grayValues[i] != 0)
                            {
                                m1++;
                            }
                            NewgrayValues1[i] = 255;
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

                    if ((NewgrayValues1[i] + NewgrayValues1[i - Width - 1] + NewgrayValues1[i - Width] + NewgrayValues1[i - Width + 1] + NewgrayValues1[i - 1] + NewgrayValues1[i + 1] + NewgrayValues1[i + Width - 1] + NewgrayValues1[i + Width] + NewgrayValues1[i + Width + 1]) >= 255)
                    {
                        int testc = (NewgrayValues1[i] + NewgrayValues1[i - Width - 1] + NewgrayValues1[i - Width] + NewgrayValues1[i - Width + 1] + NewgrayValues1[i - 1] + NewgrayValues1[i + 1] + NewgrayValues1[i + Width - 1] + NewgrayValues1[i + Width] + NewgrayValues1[i + Width + 1]);
                        int fa = 0, fb = 0, fc = 0, fd = 0;
                        if ((testc < 510) || (testc > 1530)) fa = 1;
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

                        for (int j = 0; j < 7; j++)
                            if ((values[j] == 255) && (values[j + 1] == 0)) Tp++;
                        if (Tp == 1) fb = 1;

                        if ((NewgrayValues1[i - Width] + NewgrayValues1[i + 1] + NewgrayValues1[i - 1]) >= 255) fc = 1;

                        if ((NewgrayValues1[i - Width] + NewgrayValues1[i + Width] + NewgrayValues1[i - 1]) >= 0) fd = 1;

                        for (int k = elem; k < i; k++)
                            NewgrayValues2[k] = grayValues[k];

                        if ((fa + fb + fc + fd) == 4)
                        {
                            if (NewgrayValues1[i] != 0)
                            {
                                m2++;
                            }
                            NewgrayValues2[i] = 255;
                        }
                        else
                        {
                            NewgrayValues2[i] = grayValues[i];
                        }
                        elem = i + 1;

                    }

                }

                //for (int i = grayValues.Length - Width + 1; i < grayValues.Length - 1; i++)
                //{
                //    NewgrayValues[i] = (byte)((grayValues[i] + grayValues[i - 1] + grayValues[i + 1] + grayValues[i - Width] + grayValues[i - Width + 1] + grayValues[i - Width - 1]) / 6);
                //}

                System.Runtime.InteropServices.Marshal.Copy(NewgrayValues2, 0, ptr, bytes);

                TempBitmap.UnlockBits(bmpData);

                return TempBitmap;

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

        private void button1_Click(object sender, EventArgs e)
        { // Бинаризация

           
            pictureBox2.Image = null;

            binarization binar = new binarization();

            pictureBox2.Image = (Image)binar.transformation(BitmapBinarization);

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

            

             pictureBox3.Image = (Image)test.transformation(BitmapBinarization);
        }
    }
    }

