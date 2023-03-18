using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;
using Demo.NetStandard.Core.Entities;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class LogarithmMath : WorkspaceBase, IWorkspace
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

        private async void OnCalculate()
        {
            Random rd = new Random();

            //Points = Points?.Select(p => new Point() { X = p.X, Y = p.Y - rd.Next(1000, 10000) });

            Points = MathService?.Calculate(Points, 3, 3, 3);

            await InvokeAsync(StateHasChanged);
        }
    }
}
