using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace StringExtensions.StringExtension {
  public static class StringExtension {

	public static int WordCount(this String str) {
	  return str.Split(new char[] { ' ','.','?' },
					   StringSplitOptions.RemoveEmptyEntries).Length;
	}

	public static int IndexOfEx(this String str,String value,int startIndex) {

	  var result = str.IndexOf(value,startIndex,StringComparison.InvariantCulture);
	  if(result >= 0)
		return result;

	  return str.IndexOfNoUnicode(value,startIndex);
	}

	private static int IndexOfNoUnicode(this String str,String value,int startIndex) {
	  str= str.RemoveUnicode();
	  value = value.RemoveUnicode();

	  return str.IndexOf(value,startIndex,StringComparison.InvariantCulture);
	}

	static string RemoveUnicode(this string input) {
	  string result = input.ToLower();
	  result = Regex.Replace(result,"à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ|/g","a");
	  result = Regex.Replace(result,"è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ|/g","e");
	  result = Regex.Replace(result,"ì|í|ị|ỉ|ĩ|/g","i");
	  result = Regex.Replace(result,"ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ|/g","o");
	  result = Regex.Replace(result,"ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ|/g","u");
	  result = Regex.Replace(result,"ỳ|ý|ỵ|ỷ|ỹ|/g","y");
	  result = Regex.Replace(result,"đ","d");
	  return result;
	}

	static string RemoveWhiteSpaceRegex(string input) {
	  return Regex.Replace(input,@"\s+","");
	}

	public static string RemoveSpecialCharactersRegex(string input) {
	  return Regex.Replace(input,"[^a-zA-Z0-9_.]+","",RegexOptions.Compiled);
	}

	public static string RemoveWhiteSpaceLinq(string input) {
	  return new string(input.ToCharArray()
		  .Where(c => !Char.IsWhiteSpace(c))
		  .ToArray());
	}

	public static string RemoveSpecialCharacters(string input) {
	  char[] buffer = new char[input.Length];
	  int idx = 0;

	  foreach(char c in input) {
		if((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z')
			|| (c >= 'a' && c <= 'z') || (c == '.') || (c == '_')) {
		  buffer[idx] = c;
		  idx++;
		}
	  }

	  return new string(buffer,0,idx);
	}

	public static string SubStringByLineNumber(string sourceText,int startLine,int stopLine = -1) {
	  int lineNum = 0;
	  StringBuilder result = new StringBuilder();

	  using(StringReader reader = new StringReader(sourceText)) {
		string line;
		while((line = reader.ReadLine()) != null) {
		  lineNum++;
		  if(lineNum >= startLine)
			result.AppendLine(line);

		  if(stopLine != -1 && lineNum == stopLine) {
			break;
		  }
		  if(lineNum >= 30) {
			break;
		  }

		}
	  }

	  // if(lineNum > 15) {
	  //return string.Empty;
	  // }
	  return result.Remove(result.Length - 2,2).ToString().Trim(); //remove downline char
	}

	public static int CountLinesNumber(string text) {
	  int lineNum = 0;
	  using(StringReader reader = new StringReader(text)) {
		string line;
		while((line = reader.ReadLine()) != null) {
		  lineNum++;
		}
	  }
	  return lineNum;
	}

	public static string GetValueByLineOrder(string blockText,int lineOrder) {
	  string result = "";

	  int lineNum = 0;
	  using(StringReader reader = new StringReader(blockText)) {
		string line;
		while((line = reader.ReadLine()) != null) {
		  lineNum++;
		  if(lineNum == lineOrder) {
			result = line;
			return result;
		  }
		}
	  }

	  return result;
	}

	// Summary
	//	  Đếm số từ trong một Mulltiline_Text
	public static int CountWord(string input) {
	  var text = input.Trim();
	  int wordCount = 0, index = 0;

	  while(index < text.Length) {
		// check if current char is part of a word
		while(index < text.Length && !char.IsWhiteSpace(text[index]))
		  index++;

		wordCount++;

		// skip whitespace until next word
		while(index < text.Length && char.IsWhiteSpace(text[index]))
		  index++;
	  }

	  return wordCount;
	}

	// #Start: GetBlockWords
	private static int CountWordBySplit(string input) {
	  var text = input.Trim();

	  string[] lines = text.Split(Environment.NewLine.ToCharArray());

	  return lines.Length;
	}

	// Summary
	// Eg:	CountWordAndTick("cong hoa xa hoi chu nghia viet nam", ref words) =>
	//		result: 8 ; 
	//				words=string[]{"cong","hoa","xa","hoi","chu","nghia","viet","nam"}
	private static int CountWordAndTick(string input,ref string[] words) {
	  var text = input.Trim();

	  words = text.Split(Environment.NewLine.ToCharArray());

	  if(words.Length <= 0)
		return 0;

	  return words.Length;
	}

	// Summary
	//	  
	// Eg:	SubWord(string[]{"cong","hoa","xa","hoi","chu","nghia","viet","nam"}, 2,3) => 
	//		result: "xa hoi chu"
	private static string SubWord(string[] words,int indexWordStart,int numberWord) {
	  string result = "";

	  for(int i = indexWordStart;i < words.Length-numberWord;i++) {
		result += " " + words[i];
	  }

	  return result;
	}

	// Summary
	//	  Lấy mảng từng cụm có "numberWord" từ trong input. 
	//		  Eg: GetBlockWords("cong hoa xa hoi chu nghia viet nam",3) => 
	//			  result: string[]{"cong hoa xa","hoa cong xa","xa hoi chu","hoi chu nghia","chu nghia viet","nghia viet nam"}
	private static string[] GetBlockWords(string input,int numberWord) {
	  string[] words = new string[] { };
	  List<string> result = new List<string>();

	  int count = CountWordAndTick(input,ref words);
	  if(count<= 0 || words == null || words.Length < numberWord)
		return null;

	  while(true) {
		int index = 0;
		string blockWord = SubWord(words,index,numberWord);
		if(blockWord != "") {

		  result.Add(blockWord);
		  index++;

		} else {
		  break;
		}
	  }

	  if(result == null || result.Count <=0) {
		return null;
	  }

	  return result.ToArray();
	}

	// #End: GetBlockWords
  }
}
