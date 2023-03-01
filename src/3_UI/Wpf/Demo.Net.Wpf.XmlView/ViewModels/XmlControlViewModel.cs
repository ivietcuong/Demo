using Demo.NetStandard.Core.Services;

namespace Demo.Net.Wpf.XmlView.ViewModels
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
