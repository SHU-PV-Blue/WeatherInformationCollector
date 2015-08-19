using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace WebCrawler
{
	class Task
	{
		int _lat;
		int _lon;
		public Task(int lat,int lon)
		{
			_lat = lat;
			_lon = lon;
		}
		public void Do()
		{
			DateTime dt = DateTime.Now;
			string fileName = "";

			if (_lat < 0)
				fileName += "-";
			else
				fileName += "+";
			if (Math.Abs(_lat) < 10)
				fileName += "0" + Math.Abs(_lat);
			else
				fileName += "" + Math.Abs(_lat);

			fileName += ",";

			if (_lon < 0)
				fileName += "-";
			else
				fileName += "+";
			if(Math.Abs(_lon) < 10)
				fileName += "00" + Math.Abs(_lon);
			else
			{
				if (Math.Abs(_lon) < 100)
					fileName += "0" + Math.Abs(_lon);
				else
					fileName += "" + Math.Abs(_lon);
			}
			fileName += ".html";


			if ((new FileInfo("data\\" + fileName)).Exists)
			{
				Console.WriteLine("已存在文件:" + fileName);
				return;
			}

			int count = 1;
			while (!(new Crawler("https://eosweb.larc.nasa.gov/cgi-bin/sse/grid.cgi?&num=182092&lat=" + _lat + "&hgt=100&submit=Submit&veg=17&sitelev=&email=&p=grid_id&step=2&lon=" + _lon, "data\\" + fileName)).Crawl())
			{
				++count;
				Console.WriteLine("第" + count + "次尝试抓取" + fileName);
			}
			TimeSpan ts = DateTime.Now - dt;
			Console.WriteLine("耗时" + ts + ",尝试" + count + "次");
		}
	}
}
