﻿namespace Demo.Net.Infrast.Impl.XmlService
{
	public class XmlPathService : IXmlPathService
	{
		public string GetPath()
		{
			return @"..\..\..\DataSource\points.xml";
		}
	}
}
