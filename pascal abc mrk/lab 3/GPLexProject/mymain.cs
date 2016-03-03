using System;
using System.IO;
using SimpleScanner;
using ScannerHelper;
using System.Collections.Generic;

namespace Main
{
    class mymain
    {
        static void Main(string[] args)
        {
            // Чтобы вещественные числа распознавались и отображались в формате 3.14 (а не 3,14 как в русской Culture)
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
            List<string> list = new List<string>();
            double avlen = 0;
            int sumInt = 0;
            double sumDouble = 0;
            int min = 1000, max = 0;
            var fname = @"..\..\a.txt";
            Console.WriteLine(File.ReadAllText(fname));
            Console.WriteLine("-------------------------");

            Scanner scanner = new Scanner(new FileStream(fname, FileMode.Open));

            int tok = 0;
            do
            {
                tok = scanner.yylex();
                if (tok == (int)Tok.EOF)
                    break;
                if (tok == (int)Tok.ID)
                    list.Add(scanner.TokToString((Tok)tok).Substring(3));
                if (tok == (int)Tok.RNUM)
                    sumDouble += scanner.LexValueDouble;
                if (tok == (int)Tok.EOF)
                    break;
                if (tok == (int)Tok.INUM)
                    sumInt += scanner.LexValueInt;

                Console.WriteLine(scanner.TokToString((Tok)tok));
            } while (true);
            Console.WriteLine("-------------------------");
            Console.WriteLine("count(id) =  " + list.Count.ToString());
            foreach (var item in list)
            {
                avlen += item.Length;
                if (min > item.Length)
                {
                    min = item.Length;
                }
                if (max < item.Length)
                {
                    max = item.Length;
                }

            }
            Console.WriteLine("maxLength(id) = " + max.ToString());
            Console.WriteLine("minLength(id) = " + min.ToString());
            Console.WriteLine("avrgLength(id) = " + (avlen / list.Count).ToString());
            Console.WriteLine("Int Sum = " + sumInt.ToString());
            Console.WriteLine("Float Sum = " + sumDouble.ToString());
            

            Console.ReadKey();
        }
    }
}
