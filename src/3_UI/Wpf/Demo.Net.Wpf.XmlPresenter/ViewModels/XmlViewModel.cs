using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Shared;
using Demo.Net.Wpf.Shared.ViewModels;
using Demo.NetStandard.Core.Entities;
using Demo.NetStandard.Core.Services;

using Microsoft.Extensions.Logging;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Net.Wpf.XmlPresenter.ViewModels
{
    public partial class XmlViewModel : ObservableObject
    {
        private readonly ILogger _logger;
        private readonly IPointService _pointService;

        [ObservableProperty]
        private MathServiceViewModel? _selectedMathService;

        [ObservableProperty]
        private List<Point> _points = new();

        [ObservableProperty]
        private List<MathServiceViewModel> _mathServices = new();

        public XmlViewModel(IPointService pointService, IEnumerable<MathServiceViewModel> mathServiceViewModels, ILogger<XmlViewModel> logger)
        {
            _logger = logger;
            _pointService = pointService;
            MathServices = mathServiceViewModels.ToList();

            GetPoints().InitializeData(_logger);
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

        partial void OnSelectedMathServiceChanged(MathServiceViewModel? value)
        {
            if (SelectedMathService != null)
                Points = new List<Point>(SelectedMathService.Calculate(Points, 1, 2, 3));
        }
    }
}
