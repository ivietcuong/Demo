using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
    public class ExponentiationMathServiceViewModel : MathServiceViewModel
    {        
        public ExponentiationMathServiceViewModel(IMathService mathService, ILogger<ExponentiationMathServiceViewModel> logger)
        {
            _logger = logger;
            MathService = mathService;
        }
    }
}
