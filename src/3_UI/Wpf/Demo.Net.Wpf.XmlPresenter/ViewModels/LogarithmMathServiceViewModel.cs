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
            var instance = (LogarithmMathServiceViewModel)context.ObjectInstance;

			if (instance.MathService == null)
				return new ValidationResult($"{nameof(instance.MathService)} is null");

			var result = instance.MathService.Validate(instance.CoefficientA, instance.CoefficientB, instance.CoefficientC);

			if (!string.IsNullOrEmpty(result))
				return new ValidationResult(result);

            instance.ClearErrors(nameof(CoefficientB));

            return ValidationResult.Success;
        }
    }
}
