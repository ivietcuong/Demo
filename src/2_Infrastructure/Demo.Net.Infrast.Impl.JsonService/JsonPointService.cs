using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Net.Infrast.Impl.JsonService
{
	public class JsonPointService : IPointService
	{
		private readonly IAsyncRepository _repository;

		public JsonPointService(IAsyncRepository repository)
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
