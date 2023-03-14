using Demo.Net.Infrast.Impl.SQLiteService;

namespace Demo.Infrast.Impl.SQLiteService.Test
{
	public class TestSQLitePathService : ISQLitePathService
	{
		public string GetPath()
		{
			return @"Data Source=..\..\..\..\..\..\DataSource\points.db";
		}
	}
}
