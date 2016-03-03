/*
    3. Написать визитор, определяющий стоимость вычисления каждого выражения. Стоимость операций умножения и деления положить равной 3, а стоимость сложения и вычитания - равной 1.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class costOfOneExpr : AutoVisitor
    {
        private Dictionary<string, int> casts = new Dictionary<string, int>();
        private int _cast;
        private int numbAsg = 0, numbCycle = 0, numbWrite = 0;

        public void PrintCasts()
        {
            foreach (KeyValuePair<string, int> cast in casts)
            {
                Console.WriteLine("{0} = {1}", cast.Key, cast.Value);
            }
        }

        public override void VisitBinOpNode(BinOpNode binop)
        {
            base.VisitBinOpNode(binop);
            switch (binop.Op)
            {
                case '/':
                case '*':
                    _cast += 3;
                    break;
                default:
                    _cast++;
                    break;
            }
        }

        public override void VisitAssignNode(AssignNode a)
        {
            base.VisitAssignNode(a);
            casts.Add("assign" + numbAsg++.ToString(), _cast);
            _cast = 0;
        }

        public override void VisitCycleNode(CycleNode c)
        {
            c.Expr.Visit(this);
            casts.Add("cycle" + numbCycle++.ToString(), _cast);
            _cast = 0;
            c.Stat.Visit(this);
        }

        public override void VisitWriteNode(WriteNode w)
        {
            base.VisitWriteNode(w);
            casts.Add("write" + numbWrite++.ToString(), _cast);
            _cast = 0;
        }
    }
}
