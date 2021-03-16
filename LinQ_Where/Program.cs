using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinQ_Where {
  class Program {
	static void Main(string[] args) {

	  Where_LocChuoiDuaTrenMotViTuLienQuanDenChiMucPhanTu();
	  Console.ReadKey();
	}

	/// <summary>
	/// Ví dụ mã sau đây trình bày cách sử dụng Where <TSource> (IEnumerable <TSource>, Func <TSource, Int32, Boolean>) để lọc một chuỗi dựa trên một vị từ liên quan đến chỉ mục của mỗi phần tử.
	/// </summary>
	static void Where_LocChuoiDuaTrenMotViTuLienQuanDenChiMucPhanTu() {
	  int[] numbers = { 0,30,20,15,90,85,40,75 }; // *10 {0,10,20,30,40,50,60,70}

	  // IEnumerable<int> query =
	  //  numbers.Where((number,index) => number <= index * 10);

	  // foreach(int number in query) {
	  //Console.WriteLine(number);
	  // }

	  var query = numbers
		.Where((number,index) => number <= index * 10);

	  foreach(var obj in query) {
		Console.WriteLine("{0}",obj);
	  }
	  /*
	   This code produces the following output:

	   0
	   20
	   15
	   40
	  */
	}
  }
}
