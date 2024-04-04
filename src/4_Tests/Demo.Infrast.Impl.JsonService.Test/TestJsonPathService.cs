using Demo.Net.Infrast.Impl.JsonService;

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
