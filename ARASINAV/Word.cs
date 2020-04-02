using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARASINAV
{
    public class Word //herbir kelime için class.
    {
        public string name;
        public double input = 0;

        public Word(string name)
        {
            this.name = name;
        }

       public void changeInput()
        {
            input = 1;
        }
    }
}
