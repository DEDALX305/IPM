namespace WindowsFormsApp2
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("");
            this.button1 = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.label7 = new System.Windows.Forms.Label();
            this.listView2 = new System.Windows.Forms.ListView();
            this.listView3 = new System.Windows.Forms.ListView();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(168, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 47);
            this.button1.TabIndex = 0;
            this.button1.Text = "Дискретное преобразование фурье";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(12, 120);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(300, 300);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 104);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(147, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Исследуемое изображение";
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(692, 120);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(292, 62);
            this.pictureBox2.TabIndex = 3;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(740, 104);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Выбранный фрагмент (До обработки)";
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.Location = new System.Drawing.Point(1042, 120);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(293, 62);
            this.pictureBox3.TabIndex = 5;
            this.pictureBox3.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1090, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(216, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Выбранный фрагмент (После обработки)";
            // 
            // pictureBox4
            // 
            this.pictureBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox4.Location = new System.Drawing.Point(692, 204);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(292, 150);
            this.pictureBox4.TabIndex = 7;
            this.pictureBox4.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(801, 188);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(75, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Гистограмма";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox5.Location = new System.Drawing.Point(1042, 204);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(293, 150);
            this.pictureBox5.TabIndex = 9;
            this.pictureBox5.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1149, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Гистограмма";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(144, 47);
            this.button2.TabIndex = 11;
            this.button2.Text = "Загрузить изображение";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // pictureBox6
            // 
            this.pictureBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox6.Location = new System.Drawing.Point(12, 438);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(677, 242);
            this.pictureBox6.TabIndex = 12;
            this.pictureBox6.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(273, 422);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "Гистограмма (Оригинал)";
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(358, 120);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(255, 299);
            this.listView1.TabIndex = 15;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(409, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(161, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Характеристики изображения";
            // 
            // listView2
            // 
            this.listView2.Location = new System.Drawing.Point(692, 372);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(292, 133);
            this.listView2.TabIndex = 17;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.List;
            // 
            // listView3
            // 
            this.listView3.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem2});
            this.listView3.Location = new System.Drawing.Point(1042, 372);
            this.listView3.Name = "listView3";
            this.listView3.Size = new System.Drawing.Size(293, 133);
            this.listView3.TabIndex = 18;
            this.listView3.UseCompatibleStateImageBehavior = false;
            this.listView3.View = System.Windows.Forms.View.List;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(759, 357);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(161, 13);
            this.label8.TabIndex = 19;
            this.label8.Text = "Характеристики изображения";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1110, 357);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(161, 13);
            this.label9.TabIndex = 20;
            this.label9.Text = "Характеристики изображения";
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1360, 692);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.listView3);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ListView listView3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}