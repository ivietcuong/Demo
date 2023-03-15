using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace Demo.Net.Maui.UIApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<NetStandard.Core.Entities.Point> _points;

        public HomeViewModel()
        {
            Points = new ObservableCollection<NetStandard.Core.Entities.Point>(NetStandard.Core.DataSource.Data.Points);
        }
    }
}
