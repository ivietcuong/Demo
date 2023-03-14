using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace Demo.NetStandard.Core.Entities
{
	public class Point
	{
		[XmlIgnore]
		[JsonIgnore]
		public int ID { get; set; }
		public double X { get; set; }
		public double Y { get; set; }

		public override string ToString()
		{
			return $"x = {X}: y = {Y}";
		}
	}
}
