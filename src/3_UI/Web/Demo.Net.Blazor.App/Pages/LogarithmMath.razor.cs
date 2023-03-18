using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class LogarithmMath : MathWorkspaceBase, IWorkspace
    {
        public Type Type
        {
            get => GetType();
        }

        public string Name
        {
            get => "Logarithm";
        }

        private double _coefficientb;
        public override double CoefficientB
        {
            get => _coefficientb;
            set
            {
                _coefficientb = value;
                ValidationMessage = MathService?.Validate(value, CoefficientB, CoefficientC);
                Disabled = !string.IsNullOrEmpty(ValidationMessage);
            }
        }

        [Inject, AllowNull]
        public ILogarithmMathService? MathService { get; set; }

        public LogarithmMath()
        {
            CoefficientA = 0;
            CoefficientB = 4;
            CoefficientC = 0;
        }

        public override void OnCalculate()
        {
            Points = MathService?.Calculate(Points, CoefficientA, CoefficientB, CoefficientC);
        }
    }
}
