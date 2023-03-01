using Demo.NetStandard.Core.Services;

namespace Demo.Net.Wpf.JsonView.ViewModels
{
	public class JsonControlViewModel
	{
		private readonly IPointService _pointService;

		public JsonControlViewModel(IPointService pointService)
		{
			_pointService = pointService;
		}
	}
}

