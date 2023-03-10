using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
    public class LogarithmMathServiceViewModel : MathServiceViewModel
    {
        public LogarithmMathServiceViewModel(IMathService mathService, ILogger<LogarithmMathServiceViewModel> logger)
        {
            _logger = logger;
            MathService = mathService;
        }
    }
}
