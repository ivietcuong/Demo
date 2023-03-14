using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
    public class TangentMathServiceViewModel : MathServiceViewModel
    {
        public TangentMathServiceViewModel(IMathService mathService, ILogger<TangentMathServiceViewModel> logger)
        {
            Logger = logger;
            MathService = mathService;
        }
    }
}
