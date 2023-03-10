using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Shared;
using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.JsonPresenter.ViewModels
{
    public partial class JsonControlViewModel : ObservableObject
    {
        private readonly ILogger _logger;
        private readonly IPointService _pointService;

        [ObservableProperty]
        private MathServiceViewModel? _selectedMathService;

        [ObservableProperty]
        private ObservableCollection<Point> _points = new();

        [ObservableProperty]
        private ObservableCollection<MathServiceViewModel> _mathServices = new();

        public JsonControlViewModel(IPointService pointService, IEnumerable<MathServiceViewModel> mathServiceViewModels, ILogger<JsonControlViewModel> logger)
        {
            _logger = logger;
            _pointService = pointService;
            MathServices = new ObservableCollection<MathServiceViewModel>(mathServiceViewModels);

            GetPoints().InitializeData(_logger);
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

        partial void OnSelectedMathServiceChanged(MathServiceViewModel? value)
        {
            if (SelectedMathService != null)
                Points = new ObservableCollection<Point>(SelectedMathService.Calculate(Points, 3, 4, 5));
        }
    }
}

