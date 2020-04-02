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
            Data data = new Data(); //verilerin işlendiği class
            Accord accord = new Accord(); //test datalarının tahmin yapıldığı class

            getWordFromUser();

            Console.Read();
        }

        static void getWordFromUser() //kelimenin tahmini için
        {
            Console.WriteLine();
            Console.WriteLine("Tahmin için kelime yazın yada çıkmak için 0 yazın: ");
            string word = Console.ReadLine();

            if (word == "0")
            {
                Environment.Exit(0);
            }

            int theBiggest = 0;
            string theBiggestClass = "1";
            double p = 0;

            for(int i = 1;i< 4; i++) //tüm classlar gezilir ve en fazla sayıda olandan tahmin yapılır.
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
