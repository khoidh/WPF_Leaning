using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//===========================================================
//Cấp phát động(Dynamic memory allocation)
//===========================================================

namespace Stack_Overflow01
{
    class Program
    {
        static void Main(string[] args)
        {
            StackTranBoNho();
            Console.ReadLine();
        }

        static int StackNoTranBoNho()
        {
            char[] ch_array = new char[1024 * 1024];

            return 0;
        }

        static int StackTranBoNho()
        {
            char[] ch_array = new char[1024 * 1025];

            return 0;
        }
    }
}
