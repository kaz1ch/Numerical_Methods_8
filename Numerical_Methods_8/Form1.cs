using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Numerical_Methods_8
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public class Complex
        {
            public double Re, Im;

            Complex() { }

            public Complex(double r, double i)
            {
                Re = r;
                Im = i;
            }

            public Complex(Complex c)
            {
                this.Re = c.Re;
                this.Im = c.Im;
            }

            public static Complex operator +(Complex c1, Complex c2)   
            {
                Complex res = new Complex();
                res.Re = c1.Re + c2.Re;
                res.Im = c1.Im + c2.Im;
                return (res);
            }

            public static Complex operator +(Complex c1, double z)
            {
                Complex res = new Complex();
                res.Re = c1.Re + z;
                res.Im = c1.Im;
                return (res);
            }

            public static Complex operator -(Complex c1, Complex c2)
            {
                Complex res = new Complex();
                res.Re = c1.Re - c2.Re;
                res.Im = c1.Im - c2.Im;
                return (res);
            }

            public static Complex operator -(Complex c1, double z)
            {
                Complex res = new Complex();
                res.Re = c1.Re - z;
                res.Im = c1.Im;
                return (res);
            }

            public static Complex operator *(Complex c1, Complex c2)
            {
                Complex res = new Complex();
                res.Re = c1.Re * c2.Re - c1.Im * c2.Im;
                res.Im = c1.Im * c2.Re - c1.Re * c2.Im;
                return (res);
            }

            public static Complex operator *(Complex c1, double z)
            {
                Complex res = new Complex();
                res.Re = c1.Re * z;
                res.Im = c1.Im * z;
                return (res);
            }

            public static Complex operator /(Complex c1, Complex c2)
            {
                Complex res = new Complex();
                res.Re = (c1.Re * c2.Re + c1.Im * c2.Im) / (c2.Re * c2.Re + c2.Im * c2.Im);
                res.Im = (c1.Im * c2.Re - c1.Re * c2.Im) / (c2.Re * c2.Re + c2.Im * c2.Im);
                return (res);
            }

            public static Complex operator /(Complex c1, double z)
            {
                Complex res = new Complex();
                res.Re = c1.Re / z;
                res.Im = c1.Im / z;
                return (res);
            }
        }

        Complex Get_Next(Complex z)
        {
            return (z * z * z * z * 3 + 1) / (z * z * z * 4);
        }

        Complex iterate(Complex z)
        {
            var r = z;
            for (int i = 0; i < 60; ++i)
            {
                r = Get_Next(r);
            }

            return r;
        }

        Complex GetNext_h(Complex z, double h)
        {
            return z - (z * z * z * z - 1) * h / (z * z * z * 4);
        }

        Color getColor(Complex z)
        {
            if (z.Re == 1) return Color.Blue;
            if (z.Re == -1) return Color.Red;
            if (z.Im == 1) return Color.Green;
            if (z.Im == -1) return Color.MediumTurquoise;
            return Color.White;
        }

        void drawFractal(int w, int h, Bitmap bmp)
        {
            double dx = 6.0 / w;
            double dy = 4.0 / h;
            double a = -3.0, b = 2.0;

            Complex z;

            for (int x = 1; x < w; ++x)
            {
                for (int y = 1; y < h; ++y)
                {
                    z = new Complex(a, b);
                    z = iterate(z);

                    bmp.SetPixel(x, y, getColor(z));
                    b += dy;
                }
                a += dx;
            }
        }

        void doLine(Complex z, Color c, Bitmap bmp)
        {
            int w = this.pictureBox1.Width, h = this.pictureBox1.Height;
            float dx = w / 6, dy = h / 4, w_1 = Convert.ToSingle(w * 0.5), h_1 = Convert.ToSingle(h * 0.5);
            float x1 = Convert.ToSingle(z.Re * dx + w_1), y1 = Convert.ToSingle(z.Im * dy + h_1), x, y;

            Graphics gfx = Graphics.FromImage(bmp);
            Pen pen = new Pen(c, 1);

            for (int i = 0; i < 60; ++i)
            {
                z = Get_Next(z);

                x = Convert.ToSingle(z.Re * dx + w_1);
                y = Convert.ToSingle(z.Im * dy + h_1);

                gfx.DrawLine(pen, x1, y1, x, y);

                x1 = x;
                y1 = y;
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int w = this.pictureBox1.Width, h = this.pictureBox1.Height;

            Bitmap bmp = new Bitmap(w, h);

            drawFractal(w, h, bmp);

            Complex z = new Complex(-3, 2);
            doLine(z, getColor(z), bmp);

            this.pictureBox1.Image = bmp;
        }
    }
}
