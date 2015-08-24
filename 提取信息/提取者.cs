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
		string _inFileName;
		string _outFileName;

		public 提取者(string inFileName,string outFileName)
		{
			_inFileName = inFileName;
			_outFileName = outFileName;
		}

		public bool 提取()
		{
			StringWriter strWriter = new StringWriter();
			//TimeSpan repearTime = new TimeSpan();
			string originString = (new StreamReader(_inFileName)).ReadToEnd();

			//处理重大漏洞:HTML代码中,部分行名是包含在</tr>\n<tr><td align=\"center\">中,而大多数是包含在</tr>\n<tr><td>中
			originString = originString.Replace("</tr>\n<tr><td align=\"center\">", "</tr>\n<tr><td>");


			//DateTime startTime = DateTime.Now;
			Reaper reaper = new Reaper(originString);

			string latStr = reaper.RemainAfterFirst("<br>Latitude <b>").RemainBeforeFirst("</b>").GetResult()[0];
			string lonStr = reaper.RemainAfterFirst("<br>Longitude <b>").RemainBeforeFirst("</b>").GetResult()[0];

			//repearTime += DateTime.Now - startTime;

			strWriter.Write("LATLON:");
			strWriter.WriteLine(latStr + " " + lonStr);

			//startTime = DateTime.Now;

			foreach (Reaper part in reaper.RemainBeforeFirst("<table width=\"100%\" summary=\"table used for formatting\"><tr><td>").ReapByProfix("<hr><big><b><i>"))
			{
				string partName = part.RemainBeforeFirst(":</i></b></big>").GetResult()[0];
				//ShowStrings(part.RemainBeforeFirst(":</i></b></big>").GetResult());
				foreach (Reaper table in part.ReapByProfix("<div align=\"center\"><table border=1 summary=\""))
				{
					string tableName = table.RemainBeforeFirst("\" width=\"95%\">").GetResult()[0];

					int lineCount = 0;
					foreach (Reaper line in table.ReapByProfix("<tr><td>").GiveUpContain("<td>Jan</td><td>Feb</td><td>Mar</td>"))
					{
						string lineName = line.RemainBeforeFirst("</td>").GetResult()[0];

						//repearTime += DateTime.Now - startTime;

						strWriter.WriteLine("PART:" + partName);
						strWriter.WriteLine("TABLE:" + tableName);
						strWriter.WriteLine("LINE:" + lineName);
						strWriter.WriteLine("NUM:" + ++lineCount);
						strWriter.Write("DATA:");

						//startTime = DateTime.Now;

						foreach (Reaper data in line.ReapByProfix("<td align=\"center\" nowrap>").RemainBeforeFirst("</td>"))
						{
							//repearTime += DateTime.Now - startTime;

							strWriter.Write(data.GetResult()[0] + " ");

							//startTime = DateTime.Now;
						}
						strWriter.WriteLine();
					}
				}
			}

			//处理HTML的转义符
			string result = strWriter.ToString();
			result = result.Replace("&lt;", "<");
			result = result.Replace("&gt;", ">");
			result = result.Replace("&deg;", "°");
			//处理Average Daily Temperature Range 多出的*
			result = result.Replace("* ", "");

			try
			{
				StreamWriter _outFile = new StreamWriter(_outFileName);
				_outFile.Write(result);
				_outFile.Close();
			}
			catch(Exception ex)
			{
				Program.errLog.WriteLine(ex.Message);
				Program.errLog.Flush();
			}
			//Console.WriteLine("完成:" + _inFileName + " -> " + _outFileName);
			//Console.WriteLine("Reaper\n耗时" + repearTime);
			return true;
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
