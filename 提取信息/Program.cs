using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 提取信息
{
	class Program
	{
		static void Main(string[] args)
		{
			提取者 t = new 提取者("input\\03,073.html", "output\\03,073.txt");
			t.提取();
		}
	}
}
