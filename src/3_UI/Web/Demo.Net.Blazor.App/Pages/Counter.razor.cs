﻿using Demo.Net.Blazor.Shared;

using Microsoft.AspNetCore.Components;

namespace Demo.Net.Blazor.App.Pages
{
    public partial class Counter : LayoutComponentBase, IWorkspace
    {
        private int currentCount = 0;

        public Type Type { get => GetType(); }
        public string Name { get => nameof(Counter); }
        public Dictionary<string, object?> Parameters { get; } = new Dictionary<string, object?>();

        private void IncrementCount()
        {
            currentCount++;
        }
    }
}