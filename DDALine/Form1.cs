using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DDALine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void LineDDA(int x1, int y1, int x2, int y2)
        {
            Graphics g = pictureBox1.CreateGraphics();
            int w = pictureBox1.ClientSize.Width;
            int h = pictureBox1.ClientSize.Height;

            // DDA algorithm
            int dx = x2 - x1;
            int dy = y2 - y1;
            int steps = Math.Max(Math.Abs(dx), Math.Abs(dy)); // return max عشان اعرف مين اللي هيتعمل عليه المعادله ف الجدول 
            //الفرق بين النقطه الاولي الي النقطه الاخيره معتمد علي اكس 

            float xInc = (float)Math.Abs(dx) / steps;
            float yInc = (float)Math.Abs(dy) / steps;
            float x = x1, y = y1;

            dataGridView1.Rows.Clear();

            // Draw the line
            for (int k = 0; k < steps; k++)
            {
                dataGridView1.Rows.Add(x, y);
                g.FillRectangle(Brushes.Blue, (x + w / 2), (float)Math.Abs(-y + h / 2), 2, 2);
                x += xInc;
                y += (yInc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text);
            int y1 = Convert.ToInt32(textBox2.Text);
            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);

            LineDDA(x1, y1, x2, y2);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Pen pen = new Pen(Brushes.Black, 1);
            Int32 x = 1;
            Int32 y = 1;
            Point p1 = new Point(0, 0);
            Point p2 = new Point(0, 0);
            Point p3 = new Point(0, 0);
            Point p4 = new Point(0, 0);

            using (Graphics g = pictureBox1.CreateGraphics())
            {
                for (Int32 n = 0; n < 300; n++)
                {
                    p1 = new Point(x, (pictureBox1.Height / 2));
                    p2 = new Point(x + 5, (pictureBox1.Height / 2));
                    p3 = new Point((pictureBox1.Width / 2), y);
                    p4 = new Point((pictureBox1.Width / 2), y + 5);

                    g.DrawLine(pen, p1, p2);
                    g.DrawLine(pen, p3, p4);
                    x = x + 5;
                    y = y + 5;
                }
            }
        }
    }
}


