using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolution
{
    class Resolution
    {
        public List<Disjunct> disjuncts;

        //===================================================== Решатель ==================================================
        public Resolution(List<string> l, string provableVar) // На вход 
        {
            Parse p = new Parse(l);
            disjuncts = p.getDisjuncts();
            Atom a = new Atom(provableVar);
            a.changeNegative();
            disjuncts.Add(new Disjunct(a));
        }

        //=============================================== Резольвенты =====================================================
        private bool Resolvent(Disjunct d1, Disjunct d2)
        {
            bool flag = false;
            for (int i = 0; i < d1.atoms.Count; i++)
                for (int j = 0; j < d2.atoms.Count; j++)
                    if (d1.atoms[i].name == d2.atoms[j].name && d1.atoms[i].isNegative() != d2.atoms[j].isNegative())
                    {
                        if (d1.atoms[i].isNegative())
                        {
                            d1.atoms.RemoveAt(i);
                            --i;
                            flag = true;
                        }
                        else
                        {
                            d2.atoms.RemoveAt(j);
                            --j;
                            flag = true;
                        }
                    }
            return flag;
        }

        //========================================== Вывод результата (True || False) =======================================
        public bool Process()
        {
            bool flag = true;
            while (flag)
            {
                flag = false;
                for (int i = 0; i < disjuncts.Count; i++)
                    if (disjuncts[i].isPositive())
                        for (int j = 0; j < disjuncts.Count; j++)
                        {
                            if (i == j)
                                continue;
                            flag = flag || Resolvent(disjuncts[i], disjuncts[j]);
                            if (disjuncts[i].atoms.Count == 0 || disjuncts[j].atoms.Count == 0)
                                return true;
                        }
            }
            return false;
        }
    }

    //===================================================== Класс Atom =================================================
    class Atom
    {
        public string name;
        private bool negative;

        public Atom(string str)
        {
            if (str[0] == '-')
            {
                negative = true;
                name = str.Remove(0, 1);
            }
            else
            {
                negative = false;
                name = str;
            }
        }

        public bool isNegative()
        {
            return negative;
        }

        public void changeNegative()
        {
            negative = !negative;
        }

        public override string ToString()
        {
            if (negative)
                return "-" + name;
            else
                return name;
        }
    }

    //============================================================ Дизъюнкты =====================================================
    class Disjunct
    {
        public List<Atom> atoms = new List<Atom>();

        public Disjunct(){}

        public Disjunct(string str)
        {
            atoms.Add(new Atom(str));
        }

        public Disjunct(Atom a)
        {
            atoms.Add(a);
        }

        public void addAtom(Atom a)
        {
            atoms.Add(a);
        }

        public bool isPositive()
        {
            foreach (Atom a in atoms)
                if (a.isNegative())
                    return false;
            return true;
        }

        public override string ToString()
        {
            if (atoms.Count == 0)
                return "0";
            string s = atoms[0].ToString();
            for (int i = 1; i < atoms.Count; i++)
                s += "||" + atoms[i].ToString();
            return s;
        }
    }

    //========================================================== Парсер =======================================================
    class Parse
    {
        List<string> data;
        List<Disjunct> disjuncts = new List<Disjunct>();

        public Parse(List<string> l)
        {
            data = l;
        }

        public List<Disjunct> getDisjuncts()
        {
            foreach(string str in data)
            {
                if (str.Contains("->"))
                    parseImplication(str);
                else if (str.Contains("||"))
                    parseDisjunction(str);
                else if (str.Contains("&&"))
                    parseConjunction(str);
                else
                    disjuncts.Add(new Disjunct(str));
            }
            return disjuncts;
        }

        private void parseImplication(string str) // разбиваем импликацию
        {
            string[] separator = new string[1] { "->" };
            string[] split = str.Split(separator, 2, StringSplitOptions.None);
            Atom a1 = new Atom(split[0]);
            a1.changeNegative();
            Atom a2 = new Atom(split[1]);
            Disjunct d = new Disjunct();
            d.addAtom(a1);
            d.addAtom(a2);
            disjuncts.Add(d);
        }

        private void parseDisjunction(string str) // разбиваем дизъюнкцию
        {
            string[] separator = new string[1] { "||" };
            string[] split = str.Split(separator, StringSplitOptions.None);
            Disjunct d = new Disjunct();
            foreach(string s in split)
                d.addAtom(new Atom(s));
            disjuncts.Add(d);
        }

        private void parseConjunction(string str) // разбиваем конъюнкцию
        {
            string[] separator = new string[1] { "&&" };
            string[] split = str.Split(separator, StringSplitOptions.None);
            foreach (string s in split)
                disjuncts.Add(new Disjunct(s));       
        }

    }
}
