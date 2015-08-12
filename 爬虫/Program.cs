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
			for(int i = -180; i <= 180; ++i)
			{
				DateTime dt1 = DateTime.Now;
				Thread th = new Thread(new ThreadStart((new Crawler("https://eosweb.larc.nasa.gov/cgi-bin/sse/grid.cgi?&num=182092&lat=1&hgt=100&submit=Submit&veg=17&sitelev=&email=&p=grid_id&step=2&lon=" + i, "data\\" + i + ".txt")).Crawl));
				th.Start();
				th.Join();
				DateTime dt2 = DateTime.Now;
				TimeSpan dt = dt2 - dt1;
				Console.WriteLine(dt);
			}
			
		}
	}
}
