using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

using Demo.Net.Maui.Shared.Services;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System.Collections;
using System.ComponentModel;

namespace Demo.Net.Maui.SQLitePresenter.ViewModels
{
    public partial class MathServiceViewModel : ObservableRecipient/*, INotifyDataErrorInfo*/
    {
        protected ILogger? Logger;

        [ObservableProperty]
        private double _coefficientA;

        [ObservableProperty]
        private double _coefficientB;

        [ObservableProperty]
        private double _coefficientC;

        [ObservableProperty]
        private IMathService? _mathService;

        //public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

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
            //get => string.Join(Environment.NewLine, GetErrors().Cast<string>().ToList().Select(e => e));
            get;
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

        //public bool HasErrors => throw new NotImplementedException();
        public MathServiceViewModel()
        {
            IsActive = true;
        }
        protected override void OnActivated()
        {
            Messenger.Register<MathServiceViewModel, MathServiceChangedMessage>(this, (r, m) =>
            {
            });
        }

        public IEnumerable<NetStandard.Core.Entities.Point> Calculate(IEnumerable<NetStandard.Core.Entities.Point> points)
        {
            if (MathService == null)
                return points;

            return MathService.Calculate(points, CoefficientA, CoefficientB, CoefficientB);
        }

        //public IEnumerable GetErrors(string? propertyName = null)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
