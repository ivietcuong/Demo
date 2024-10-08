using Demo.Infrast.Impl.JsonService.Test;
using Demo.Net.Infrast.Impl.JsonService;
using Demo.NetStandard.Core.DataSource;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Demo.Infrast.Impl.JsonSerivce.Test
{
	public class JsonContextFixture
	{
		private IUnitOfWork _unitOfWork;
		private IPointService _pointService;
		private ILogger<JsonContext> _logger;
		private IJsonPathService _pathService;
		private IAsyncRepository _asyncRepository;

		public JsonContextFixture()
		{
			_logger = new NullLogger<JsonContext>();
			_pathService = new TestJsonPathService();
			_unitOfWork = new JsonContext(_pathService, _logger);
			_asyncRepository = new AsyncJsonRepository(_unitOfWork);
			_pointService = new JsonPointService(_asyncRepository);
		}

		[Fact]
		public async Task IsWrittingJsonFile_Successful()
		{
			await ((JsonContext)_unitOfWork).WriteJsonDataAsync();
			var result = File.Exists(_pathService.GetPath());
			Assert.True(result);
		}

		[Fact]
		public async Task DataCollectionInJsonFile_IsTheSame_AsSourceCollection()
		{
			var result = await ((JsonContext)_unitOfWork).ReadJsonDataAsync();
			Assert.True(result.Any());
			Assert.Equal(Data.Points.Count, result.Count());
		}

		[Fact]
		public async Task PointSet_Is_Not_Empty()
		{
			var result = await _unitOfWork.SetAsync<Point>();
			Assert.True(result.Any());
			Assert.Equal(Data.Points.Count, result.Count());
		}

		[Fact]
		public async Task Points_From_Repository_Are_Equal_To_Points_From_Context()
		{
			var pointsInContext = await _unitOfWork.SetAsync<Point>();
			var pointsInRepo = await _asyncRepository.GetAsync<Point>();

			Assert.Equal(pointsInContext.Count(), pointsInRepo.Count());
		}

		[Fact]
		public async Task Throw_Exception_When_Call_AddAsync()
		{
			await Assert.ThrowsAsync<NotImplementedException>(async () =>
			{
				await _asyncRepository.AddAsync<Point>(new Point());
			});
		}

		[Fact]
		public async Task GetPointsFromService()
		{
			var result = await _pointService.GetPointListAsync();
			Assert.True(result.Any());
		}
	}
}