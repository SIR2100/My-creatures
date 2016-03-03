using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindEasy
{
    class State
    {
        public State Parent;
        public int Value;
        public string Name;
        public State(int v, State p, string n)
        {
            Parent = p;
            Value = v;
            Name = n;
        }
    }
}
