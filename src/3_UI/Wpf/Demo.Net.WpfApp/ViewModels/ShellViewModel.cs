using Demo.Net.Wpf.Core;

using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.WpfApp.ViewModels
{
    public class ShellViewModel
    {
        public ShellViewModel(IEnumerable<IView> views)
        {
            var xmlview = views.First();
        }
    }
}
