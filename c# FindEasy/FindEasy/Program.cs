using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindEasy
{
    class Program
    {
        static int main()
        {
            int start = 2;
            int to = 100;
            Queue<State> states = new Queue<State>();
            states.Enqueue(new State(start, null, ""));
            State now = states.Dequeue();
            while (now.Value != to)
            {
                states.Enqueue(new State(now.Value + 3, now, "+3"));
                states.Enqueue(new State(now.Value * 2, now, "*2"));
                now = states.Dequeue();
            }
            string result = "";
            int count = 1;
            while (now.Parent != null)
            {
                result = now.Name + result;
                now = now.Parent;
                count++;
            }
            return count;
        }

        static string task_2_2(int value, int to, string result, int count)
        {
            if (value == to)
            {
                return result;
            }
            if (count == 0)
            {
                return "";
            }
            List<string> res = new List<string>();
            res.Add(task_2_2(value + 3, to, result + "+3", count - 1));
            res.Add(task_2_2(value * 2, to, result + "*2", count - 1));
            string min = "";
            foreach (var item in res)
            {
                if (item.Length > 0 && item.Length < min.Length && min.Length > 0)
                {
                    min = item;
                }
                else if (min.Length == 0)
                {
                    min = item;
                }
            }

            return min;
        }

        static void Main(string[] args)
        {
            int x = main();
            for (int i = 0; i < 100; i++)
            {
                string result = task_2_2(2, 100, "", i);
                if (result == "")
                {
                    continue;
                }
                Console.WriteLine("2 (" + result + ") =100");
                Console.WriteLine("Шагов: " + i);
                break;
            }
            
            Console.Read();

        }
    }
}
