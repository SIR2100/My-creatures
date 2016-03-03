/*
    1. 1. Ќаписать визитор, определ€ющий среднее количество операторов в теле каждого цикла программы
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;


namespace SimpleLang.Visitors
{
    class averageOper : AutoVisitor
    {
        private int countCycle = 0, sumStatment = 0;
        private bool cycle = false;

        public double avgCountStatment {
            get { return (double) sumStatment/countCycle; }
        }

        public override void VisitCycleNode(CycleNode c)
        {
            cycle = true;
            base.VisitCycleNode(c);
            countCycle++;
            cycle = false;
        }

        public override void VisitBlockNode(BlockNode bl)
        {
            foreach (var st in bl.StList)
            {
                st.Visit(this);
                if (cycle)
                    sumStatment++;
            }
        }
    }
}
