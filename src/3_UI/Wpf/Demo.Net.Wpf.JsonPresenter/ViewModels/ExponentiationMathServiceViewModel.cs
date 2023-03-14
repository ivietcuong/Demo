using Demo.Net.Wpf.Shared.ViewModels;
using Demo.Net.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
    public class ExponentiationMathServiceViewModel : MathServiceViewModel
    {        
        public ExponentiationMathServiceViewModel(IMathService mathService, ILogger<ExponentiationMathServiceViewModel> logger)
        {
            Logger = logger;
            CoefficientA = 3;
            MathService = mathService;
        }
    }
}
