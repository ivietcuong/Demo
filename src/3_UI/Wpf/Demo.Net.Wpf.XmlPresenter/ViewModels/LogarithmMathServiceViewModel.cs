using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System.ComponentModel.DataAnnotations;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
    public class LogarithmMathServiceViewModel : MathServiceViewModel
    {
        private double _coefficientB = 2;

        [CustomValidation(typeof(LogarithmMathServiceViewModel), nameof(ValidateLogarithmMathService))]
        public override double CoefficientB
        {
            get => _coefficientB;
            set
            {
                SetProperty(ref _coefficientB, value, true);
                OnPropertyChanged(nameof(Message));
            }
        }

        public LogarithmMathServiceViewModel(IMathService mathService, ILogger<LogarithmMathServiceViewModel> logger)
        {
            Logger = logger;
            MathService = mathService;
        }

        public static ValidationResult? ValidateLogarithmMathService(double @value, ValidationContext context)
        {
            if (value <= 0)
                return new ValidationResult("Coefficient B should be greater than zero");
            if (value == 1)
                return new ValidationResult("Coefficient B should not equal 1");

            var instance = (LogarithmMathServiceViewModel)context.ObjectInstance;
            instance.ClearErrors(nameof(CoefficientB));

            return ValidationResult.Success;
        }
    }
}
