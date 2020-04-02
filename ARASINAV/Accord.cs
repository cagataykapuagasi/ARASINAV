using Accord.MachineLearning.VectorMachines.Learning;
using Accord.Statistics.Kernels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARASINAV
{
    class Accord
    {

        public Accord()
        {
            init();
        }

      
        void init()
        {
            Console.Write("Training...");
            List<double[]> list = new List<double[]>();
            List<double[]> testList = new List<double[]>();
            List<int> outputsList = new List<int>();

            foreach(Column c in Data.list)
            {
                list.Add(c.input.ToArray());
                outputsList.Add(int.Parse(c.className) -1);
            }


            foreach (Test t in Data.testData)
            {
                testList.Add(t.input.ToArray());
            }

           
            var teacher = new MulticlassSupportVectorLearning<Linear>()
            {
                Learner = (p) => new SequentialMinimalOptimization<Linear>()
                {
                    Complexity = 10000.0 // Create a hard SVM
                }
            };

            var svm = teacher.Learn(list.ToArray(), outputsList.ToArray());

            Console.WriteLine("Ok.");

            int[] answers = svm.Decide(testList.ToArray());

            for(int i = 0;i< answers.Length;i++)
            {
                Console.WriteLine($"{i}.txt      {numberToText(answers[i])}");
            }
          
        }

        public static string numberToText(int n)
        {
            switch (n)
            {
                case 0:
                    return "Olumlu";
                case 1:
                    return "Olumsuz";
                default:
                    return "Belirsiz";
            }
        }
    }
}
