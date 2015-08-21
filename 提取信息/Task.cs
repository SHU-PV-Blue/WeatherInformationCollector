using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Wolfogre.Tool;
using System.IO;

namespace 提取信息
{
	class Task
	{
		double _lat;
		double _lon;
		string _inFileName;
		string _outFileName;
		public Task(int lat, int lon)
		{
			_inFileName = "input\\";

			if (lat < 0)
				_inFileName += "-";
			else
				_inFileName += "+";
			if (Math.Abs(lat) < 10)
				_inFileName += "0" + Math.Abs(lat);
			else
				_inFileName += "" + Math.Abs(lat);

			_inFileName += ",";

			if (lon < 0)
				_inFileName += "-";
			else
				_inFileName += "+";
			if (Math.Abs(lon) < 10)
				_inFileName += "00" + Math.Abs(lon);
			else
			{
				if (Math.Abs(lon) < 100)
					_inFileName += "0" + Math.Abs(lon);
				else
					_inFileName += "" + Math.Abs(lon);
			}
			_inFileName += ".html";

			
			Reaper reaper = new Reaper((new StreamReader(_inFileName).ReadToEnd()));
			_lat = Convert.ToDouble(reaper.RemainAfterFirst("<br>Latitude <b>").RemainBeforeFirst("</b>").GetResult()[0]);
			_lon = Convert.ToDouble(reaper.RemainAfterFirst("<br>Longitude <b>").RemainBeforeFirst("</b>").GetResult()[0]);
		}
		public void Do()
		{
			//DateTime startTime = DateTime.Now;
			_outFileName = "output\\" + _lat.ToString() + "," + _lon.ToString() + ".txt";
			if ((new FileInfo(_outFileName)).Exists)
			{
				Console.WriteLine(_inFileName + "-X>" + "已存在文件:" + _outFileName);
				Program.errLog.WriteLine(_inFileName + "-X>" + "已存在文件:" + _outFileName);
				Program.errLog.Flush();
				return;
			}
			提取者 t = new 提取者(_inFileName, _outFileName);
			t.提取();
			//TimeSpan ts = DateTime.Now - startTime;
			//Console.WriteLine("耗时" + ts);
		}
	}
}
