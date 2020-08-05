using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
// Import the extension method namespace.
using StringExtensions.StringExtension;

namespace StringExtensions {
  class Program {
	static void Main(string[] args) {

	  //string s = "The quick brown fox jumped over the lazy dog. \n sdjghsjd ghs ghsdjg hs sg sa";
	  //Console.WriteLine("Input string: "+ s );
	  //Console.WriteLine(); 

	  //// Call the method as if it were an
	  //// instance method on the type. Note that the first
	  //// parameter is not specified by the calling code.
	  //int i = s.WordCount();
	  //System.Console.WriteLine("Word count of s is {0}",i);
	  //Console.ReadLine();


	  // ========================================
	  // **** Test indexOf ****
	  // ========================================
	  string str = "   Trong đầm gì đẹp bằng sen, nhị xanh bông trắng!";
	  string value = "Tron";

	  Console.WriteLine("str = " + str);
	  Console.WriteLine("value = " + value);
	  Console.WriteLine("CurrentCulture : " + 
		((str.IndexOf(value,0,StringComparison.CurrentCulture))>-1? true : false).ToString());
	  Console.WriteLine("CurrentCultureIgnoreCase : " + 
		((str.IndexOf(value,0,StringComparison.CurrentCultureIgnoreCase))>-1 ? true : false).ToString());
	  Console.WriteLine("InvariantCulture : " + 
		((str.IndexOf(value,0,StringComparison.InvariantCulture))>-1 ? true : false).ToString());
	  Console.WriteLine("InvariantCultureIgnoreCase : " + 
		((str.IndexOf(value,0,StringComparison.InvariantCultureIgnoreCase))>-1 ? true : false).ToString());
	  Console.WriteLine("Ordinal : " + 
		((str.IndexOf(value,0,StringComparison.Ordinal))>-1 ? true : false).ToString());
	  Console.WriteLine("OrdinalIgnoreCase : " + 
		((str.IndexOf(value,0,StringComparison.OrdinalIgnoreCase))>-1 ? true : false).ToString());

	  Console.ReadLine();
	}
  }
}
