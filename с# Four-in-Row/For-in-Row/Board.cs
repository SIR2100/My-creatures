using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace For_in_Row
{
    //--------------------------------------------------------------СОСТОЯНИЕ---------------------------------------------------------------//

    public class State
    {
        public int[,] data = new int[6, 7];

        public State(int[,] m)
        {
            data = m;
        }

        public State cloneState() // клонируем состояние
        {
            int[,] t = new int[6, 7];
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                    t[i, j] = data[i, j];
            return new State(t);
        }

        //=========================================== Проверка на собранную вертикальную линию ============================================
        bool hasVerticalLine(int i, int j) 
        {
            return (data[i, j] == data[i + 1, j]) && (data[i, j] == data[i + 2, j]) && (data[i, j] == data[i + 3, j]);
        }

        //=========================================== Проверка на собранную горизонтальную линию ==========================================
        bool hasHorizontalLine(int i, int j) 
        {
            return (data[i, j] == data[i, j + 1]) && (data[i, j] == data[i, j + 2]) && (data[i, j] == data[i, j + 3]);
        }

        //=========================================== Проверка на собранную диагональную линию 1 ==========================================
        bool hasDiagonalLine1(int i, int j) 
        {
            return (data[i, j] == data[i + 1, j + 1]) && (data[i, j] == data[i + 2, j + 2]) && (data[i, j] == data[i + 3, j + 3]);
        }

        //=========================================== Проверка на собранную диагональную линию 2 ==========================================
        bool hasDiagonalLine2(int i, int j) 
        {
            return (data[i, j] == data[i + 1, j - 1]) && (data[i, j] == data[i + 2, j - 2]) && (data[i, j] == data[i + 3, j - 3]);
        }

        //=========================================== Проверка на выигрышное состояние ====================================================
        bool isWinningState(ref int player)
        {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 7; j++)
                    if (data[i, j] != 0 && hasVerticalLine(i, j))
                    {
                        player = data[i, j];
                        return true;
                    }

            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 4; j++)
                    if (data[i, j] != 0 && hasHorizontalLine(i, j))
                    {
                        player = data[i, j];
                        return true;
                    }

            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                    if (data[i, j] != 0 && hasDiagonalLine1(i, j))
                    {
                        player = data[i, j];
                        return true;
                    }

            for (int i = 0; i < 3; i++)
                for (int j = 3; j < 7; j++)
                    if (data[i, j] != 0 && hasDiagonalLine2(i, j))
                    {
                        player = data[i, j];
                        return true;
                    }
            return false;
        }

        //===================================================== Тупиковое состояние =======================================================
        bool isDeadState()
        {
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                    if (data[i, j] == 0)
                        return false;
            return true;
        }

        //=========================================== Проверка на конечное или тупиковое состояние ========================================
        public bool isTerminalState()
        {
            int player = 0;
            return isDeadState() || isWinningState(ref player);
        }

        //=========================================== Проверка на ТРИ вертикальные открытые точки =========================================
        bool hasVerticalThree(int i, int j, ref int player)
        {
            player = data[i, j];
            return (player != 0) && (player == data[i - 1, j]) && (player == data[i - 2, j]) && (data[i - 3, j] == 0);
        }

        //=========================================== Проверка на две вертикальные открытые точки =========================================
        bool hasVerticalTwo(int i, int j, ref int player)
        {
            player = data[i, j];
            return (player != 0) && (player == data[i - 1, j]) && (data[i - 2, j] == 0) && (data[i - 3, j] == 0);
        }

        //=========================================== Проверка на ТРИ горизонтальные открытые точки =========================================
        bool hasHorizontalThree(int i, int j, ref int player) 
        {
            if ((data[i, j] == 0) && ((player = data[i, j + 1]) != 0) && (data[i, j + 2] == player) && (data[i, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i, j + 1] == 0) && (data[i, j + 2] == player) && (data[i, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i, j + 1] == player) && (data[i, j + 2] == 0) && (data[i, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i, j + 1] == player) && (data[i, j + 2] == player) && (data[i, j + 3] == 0))
                return true;
            return false;
        }

        //=========================================== Проверка на две горизонтальные открытые точки =========================================
        bool hasHorizontalTwo(int i, int j, ref int player) 
        {
            if ((data[i, j] == 0) && ((data[i, j + 1]) == 0) && ((player = data[i, j + 2]) != 0) && (data[i, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (player == data[i, j + 1]) && (data[i, j + 2] == 0) && (data[i, j + 3] == 0))
                return true;
            if (((player = data[i, j]) != 0) && (data[i, j + 1] == 0) && (data[i, j + 2] == player) && (data[i, j + 3] == 0))
                return true;
            if ((data[i, j] == 0) && ((player = data[i, j + 1]) != 0) && (data[i, j + 2] == 0) && (data[i, j + 3] == player))
                return true;
            return false;
        }

        //======================================== Проверка на ТРИ диагональные открытые точки (1) ==========================================
        bool hasDiagonalThree1(int i, int j, ref int player) 
        {
            if ((data[i, j] == 0) && ((player = data[i + 1, j + 1]) != 0) && (data[i + 2, j + 2] == player) && (data[i + 3, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j + 1] == 0) && (data[i + 2, j + 2] == player) && (data[i + 3, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j + 1] == player) && (data[i + 2, j + 2] == 0) && (data[i + 3, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j + 1] == player) && (data[i + 2, j + 2] == player) && (data[i + 3, j + 3] == 0))
                return true;
            return false;
        }

        //======================================== Проверка на две диагональные открытые точки (1) ==========================================
        bool hasDiagonalTwo1(int i, int j, ref int player)
        {
            if ((data[i, j] == 0) && ((data[i + 1, j + 1]) == 0) && ((player = data[i + 2, j + 2]) != 0) && (data[i + 3, j + 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (player == data[i + 1, j + 1]) && (data[i + 2, j + 2] == 0) && (data[i + 3, j + 3] == 0))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j + 1] == 0) && (data[i + 2, j + 2] == player) && (data[i + 3, j + 3] == 0))
                return true;
            if ((data[i, j] == 0) && ((player = data[i + 1, j + 1]) != 0) && (data[i + 2, j + 2] == 0) && (data[i + 3, j + 3] == player))
                return true;
            return false;
        }

        //======================================== Проверка на ТРИ диагональные открытые точки (2) ==========================================
        bool hasDiagonalThree2(int i, int j, ref int player)
        {
            if ((data[i, j] == 0) && ((player = data[i + 1, j - 1]) != 0) && (data[i + 2, j - 2] == player) && (data[i + 3, j - 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j - 1] == 0) && (data[i + 2, j - 2] == player) && (data[i + 3, j - 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j - 1] == player) && (data[i + 2, j - 2] == 0) && (data[i + 3, j - 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j - 1] == player) && (data[i + 2, j - 2] == player) && (data[i + 3, j - 3] == 0))
                return true;
            return false;
        }

        //======================================== Проверка на две диагональные открытые точки (2) ==========================================
        bool hasDiagonalTwo2(int i, int j, ref int player)
        {
            if ((data[i, j] == 0) && ((data[i + 1, j - 1]) == 0) && ((player = data[i + 2, j - 2]) != 0) && (data[i + 3, j - 3] == player))
                return true;
            if (((player = data[i, j]) != 0) && (player == data[i + 1, j - 1]) && (data[i + 2, j - 2] == 0) && (data[i + 3, j - 3] == 0))
                return true;
            if (((player = data[i, j]) != 0) && (data[i + 1, j - 1] == 0) && (data[i + 2, j - 2] == player) && (data[i + 3, j - 3] == 0))
                return true;
            if ((data[i, j] == 0) && ((player = data[i + 1, j - 1]) != 0) && (data[i + 2, j - 2] == 0) && (data[i + 3, j - 3] == player))
                return true;
            return false;
        }

        //============================================== Подсчет открытых двоек и троек =====================================================
        void countPieces(ref int three1, ref int three2, ref int two1, ref int two2) 
        {
            three1 = three2 = two1 = two2 = 0;
            int player = 0;

            // Вертикаль 2 и 3
            for (int i = 3; i < 5; i++)
                for (int j = 0; j < 7; j++)
                {
                    if (hasVerticalThree(i, j, ref player))
                        if (player == 1)
                            ++three1;
                        else
                            ++three2;
                    else if (hasVerticalTwo(i, j, ref player))
                        if (player == 1)
                            ++two1;
                        else
                            ++two2;
                }

            //Горизонталь 2 и 3
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (hasHorizontalThree(i, j, ref player))
                        if (player == 1)
                            ++three1;
                        else
                            ++three2;
                    else if (hasHorizontalTwo(i, j, ref player))
                        if (player == 1)
                            ++two1;
                        else
                            ++two2;
                }
            
            // Диагональ (1) 2 и 3
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 4; j++)
                {
                    if (hasDiagonalThree1(i, j, ref player))
                        if (player == 1)
                            ++three1;
                        else
                            ++three2;
                    else if (hasDiagonalTwo1(i, j, ref player))
                        if (player == 1)
                            ++two1;
                        else
                            ++two2;
                }

            // Диагональ (2) 2 и 3
            for (int i = 0; i < 3; i++)
                for (int j = 3; j < 7; j++)
                {
                    if (hasDiagonalThree2(i, j, ref player))
                        if (player == 1)
                            ++three1;
                        else
                            ++three2;
                    else if (hasDiagonalTwo2(i, j, ref player))
                        if (player == 1)
                            ++two1;
                        else
                            ++two2;
                }
        }

        //===================================================== Вычисляем эвристику =========================================================
        public int countHeuristic() 
        {
            if (this.isDeadState())
                return 250000; // Ничья

            int player = 0;
            if (this.isWinningState(ref player))
            {
                if (player == 1)
                    return 100000; //Победа AI
                else
                    return -100000; //Победа игрока
            }

            int three1 = 0, three2 = 0, two1 = 0, two2 = 0;
            countPieces(ref three1, ref three2, ref two1, ref two2);
            return three1 * 3 + two1 * 2 - three2 * 3 + two2 * 2;
        }

        //====================================================== Делаем ход ================================================================
        public State newTurn(bool flag, int ii, int jj) 
        {
            State st = this.cloneState();
            if (flag)
                st.data[ii, jj] = 1;
            else
                st.data[ii, jj] = 2;
            return st;
        }

        //================================================ Строим варианты событий =========================================================
        public List<State> buildAllSuccessors(bool flag) 
        {
            List<State> res = new List<State>();
            for (int i = 0; i < 6; i++)
                for (int j = 0; j < 7; j++)
                    if (data[i, j] == 0 && (i == 5 || data[i + 1, j] != 0)) //Если поле пусто && (Дно || Под нами есть фишка)
                        res.Add(newTurn(flag, i, j));
            return res;
        }
    }

    // Result = Состояние + номер + глубина
    public class Result
    {
        public State st;
        public int number, depth;

        public Result(State s, int n, int d)
        {
            st = s;
            number = n;
            depth = d;
        }
    }
}
