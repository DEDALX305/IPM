using System;
using System.Drawing;


namespace WindowsFormsApp2
{
    class gistogram
    {

        public Bitmap GistogramNew(Bitmap image)
        {

            Bitmap barChart = null;

            Rectangle rect = new Rectangle(0, 0, image.Width, image.Height);
            System.Drawing.Imaging.BitmapData imageData = image.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, image.PixelFormat);

            int width = 256, height = 400;

            IntPtr ptr = imageData.Scan0;
            int bytes = imageData.Stride * image.Height;// общее количество пикселей в изображении
            byte[] grayValues = new byte[bytes];   // значение яркости в каждом пикселе
            int[] R = new int[256]; //
            byte[] N = new byte[256];
            byte[] left = new byte[256];
            byte[] right = new byte[256];
            System.Runtime.InteropServices.Marshal.Copy(ptr, grayValues, 0, bytes);

            int i, j;
            int[] grayValuesNew = new int[256];

            for (i = 0; i < 256; i++)
            {
                grayValuesNew[i] = 0;
            }

            for (i = 0; i < grayValues.Length; i++)
            {
                grayValuesNew[grayValues[i]] = grayValuesNew[grayValues[i]] + 1;
            }

            barChart = new Bitmap(width, height);

            int max = 0;

            for (i = 0; i < 256; i++)
            {
                if (grayValuesNew[i] > max)
                    max = grayValuesNew[i];
            }

            double point = (double)max / height;

            int last = 1;

            for (i = 1; i < width - 1; i++)
            {
                if (grayValuesNew[i] != 0)
                {
                    for (j = height - 1; j > height - grayValuesNew[i] / point; j--)
                    {
                        barChart.SetPixel(last, j, Color.Black);
                    }

                    last += 1;
                }
            }

            System.Runtime.InteropServices.Marshal.Copy(grayValues, 0, ptr, bytes);
            image.UnlockBits(imageData);

            return barChart;
        }


        public Bitmap gistogramM(Bitmap barChart)
        {

            // определяем размеры гистограммы. В идеале, ширина должны быть кратна 768 - 
            // по пикселю на каждый столбик каждого из каналов
            int width = 768, height = 300;
            // получаем битмап из изображения
            Bitmap bmp = new Bitmap(barChart);
            // создаем саму гистограмму
            barChart = new Bitmap(width, height);
            // создаем массивы, в котором будут содержаться количества повторений для каждого из значений каналов.
            // индекс соответствует значению канала
            int[] R = new int[256];
            int[] G = new int[256];
            int[] B = new int[256];
            int i, j;
            Color color;
            // собираем статистику для изображения
            for (i = 0; i < bmp.Width; ++i)
                for (j = 0; j < bmp.Height; ++j)
                {
                    color = bmp.GetPixel(i, j);
                    ++R[color.R];
                    ++G[color.G];
                    ++B[color.B];
                }
            // находим самый высокий столбец, чтобы корректно масштабировать гистограмму по высоте
            int max = 0;
            for (i = 0; i < 256; ++i)
            {
                if (R[i] > max)
                    max = R[i];
                if (G[i] > max)
                    max = G[i];
                if (B[i] > max)
                    max = B[i];
            }
            // определяем коэффициент масштабирования по высоте
            double point = ((double)max / height) + 1;
            // отрисовываем столбец за столбцом нашу гистограмму с учетом масштаба
            for (i = 0; i < width - 3; ++i)
            {
                for (j = height - 1; j > height - R[i / 3] / point; --j)
                {
                    barChart.SetPixel(i, j, Color.Red);
                }
                ++i;
                for (j = height - 1; j > height - G[i / 3] / point; --j)
                {
                    barChart.SetPixel(i, j, Color.Green);
                }
                ++i;
                for (j = height - 1; j > height - B[i / 3] / point; --j)
                {
                    barChart.SetPixel(i, j, Color.Blue);
                }
            }
            

            return barChart;

        }
        ~gistogram()
        {

        }

    }
}
