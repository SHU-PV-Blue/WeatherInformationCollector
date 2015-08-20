using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SHUPV.Weather
{
	public class Line
	{
		public bool IsEmpty { get { return _isEmpty; } }
		bool _isEmpty;
		string _name;
		int _num;
		double[] _datas = new double[12];

		public Line()
		{
			_isEmpty = true;
		}

		public override string ToString()
		{
			if (_isEmpty)
				throw new Exception("Trying to call ToString() of a empty SHUPV.Weather.Line object");
			string result = "";
			foreach(double it in _datas)
			{
				result += it.ToString() + " ";
			}
			return result;
		}
	}
}
