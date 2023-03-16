namespace Demo.Net.Blazor.Shared
{
	public interface IWorkspace
	{
		public Type Type { get; }
		public string Name { get; }
		public Dictionary<string, object?> Properties { get; }
	}
}
