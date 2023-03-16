using Demo.Net.Blazor.Shared;

namespace Demo.Net.Blazor.App.Shared
{
    public partial class SurveyPrompt : IWorkspace
    {
        public Type Type { get => GetType(); }
        public string Name { get => nameof(SurveyPrompt); }
        public Dictionary<string, object?> Properties { get; }
    }
}
