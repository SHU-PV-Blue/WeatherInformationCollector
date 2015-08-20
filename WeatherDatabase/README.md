为了防止数据库密码泄露，
添加如下.gitignore忽略了记录数据库密码的文件

    # Prevent the database password disclosure
    WeatherDatabaseAccount.cs

WeatherDatabaseAccount.cs的内容为：

    namespace SHUPV.Database
    {
    	partial class WeatherDatabase
    	{
    		string _server = "服务器";
    		string _user = "用户";
    		string _password = "密码";
    	}
    }
