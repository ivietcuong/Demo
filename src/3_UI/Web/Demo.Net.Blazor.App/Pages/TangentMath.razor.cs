using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class TangentMath : MathWorkspaceBase, IWorkspace
    {
        public Type Type
        {
            get => GetType();
        }

        public string Name
        {
            get => "Tangent";
        }

        [Inject, AllowNull]
        public ITangentMathService? MathService { get; set; }

        public TangentMath()
        {
            CoefficientA = 5;
            CoefficientB = 5;
            CoefficientC = 5;
        }

        public override void OnCalculate()
        {
            Points = MathService?.Calculate(Points, CoefficientA, CoefficientB, CoefficientC);
        }
    }
}
