using Microsoft.AspNetCore.Components.Rendering;

namespace Demo.Net.Blazor.Shared
{
	public static class RenderTreeBuilderExtensions
	{
		public static void AddAttributes(this RenderTreeBuilder builder, ref int sequence, IEnumerable<KeyValuePair<string, object>> attributes)
		{
			if (attributes != null)
				foreach (var attribute in attributes)
					builder.AddAttribute(sequence++, attribute.Key, attribute.Value);
		}
	}
}
