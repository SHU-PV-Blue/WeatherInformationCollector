using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 上传信息
{
	class Task
	{
		string _inFileName;
		public Task(double lat, double lon)
		{
			_inFileName = "data\\" + lat + "," +  lon + ".txt";
		}
		public void Do()
		{
			Uploader uploader = new Uploader(_inFileName);
			uploader.Upload();
		}
	}
}
