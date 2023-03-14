namespace Demo.Net.Infrast.Impl.EfCoreService
{
	public class EfCorePathService : IEfCorePathService
	{
		public string GetPath()
		{
			return @"..\..\..\DataSource\points.json";
		}
	}
}
