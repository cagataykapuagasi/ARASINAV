using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARASINAV
{
    public class Test: Column
    {
        public Test(string[] _words, string className): base(_words, className)
        {
            List<string> wordsList = new List<string>();
            wordsList.AddRange(_words);
        }
    }
}
