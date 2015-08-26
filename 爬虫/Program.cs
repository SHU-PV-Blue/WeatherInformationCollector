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
		static void Main(string[] args)
		{
			//定义要抓取的全球数据经纬度范围
			double latStart = -89.5;
			double latEnd = 89.5;
			double lonStart = -179.5;
			double lonEnd = 179.5;

			//类似于循环变量
			double lat = latStart;
			double lon = lonStart;

			//创建线程数组,定10个差不多了,我试过来他个100个,结果线程直接互相争夺资源,速度没有提升
			Thread[] ths = new Thread[10];

			//标记任务是否分配完
			bool ifEnd = false;

			//如果任务没有分配完继续循环
			while (!ifEnd)
			{
				//依次检查10个线程
				for(int i = 0; i < 10; ++i)
				{
					//如果线程是null(说明这个线程还没有实例化)或者线程的状态时停止(说明已完成上一次任务)
					if (ths[i] == null || ths[i].ThreadState == ThreadState.Stopped)
					{
						//重新实例化该线程,分配新的任务
						ths[i] = new Thread(new ThreadStart((new Task(lat,lon).Do)));
						//启动该线程
						ths[i].Start();

						//下面一段都是计算下一次任务经纬度值,封装成一个函数可能更好些
						if (lon < lonEnd)
							++lon;
						else
						{
							if (lat == latEnd)
							{
								ifEnd = false;
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

			//这里最好写一个循环,当10条线程的状态都是Stop时,主函数退出
			//但是我省懒就让主函数停着,等我来关,这样我也知道啥时候完成的,我不可能几个小时一直盯着它,如果它自己关了我会看不到
			Console.WriteLine("所有任务完成（可能有个别线程未终止）");
			Console.WriteLine("等待用户关闭程序");
			while (true)
				Thread.Sleep(100);
		}
	}
}
