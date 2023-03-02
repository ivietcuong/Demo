using Demo.NetStandard.Core.Services;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
	public class XmlControlViewModel
	{
		private readonly IPointService _pointService;

		public XmlControlViewModel(IPointService pointService)
		{
			_pointService = pointService;
		}
	}
}
