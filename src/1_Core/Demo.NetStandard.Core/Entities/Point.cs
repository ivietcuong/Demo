namespace Demo.Net.Core.Entities
{
	public class Point
	{
		public double X { get; set; }
		public double Y { get; set; }

		public override string ToString()
		{
			return $"x = {X}: y = {Y}";
		}
	}
}
