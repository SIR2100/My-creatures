using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProdModel
{
    //-----------------------------------------------------ЛОГИКА----------------------------------------------------//

    class Logic
    {
        List<Production> ruleBase = new List<Production>(); // база правил
        List<string> knowledgeBase = new List<string>(); // база знаний

        public Logic() { }

        public Logic(List<Production> rB, List<string> kB)
        {
            ruleBase = rB;
            knowledgeBase = kB;
        }

        public void addRule(Production p) // добавить продукцию
        {
            ruleBase.Add(p);
        }

        public void addFact(string s) // добавить факт
        {
            knowledgeBase.Add(s);
        }

        public void delRules() // удалить продукции
        {
            ruleBase.Clear();
        }

        public void delFacts() // удалить факты
        {
            knowledgeBase.Clear();
        }

        public string[] rulesToLines() // преобразуем список правил в список строк
        {
            string res = "";
            for (int i = 0; i < ruleBase.Count; i++)
                res += ruleBase[i].ToString() + '\n';
            return res.Split('\n');
        }

        public string[] factsToLines() // преобразуем список фактов в список строк
        {
            string res = "";
            for (int i = 0; i < knowledgeBase.Count; i++)
                res += knowledgeBase[i].ToString() + '\n';
            return res.Split('\n');
        }

        public string forwardChaining(string provableFact) // прямой вывод
        {
            // если факт уже доказан
            if (knowledgeBase.Contains(provableFact))
                return "Факт " + provableFact + " есть в базе знаний." + '\n';

            string res = "";

            bool flag;
            do
            {
                flag = false;
                foreach (Production rule in ruleBase)
                    // если правило можно применить и в базе данных еще нет такого факта
                    if (rule.isSuitable(knowledgeBase) && !knowledgeBase.Contains(rule.getConsequence()))
                    {
                        res += "Применяем продукцию " + rule.ToString() + ", выводим " + rule.getConsequence() + "." + '\n';
                        this.addFact(rule.getConsequence());
                        flag = true;
                        if (rule.getConsequence() == provableFact)
                            return res + "Факт " + provableFact + " доказан.";
                    }
            } while (flag); // пока мы выводим новые факты
            return res + "Больше применимых продукций нет." + '\n' + "Факт " + provableFact + " не доказан.";
        }

        private int countUnknownConditions(Production p) // количество условий, не являющихся фактами
        {
            int count = 0;
            List<string> conditions = p.getConditions();
            foreach (string s in conditions)
                if (!knowledgeBase.Contains(s))
                    ++count;
            return count;
        }

        public bool backwardChaining(string provableFact, ref string output, ref string tab) // обратный вывод
        {
            // если факт уже доказан
            if (knowledgeBase.Contains(provableFact))
            {
                output += tab + "Факт " + provableFact + " есть в базе знаний." + '\n';
                return true;
            }

            // ищем все продукции, в которых наш факт явлется следствием 
            List<Production> acceptableRules = new List<Production>();
            for (int i = 0; i < ruleBase.Count; i++)
                if (ruleBase[i].getConsequence() == provableFact)
                    acceptableRules.Add(ruleBase[i]);

            tab = "   " + tab;

            // если подходящих продукций нет
            if (acceptableRules.Count == 0)
            {
                output += tab + "Нет подходящих продукций." + '\n';
                tab = tab.Substring(3, tab.Length - 3);
                return false;
            }

            // сортируем продукции
            acceptableRules.Sort(delegate(Production p1, Production p2)
            {
                int n1 = countUnknownConditions(p1);
                int n2 = countUnknownConditions(p2);
                if (n1 == n2) return 0;
                else if (n1 < n2) return -1;
                else return 1;
            });

            for (int i = 0; i < acceptableRules.Count; i++)
            {
                output += tab + "Смотрим продукцию " + acceptableRules[i].ToString() + "." + '\n';
                tab = "   " + tab;
                List<string> conditions = acceptableRules[i].getConditions();
                foreach (string s in conditions)
                {
                    output += tab + "Доказываем факт " + s + "." + '\n';
                    if (backwardChaining(s, ref output, ref tab) == false)
                    {
                        tab = tab.Substring(3, tab.Length - 3);
                        output += tab + "Факт " + provableFact + " нельзя доказать. " + '\n';
                        return false;
                    }
                }
                knowledgeBase.Add(provableFact);
                tab = tab.Substring(3, tab.Length - 3);
                output += tab + "Факт " + provableFact + " получен из продукции " + acceptableRules[i].ToString() + "." + '\n';
                tab = tab.Substring(3, tab.Length - 3);
                return true;
            }
            return false;
        }
    }

    //---------------------------------------------------ПРОДУКЦИЯ---------------------------------------------------//

    public class Production
    {
        List<string> conditions = new List<string>(); // условия
        string consequence; // следствие

        public Production() { }

        public Production(List<string> cond, string cons)
        {
            conditions = cond;
            consequence = cons;
        }

        public void addCondition(string cond) // добавить условие
        {
            conditions.Add(cond);
        }

        public List<string> getConditions() // получить условия
        {
            return conditions;
        }

        public void setConsequence(string cons) // задать следствие
        {
            consequence = cons;
        }

        public string getConsequence() // получить следствие
        {
            return consequence;
        }

        public bool isSuitable(List<string> facts) // можно ли применить правило при заданном наборе фактов
        {
            foreach (string s in conditions)
                if (!facts.Contains(s))
                    return false;
            return true;
        }

        public override string ToString()
        {
            if (conditions.Count == 0)
                return "";
            string str = conditions[0];
            for (int i = 1; i < conditions.Count; i++)
                str += ", " + conditions[i];
            str += " => " + consequence;
            return str;
        }
    }
}
