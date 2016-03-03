using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeuralNetwork
{
    public partial class Form1 : Form
    {
        private Neuron neuron = null;

        public Form1()
        {
            InitializeComponent();
            openFileDialog1.InitialDirectory = Directory.GetCurrentDirectory();
        }

        private void open_button_Click(object sender, EventArgs e)
        {
            teaching_button.Enabled = true;
            label1.Text = "Начните распознование";
            openFileDialog1.Title = "Укажите тестируемый файл";
            openFileDialog1.ShowDialog();
            pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            Bitmap bmp = pictureBox1.Image as Bitmap;
            if (neuron == null)
                neuron = new Neuron(bmp.Width, bmp.Height, 3);

            neuron.Scan(bmp);
        }

        private void recognize_button_Click(object sender, EventArgs e)
        {
            int shape = neuron.Recognize();
            switch (shape)
            {
                case 0: label1.Text = "Это круг"; break;
                case 1: label1.Text = "Это прямоугольник"; break;
                case 2: label1.Text = "Это треугольник"; break;
            }
        }

        private void teaching_button_Click(object sender, EventArgs e)
        {
            int res = 0;
            if (rectangle.Checked)
                res = 1;
            else if (triangle.Checked)
                res = 2;
            neuron.Teaching(res);
            label1.Text = "Запомнил!";
        }

    }
}
