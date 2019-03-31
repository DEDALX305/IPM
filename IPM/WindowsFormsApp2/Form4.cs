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

            pictureBox1.Image = null;
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
    }
    }

