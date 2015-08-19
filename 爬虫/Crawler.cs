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
		string _link;
		string _fileName;
		public Crawler(string link, string fileName)
		{
			_link = link;
			_fileName = fileName;
		}

		public bool Crawl()
		{
			HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(_link);
			request.Method = WebRequestMethods.Http.Get;
			HttpWebResponse response;
			try
			{
				response = (HttpWebResponse)request.GetResponse();
			}
			catch (System.Net.WebException)
			{
				Console.WriteLine("\n!!!连接网站超时:!!!");
				Console.WriteLine(_link);
				return false;
			}
			StreamReader reader = new StreamReader(response.GetResponseStream());
			string data = reader.ReadToEnd();
			response.Close();

			FileStream outFile = new FileStream(_fileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
			outFile.Write(Encoding.Default.GetBytes(data),0,data.Length);
			outFile.Close();
			Console.WriteLine("\n成功抓取:");
			Console.WriteLine(_link);
			return true;
		}
	}
}
