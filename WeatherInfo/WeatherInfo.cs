using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUPV.Weather
{
	public class WeatherInfo
	{
		public bool IsEmpty { get { return _isEmpty; } }

		bool _isEmpty;
		double _lat;
		double _lon;
		List<Part> _parts;

		public WeatherInfo(double latitude, double longitude)
		{
			if (latitude > 90 || latitude < -90)
				throw new ArgumentOutOfRangeException("latitude", "Value " + latitude + " is not in range [-90,+90]");
			if (longitude > 180 || longitude < -180)
				throw new ArgumentOutOfRangeException("longitude", "Value " + longitude + " is not in range [-180,+180]");
			_isEmpty = true;
			_parts = new List<Part>();
		}

		public bool Load()
		{
			//访问数据库，下载数据
#warning 待完成
			return false;
		}

		public Part GetPart(string partName)
		{
#warning 待完成
			return new Part();
		}

		public override string ToString()
		{
			#warning 待完成
			if (_isEmpty)
				throw new Exception("Trying to call ToString() of a empty SHUPV.Weather.Line object");
			string result = "";
			return result;
		}
	}
}
