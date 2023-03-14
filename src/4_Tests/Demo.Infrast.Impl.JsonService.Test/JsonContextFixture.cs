using Demo.Infrast.Impl.JsonService.Test;
using Demo.Net.Core.DataSource;
using Demo.Net.Core.Entities;
using Demo.Net.Core.Interfaces;
using Demo.Net.Core.Services;
using Demo.Net.Infrast.Impl.SQLiteService;

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
		public async void IsWrittingJsonFile_Successful()
		{
			await ((JsonContext)_unitOfWork).WriteJsonDataAsync();
			var result = File.Exists(_pathService.GetPath());
			Assert.True(result);
		}

		[Fact]
		public async void DataCollectionInJsonFile_IsTheSame_AsSourceCollection()
		{
			var result = await ((JsonContext)_unitOfWork).ReadJsonDataAsync();
			Assert.True(result.Any());
			Assert.Equal(Data.Points.Count, result.Count());
		}

		[Fact]
		public async void PointSet_Is_Not_Empty()
		{
			var result = await _unitOfWork.SetAsync<Point>();
			Assert.True(result.Any());
			Assert.Equal(Data.Points.Count, result.Count());
		}

		[Fact]
		public async void Points_From_Repository_Are_Equal_To_Points_From_Context()
		{
			var pointsInContext = await _unitOfWork.SetAsync<Point>();
			var pointsInRepo = await _asyncRepository.GetAsync<Point>();

			Assert.Equal(pointsInContext.Count(), pointsInRepo.Count());
		}

		[Fact]
		public async void Throw_Exception_When_Call_AddAsync()
		{
			await Assert.ThrowsAsync<NotImplementedException>(async () =>
			{
				await _asyncRepository.AddAsync<Point>(new Point());
			});
		}

		[Fact]
		public async void GetPointsFromService()
		{
			var result = await _pointService.GetPointListAsync();
			Assert.True(result.Any());
		}
	}
}