using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringExtractor
{
	public class Extractor
	{
		List<string> baseStrs;

		public Extractor(string inputStr)
		{
			if (inputStr == null || inputStr == "")
				throw new Exception("参数不能为空");
			baseStrs = new List<string>();
			baseStrs.Add(inputStr);
		}

		public Extractor(List<string> inputStrs)
		{
			if (inputStrs == null)
				throw new Exception("参数不能为空");
			baseStrs = new List<string>(inputStrs);
		}

		private List<int> FindIndexOf(string mainStr,string subStr)
		{
			if (subStr == null || subStr == "")
				throw new Exception("参数不能为空");
			List<int> result = new List<int>();
			int startIndex = 0;
			while (mainStr.IndexOf(subStr,startIndex) != -1)
			{
				result.Add(mainStr.IndexOf(subStr, startIndex));
				startIndex = mainStr.IndexOf(subStr, startIndex) + subStr.Length;
			}
			return result;
		}

		public Extractor GetBefore(string startStr)
		{
			if (startStr == null || startStr == "")
				throw new Exception("参数不能为空");
			List<string> result = new List<string>();
			foreach (var str in baseStrs)
			{
				List<int> indexs = FindIndexOf(str, startStr);
				if (indexs.Count == 0)
					continue;
				for (int i = 0; i < indexs.Count; ++i)
				{
					if (i > 0)
						result.Add(str.Substring(indexs[i - 1] + startStr.Length, indexs[i] - indexs[i - 1] - startStr.Length));
					else
						result.Add(str.Substring(0, indexs[i] - 0));
				}
			}
			return new Extractor(result);
		}

		public Extractor GetAfter(string endStr)
		{
			if (endStr == null || endStr == "")
				throw new Exception("参数不能为空");
			List<string> result = new List<string>();
			foreach (var str in baseStrs)
			{
				List<int> indexs = FindIndexOf(str,endStr);
				if(indexs.Count == 0)
					continue;
				for(int i = 0; i < indexs.Count; ++i)
				{
					if(i + 1 < indexs.Count)
						result.Add(str.Substring(indexs[i] + endStr.Length, indexs[i + 1] - indexs[i] - endStr.Length));
					else
						result.Add(str.Substring(indexs[i] + endStr.Length, str.Length - indexs[i] - endStr.Length));
				}
			}
			return new Extractor(result);
		}

		public List<string> GetResult()
		{
			return new List<string>(baseStrs);
		}

		public Extractor Clone()
		{
			return new Extractor(baseStrs);
		}
		/*public List<string> FindStringBetween(string startStr, string endStr)
		{
			string tempStr = baseStrs;
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
		}*/
	}
}
