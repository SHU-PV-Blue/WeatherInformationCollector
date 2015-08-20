using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;

namespace SHUPV.Database
{
	public static partial class WeatherDatabase
	{
		static public SqlConnection GetSqlConnection()
		{
			string connectString = "Server = " + _server + "; Database = " + _database + ";Uid = " + _user + ";Pwd = " + _password + ";";
			return new SqlConnection(connectString);
		}
	}
}
