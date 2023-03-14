using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using Demo.Net.Wpf.Shared;
using Demo.Net.Wpf.Shared.ViewModels;
using Demo.Net.Core.Entities;
using Demo.Net.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
    public partial class JsonViewModel : ObservableObject
    {
        private readonly ILogger _logger;
        private readonly IPointService _pointService;

        private MathServiceViewModel? _selectedMathService;

        [ObservableProperty]
        private ObservableCollection<Point> _points = new();

        [ObservableProperty]
        private ObservableCollection<MathServiceViewModel> _mathServices = new();

        public MathServiceViewModel? SelectedMathService
        {
            get => _selectedMathService;
            set
            {
                if (value == null)
                    return;

                if (_selectedMathService != null)
                    _selectedMathService.ErrorsChanged -= OnSelectedMathServiceErrorsChanged;

                SetProperty(ref _selectedMathService, value);
                CalculateCommand.NotifyCanExecuteChanged();
                _selectedMathService.ErrorsChanged += OnSelectedMathServiceErrorsChanged;
            }
        }

        public JsonViewModel(IPointService pointService, IEnumerable<MathServiceViewModel> mathServiceViewModels, ILogger<JsonViewModel> logger)
        {
            _logger = logger;
            _pointService = pointService;
            MathServices = new ObservableCollection<MathServiceViewModel>(mathServiceViewModels);

            GetPoints().InitializeData(_logger);
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void Calculate()
        {
            if (SelectedMathService != null)
                Points = new ObservableCollection<Point>(SelectedMathService.Calculate(Points));
        }

        private bool CanExecute()
        {
            return SelectedMathService != null && 
                   !SelectedMathService.HasErrors && 
                   (SelectedMathService is ExponentiationMathServiceViewModel || SelectedMathService is TangentMathServiceViewModel);
        }

        private async Task GetPoints()
        {
            try
            {
                var points = await _pointService.GetPointListAsync();
                var meanvalue = points.Count() / 2;
                Points = new ObservableCollection<Point>(points.Select(p => new Point() { X = p.X - meanvalue, Y = p.Y }));
                _logger.LogTrace($"{nameof(GetPoints)} {Points.Count}");
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        private void OnSelectedMathServiceErrorsChanged(object? sender, DataErrorsChangedEventArgs e)
        {
            CalculateCommand.NotifyCanExecuteChanged();
        }

    }
}

