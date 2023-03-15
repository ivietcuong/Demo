using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

using Point = Demo.NetStandard.Core.Entities.Point;
namespace Demo.Net.Maui.UIApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<Point> _points;

        public HomeViewModel()
        {
            Points = new ObservableCollection<Point>(NetStandard.Core.DataSource.Data.Points);
        }
    }
}
