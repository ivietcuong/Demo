using Demo.Net.Infrast.Impl.SQLiteService;

namespace Demo.Infrast.Impl.JsonService.Test
{
	internal class TestJsonPathService : IJsonPathService
	{
		public string GetPath()
		{
			return @"..\..\..\..\..\..\DataSource\points.json";
		}
	}
}
