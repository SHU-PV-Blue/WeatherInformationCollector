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
				Console.WriteLine("!!!!!!!!!!无法连接到网站" + _link);
				return false;
			}
			System.IO.StreamReader reader = new System.IO.StreamReader(response.GetResponseStream());
			string data = reader.ReadToEnd();
			response.Close();

			FileStream output = new FileStream(_fileName, FileMode.CreateNew, FileAccess.Write, FileShare.None);
			output.Write(Encoding.Default.GetBytes(data),0,data.Length);
			output.Close();
			Console.WriteLine("成功抓取:" + _link);
			return true;
		}
	}
}
