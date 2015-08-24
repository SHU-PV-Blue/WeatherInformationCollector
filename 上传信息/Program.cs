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
			Uploader uper = new Uploader("data\\0.5,0.5.txt");
			uper.Upload();
		}
	}
}
