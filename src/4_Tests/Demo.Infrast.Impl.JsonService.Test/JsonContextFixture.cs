using Demo.NetStandard.Core.Data;
using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Infrast.JsonService.Impl;

namespace Demo.Infrast.Impl.JsonSerivce.Test
{
	public class JsonContextFixture
	{
		private readonly IUnitOfWork _unitOfWork;

		public JsonContextFixture()
		{
			_unitOfWork = new JsonContext();
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
	}
}