using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using StringExtractor;

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
			Extractor ex = new Extractor(_inString);
			List<string> partResult =
				ex
				.GetBefore("</i></b></div>\n<hr width=\"80%\">\n<div align=\"center\">\n<table width=\"100%\" summary=\"table used for formatting\"><tr><td>\n<a href=\"/cgi-bin/sse/sse.cgi?\"><img src=\"/sse/images/back.jpg\" alt=\"Back to SSE Data Set Home Page\" width=\"160\" height=\"160\" border=\"0\"></a>")
				.GetAfter("<hr><big><b><i>")
				.GetResult();
			foreach (var partStr in partResult)
			{
				string partName = (new Extractor(partStr)).GetBefore(":</i></b></big>").GetResult()[0];
				//ShowStrings((new Extractor(partStr)).GetBefore(":</i></b></big>").GetResult());
				List<string> tableResult = (new Extractor(partStr)).GetAfter("<div align=\"center\"><table border=1 summary=\"").GetResult();
				foreach (var tableStr in tableResult)
				{
					string tableName = (new Extractor(tableStr)).GetBefore("\" width=\"95%\">").GetResult()[0];
					//ShowStrings((new Extractor(tableStr)).GetBefore("\" width=\"95%\">").GetResult());
					List<string> lineResult = (new Extractor(partStr)).GetAfter("<tr><td>").GetResult();
					int lineIndex = 1;
					foreach (var lineStr in lineResult)
					{
						string lineName = (new Extractor(lineResult)).GetBefore("  </td>").GetResult()[0];
						ShowStrings((new Extractor(lineResult)).GetBefore("  </td>").GetResult());
					}
				}
				
			}
			_outFile.Close();
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
