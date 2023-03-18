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

        [Inject, AllowNull]
        public ILogarithmMathService? MathService { get; set; }

        public LogarithmMath()
        {
            CoefficientA = 3;
            CoefficientB = 3;
            CoefficientC = 3;
        }

        public override void OnCalculate()
        {
            Points = MathService?.Calculate(Points, CoefficientA, CoefficientB, CoefficientC);
        }
    }
}
