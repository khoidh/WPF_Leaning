using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThuatToan_Learning.List_Collection
{
    static class ListDiffAndGetDistinceValue
    {
        internal static void run()
        {
            var A = new List<string>() { "A", "B", "C", "D" };
            var B = new List<string>() { "A", "E", "F", "G" };

            A.Except(B).ToList().ForEach(i => Console.Write("{0}\t", i));
            // outputs List<string>(2) { "B", "C", "D" }
            Console.WriteLine();

            B.Except(A).ToList().ForEach(i => Console.Write("{0}\t", i));
            // outputs List<string>(2) { "E", "F", "G" }
            Console.WriteLine();

            B.Intersect(A).ToList().ForEach(i => Console.Write("{0}\t", i));
            // outputs List<string>(2) { "A" }
            Console.WriteLine();

        }

        internal static void runObject()
        {
            List<string> dapanA = new List<string> { "ABC", "A" };
            List<string> dapanB = new List<string>(dapanA);

            var A = new List<object>() { "A", dapanA, "C", "D" };
            var B = new List<object>() { "A", dapanB, "F", "G" };

            A.Except(B).ToList().ForEach(i => Console.Write("{0}\t", i));
            // outputs List<string>(2) { "B", "C", "D" }
            Console.WriteLine();

            //B.Except(A).ToList().ForEach(i => Console.Write("{0}\t", i));
            //// outputs List<string>(2) { "E", "F", "G" }
            //Console.WriteLine();

            B.Union(A).Distinct().ToList().ForEach(i => Console.Write("{0}\t", i));
            Console.WriteLine();

            B.Intersect(A).ToList().ForEach(i => Console.Write("{0}\t", i));
            // outputs List<string>(2) { "A" }
            Console.WriteLine();

        }
    }
}
