﻿namespace Demo.Net.Infrast.Impl.EfCoreService
{
	public class JsonPathService : IJsonPathService
	{
		public string GetPath()
		{
			return @"..\..\..\DataSource\points.json";
		}
	}
}