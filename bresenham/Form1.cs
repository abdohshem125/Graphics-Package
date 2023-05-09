using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bresenham
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


        }

        private void swap(ref int x1, ref int y1, ref int x2, ref int y2)
        {
            int templ, temp2;
            templ = x1;
            x1 = y2;
            y2 = templ;

            temp2 = x2;
            x2 = y2;
            y2 = temp2;
        }

        private void linebresenham(int x1, int y1, int x2, int y2)
        {

            Graphics g = pictureBox1.CreateGraphics();
            int w = pictureBox1.ClientSize.Width;
            int h = pictureBox1.ClientSize.Height;

            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);
            int p;
            double slope = (double)(y2 - y1) / (x2 - x1);

            // Determine which endpoint to use as start position
            if (dx == 0)
            {
                slope = 99999;
                // First Ocstant
                if (x1 < x2 && slope >= 0 && slope <= 1)
                {
                    p = (2 * dy) - dx;
                    int x = x1, y = y1;
                    for (int i = x1; i < x2; i++)
                    {
                        x++;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y++;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, x + w / 2, -y + h / 2, 2, 2);
                    }
                }

                //second
                else if (y1 < y2 && slope > 1)
                {
                    swap (ref x1 , ref y1 , ref x2, ref y2);
                    dx = x2 - x1;
                    dy = y2 - y1;

                    p = (2 * dy) - dx;
                    int x = x1;
                    int y = y1;

                    for (int i = x1; i < x2;i++)
                    {
                        x++;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y++;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, x + w / 2, -y + h / 2, 2, 2);

                    }
                }
                // third 

                else if (y1 < y2 && slope < -1 && slope > -9999)
                {
                    swap(ref x1, ref y1, ref x2, ref y2);
                    dx = x2 - x1;
                    dy = y2 - y1;
                    dy = -dy;

                    p = (2 * dy) - dx;

                    int x = x1;
                    int y = y1;

                    for (int i = x1; i < x2; i++)
                    {
                        x++;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y--;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, x + w / 2, -y + h / 2, 2, 2);

                    }
                }

                // fourth
                else if (x1 > x2 && slope <= 0 && slope >= -1)
                {
                   
                   

                    p = (2 * dy) - dx;

                    int x = x1;
                    int y = y1;

                    for (int i = x2; i < x1; i++)
                    {
                        x--;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y++;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, x + w / 2, -y + h / 2, 2, 2);

                    }
                }
                // fifth
                else if (x1 > x2 && slope > 0 && slope <= 1 )
                {
                    swap(ref x1, ref y1, ref x2, ref y2);
                   

                    p = (2 * dy) - dx;

                    int x = x1;
                    int y = y1;

                    for (int i = x2; i < x1; i++)
                    {
                        x--;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y--;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, x + w / 2, -y + h / 2, 2, 2);

                    }
                }
               // sixth
                else if (y1 > y2 && slope > 1 && slope > 99999)
                {
                    swap(ref x1, ref y1, ref x2, ref y2);
                    dx = x2 - x1;
                    dy = y2 - y1;
                    dx = -dx;
                    dy = -dy;

                    p = (2 * dy) - dx;

                    int x = x1;
                    int y = y1;

                    for (int i = x2; i < x1; i++)
                    {
                        x--;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y--;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, y + w / 2, -x + h / 2, 2, 2);

                    }
                }
                // seventh
                else if (y1 > y2 && slope < -1 && slope < 9999)
                {
                    swap(ref x1, ref y1, ref x2, ref y2);
                    dx = x2 - x1;
                    dy = y2 - y1;
                    dx = -dx;

                    p = (2 * dy) - dx;

                    int x = x1;
                    int y = y1;

                    for (int i = x2; i < x1; i++)
                    {
                        x--;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y++;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, y + w / 2, -x + h / 2, 2, 2);

                    }
                }
                // eighth
                else if (x1 < x2 && slope <= 0 && slope >= -1)
                {
                    

                    p = (2 * dx) - dx;

                    int x = x1;
                    int y = y1;

                    for (int i = x1; i < x2; i++)
                    {
                        x++;
                        if (p < 0)
                        {
                            p += (2 * dy);
                        }
                        else
                        {
                            y--;
                            p += (2 * dy) - (2 * dx);

                        }
                        g.FillRectangle(Brushes.Blue, x + w / 2, -y + h / 2, 2, 2);

                    }
                }

            }


            















                       

                
        private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text);
            int y1 = Convert.ToInt32(textBox2.Text);
            int x2 = Convert.ToInt32(textBox3.Text);
            int y2 = Convert.ToInt32(textBox4.Text);

            // Check which octant the line is in and adjust the coordinates accordingly
            int dx = x2 - x1;
            int dy = y2 - y1;


            if (dx > 0 && dy >= 0 && dy <= dx) // Octant 1
                linebresenham(x1, y1, x2, y2);
            else if (dx > 0 && dy > 0 && dy > dx) // Octant 2
                linebresenham(x1, y1, x2, y2);
            else if (dy > 0 && dx < 0 && -dx <= dy) // Octant 3
                linebresenham(-x1, y1, -x2, y2);
            else if (dx < 0 && dy > 0 && dy <= -dx) // Octant 4
                linebresenham(-x1, y1, -x2, y2);
            else if (dx < 0 && dy < 0 && dy > dx) // Octant 5
                linebresenham(-x1, -y1, -x2, -y2);
            else if (dx < 0 && dy < 0 && dy >= dx) // Octant 6
                linebresenham(-x1, -y1, -x2, -y2);
            else if (dx > 0 && dy < 0 && dx <= -dy) // Octant 7
                linebresenham(x1, -y1, x2, -y2);
            else if (dy < 0 && dx > 0 && dx <= -dy) // Octant 8
                linebresenham(x1, -y1, x2, -y2);

            /*
            double slope = 0.0;
            if (dx > 0 && dy > 0 && dy <= dx) // Octant 1
            {
                slope = (double)(y2 - y1) / (double)(x2 - x1);
                linebresenham(x1, y1, x2, y2);
            }
            else if (dx > 0 && dy > 0 && dy > dx) // Octant 2
            {
                slope = (double)(x2 - x1) / (double)(y2 - y1);
                linebresenham(x1, y1, x2, y2);
            }
            else if (dx < 0 && dy > 0 && -dx <= dy) // Octant 3
            {
                slope = (double)(-y2 + y1) / (double)(-x2 + x1);
                linebresenham(-x1, y1, -x2, y2);
            }
            else if (dx < 0 && dy > 0 && -dx > dy) // Octant 4
            {
                slope = (double)(-x2 + x1) / (double)(y2 - y1);
                linebresenham(-x1, y1, -x2, y2);
            }
            else if (dx > 0 && dy < 0 && dx <= -dy) // Octant 5
            {
                slope = (double)(-x2 + x1) / (double)(-y2 + y1);
                linebresenham(-y1, -x1, -y2, -x2);
            }
            else if (dx > 0 && dy > 0 && dy > dx) // Octant 6
            {
                slope = (double)(-y2 + y1) / (double)(-x2 + x1);
                linebresenham(-x1, -y1, -x2, -y2);
            }
            else if (dx > 0 && dy < 0 && dx > -dy) // Octant 7
            {
                slope = (double)(y2 - y1) / (double)(-x2 + x1);
                linebresenham(x1, -y1, x2, -y2);
            }
            else if (dx > 0 && dy < 0 && dx <= -dy) // Octant 8
            {
                slope = (double)(x2 - x1) / (double)(-y2 + y1);
                linebresenham(x1, -y1, x2, -y2);
            }
            */

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
    // Returns the octant of the line
    
}
