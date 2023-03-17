using Demo.Net.Blazor.App.Services;
using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class TangentMath : WorkspaceBase, IWorkspace
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
	}
}
