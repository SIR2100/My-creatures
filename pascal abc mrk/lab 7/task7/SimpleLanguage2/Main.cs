using System;
using System.IO;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using SimpleScanner;
using SimpleParser;
using SimpleLang.Visitors;

namespace SimpleCompiler
{
    public class SimpleCompilerMain
    {

        public static void Main()
        {
            string FileName = @"..\..\a.txt";
            try
            {
                string Text = File.ReadAllText(FileName);

                Scanner scanner = new Scanner();
                scanner.SetSource(Text, 0);

                Parser parser = new Parser(scanner);

                var b = parser.Parse();
                if (!b)
                    Console.WriteLine("Ошибка");
                else
                {
                    Console.WriteLine("Синтаксическое дерево построено");

                    var avis = new AssignCountVisitor();
                    parser.root.Visit(avis);
                    Console.WriteLine("\nКоличество присваиваний = {0}", avis.Count);
                    Console.WriteLine();

                    var pp = new PrettyPrintVisitor();
                    parser.root.Visit(pp);
                    Console.WriteLine(pp.Text);

                    var avg = new averageOper();
                    parser.root.Visit(avg);
                    Console.WriteLine("\n1:\nсреднее количество операторов в теле каждого цикла программы = {0}", avg.avgCountStatment);

                    var often = new mostUsed();
                    parser.root.Visit(often);
                    Console.WriteLine("\n2:\nнаиболее часто используемая переменная = {0}", often.OffenVariable);

                    var casts = new costOfOneExpr();
                    parser.root.Visit(casts);
                    Console.WriteLine("\n3:\nстоимость вычисления каждого выражения");
                    casts.PrintCasts();

                    var changeName = new changeName("a", "variable");
                    parser.root.Visit(changeName);
                    Console.WriteLine("\n4:\nимя переменной после замены:");
                    parser.root.Visit(pp);
                    Console.WriteLine(pp.Text);

                    var depth = new maxCycleDepth();
                    parser.root.Visit(depth);
                    Console.WriteLine("\n5:\nмаксимальная вложенность циклов = {0}", depth.MaxDepth);
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Файл {0} не найден", FileName);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}", e);
            }

            Console.ReadLine();
        }

    }
}
