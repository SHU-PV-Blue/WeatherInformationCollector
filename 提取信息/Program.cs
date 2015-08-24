using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Threading;

namespace 提取信息
{
	class Program
	{
		public static StreamWriter errLog;
		static void Main(string[] args)
		{
			Work();
		}

		static void Work()
		{
			errLog = new StreamWriter("errLog.txt");
			int latStart = -90;
			int latEnd = 90;
			int lonStart = -180;
			int lonEnd = 180;

			int count = 0;
			int sum = 181 * 361;
			DateTime startTime = DateTime.Now;
			DateTime eachTime = DateTime.Now;
			int lat = latStart;
			int lon = lonStart;
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
