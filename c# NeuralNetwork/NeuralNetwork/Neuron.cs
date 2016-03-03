using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace NeuralNetwork
{
    class Neuron
    {
        public int[,] S_elems;
        public int[] A_elems;
        public int[] R_elems;
        public int[,] weights;
        public int limitA = 15; // порог для A-элементов
        public int w;
        public int h;
        public int count;

        public Neuron(int width, int height, int shapesCount)
        {
            w = width;
            h = height;
            count = shapesCount;
            S_elems = new int[w, h];
            A_elems = new int[w + h];
            R_elems = new int[count];
            weights = new int[count, w + h];
        }

        //Ищем темные пиксели на светлом фоне
        private void FillFirstLayer(Bitmap bmp) // Заполняем S-елементы
        {
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    if (bmp.GetPixel(i, j).R <= 240 && bmp.GetPixel(i, j).G <= 240 && bmp.GetPixel(i, j).B <= 240)
                        S_elems[i, j] = 1; 
                    else
                        S_elems[i, j] = 0;
        }

        private void FillSecondLayer() // Заполняем A-елементы
        {
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                {
                    A_elems[i] += S_elems[i, j]; // Количество темных пикселей в строчке
                    A_elems[w + j] += S_elems[i, j]; // Ширина картинки + текущий пиксель ширины
                }

            for (int i = 0; i < w + h; i++)
                if (A_elems[i] >= limitA)
                    A_elems[i] = 1;
                else
                    A_elems[i] = 0;
        }

        private void FillThirdLayer() // Заполняем R-елементы
        {
            for (int i = 0; i < count; i++)
                for (int j = 0; j < w + h; j++)
                    R_elems[i] += A_elems[j] * weights[i, j];
        }

        public void Scan(Bitmap bmp) // Заполняем первые два слоя
        {
            FillFirstLayer(bmp);
            FillSecondLayer();
        }

        private int ChosenShape() // Номер выбранной фигуры 
        {
            int max = Int32.MinValue, res = 0;
            for (int i = 0; i < count; i++)
                if (R_elems[i] > max)
                {
                    max = R_elems[i];
                    res = i;
                }
            return res;
        }

        public int Recognize() // Распознаем изображение
        {
            FillThirdLayer();
            return ChosenShape();
        }

        public void Teaching(int i) // Обучение
        {
            int res = ChosenShape();
            if (res == i)
                return;

            for (int j = 0; j < w + h; j++)
            {
                weights[i, j] += A_elems[j];
                weights[res, j] -= A_elems[j];
            }  
        } 
    }
}
