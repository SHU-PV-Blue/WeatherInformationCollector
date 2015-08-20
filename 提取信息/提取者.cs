using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using Wolfogre.Tool;

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

		public void 提取()
		{
			Reaper reaper = new Reaper(_inString);
			foreach (Reaper part in reaper.RemainBeforeFirst("<table width=\"100%\" summary=\"table used for formatting\"><tr><td>").ReapByProfix("<hr><big><b><i>"))
			{
				string partName = part.RemainBeforeFirst(":</i></b></big>").GetResult()[0];
				ShowStrings(part.RemainBeforeFirst(":</i></b></big>").GetResult());
				foreach (Reaper table in part.ReapByProfix("<div align=\"center\"><table border=1 summary=\""))
				{
					string tableName = table.RemainBeforeFirst("\" width=\"95%\">").GetResult()[0];

					int lineCount = 0;
					foreach (Reaper line in table.ReapByProfix("<tr><td>").GiveUpContain("<td>Jan</td><td>Feb</td><td>Mar</td>"))
					{
						string lineName = line.RemainBeforeFirst("</td>").GetResult()[0];

						_outFile.WriteLine("PART:" + partName);
						_outFile.WriteLine("TABLE:" + tableName);
						_outFile.WriteLine("LINE:" + lineName);
						_outFile.WriteLine("NUM:" + ++lineCount);
						_outFile.Write("DATA:");
						foreach (Reaper data in line.ReapByProfix("<td align=\"center\" nowrap>").RemainBeforeFirst("</td>"))
						{
							_outFile.Write(data.GetResult()[0] + " ");
						}
						_outFile.WriteLine();
					}
				}
			}
			_outFile.Close();
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
