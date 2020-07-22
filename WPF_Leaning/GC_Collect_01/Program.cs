using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC_Collect_01
{
    class Product
    {
        private string product_name;
        public Product(string name)
        {
            this.product_name = name;
            Console.WriteLine("Taoj - " + product_name);
        }

        ~Product()
        {
            Console.WriteLine("Huyr - " + product_name);
        }
    }

    class Program
    {
        static void Test()
        {
            Product p = new Product("ABC");
            p = null;
        }

        static void Main(string[] args)
        {
            Test();
            Console.ReadLine();  //Chua giai phong bo nho
            GC.Collect();
            Console.ReadLine();     //Da giai phogn bo nho
        }
    }
}
