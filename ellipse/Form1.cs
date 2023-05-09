using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ellipse
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Ellipsealgo(Point center, double rx, double ry)

        {
            int xc;
            int yc;
            xc = center.X;
            yc = center.Y;
            var brush = Brushes.Black;
            var g = panel1.CreateGraphics();
            float dx, dy, P1, P2, x, y;
            x = 0;
            y = (float)ry;

            P1 = ((float)((ry * ry) - (rx * rx * ry) + (0.25 * rx * rx)));

            dx = (float)(2 * ry * ry * x);
            dy = (float)(2 * rx * rx * y);
            dataGridView1.Rows.Clear();
            while (dx < dy)
            {
                dataGridView1.Rows.Add(x, y, dx, dy);

                g.FillRectangle(Brushes.Black, (center.X + x), (center.Y + y), 2, 2);
                g.FillRectangle(Brushes.Black, (center.X - x), (center.Y + y), 2, 2);
                g.FillRectangle(Brushes.Black, (center.X + x), (center.Y - y), 2, 2);
                g.FillRectangle(Brushes.Black, (center.X - x), (center.Y - y), 2, 2);

                if (P1 < 0)
                {
                    x++;
                    dx = (float)(dx + (2 * ry * ry)); // بحسب الدي اكس اللي عليها الدور
                    P1 = (float)(P1 + dx + (ry * ry));
                }
                else
                {
                    x++;
                    y--;
                    dx = (float)(dx + (2 * ry * ry));// نفس الكومنت
                    dy = (float)(dy - (2 * rx * rx)); //  نفس الكومنت
                    P1 = (float)(P1 + dx - dy + (ry * ry));
                }
            }

            P2 = (float)((ry * ry) * ((x + 0.5) * (x + 0.5)) + (rx * rx) * ((y - 1) * (y - 1)) - (rx * rx * ry * ry));

            while (y >= 0)
            {
                g.FillRectangle(Brushes.Black, (center.X + x), (center.Y + y), 2, 2);
                g.FillRectangle(Brushes.Black, (center.X - x), (center.Y + y), 2, 2);
                g.FillRectangle(Brushes.Black, (center.X + x), (center.Y - y), 2, 2);
                g.FillRectangle(Brushes.Black, (center.X - x), (center.Y - y), 2, 2);


                if (P2 > 0)
                {
                    y--;

                    dy = (float)(dy - (2 * rx * rx));
                    P2 = (float)(P2 + (rx * rx) - dy);

                }
                else
                {
                    y--;
                    x++;

                    dx = (float)(dx + (2 * ry * ry));
                    dy = (float)(dy - (2 * rx * rx));
                    P2 = (float)(P2 + dx - dy + (rx * rx));
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int xc = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int yc = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            double rx = Convert.ToDouble(textBox3.Text);
            double ry = Convert.ToDouble(textBox4.Text);

            Point center = new Point(xc, yc);
            Ellipsealgo(center, rx, ry);

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

            using (Graphics g = panel1.CreateGraphics())
            {
                for (Int32 n = 0; n < 300; n++)
                {
                    p1 = new Point(x, (panel1.Height / 2));
                    p2 = new Point(x + 5, (panel1.Height / 2));
                    p3 = new Point((panel1.Width / 2), y);
                    p4 = new Point((panel1.Width / 2), y + 5);

                    g.DrawLine(pen, p1, p2);
                    g.DrawLine(pen, p3, p4);
                    x = x + 5;
                    y = y + 5;
                }
            }
        }
    }
}
