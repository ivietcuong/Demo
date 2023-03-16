namespace Demo.Net.Blazor.App.Data
{
    public interface IDemoData
    {
        string Name { get; }
    }
    public class DemoData : IDemoData {
        public string Name { get; } = "Le Viet Cuong";
    }
}
