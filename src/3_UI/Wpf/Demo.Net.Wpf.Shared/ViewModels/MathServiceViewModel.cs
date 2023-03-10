using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System.Collections.Generic;

namespace Demo.Net.Wpf.Shared.ViewModels
{
    public abstract partial class MathServiceViewModel : ObservableValidator
    {
        protected ILogger? _logger;
        public IMathService? MathService { get; protected set; }

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

        public virtual bool IsValidated(double coefficienta, double coefficientb, double coefficientc)
        {
            return true;
        }
        public virtual IEnumerable<Point> Calculate(IEnumerable<Point> points, double coefficienta, double coefficientb, double coefficientc)
        {
            if (MathService == null)
                return points;

            return MathService.Calculate(points, coefficienta, coefficientb, coefficientc);
        }
    }
}
