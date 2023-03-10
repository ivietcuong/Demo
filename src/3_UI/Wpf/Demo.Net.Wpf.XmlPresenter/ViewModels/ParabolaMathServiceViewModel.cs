using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
    public class ParabolaMathServiceViewModel : MathServiceViewModel
    {
        public ParabolaMathServiceViewModel(IMathService mathService, ILogger<ParabolaMathServiceViewModel> logger)
        {
            _logger = logger;
            MathService = mathService;
        }
    }
}
