using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class maxCycleDepth : AutoVisitor
    {
        private int currentDepth = 0, maxDepth = 0;

        public int MaxDepth
        {
            get { return maxDepth; }
        }

        public override void VisitCycleNode(CycleNode c)
        {
            currentDepth++;
            if (currentDepth > maxDepth)
                maxDepth = currentDepth;
            base.VisitCycleNode(c);
            currentDepth--;
        }
    }
}
