using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARASINAV
{
    public class Column //Herbir text dosyasını ifade eder.
    {
        public List<Word> words = new List<Word>();
        public List<Word> totalWords = null;
        public List<double> input = new List<double>();
        public string className;

        public Column(string[] _words, string className)
        {
            this.className = className;

            List<string> wordsList = new List<string>();
            wordsList.AddRange(_words);
        }

        public void handleTotalWords()
        {

            foreach(Word w in words)
            {
                Word a = totalWords.Find(x => x.name == w.name);
                if (a != null)
                    a.changeInput();
            }

            foreach(Word w in totalWords)
            {
                input.Add(w.input);
            }
        }
    }
}
