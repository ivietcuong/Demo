using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

using System.Diagnostics.CodeAnalysis;

namespace Demo.Net.Blazor.Shared
{
    public abstract class WorkspaceBase : ComponentBase
    {
        protected IEnumerable<Point>? Points;

        public IEnumerable<double>? _x;
        public IEnumerable<double>? _y;

        [Inject, AllowNull]
        public IJSRuntime? JSRuntime
        {
            get; set;
        }

        [Inject, AllowNull]
        public IPointService? PointService
        {
            get; set;
        }

        public Dictionary<string, object?> Parameters
        {
            get; set;
        }

        public WorkspaceBase()
        {
            Parameters = new Dictionary<string, object?>();
        }

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            if (PointService == null)
                return;

            var points = await PointService.GetPointListAsync();
            Points = points.Select(p => new Point() { X = p.X - points.Count() / 2, Y = p.Y });

            _x = Points.Select(p => p.X);
            _y = Points.Select(p => p.Y);
        }
    }
}
