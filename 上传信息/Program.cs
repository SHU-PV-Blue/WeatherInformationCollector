using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 上传信息
{
	class Program
	{
		static void Main(string[] args)
		{
			for(double lat = 0.5; lat < 11; ++lat)
				for(double lon = 0.5; lon < 11; ++lon)
				{
					Uploader uper = new Uploader("data\\" + lat + "," + lon + ".txt");
					uper.Upload();
				}
			
		}
	}
}
