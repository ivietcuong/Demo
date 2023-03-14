
using Demo.Net.Infrast.Impl.XmlService;

namespace Demo.Infrast.Impl.XmlService.Test
{
	public class TestXmlPathService : IXmlPathService
	{
		public string GetPath()
		{
			return @"..\..\..\..\..\..\DataSource\points.xml";
		}
	}
}
