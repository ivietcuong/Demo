namespace Demo.NetStandard.Infrast.Impl.XmlService
{
	public class XmlPathService : IXmlPathService
	{
		public string GetPath()
		{
			return @"..\..\..\DataSource\points.xml";
		}
	}
}
