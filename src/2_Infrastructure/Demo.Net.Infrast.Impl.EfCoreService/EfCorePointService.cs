using Demo.Net.Core.Entities;
using Demo.Net.Core.Interfaces;
using Demo.Net.Core.Services;

namespace Demo.Net.Infrast.Impl.EfCoreService
{
	public class EfCorePointService : IPointService
	{
		private readonly IAsyncRepository _repository;

		public EfCorePointService(IAsyncRepository repository)
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
