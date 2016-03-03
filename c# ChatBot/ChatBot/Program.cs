using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIMLbot;

namespace ChatBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Bot myBot = new Bot();
            myBot.loadSettings();
            User myUser = new User("consoleUser", myBot);
            myBot.isAcceptingUserInput = false;
            myBot.loadAIMLFromFiles();
            myBot.isAcceptingUserInput = true;
            Console.Write("Hello. English please \n");
            while (true)
            {
                Console.Write("Вы: ");
                string input = Console.ReadLine();
                if (input.ToLower() == "выход")
                {
                    break;
                }
                else
                {
                    Request r = new Request(input, myUser, myBot);
                    Result res = myBot.Chat(r);
                    Console.WriteLine("Бот: " + res.Output);
                }
            }
        }
    }
}
