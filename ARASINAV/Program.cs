using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARASINAV
{
    class Program
    {
        static void Main(string[] args)
        {
            Data data = new Data();
            //Accord accord = new Accord();

            foreach(GuessWord g in Data.guessWords)
            {
               // Console.WriteLine(g.name);
            }

            getWordFromUser();

            

            Console.Read();
        }

        static void getWordFromUser()
        {
            Console.WriteLine("Tahmin için kelime yazın yada çıkmak için 0 yazın: ");
            string word = Console.ReadLine();

            if (word == "0")
            {
                Environment.Exit(0);
            }

            int theBiggest = 0;
            string theBiggestClass = "1";
            double p = 0;

            for(int i = 1;i< 4; i++)
            {
                GuessWord g = Data.guessWords.Find(x => x.name == word && x.className == $"{i}");
                if(g != null && g.count > theBiggest)
                {
                    theBiggest = g.count;
                    theBiggestClass = g.className;
                }
            }

            p = theBiggest * 1.0 / Data.guessWords.Count;
            if(theBiggest == 0)
            {
                Console.WriteLine("Kelime için tahmin yapılamadı.");
            } else
            {
                Console.WriteLine(Accord.numberToText(int.Parse(theBiggestClass) -1));
                Console.WriteLine($"p={p}");
            }
            

            getWordFromUser();
        }

    }
}
