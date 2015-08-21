using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace 提取信息
{
	class Program
	{
		public static StreamWriter errLog;
		static void Main(string[] args)
		{
			errLog = new StreamWriter("errLog.txt");
			for(int i = -90; i <= 90; ++i)
				for(int j = -90; j <= 90; ++j)
				{
					Task t = new Task(i, j);
					t.Do();
				}
		}
	}
}
