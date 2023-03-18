using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Demo.NetStandard.Core.Extentions;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
    public partial class XmlViewModel : ObservableObject
    {
        private readonly ILogger _logger;
        private readonly IPointService _pointService;

        private MathServiceViewModel? _selectedMathService;

        [ObservableProperty]
        private List<Point> _points = new();

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

        [ObservableProperty]
        private List<MathServiceViewModel> _mathServices = new();

        public XmlViewModel(IPointService pointService, IEnumerable<MathServiceViewModel> mathServiceViewModels, ILogger<XmlViewModel> logger)
        {
            _logger = logger;
            _pointService = pointService;
            MathServices = mathServiceViewModels.ToList();

            GetPoints().Run();
        }

        [RelayCommand(CanExecute = nameof(CanExecute))]
        private void Calculate()
        {
            if (SelectedMathService != null)
                Points = new List<Point>(SelectedMathService.Calculate(Points));
        }

        private bool CanExecute()
        {
            return SelectedMathService != null && !SelectedMathService.HasErrors && (SelectedMathService is ParabolaMathServiceViewModel || SelectedMathService is LogarithmMathServiceViewModel);
        }

        private async Task GetPoints()
        {
            try
            {
                var points = await _pointService.GetPointListAsync();
                var meanvalue = points.Count() / 2;
                Points = new List<Point>(points.Select(p => new Point() { X = p.X - meanvalue, Y = p.Y }));
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
