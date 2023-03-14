namespace Demo.Net.Infrast.Impl.SQLiteService
{
	public class SQLitePathService : ISQLitePathService
	{
		public string GetPath()
		{
			return @"..\..\..\DataSource\points.json";
		}
	}
}
