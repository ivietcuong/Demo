using Demo.Net.Blazor.App.Data;
using Demo.Net.Blazor.App.Pages;

using Microsoft.AspNetCore.Components;

namespace Demo.Net.Blazor.App.Shared
{
    public partial class Shell : LayoutComponentBase
    {

        [Inject]
        public IDemoData? DemoData { get; set; }

        public Shell()
        {
            Body = builder => 
            {
                builder.OpenComponent(0, typeof(Counter));
                builder.CloseComponent();
            };
        }      
        protected override void OnInitialized()
        {
            base.OnInitialized();
        }
    }
}
