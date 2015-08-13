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
			test2();
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
	}
}
