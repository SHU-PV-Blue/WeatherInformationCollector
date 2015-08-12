using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtractor
{
	class Extractor
	{
		string baseStr;
		public Extractor(string input_str)
		{
			baseStr = input_str;
		}
		public List<int> FindIndexOf(string input_str)
		{
			List<int> result = new List<int>();
			string str = baseStr;
			int start = 0;
			while(str.Length > 0)
			{
				int index = str.IndexOf(input_str);
				if(index != -1)
				{
					result.Add(start + index);
					str = str.Remove(0, index + input_str.Length);
					start += index + input_str.Length;
				}
				else
					str = str.Remove(0, str.Length);
			}
			return result;
		}
		public List<string> FindStringBetween(string startStr, string endStr)
		{
			string tempStr = baseStr;
			List<string> result = new List<string>();
			List<int> startIndex = FindIndexOf(startStr);
			int count = 0;
			int start = 0;
			while(count < startIndex.Count)
			{
				tempStr = tempStr.Remove(0, startIndex[count] - start);
				start += startIndex[count] - start;
				int realStartIndex = startIndex[count] - start;
				int realEndIndex = tempStr.IndexOf(endStr);
				result.Add(new string(tempStr.ToCharArray(), realStartIndex + startStr.Length, realEndIndex - (realStartIndex + startStr.Length)));
				
				++count;
			}
			return result;
		}
	}
}
