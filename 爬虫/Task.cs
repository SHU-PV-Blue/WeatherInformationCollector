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
		//需要抓取的NASA数据的经纬度
		double _lat;
		double _lon;

		//构造函数
		public Task(double lat, double lon)
		{
			_lat = lat;
			_lon = lon;
		}

		//执行函数
		public void Do()
		{
			//记录一下任务开始时的时间
			DateTime dt = DateTime.Now;
			//确定文件名
			string fileName = _lat + "," + _lon + ".html";

			//修正了之前的错误,程序自己创建data文件夹,如果已存在则忽略
			DirectoryInfo dir = new DirectoryInfo("data\\");
			dir.Create();

			//如果已经存在这个文件了,说明抓过了,任务取消
			if ((new FileInfo("data\\" + fileName)).Exists)
			{
				Console.WriteLine("已存在文件:" + fileName);
				return;
			}

			//记录一下为抓这个数据尝试了几次
			int count = 1;
			//确认网址链接,这里没什么门道,浏览NASA网页时看浏览器地址,找规律
			string link = "https://eosweb.larc.nasa.gov/cgi-bin/sse/grid.cgi?&num=182092&lat=" + _lat + "&hgt=100&submit=Submit&veg=17&sitelev=&email=&p=grid_id&step=2&lon=" + _lon;
			//新建抓取者实例
			Crawler crawler = new Crawler(link, "data\\" + fileName);

			//不停地循环尝试抓取,直到成功
			while (!(crawler.Crawl()))
			{
				++count;
				Console.WriteLine("第" + count + "次尝试抓取" + fileName);
			}

			//计算总耗时并显示
			TimeSpan ts = DateTime.Now - dt;
			Console.WriteLine("耗时" + ts + ",尝试" + count + "次");
		}
	}
}
