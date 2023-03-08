using Demo.NetStandard.Core.Data;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Infrast.JsonService.Impl;

namespace Demo.Infrast.Impl.JsonSerivce.Test
{
	public class JsonContextFixture
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IAsyncRepository _asyncRepository;

		public JsonContextFixture()
		{
			_unitOfWork = new JsonContext();
			_asyncRepository = new AsyncJsonRepository(_unitOfWork);
		}

		[Fact]
		public async void IsWrittingJsonFile_Successful()
		{
			await ((JsonContext)_unitOfWork).WriteJsonDataAsync();
			var result = File.Exists(@"..\..\..\..\..\..\DataSource\points.json");
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
	}
}