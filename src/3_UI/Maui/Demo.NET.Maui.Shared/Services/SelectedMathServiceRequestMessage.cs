using CommunityToolkit.Mvvm.Messaging.Messages;

using Demo.NetStandard.Core.Services;

namespace Demo.Net.Maui.Shared.Services
{
    public sealed class SelectedMathServiceRequestMessage : RequestMessage<IMathService>
    {
    }
}
