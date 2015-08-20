using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace 提取信息
{
	class 提取者
	{
		string _inString;
		StreamWriter _outFile;

		public 提取者(string inFileName,string outFileName)
		{
			StreamReader reader = new StreamReader(inFileName);
			_inString = reader.ReadToEnd();
			_outFile = new StreamWriter(outFileName);
		}

		public bool 提取()
		{
			//准备搭载收割者
			return false;
		}


		int count = 0;
		private bool ResetCount()
		{
			count = 0;
			return true;
		}
		private bool ShowStrings(List<string> strings)
		{
			if (strings == null)
				return false;
			foreach (var str in strings)
			{
				Console.Write(++count + ":");
				Console.WriteLine("-------------------------------------------------------------------------");
				Console.WriteLine(str);
			}
			return true;
		}
	}
}
