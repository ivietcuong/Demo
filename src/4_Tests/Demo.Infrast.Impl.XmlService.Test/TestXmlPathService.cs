using Demo.NetStandard.Infrast.Impl.XmlService;

namespace Demo.Infrast.Impl.XmlService.Test
{
	internal class TestXmlPathService : IXmlPathService
	{
		public string GetPath()
		{
			return @"..\..\..\..\..\..\DataSource\points.xml";
		}
	}
}
