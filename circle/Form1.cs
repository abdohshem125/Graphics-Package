using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace circle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void circlealgo(Point center, double r)
        {
            var brush = Brushes.Black;
            var g = panel1.CreateGraphics();

            double x = 0; double y = r; double P = 1 - r;
            dataGridView1.Rows.Clear();
            while (x <= y)
            {
                dataGridView1.Rows.Add(x, y , 2 * x , 2 * y);

                g.FillRectangle(Brushes.Black, (int)(center.X + x), (int)(center.Y + y), 1, 1);
                g.FillRectangle(Brushes.Black, (int)(center.X - x), (int)(center.Y + y), 1, 1);
                g.FillRectangle(Brushes.Black, (int)(center.X + x), (int)(center.Y - y), 1, 1);
                g.FillRectangle(Brushes.Black, (int)(center.X - x), (int)(center.Y - y), 1, 1);
                g.FillRectangle(Brushes.Black, (int)(center.X + y), (int)(center.Y + x), 1, 1);
                g.FillRectangle(Brushes.Black, (int)(center.X - y), (int)(center.Y + x), 1, 1);
                g.FillRectangle(Brushes.Black, (int)(center.X + y), (int)(center.Y - x), 1, 1);
                g.FillRectangle(Brushes.Black, (int)(center.X - y), (int)(center.Y - x), 1, 1);

                if (P <= 0)
                {
                    P = P + 2 * x + 1;
                }
                else
                {
                    P = P + 2 * x + 1 - 2 * y + 1;
                    y--;
                }
                x++;

            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int xc = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int yc = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            double r = Convert.ToDouble(textBox3.Text);
            Point center = new Point(xc, yc);
            circlealgo(center, r);
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