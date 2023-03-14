using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Core.Entities;
using Demo.Net.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.Wpf.Shared.ViewModels
{
    public abstract partial class MathServiceViewModel : ObservableValidator
    {        
        protected ILogger? Logger;

        public string? Name
        {
            get => MathService?.Name;
            set
            {
                if (MathService == null)
                    return;

                MathService.Name = value;
                OnPropertyChanged();
            }
        }
        public string Message 
        {
            get => string.Join(Environment.NewLine, GetErrors().Select(e => e.ErrorMessage));
        }
        public string? Description
        {
            get => MathService?.Description;
            set
            {
                if (MathService == null)
                    return;

                MathService.Description = value;
                OnPropertyChanged();
            }
        }
        public IMathService? MathService
        {
            get; 
            protected set; 
        }
        public virtual double CoefficientA
        {
            get; 
            set; 
        }
        public virtual double CoefficientB
        {
            get; 
            set; 
        }
        public virtual double CoefficientC
        { 
            get; 
            set; 
        }
        public virtual IEnumerable<Point> Calculate(IEnumerable<Point> points)
        {
            if (MathService == null)
                return points;

            return MathService.Calculate(points, CoefficientA, CoefficientB, CoefficientB);
        }
    }
}
