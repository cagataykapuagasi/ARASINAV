using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARASINAV
{
    public class GuessWord: Word
    {
        public string className;
       public int count;
        public GuessWord(string name, string className): base(name)
        {
            this.className = className;
            incrementCount();
        }

        public void incrementCount()
        {
            count++;
        }
    }
}
