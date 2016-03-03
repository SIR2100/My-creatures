using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace For_in_Row
{
    public partial class Form1 : Form
    {
        //=========================================== Переменные и инициализация формы =======================================================
        private Image emptyImg = null;
        private Image player1Img = null;
        private Image player2Img = null;
        State currentState = null;
        bool end = false;

        public Form1()
        {
            InitializeComponent();
            emptyImg = new Bitmap("empty.png");
            player1Img = new Bitmap("player1.png");
            player2Img = new Bitmap("player2.png");
            int[,] m = new int[6, 7] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 } };
            drawBoard(m);
            currentState = new State(m);
        }

        //=========================================== Заполянем pictureBox'ы нужными картинками ==============================================
        private void drawBoard(int[,] board) 
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                {
                    string s = "pictureBox" + i.ToString() + j.ToString();
                    Control pictBox = this.tableLayoutPanel1.Controls[s];
                    switch (board[i, j])
                    {
                        case 0: (pictBox as PictureBox).Image = emptyImg; break;
                        case 1: (pictBox as PictureBox).Image = player1Img; break;
                        case 2: (pictBox as PictureBox).Image = player2Img; break;
                    }
                }
        }

        //============================================ Алгоритм минмакс с  альфа-бета отсечением =============================================

        // (Текущее состояние доски, 5, Int32.MinValue, Int32.MaxValue, 5, true)
        public Result alphaBeta(State node, int depth, int a, int b, int d, bool maximizingPlayer) 
        {
            // Конец игры
            if (depth == 0 || node.isTerminalState())
                return new Result(node, node.countHeuristic(), depth);

            State res = node;

            // Цель - максимизировать
            if (maximizingPlayer)
            {
                    List<State> successors = node.buildAllSuccessors(true); // Все возможные ходы
                for (int i = 0; i < successors.Count; i++)
                {
                    Result p = alphaBeta(successors[i], depth - 1, a, b, d, false); // Пытаемся найти самый вероятный ход противника
                    if (p != null && p.number > a || (p.number == a && p.depth > d)) // Ищем самую большую глубину, для победы "наверняка"
                    {
                        a = p.number;
                        res = successors[i];
                        d = p.depth;
                    }

                    //На случай, если что-то пойдет не так
                    if (b <= a)
                        break;
                }
                return new Result(res, a, d);
            }
            else 
            {
                List<State> successors = node.buildAllSuccessors(false); // Вычисляем ход, который может привести AI к поражению
                for (int i = 0; i < successors.Count; i++)
                {
                    Result p = alphaBeta(successors[i], depth - 1, a, b, d, true);
                    if (p != null && p.number < b || (p.number == b && p.depth > d))
                    {
                        b = p.number;
                        res = successors[i];
                        d = p.depth;
                    }

                    //На случай, если что-то пойдет не так
                    if (b <= a)
                        break;
                }
                return new Result(res, b, d);
            }
        }
        //============================================================ Конец игры ============================================================
       
        private bool endOfGame()
        {
            int heuristic = currentState.countHeuristic();
            bool flag = false;
            if (heuristic == 250000)
            {
                MessageBox.Show("Ничья!", "Поздравляем", MessageBoxButtons.OK);
                flag = true;
            }
            else if (heuristic == 100000) 
            {
                MessageBox.Show("Победил компьютер!", "Сожалеем", MessageBoxButtons.OK);
                flag = true;
            }
            if (heuristic == -100000)
            {
                MessageBox.Show("Вы победили!", "Поздравляем", MessageBoxButtons.OK);
                flag = true;
            }

            if (flag)
            {
                int[,] m = new int[6, 7] { { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0, 0 } };
                drawBoard(m);
                currentState = new State(m);
                play_button.Enabled = true;
            }
            return flag;
        }

        //============================================================ Ход игрока ============================================================
        private void playerTurn(int column)
        {
            for (int i = 5; i >= 0; i--)
                if (currentState.data[i, column] == 0) //Если текущая точка пуста
                {
                    currentState.data[i, column] = 2; //Присваиваем ей 2
                    break;
                }
            drawBoard(currentState.data); // Переотрисовка доски
            if (endOfGame()) // Конец игры
                end = true;
        }

        //============================================================ Ход компьютера ========================================================
        private void computerTurn() 
        {
            Result currentResult = alphaBeta(currentState, 1, Int32.MinValue, Int32.MaxValue, 5, true);
            currentState = currentResult.st;
            drawBoard(currentState.data);// Переотрисовка доски
            if (endOfGame()) // Конец игры
                end = true;
        }

        //============================================================ Уступить ход AI =======================================================
        private void play_button_Click(object sender, EventArgs e) 
        {
            end = false;
            computerTurn();
            play_button.Enabled = false;
        }

        //============================================================ Игрок нажимает кнопку =================================================
        private void button1_Click(object sender, EventArgs e) 
        {
            int column = Convert.ToInt32((sender as Button).Text) - 1;
            playerTurn(column);
            if (!end)
                computerTurn();
            end = false;
        }
    }
}
