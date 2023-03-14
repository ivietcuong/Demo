using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;

namespace Demo.Net.Infrast.Impl.SQLiteService
{
	public class SQLitePointService : IPointService
	{
		private readonly IAsyncRepository _repository;

		public SQLitePointService(IAsyncRepository repository)
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
