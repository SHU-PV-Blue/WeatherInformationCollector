using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace StringExtractor
{
	class Program
	{
		static void Main(string[] args)
		{
			test3();
		}

		static void test1()
		{
			Extractor ex = new Extractor((new System.IO.StreamReader("03,073.html")).ReadToEnd());
			List<string> result = ex.GetAfter("<div align=\"center\"><table border=1 summary=\"").GetBefore("\" width=\"95%\">").GetResult();
			foreach(var str in result)
			{
				Console.WriteLine("result:" + str);
			}
			Console.Read();
		}

		static void test2()
		{
			Extractor ex = new Extractor((new System.IO.StreamReader("03,073.html")).ReadToEnd());
			List<string> result = 
				ex
				.GetAfter("<hr><big><b><i>Parameters for Solar Cooking:</i></b></big>")
				.GetBefore("<hr><big><b><i>Parameters for Sizing and Pointing of Solar Panels and for Solar Thermal Applications:</i></b></big>")
				.GetAfter("<caption><b>Monthly Averaged Insolation Incident On A Horizontal Surface (kWh/m<sup>2</sup>/day)</b></caption>")
				.GetBefore("<caption><b>Monthly Averaged Midday Insolation Incident On A Horizontal Surface (kW/m<sup>2</sup>)</b></caption><tr><td>Lat 3 <br> Lon 73</td>")
				.GetAfter("<td align=\"center\" nowrap>")
				.GetBefore("</td>")
				.GetResult();
			foreach (var str in result)
			{
				Console.WriteLine("result:" + str);
			}
			Console.WriteLine("count:" + result.Count);
			Console.Read();
		}

		static void test3()
		{
			Extractor ex = new Extractor((new System.IO.StreamReader("03,073.html")).ReadToEnd());
			List<string> partResult =
				ex
				.GetAfter("<hr><big><b><i>")
				.GetResult();
			/*foreach (var str in partResult)
			{
				Console.WriteLine("----------------------------------------------------");
				Console.WriteLine("result:" + str);
			}*/
			foreach(var str1 in partResult)
			{
				List<string> tableResult = (new Extractor(str1)).GetAfter("<div align=\"center\"><table border=1 summary=\"").GetBefore("\" width=\"95%\">").GetResult();
				ShowStrings(tableResult);
			}
			//Console.WriteLine("count:" + partResult.Count);
			Console.Read();
		}
		static int count = 0;
		static bool ShowStrings(List<string> strings)
		{
			if (strings == null)
				return false;
			foreach(var str in strings)
			{
				Console.Write(++count + ":");
				Console.WriteLine("-------------------------------------------------------------------------");
				Console.WriteLine(str);
			}
			return true;
		}
	}
}
