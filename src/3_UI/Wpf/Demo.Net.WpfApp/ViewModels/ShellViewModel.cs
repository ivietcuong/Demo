using CommunityToolkit.Mvvm.ComponentModel;

using Demo.Net.Wpf.Core;

using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.WpfApp.ViewModels
{
    public partial class ShellViewModel : ObservableObject
    {

        [ObservableProperty]
        private object? _content;

        public ShellViewModel(IEnumerable<IWorkspace> views)
        {
            Content = views.ElementAt(1);
        }
    }
}
