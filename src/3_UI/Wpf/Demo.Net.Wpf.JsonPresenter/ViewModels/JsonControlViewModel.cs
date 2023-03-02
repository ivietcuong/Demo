using Demo.NetStandard.Core.Services;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
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

