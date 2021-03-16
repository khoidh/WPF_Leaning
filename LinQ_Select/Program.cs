using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinQ_Select {
  class Program {
	static void Main(string[] args) {
	  Select_ThamChieuLenMotChuoiGiaTriSuDungChiMucTungPhanTu();
	  Console.ReadKey();

	  Select_ThamChieuMotChuoiGiaTri();
	  Console.ReadKey();
	}

	/// <summary>
	/// Ví dụ mã sau đây trình bày cách sử dụng Select <TSource, TResult> (IEnumerable <TSource>, Func <TSource, TResult>) để chiếu trên một chuỗi giá trị.
	/// </summary>
	static void Select_ThamChieuLenMotChuoiGiaTriSuDungChiMucTungPhanTu() {
	  string[] fruits = { "apple", "banana", "mango", "orange",
					  "passionfruit", "grape" };

	  var query =
		  fruits.Select((fruit,index) =>
							new { index,str = fruit.Substring(0,index) });

	  foreach(var obj in query) {
		Console.WriteLine("{0}",obj);
	  }

	  /*
	   This code produces the following output:

	   {index=0, str=}
	   {index=1, str=b}
	   {index=2, str=ma}
	   {index=3, str=ora}
	   {index=4, str=pass}
	   {index=5, str=grape}
	  */
	}

	/// <summary>
	/// Ví dụ mã sau đây trình bày cách sử dụng Select <TSource, TResult> (IEnumerable <TSource>, Func <TSource, TResult>) để chiếu trên một chuỗi giá trị.
	/// </summary>
	static void Select_ThamChieuMotChuoiGiaTri() {
	  IEnumerable<int> squares =
		  Enumerable.Range(1,10).Select(x => x * x);

	  foreach(int num in squares) {
		Console.WriteLine(num);
	  }
	  /*
	   This code produces the following output:

	   1
	   4
	   9
	   16
	   25
	   36
	   49
	   64
	   81
	   100
	  */
	}
  }
}
