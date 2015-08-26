using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace 上传信息
{
	class Uploader
	{
		string _inFileName;
		SqlConnection _sqlCon;
		double _lat;
		double _lon;


		public Uploader(string inFileName)
		{
			_inFileName = inFileName;
			//Console.WriteLine(_inFileName.Substring(5, _inFileName.IndexOf(",") - 5));
			//Console.WriteLine(_inFileName.Substring(_inFileName.IndexOf(",") + 1, _inFileName.IndexOf(".txt") - _inFileName.IndexOf(",") - 1));
			_lat = Convert.ToDouble(inFileName.Substring(5, inFileName.IndexOf(",") - 5));
			//文件名开头有 \data
			_lon = Convert.ToDouble(inFileName.Substring(inFileName.IndexOf(",") + 1, inFileName.IndexOf(".txt") - inFileName.IndexOf(",") - 1));
			string connectString = "server=.;database=PV-Weather;uid=sa;pwd='20150824'";
			//先传到本地服务器
			_sqlCon = new SqlConnection(connectString);
		}
		public bool Upload()
		{
			StreamReader reader = new StreamReader(_inFileName);


			string tempLine = reader.ReadLine();
			if (tempLine.IndexOf("LATLON:") == -1)
				throw new Exception("LATLON: 缺失!");
			tempLine = tempLine.Replace("LATLON:", "");

			List<string> tempStrs = CutUp(tempLine);
			double readLat = Convert.ToDouble(tempStrs[0]);
			double readLon = Convert.ToDouble(tempStrs[1]);
			if (readLat != _lat || readLon != _lon)
			{
				Console.WriteLine("readLat:" + readLat);
				Console.WriteLine("_lat:" + _lat);
				Console.WriteLine("readLon:" + readLon);
				Console.WriteLine("_lon:" + _lon);
				throw new Exception("文件名与文件内记录的经纬度不相符!");
			}

			//int countDataNum = 0;
			_sqlCon.Open();

			while ((tempLine = reader.ReadLine()) != string.Empty && tempLine != null)
			{
				string partName;
				string tableName;
				string lineName;
				int lineNum;
				double [] data = new double [12];

				if (tempLine.IndexOf("PART:") == -1)
					throw new Exception("PART: 缺失!");
				tempLine = tempLine.Replace("PART:", "");
				partName = DeleteUselessSpace(tempLine);

				tempLine = reader.ReadLine();
				if (tempLine.IndexOf("TABLE:") == -1)
					throw new Exception("TABLE: 缺失!");
				tempLine = tempLine.Replace("TABLE:", "");
				tableName = DeleteUselessSpace(tempLine);

				tempLine = reader.ReadLine();
				if (tempLine.IndexOf("LINE:") == -1)
					throw new Exception("LINE: 缺失!");
				tempLine = tempLine.Replace("LINE:", "");
				lineName = DeleteUselessSpace(tempLine);

				tempLine = reader.ReadLine();
				if (tempLine.IndexOf("NUM:") == -1)
					throw new Exception("NUM: 缺失!");
				tempLine = tempLine.Replace("NUM:", "");
				lineNum = Convert.ToInt32(tempLine);

				tempLine = reader.ReadLine();
				if (tempLine.IndexOf("DATA:") == -1)
					throw new Exception("DATA: 缺失!");
				tempLine = tempLine.Replace("DATA:", "");
				tempStrs = CutUp(tempLine);

				try
				{
					if (tempStrs.Count != 12 && tempStrs.Count != 13)
					{
						throw new Exception("DATA数据个数异常!");
					}

					//if (tempStrs.Count == 13)
					//{
						//Console.WriteLine("LineName:" + lineName);
						//++countDataNum;
					//}
						

					for (int i = 0; i < 12; ++i)
					{
						//Console.WriteLine(tempStrs[i] +"  countDataNum:" +  ++countDataNum);
						if (tempStrs[i] == "n/a")
							data[i] = double.NaN;
						else
							data[i] = (Convert.ToDouble(tempStrs[i]));
					}
				}
				catch(Exception ex)
				{
					Console.WriteLine(ex.Message);
					Console.WriteLine(tableName);
					Console.WriteLine(lineName);
					Console.WriteLine(tempStrs.Count);
					Console.WriteLine("任意键继续");
					Console.Read();
					return false;
				}
				

				SqlDataAdapter myDataAdapter = new SqlDataAdapter("select * from Parts where [PartName] = '" + partName + "'", _sqlCon);
				DataSet myDataSet = new DataSet();
				myDataAdapter.Fill(myDataSet, "Parts");
				
				

				//如下代码很有参考意义
				//DataTable myTable = myDataSet.Tables["Parts"];
				//foreach (DataRow myRow in myTable.Rows)
				//{
				//	foreach (DataColumn myColumn in myTable.Columns)
				//	{
				//		Console.Write(myRow[myColumn] + "     |     ");
				//	}
				//	Console.WriteLine();
				//}

				DataTable myTable = myDataSet.Tables["Parts"];
				string partID = myTable.Rows[0]["PartID"].ToString();


				myDataAdapter = new SqlDataAdapter("select * from Tables where [TableName] = '" + tableName + "' and [PartID] = '" + partID + "'", _sqlCon);
				myDataSet = new DataSet();
				myDataAdapter.Fill(myDataSet, "Tables");
				myTable = myDataSet.Tables["Tables"];
				string tableID = myTable.Rows[0]["TableID"].ToString();

				SqlCommand command = _sqlCon.CreateCommand();
				//command.CommandType = CommandType.Text;
				string commandText1 = "insert into Lines(Lat,Lon,TableID,LineNum,LineName";
				string[] month = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
				string commandText2	= "values ('" + _lat + "','" + _lon + "','" + tableID + "','" + lineNum + "','" + lineName + "'";
				for (int i = 0; i < 12; ++i)
				{
					if (!Double.IsNaN(data[i]))
					{
						commandText1 += ",[" + month[i] + "]";
						commandText2 += ",'" + data[i] + "'";
					}
				}
				commandText1 += ")";
				commandText2 += ")";
				command.CommandText = commandText1 + commandText2;
				//Console.WriteLine(command.CommandText);
				command.ExecuteNonQuery();



			}

			
			


			_sqlCon.Close();
			return true;
		}

		private List<string> CutUp(string baseString)
		{
			List<string> result = new List<string>();
			string word = "";
			bool isInWord = false;
			foreach(char ch in baseString)
			{
				if(ch == ' ')
				{
					if(isInWord)
					{
						result.Add(word);
						isInWord = false;
					}
				}
				else
				{
					if (isInWord)
					{
						word += ch;
					}
					else
					{
						isInWord = true;
						word = "" + ch;
					}
				}
			}
			if (isInWord)
				result.Add(word);
			return result;
		}

		private string DeleteUselessSpace(string originString)
		{
			return DeleteLeftSpace(DeleteRigetSpace(originString));
		}

		private string DeleteLeftSpace(string originString)
		{
			if (originString[0] != ' ')
				return originString;
			return DeleteLeftSpace(originString.Remove(0, 1));
		}

		private string DeleteRigetSpace(string originString)
		{
			if (originString[originString.Length - 1] != ' ')
				return originString;
			return DeleteRigetSpace(originString.Remove(originString.Length - 1, 1));
		}
	}
}
