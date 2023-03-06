using Demo.NetStandard.Core.Interfaces;
using Demo.NetStandard.Infrast.JsonService.Impl;

namespace Demo.Infrastructure.Test
{
	public class JsonContextFixture
	{
		private readonly IUnitOfWork _unitOfWork;

		public JsonContextFixture()
		{
			_unitOfWork = new JsonContext();
		}

		[Fact]
		public async void CreateJsonFileFixture()
		{
			await (_unitOfWork as JsonContext).ReadDataAsync();
		}
	}
}