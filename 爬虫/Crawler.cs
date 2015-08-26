using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

namespace WebCrawler
{
	class Crawler
	{
		//需要访问的链接
		string _link;
		//将得到的HTML代码保存的文件名
		string _fileName;

		//构造函数
		public Crawler(string link, string fileName)
		{
			_link = link;
			_fileName = fileName;
		}

		//抓取函数
		public bool Crawl()
		{
			//新建一个网络请求实例,相当于打开浏览器,输入了地址
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_link);

			//设置请求的方法,相当于设置一下浏览器,不过现实的浏览器都不用手动设置这个
			request.Method = WebRequestMethods.Http.Get;

			//新建一个网络请求回应实例,相当于浏览器向网站发送请求后,需要对象接住网站返回的数据
			HttpWebResponse response;

			//尝试向网站发出请求
			try
			{
				//如果没有异常,response会接住网站返回的数据
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (WebException)
			{
				//如果catch到了异常,说明联网不成功,可能是网断了,可能是网络卡,也是是人家网站服务器问题
				Console.WriteLine("\n!!!连接网站超时:!!!");
				Console.WriteLine(_link);
				//返回失败
				return false;
			}

			//新建一个输出流对象,将从网站获取的数据转换为输出流
			StreamReader reader = new StreamReader(response.GetResponseStream());
			//从输入流中读取所有字符串,保存到data
			string data = reader.ReadToEnd();

			//response功成身退
			response.Close();

			//新建一个文件输出流实例
			StreamWriter writer = new StreamWriter(_fileName);

			//将data写入文件
			writer.Write(data);
			writer.Close();

			//返回成功
			Console.WriteLine("\n成功抓取:");
			Console.WriteLine(_link);
			return true;
		}
	}
}
