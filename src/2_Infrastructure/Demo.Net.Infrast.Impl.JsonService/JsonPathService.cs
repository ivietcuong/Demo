namespace Demo.Net.Infrast.Impl.SQLiteService
{
	public class JsonPathService : IJsonPathService
	{
		public string GetPath()
		{
			return @"..\..\..\DataSource\points.json";
		}
	}
}
