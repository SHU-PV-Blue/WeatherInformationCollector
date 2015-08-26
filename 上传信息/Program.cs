using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace 上传信息
{
	class Program
	{
		static void Main(string[] args)
		{
			double latStart = -89.5;
			double latEnd = 89.5;
			double lonStart = -179.5;
			double lonEnd = 179.5;

			int count = 0;
			int sum = 180 * 360;
			DateTime startTime = DateTime.Now;
			DateTime eachTime = DateTime.Now;
			double lat = latStart;
			double lon = lonStart;
			Thread[] ths = new Thread[10];
			while (lat <= latEnd)
			{
				for (int i = 0; i < 10; ++i)
				{
					if (ths[i] == null || ths[i].ThreadState == ThreadState.Stopped)
					{
						ths[i] = new Thread(new ThreadStart((new Task(lat, lon).Do)));
						ths[i].Start();
						++count;
						if ((DateTime.Now - eachTime) > (new TimeSpan(0, 0, 5)))
						{
							Console.WriteLine();
							Console.WriteLine("完成" + count + "个,");
							Console.WriteLine((double)count / sum * 100 + "%");
							Console.WriteLine("用时" + (DateTime.Now - startTime));
							double speed = (double)count / (DateTime.Now - startTime).TotalSeconds;
							TimeSpan ts = new TimeSpan(0, 0, (int)((sum - count) / speed));
							Console.WriteLine("平均每秒" + speed);
							Console.WriteLine("预计" + ts + "后完成");

							eachTime = DateTime.Now;
						}
						if (lon < lonEnd)
							++lon;
						else
						{
							if (lat == latEnd)
							{
								lat = 200;//to stop while
								break;
							}
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
			while (true)
				Thread.Sleep(100);
			
		}
	}
}
