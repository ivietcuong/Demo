namespace Demo.Net.Infrast.Impl.JsonService
{
	public class JsonPathService : IJsonPathService
	{
		public string GetPath()
		{
			return @"..\..\..\DataSource\points.json";
		}
	}
}
