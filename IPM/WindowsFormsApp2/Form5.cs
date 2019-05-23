using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IMPSpace
{
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
        }

        public static Bitmap BitmapPicture1;
        public static Bitmap BitmapBinarization;
        public static Bitmap bitNew;

        private void button1_Click(object sender, EventArgs e)
        {// Загурзить изображение
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

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
