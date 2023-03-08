using CommunityToolkit.Mvvm.ComponentModel;

using Demo.NetStandard.Core.Data;
using Demo.NetStandard.Core.Entities;

using OxyPlot;
using OxyPlot.Series;

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Demo.Net.WpfApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private PlotModel? _model;

        [ObservableProperty]
        public ObservableCollection<Point> _points = null!;

        public HomeViewModel()
        {
            Points = new ObservableCollection<Point>(Data.Points);
        }
    }
}
