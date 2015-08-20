using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using SHUPV.Database;
using System.Data.SqlClient;

namespace WeatherDatabaseTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			SqlConnection sqlCnt = WeatherDatabase.GetSqlConnection();
			try
			{
				sqlCnt.Open();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return;
			}
			MessageBox.Show("链接数据库成功");

			SqlCommand cmd = new SqlCommand();
			cmd.Connection = sqlCnt;

			cmd.CommandType = CommandType.Text;
			cmd.CommandText = "Select * from TestCon";
			SqlDataReader reader = cmd.ExecuteReader();
			while (reader.Read())
			{
				MessageBox.Show(reader["DataTime"].ToString());
			}

			sqlCnt.Close();
		}
	}
}
