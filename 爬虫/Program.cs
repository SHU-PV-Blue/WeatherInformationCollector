using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace WebCrawler
{
	class Program
	{
#warning 本程序为赶工之作，隐藏bug很多，不宜直接重用
		static void Main(string[] args)
		{
			/*for(int i = -180; i <= 180; ++i)
			{
				DateTime dt1 = DateTime.Now;
				Thread th = new Thread(new ThreadStart((new Crawler("https://eosweb.larc.nasa.gov/cgi-bin/sse/grid.cgi?&num=182092&lat=1&hgt=100&submit=Submit&veg=17&sitelev=&email=&p=grid_id&step=2&lon=" + i, "data\\" + i + ".txt")).Crawl));
				th.Start();
				th.Join();
				DateTime dt2 = DateTime.Now;
				TimeSpan dt = dt2 - dt1;
				Console.WriteLine(dt);
			}*/
			/*int latStart = 3;
			int latEnd = 54;
			int lonStart = 73;
			int lonEnd = 136;*/

			int latStart = -90;
			int latEnd = 90;
			int lonStart = -180;
			int lonEnd = 180;

			int lat = latStart;
			int lon = lonStart;
			Thread[] ths = new Thread[10];
			DateTime dt = DateTime.Now;
			while(lat <= latEnd)
			{
				
				for(int i = 0; i < 10; ++i)
				{
					if (ths[i] == null || ths[i].ThreadState == ThreadState.Stopped)
					{
						ths[i] = new Thread(new ThreadStart((new Task(lat,lon).Do)));
						ths[i].Start();
						if (lon < lonEnd)
							++lon;
						else
						{
							if (lat == latEnd)
								break;
							else
							{
								++lat;
								lon = lonStart;
							}
						}
					}
				}
			}
			Console.WriteLine("所有任务完成（可能有个别线程未终止）");
			Console.WriteLine("等待用户关闭程序");
			Console.WriteLine("用时" + (DateTime.Now - dt));
			while (true)
				Thread.Sleep(100);
		}
	}
}
