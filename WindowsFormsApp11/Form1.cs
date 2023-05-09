using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp11
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void Draw(int x1, int y1, int x2, int y2)
        {
            var brush = Brushes.Black;
            var g = panel1.CreateGraphics();
            /*
            // تحويل الإحداثيات من الشاشة إلى الإحداثيات الرئيسية
            int rx1 = x1 - panel1.Width / 2;
            int ry1 = -1 * (y1 - panel1.Height / 2);
            int rx2 = x2 - panel1.Width / 2;
            int ry2 = -1 * (y2 - panel1.Height / 2);

            // استخدام الإحداثيات الرئيسية لرسم الخط
            //   g.DrawLine(new Pen(brush), rx1, ry1, rx2, ry2);
            */
            int dx = Math.Abs(x2 - x1);
            int dy = Math.Abs(y2 - y1);

            int sx = x1 < x2 ? 1 : -1;
            int sy = y1 < y2 ? 1 : -1;

            int err = dx - dy;
            int e2;

            while (true)
            {
                g.FillRectangle(Brushes.Black, x1, y1, 1, 1);

                if (x1 == x2 && y1 == y2)
                    break;

                e2 = 2 * err;

                if (e2 > -dy)
                {
                    err = err - dy;
                    x1 = x1 + sx;
                }

                if (e2 < dx)
                {
                    err = err + dx;
                    y1 = y1 + sy;
                }
            }
        }

        private float[,] MatrixMultiply(float[,] matrix1, float[,] matrix2)
        {
            int rows1 = matrix1.GetLength(0);
            int cols1 = matrix1.GetLength(1);
            int rows2 = matrix2.GetLength(0);
            int cols2 = matrix2.GetLength(1);

            if (cols1 != rows2)
            {
                throw new ArgumentException("The number of columns in matrix1 must match the number of rows in matrix2");
            }

            float[,] result = new float[rows1, cols2];

            for (int i = 0; i < rows1; i++)
            {
                for (int j = 0; j < cols2; j++)
                {
                    float sum = 0;
                    for (int k = 0; k < cols1; k++)
                    {
                        sum += matrix1[i, k] * matrix2[k, j];
                    }
                    result[i, j] = sum;
                }
            }

            return result;
        }
    
        private void triangle(Point p1, Point p2, Point p3, Brush brush)
        {
            Draw(p1.X, p1.Y, p2.X, p2.Y);
            Draw(p2.X, p2.Y, p3.X, p3.Y);
            Draw(p3.X, p3.Y, p1.X, p1.Y);
        }

        private void rotation(float x1, float y1, float x2, float y2, float x3, float y3, float angleInDegrees, float fixedX, float fixedY)
        {
            // Apply translation to move the triangle to the origin
            float[,] translationmatrix1 = new float[3, 3]
            {
                {1 , 0 , -fixedX },
                {0 , 1 , -fixedY },
                {0 , 0 , 1 }
            };

            float[,] point1matrix = new float[3, 1]
            {
                {x1} ,
                {y1},
                {1}
            };

            float[,] point2matrix = new float[3, 1]
            {
                {x2} ,
                {y2},
                {1}
            };

            float[,] point3matrix = new float[3, 1]
            {
                {x3} ,
                {y3},
                {1}
            };

            float[,] translatepoint1matrix = MatrixMultiply(translationmatrix1, point1matrix);
            float[,] translatepoint2matrix = MatrixMultiply(translationmatrix1, point2matrix);
            float[,] translatepoint3matrix = MatrixMultiply(translationmatrix1, point3matrix);

            // Apply rotation to the translated triangle
            float angleInRadians = (float)(angleInDegrees * Math.PI / 180.0f);
            float[,] rotationmatrix = new float[3, 3]
            {
                { (float) Math.Cos(angleInRadians), (float) -Math.Sin(angleInRadians), 0 },
                { (float) Math.Sin(angleInRadians), (float) Math.Cos(angleInRadians), 0 },
                { 0, 0, 1 }
            };

            float[,] rotatedp1matrix = MatrixMultiply(rotationmatrix, translatepoint1matrix);
            float[,] rotatedp2matrix = MatrixMultiply(rotationmatrix, translatepoint2matrix);
            float[,] rotatedp3matrix = MatrixMultiply(rotationmatrix, translatepoint3matrix);

            // Apply translation to move the triangle back to its original position
            float[,] translationmatrix2 = new float[3, 3]
            {
                {1 , 0 , fixedX },
                {0 , 1 , fixedY },
                {0 , 0 , 1 }
            };

            float[,] rotatedtranslatematrix1 = MatrixMultiply(translationmatrix2, rotatedp1matrix);
            float[,] rotatedtranslatematrix2 = MatrixMultiply(translationmatrix2, rotatedp2matrix);
            float[,] rotatedtranslatematrix3 = MatrixMultiply(translationmatrix2, rotatedp3matrix);

            // Create Homogenized objects from the rotated matrices
            Point p1matrix = new Point((int)rotatedtranslatematrix1[0, 0], (int)rotatedtranslatematrix1[1, 0]);
            Point p2matrix = new Point((int)rotatedtranslatematrix2[0, 0], (int)rotatedtranslatematrix2[1, 0]);
            Point p3matrix = new Point((int)rotatedtranslatematrix3[0, 0], (int)rotatedtranslatematrix3[1, 0]);

            triangle(p1matrix, p2matrix, p3matrix, Brushes.Red);
        }

        
        private void translation(float x1, float y1, float x2, float y2, float x3, float y3, float tx, float ty, float fixedX, float fixedY)       
        {            
            float[,] translationmatrix = new float[3, 3]
            {
                { 1, 0, tx },
                { 0, 1, ty },
                { 0, 0, 1 }
            };

            // Apply translation to move the triangle to the origin
            float[,] translationmatrix1 = new float[3, 3]
            {
                {1 , 0 , -fixedX },
                {0 , 1 , -fixedY },
                {0 , 0 , 1 }
            };

            float[,] point1matrix = new float[3, 1]
            {
                {x1} ,
                {y1},
                {1}
            };

            float[,] point2matrix = new float[3, 1]
            {
                {x2} ,
                {y2},
                {1}
            };

            float[,] point3matrix = new float[3, 1]
            {
                {x3} ,
                {y3},
                {1}
            };

            float[,] translatepoint1matrix = MatrixMultiply(translationmatrix1, point1matrix);
            float[,] translatepoint2matrix = MatrixMultiply(translationmatrix1, point2matrix);
            float[,] translatepoint3matrix = MatrixMultiply(translationmatrix1, point3matrix);

            float[,] translatmatrix = new float[3, 3]
           {
                { 1, 0, tx },
                { 0, 1, ty },
                { 0, 0, 1 }
           };

            float[,] translatedp1matrix = MatrixMultiply(translatmatrix, translatepoint1matrix);
            float[,] translatedp2matrix = MatrixMultiply(translatmatrix, translatepoint2matrix);
            float[,] translatedp3matrix = MatrixMultiply(translatmatrix, translatepoint3matrix);

            float[,] translationmatrix2 = new float[3, 3]
            {
                {1 , 0 , fixedX },
                {0 , 1 , fixedY },
                {0 , 0 , 1 }
            };
            float[,] transtranslatematrix1 = MatrixMultiply(translationmatrix2, translatedp1matrix);
            float[,] transtranslatematrix2 = MatrixMultiply(translationmatrix2, translatedp2matrix);
            float[,] transtranslatematrix3 = MatrixMultiply(translationmatrix2, translatedp3matrix);
           

            Point p1matrix = new Point((int)transtranslatematrix1[0, 0], (int)transtranslatematrix1[1, 0]);
            Point p2matrix = new Point((int)transtranslatematrix2[0, 0], (int)transtranslatematrix2[1, 0]);
            Point p3matrix = new Point((int)transtranslatematrix3[0, 0], (int)transtranslatematrix3[1, 0]);

            triangle(p1matrix, p2matrix, p3matrix, Brushes.Yellow);
        }

        
        private void scaling(float x1, float y1, float x2, float y2, float x3, float y3, float sx, float sy, float fixedX, float fixedY)
        {
            // Apply translation to move the triangle to the origin
            float[,] translationmatrix1 = new float[3, 3]
            {
                {1 , 0 , -fixedX },
                {0 , 1 , -fixedY },
                {0 , 0 , 1 }
            };

            float[,] point1matrix = new float[3, 1]
            {
                    {x1} ,
                    {y1},
                    {1}
            };

            float[,] point2matrix = new float[3, 1]
            {
                    {x2} ,
                    {y2},
                    {1}
            };

            float[,] point3matrix = new float[3, 1]
            {
                    {x3} ,
                    {y3},
                    {1}
            };

            float[,] translatepoint1matrix = MatrixMultiply(translationmatrix1, point1matrix);
            float[,] translatepoint2matrix = MatrixMultiply(translationmatrix1, point2matrix);
            float[,] translatepoint3matrix = MatrixMultiply(translationmatrix1, point3matrix);

            // Apply scaling to the translated triangle
            float[,] scalingmatrix = new float[3, 3]
            {
                { sx, 0, 0 },
                { 0, sy, 0 },
                { 0, 0, 1 }
            };

            float[,] scaledp1matrix = MatrixMultiply(scalingmatrix, translatepoint1matrix);
            float[,] scaledp2matrix = MatrixMultiply(scalingmatrix, translatepoint2matrix);
            float[,] scaledp3matrix = MatrixMultiply(scalingmatrix, translatepoint3matrix);

            // Apply translation to move the triangle back to its original position
            float[,] translationmatrix2 = new float[3, 3]
            {
                {1 , 0 , fixedX },
                {0 , 1 , fixedY },
                {0 , 0 , 1 }
            };

            float[,] scaledtranslatematrix1 = MatrixMultiply(translationmatrix2, scaledp1matrix);
            float[,] scaledtranslatematrix2 = MatrixMultiply(translationmatrix2, scaledp2matrix);
            float[,] scaledtranslatematrix3 = MatrixMultiply(translationmatrix2, scaledp3matrix);

            

            Point p1matrix = new Point((int)scaledtranslatematrix1[0, 0], (int)scaledtranslatematrix1[1, 0]);
            Point p2matrix = new Point((int)scaledtranslatematrix2[0, 0], (int)scaledtranslatematrix2[1, 0]);
            Point p3matrix = new Point((int)scaledtranslatematrix3[0, 0], (int)scaledtranslatematrix3[1, 0]);

            triangle(p1matrix, p2matrix, p3matrix, Brushes.Red);
        }

        private void reflection(float x1, float y1, float x2, float y2, float x3, float y3, float rfx, float rfy, float fixedX, float fixedY)
        {
            float[,] translationmatrix1 = new float[3, 3]
           {
                {1 , 0 , -fixedX },
                {0 , 1 , -fixedY },
                {0 , 0 , 1 }
           };

            float[,] point1matrix = new float[3, 1]
            {
                    {x1} ,
                    {y1},
                    {1}
            };

            float[,] point2matrix = new float[3, 1]
            {
                    {x2} ,
                    {y2},
                    {1}
            };

            float[,] point3matrix = new float[3, 1]
            {
                    {x3} ,
                    {y3},
                    {1}
            };

            float[,] translatepoint1matrix = MatrixMultiply(translationmatrix1, point1matrix);
            float[,] translatepoint2matrix = MatrixMultiply(translationmatrix1, point2matrix);
            float[,] translatepoint3matrix = MatrixMultiply(translationmatrix1, point3matrix);

            float[,] reflectionmatrix = new float[3, 3]
           {
                { -rfx, 0, 0 },
                { 0, -rfy, 0 },
                { 0, 0, 1 }
           };

            float[,] reflectp1matrix = MatrixMultiply(reflectionmatrix, translatepoint1matrix);
            float[,] reflectp2matrix = MatrixMultiply(reflectionmatrix, translatepoint2matrix);
            float[,] reflectp3matrix = MatrixMultiply(reflectionmatrix, translatepoint3matrix);

            float[,] translationmatrix2 = new float[3, 3]
           {
                {1 , 0 , fixedX },
                {0 , 1 , fixedY },
                {0 , 0 , 1 }
           };

            float[,] reflecttranslatematrix1 = MatrixMultiply(translationmatrix2, reflectp1matrix);
            float[,] reflecttranslatematrix2 = MatrixMultiply(translationmatrix2, reflectp2matrix);
            float[,] reflecttranslatematrix3 = MatrixMultiply(translationmatrix2, reflectp3matrix);

            Point p1matrix = new Point((int)reflecttranslatematrix1[0, 0], (int)reflecttranslatematrix1[1, 0]);
            Point p2matrix = new Point((int)reflecttranslatematrix2[0, 0], (int)reflecttranslatematrix2[1, 0]);
            Point p3matrix = new Point((int)reflecttranslatematrix3[0, 0], (int)reflecttranslatematrix3[1, 0]);

            triangle(p1matrix, p2matrix, p3matrix, Brushes.Red);

        }
        
        private void shearing (float x1, float y1, float x2, float y2, float x3, float y3, float shx, float shy, float fixedX, float fixedY)
        {
            float[,] translationmatrix1 = new float[3, 3]
          {
                {1 , 0 , -fixedX },
                {0 , 1 , -fixedY },
                {0 , 0 , 1 }
          };

            float[,] point1matrix = new float[3, 1]
            {
                    {x1} ,
                    {y1},
                    {1}
            };

            float[,] point2matrix = new float[3, 1]
            {
                    {x2} ,
                    {y2},
                    {1}
            };

            float[,] point3matrix = new float[3, 1]
            {
                    {x3} ,
                    {y3},
                    {1}
            };

            float[,] translatepoint1matrix = MatrixMultiply(translationmatrix1, point1matrix);
            float[,] translatepoint2matrix = MatrixMultiply(translationmatrix1, point2matrix);
            float[,] translatepoint3matrix = MatrixMultiply(translationmatrix1, point3matrix);

            float[,] shearingmatrix = new float[3, 3]
          {
                { 1, shx, 0 },
                { shy, 1, 0 },
                { 0, 0, 1 }
          };

            float[,] shearp1matrix = MatrixMultiply(shearingmatrix, translatepoint1matrix);
            float[,] shearp2matrix = MatrixMultiply(shearingmatrix, translatepoint2matrix);
            float[,] shearp3matrix = MatrixMultiply(shearingmatrix, translatepoint3matrix);

            float[,] translationmatrix2 = new float[3, 3]
           {
                {1 , 0 , fixedX },
                {0 , 1 , fixedY },
                {0 , 0 , 1 }
           };

            float[,] sheartranslatematrix1 = MatrixMultiply(translationmatrix2, shearp1matrix);
            float[,] sheartranslatematrix2 = MatrixMultiply(translationmatrix2, shearp2matrix);
            float[,] sheartranslatematrix3 = MatrixMultiply(translationmatrix2, shearp3matrix);

            Point p1matrix = new Point((int)sheartranslatematrix1[0, 0], (int)sheartranslatematrix1[1, 0]);
            Point p2matrix = new Point((int)sheartranslatematrix2[0, 0], (int)sheartranslatematrix2[1, 0]);
            Point p3matrix = new Point((int)sheartranslatematrix3[0, 0], (int)sheartranslatematrix3[1, 0]);

            triangle(p1matrix, p2matrix, p3matrix, Brushes.Red);

        }

        // draw traingle
            private void button1_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int y1 = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            int x2 = Convert.ToInt32(textBox3.Text) + panel1.Width / 2;
            int y2 = -1 * (Convert.ToInt32(textBox4.Text)) + panel1.Height / 2;
            int x3 = Convert.ToInt32(textBox5.Text) + panel1.Width / 2;
            int y3 = -1 * (Convert.ToInt32(textBox6.Text)) + panel1.Height / 2;

            Brush brush = Brushes.Black;
          
            Point p1 = new Point(x1, y1);
            Point p2 = new Point(x2, y2);
            Point p3 = new Point(x3, y3);

            triangle(p1, p2, p3, brush);
        }
        //translation
        private void button2_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int y1 = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            int x2 = Convert.ToInt32(textBox3.Text) + panel1.Width / 2;
            int y2 = -1 * (Convert.ToInt32(textBox4.Text)) + panel1.Height / 2;
            int x3 = Convert.ToInt32(textBox5.Text) + panel1.Width / 2;
            int y3 = -1 * (Convert.ToInt32(textBox6.Text)) + panel1.Height / 2;

            float fixedX = (float)panel1.Width / 2;
            float fixedY = (float)panel1.Height / 2;

            int tx = Convert.ToInt32(textBox7.Text);
            int ty = Convert.ToInt32(textBox8.Text);

            translation(x1, y1, x2, y2, x3, y3, tx, ty, fixedX, fixedY);
        }
        //scaling
        private void button3_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int y1 = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            int x2 = Convert.ToInt32(textBox3.Text) + panel1.Width / 2;
            int y2 = -1 * (Convert.ToInt32(textBox4.Text)) + panel1.Height / 2;
            int x3 = Convert.ToInt32(textBox5.Text) + panel1.Width / 2;
            int y3 = -1 * (Convert.ToInt32(textBox6.Text)) + panel1.Height / 2;
            float sx = float.Parse(textBox9.Text);
            float sy = float.Parse(textBox10.Text);
            float fixedX = (float)panel1.Width / 2;
            float fixedY = (float)panel1.Height / 2;

            scaling(x1, y1, x2, y2, x3, y3, sx, sy, fixedX, fixedY);

        }
        //rotation
        private void button4_Click_1(object sender, EventArgs e)
        {
            // Parse triangle points and angle from text boxes
            int x1 = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int y1 = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            int x2 = Convert.ToInt32(textBox3.Text) + panel1.Width / 2;
            int y2 = -1 * (Convert.ToInt32(textBox4.Text)) + panel1.Height / 2;
            int x3 = Convert.ToInt32(textBox5.Text) + panel1.Width / 2;
            int y3 = -1 * (Convert.ToInt32(textBox6.Text)) + panel1.Height / 2;
            float fixedX = (float)panel1.Width / 2;
            float fixedY = (float)panel1.Height / 2;

            float angleInDegrees = float.Parse(textBox11.Text);
            
            rotation(x1, y1, x2, y2, x3, y3, angleInDegrees, fixedX, fixedY);
        }
        //reflection
        private void button6_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int y1 = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            int x2 = Convert.ToInt32(textBox3.Text) + panel1.Width / 2;
            int y2 = -1 * (Convert.ToInt32(textBox4.Text)) + panel1.Height / 2;
            int x3 = Convert.ToInt32(textBox5.Text) + panel1.Width / 2;
            int y3 = -1 * (Convert.ToInt32(textBox6.Text)) + panel1.Height / 2;

            float rfx = float.Parse(textBox12.Text);
            float rfy = float.Parse(textBox13.Text);

            float fixedX = (float)panel1.Width / 2;
            float fixedY = (float)panel1.Height / 2;

            reflection(x1, y1, x2, y2, x3, y3, rfx, rfy, fixedX, fixedY);

        }

        //shearing
        private void button7_Click(object sender, EventArgs e)
        {
            int x1 = Convert.ToInt32(textBox1.Text) + panel1.Width / 2;
            int y1 = -1 * (Convert.ToInt32(textBox2.Text)) + panel1.Height / 2;
            int x2 = Convert.ToInt32(textBox3.Text) + panel1.Width / 2;
            int y2 = -1 * (Convert.ToInt32(textBox4.Text)) + panel1.Height / 2;
            int x3 = Convert.ToInt32(textBox5.Text) + panel1.Width / 2;
            int y3 = -1 * (Convert.ToInt32(textBox6.Text)) + panel1.Height / 2;

            float shx = Convert.ToSingle(textBox14.Text);
            float shy = Convert.ToSingle(textBox15.Text);

            float fixedX = (float)panel1.Width / 2;
            float fixedY = (float)panel1.Height / 2;

            shearing(x1 , y1 , x2 , y2 , x3 , y3 , shx , shy , fixedX , fixedY );
        }

        // coordinates
        private void button5_Click(object sender, EventArgs e)
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
    
