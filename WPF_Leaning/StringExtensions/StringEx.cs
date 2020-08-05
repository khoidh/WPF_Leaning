using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringExtensions.StringEx {
  public static class StringEx {
	// This is the extension method.
	// The first parameter takes the "this" modifier
	// and specifies the type for which the method is defined.
	public static int WordCount(this String str) {
	  return str.Split(new char[] { ' ','.','?' },StringSplitOptions.RemoveEmptyEntries).Length;
	}
  }

}
