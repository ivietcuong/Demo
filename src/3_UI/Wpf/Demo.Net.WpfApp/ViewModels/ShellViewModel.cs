using Demo.Net.Wpf.Core;

using System.Collections.Generic;
using System.Linq;

namespace Demo.Net.WpfApp.ViewModels
{
    public class ShellViewModel
    {

        private object _content = null!;

        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }

        public ShellViewModel(IEnumerable<IWorkspace> views)
        {
            var xmlview = views.First();
        }
    }
}
