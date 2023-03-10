using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System.ComponentModel.DataAnnotations;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
    public class ParabolaMathServiceViewModel : MathServiceViewModel
    {
        private double _coefficientA = 1;
        private double _coefficientB = 1;
        private double _coefficientC = 1;
        
        [CustomValidation(typeof(ParabolaMathServiceViewModel), nameof(ValidateParabolaMathService))]
        public override double CoefficientA
        {
            get => _coefficientA;
            set 
            {
                SetProperty(ref _coefficientA, value, true);
                OnPropertyChanged(nameof(Message));
            }
        }
        public override double CoefficientB
        {
            get => _coefficientB;
            set => SetProperty(ref _coefficientB, value);
        }
        public override double CoefficientC
        {
            get => _coefficientC;
            set => SetProperty(ref _coefficientC, value);
        }
        public ParabolaMathServiceViewModel(IMathService mathService, ILogger<ParabolaMathServiceViewModel> logger)
        {
            Logger = logger;
            MathService = mathService;
        }
        public static ValidationResult? ValidateParabolaMathService(double @value, ValidationContext context)
        {
            if (value == 0)
                return new ValidationResult("Coefficient A should not be zero");

            var instance = (ParabolaMathServiceViewModel)context.ObjectInstance;
            instance.ClearErrors(nameof(CoefficientA));

            return ValidationResult.Success;
        }
    }
}
