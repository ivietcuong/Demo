using Demo.Net.Core.Entities;
using Demo.Net.Core.Interfaces;
using Demo.Net.Core.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Net.Infrast.Impl.XmlService
{
	public class XmlPointService : IPointService
	{
		private readonly IAsyncRepository _repository;

		public XmlPointService(IAsyncRepository repository)
		{
			_repository = repository;
		}

		public async Task<IEnumerable<Point>> GetPointListAsync()
		{
			var result = await _repository.GetAsync<Point>();
			return result;
		}
	}
}
