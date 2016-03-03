using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic;

namespace Bezje
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            this.g = Graphics.FromImage(this.pictureBox1.Image);
            this.g.Clear(Color.White);
            this.M = this.matrixM();
            button1.Enabled = false;
        }
        private Graphics g;
        private double[,] M;
        private List<Point> pts = new List<Point>();
        private Font fnt = new Font("Courier", 10);
        private Brush b1 = Brushes.Black;
        private Pen p1 = Pens.Black;
        private double[,] matrixM()
        {
            return new double[4, 4] { { 1, -3, 3, -1 }, { 0, 3, -6, 3 }, { 0, 0, 3, -3 }, { 0, 0, 0, 1 } };
        }
        private double[,] product_matrix(double[,] m1, double[,] m2)
        {
            int l = m1.GetLength(0);
            int n1 = m1.GetLength(1);
            int n2 = m2.GetLength(1);
            double[,] res = new double[n1, n2];
            for (int i = 0; i < l; ++i)
                for (int j = 0; j < n2; ++j)
                    for (int k = 0; k < n1; ++k)
                        res[i, j] += m1[i, k] * m2[k, j];
            return res;
        }
        private void get_points()
        {
            this.g.Clear(Color.White);
            for (int i = 0; i < pts.Count; ++i)
            {
                Point p1 = pts[i]; Point p2 = pts[i]; Point p3 = pts[i];
                this.g.FillEllipse(this.b1, p1.X - 5, p2.Y - 5, 10, 10);
            }
            this.pictureBox1.Refresh();
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if ((e.Button == System.Windows.Forms.MouseButtons.Left) && (this.pts.Count < 3))
            {
                pts.Add(e.Location);
                get_points();
                if (this.pts.Count == 3) { button1.Enabled = true; }
            }
        }

        private void foo()
        {
            //Перерисовываем точки
            this.g.Clear(Color.White);
            for (int i = 0; i < this.pts.Count; ++i)
            {
                var pt = this.pts[i];
                this.g.FillEllipse(this.b1, pt.X - 5, pt.Y - 5, 10, 10);
            }

            double n;
            var pts = this.pts[0];
            var V = new double[2, 4];
            var T = new double[4, 1];
            double[,] endMatrix;

            V[0, 1] = this.pts[0].X; V[1, 1] = this.pts[0].Y;
            V[0, 2] = this.pts[1].X; V[1, 2] = this.pts[1].Y;
            V[0, 3] = this.pts[2].X; V[1, 3] = this.pts[2].Y;

            V[0, 0] = pts.X; V[1, 0] = pts.Y;

            V[0, 3] = this.pts[2].X; V[1, 3] = this.pts[2].Y;

            n = 0.0f;
            T[0, 0] = 1f;
            T[1, 0] = n;
            T[2, 0] = n * n;
            T[3, 0] = n * n * n;

            endMatrix = this.product_matrix(V, this.product_matrix(this.M, T));
            for (n += 0.01; n <= 1.0; n += 0.01)
            {
                T[0, 0] = 1f;
                T[1, 0] = n;
                T[2, 0] = n * n;
                T[3, 0] = n * n * n;
                var tm = this.product_matrix(V, this.product_matrix(this.M, T));
                this.g.DrawLine(
                    this.p1,                            //цвет
                    new PointF((float)endMatrix[0, 0],  //Точка 1, X
                    (float)endMatrix[1, 0]),            //Точка 1, Y
                    new PointF((float)tm[0, 0],         //Точка 2, X
                    (float)tm[1, 0])                    //Точка 2, Y
                );
                endMatrix = tm;
            }

            this.pictureBox1.Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.foo();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.pts.Clear();
            this.g.Clear(Color.White);
            this.pictureBox1.Refresh();
            button1.Enabled = false;
        }
    }
}
