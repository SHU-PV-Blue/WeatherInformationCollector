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
		string _inFileName;
		FileStream _outFile;
		public 提取者(string inFileName,string outFileName)
		{
			_inFileName = inFileName;
			StreamReader reader = new System.IO.StreamReader(inFileName);
			_inString = reader.ReadToEnd();
			_outFile = new FileStream(outFileName, FileMode.Create, FileAccess.Write, FileShare.None);
		}
		public bool 提取()
		{
			Console.WriteLine(提取MAMIIOAHS());
			_outFile.Close();
			return true;
		}
		//提取Monthly Averaged Insolation Incident On A Horizontal Surface
		public string 提取MAIIOAHS()
		{
#warning 提取MAIIOAHS 有歧义！
			return "";
		}
		//提取Monthly Averaged Midday Insolation Incident On A Horizontal Surface
		public string 提取MAMIIOAHS()
		{
			string result = "Monthly Averaged Midday Insolation Incident On A Horizontal Surface" + Environment.NewLine;
			result += "kW/m^2" + Environment.NewLine;
			result += "Jan Feb Mar Apr May Jun Jul Aug Sep Oct Nov Dec" + Environment.NewLine;
			Extractor ex = new Extractor(_inString);
			List<string> temp = ex.FindStringBetween(@"Monthly Averaged Midday Insolation Incident On A Horizontal Surface (kW/m<sup>2</sup>)</b></caption><tr><td>Lat 3 <br> Lon 73</td>", @"</tr></table></div>");
			if (temp.Count != 1)
				throw new Exception("在文件" + _inFileName + "中提取不到不到信息:" + "Monthly Averaged Midday Insolation Incident On A Horizontal Surface");
			ex = new Extractor(temp[0] + "</tr></table></div>");
#warning !!!!!!!!
			temp = ex.FindStringBetween(@"<tr><td>22-year Average     </td>", @"</tr></table></div>");
			if (temp.Count != 1)
				throw new Exception("在文件" + _inFileName + "中提取不到不到信息:" + "Monthly Averaged Midday Insolation Incident On A Horizontal Surface");
			temp = ex.FindStringBetween("<td align=\"center\" nowrap>", @"</td>");
			foreach(var it in temp)
			{
				result += it + " ";
			}
			result += Environment.NewLine;
			return result;
		}
	}
}
