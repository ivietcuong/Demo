using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class ParabolaMath : MathWorkspaceBase, IWorkspace
    {
        public Type Type
        {
            get => GetType();
        }
        public string Name
        {
            get => "Parabola";
        }

        private double _coefficienta;
        public override double CoefficientA
        {
            get => _coefficienta;
            set
            {
                _coefficienta = value;
                ValidationMessage = MathService?.Validate(value, CoefficientB, CoefficientC);
                Disabled = !string.IsNullOrEmpty(ValidationMessage);
            }
        }

        [Inject, AllowNull]
        public IParabolaMathService? MathService { get; set; }

        public ParabolaMath()
        {
            CoefficientA = 4;
            CoefficientB = 4;
            CoefficientC = 4;
        }

        public override void OnCalculate()
        {
            Points = MathService?.Calculate(Points, CoefficientA, CoefficientB, CoefficientC);
        }
    }
}
