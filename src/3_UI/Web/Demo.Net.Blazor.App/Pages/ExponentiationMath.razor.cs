using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class ExponentiationMath : MathWorkspaceBase, IWorkspace
    {
        public Type Type
        {
            get => GetType();
        }

        public string Name
        {
            get => "Exponentiation";
        }
     
        [Inject, AllowNull]
        public IExponentiationMathService? MathService
        {
            get; set;
        }

        public ExponentiationMath()
        {
            CoefficientA = 2;
            CoefficientB = 4;
            CoefficientC = 6;
        }

        public override void OnCalculate()
        {
            Points = MathService?.Calculate(Points, CoefficientA, CoefficientB, CoefficientC);
        }
    }
}
