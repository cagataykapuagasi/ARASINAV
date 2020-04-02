using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using net.zemberek.erisim;
using net.zemberek.tr.yapi;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;
using System.Reflection;

namespace ARASINAV
{
    public class Data
    {
        List<string> stop_words = new List<string>();
        Zemberek zemberek = new Zemberek(new TurkiyeTurkcesi());
        static public List<Word> totalWords = new List<Word>();
        static public List<GuessWord> guessWords = new List<GuessWord>();
        static public List<Column> list = new List<Column>();
        static public List<Test> testData = new List<Test>();

        public Data()
        {
            init();
        }


        void init()
        {

            try //klasördeki verilerin okunması source: https://stackoverflow.com/questions/5840443/how-to-read-all-files-inside-particular-folder
            {
                Console.WriteLine("Processing...");
                stop_words.AddRange(File.ReadLines("stop-words.txt"));


                readAndHandle("1"); //eski projedeki veriler eğitim için kullanıldı.
                readAndHandle("2");
                readAndHandle("3");
                readAndHandle("metinler"); //yeni veriler ise test için

                foreach (Column c in list) { //her satır için tüm kelimeler koyulup binary şeklinde yapıldı.
                    c.totalWords = totalWords;
                    c.handleTotalWords();
                }

                foreach (Test t in testData)
                {
                    t.totalWords = totalWords;
                    t.handleTotalWords();
                }

               
                Console.WriteLine();
                Console.WriteLine("The data processing was finished.");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Hata: " + e.Message);

                Environment.Exit(0);  //hata olma durumunda işlemlerin devam etmemesi için
            }
        }

        void readAndHandle(string num) //dosyayı okuyup gerekli işlemlerden sonra frekans sayılarını hesaplayıp listeye ekler.
        {
            Console.WriteLine();
            Console.Write("Reading folder " + num + "...");

            foreach (string file in Directory.EnumerateFiles(num))
            {
                var tokenizedStrings = tokenizer(File.ReadAllText(file).ToLower(new CultureInfo("tr-TR", false)));
                var stopWordedStrings = stopWords(tokenizedStrings);
                var stemmedStrings = stemming(stopWordedStrings);

                if (num != "metinler")
                {
                    foreach(string i in tokenizedStrings)
                    {
                        GuessWord g = guessWords.Find(x => x.name == i && x.className == num);

                        if (g == null)
                        {
                            g = new GuessWord(i, num);
                            guessWords.Add(g);
                        }
                        else
                        {
                            g.incrementCount();
                        }
                    }
                }

                    if (num !="metinler")
                {
                    Column column = new Column(stemmedStrings, num);
                    list.Add(column);

                    foreach (string i in stemmedStrings) //unique olarak geçen kelimeleri toplamak
                    {
                        Word a = totalWords.Find(x => x.name == i);
                        if (a == null)
                        {
                            a = new Word(i);
                            totalWords.Add(a);
                        }
                    }
                } else
                {
                    Test test = new Test(stemmedStrings, num);
                    testData.Add(test);
                }
            }

            Console.Write(" Ok.");
            Console.WriteLine();
        }


        string[] tokenizer(string value) //cümleyi kelimelere parçalama
        {
            return value.Split(null);
        }

        string[] stopWords(string[] array) //dosyadaki stopwordlerde geçen elemanın silinmesi
        {
            List<string> newArray = new List<string>(array);

            foreach (string a in array)
            {
                foreach (string s in stop_words)
                {

                    if (a == s)
                    {
                        newArray.Remove(a);
                        break;
                    }
                }
            }

            return newArray.ToArray();
        }

        string[] stemming(string[] array) //zemberek kullanılarak kök haline getirme
        {
            List<string> newArray = new List<string>(array);

            foreach (string i in array)
            {

                var stems = zemberek.kelimeCozumle(i);
                if (stems.Length > 0)
                {
                    newArray.Add(stems[0].kok().icerik());
                }

            }


            return newArray.ToArray();
        }

    }
}
