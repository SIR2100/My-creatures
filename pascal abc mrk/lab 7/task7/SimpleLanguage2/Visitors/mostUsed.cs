/*
    2. Написать визитор, определяющий наиболее часто используемую переменную.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProgramTree;

namespace SimpleLang.Visitors
{
    class mostUsed : AutoVisitor
    {
        private Dictionary<String, int> vars = new Dictionary<string, int>();

        public string OffenVariable
        {
            get
            {
                if (vars.Count > 0)
                {
                    string name = "";
                    int count = 0;
                    foreach (var pair in vars)
                    {
                        if (pair.Value > count)
                        {
                            name = pair.Key;
                            count = pair.Value;
                        }
                    }
                    return name;
                }
                else
                {
                    return "";
                }
            }
        }

        public override void VisitIdNode(IdNode id)
        {
            base.VisitIdNode(id);
            if (vars.ContainsKey(id.Name))
                vars[id.Name] += 1;
            else
            {
                vars.Add(id.Name, 1);
            }
        }
    }
}
