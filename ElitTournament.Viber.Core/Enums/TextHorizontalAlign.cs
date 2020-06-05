using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ElitTournament.Viber.Core.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum TextHorizontalAlign
	{
		[EnumMember(Value = "left")]
		Left = 1,

		[EnumMember(Value = "center")]
		Center = 2,

		[EnumMember(Value = "right")]
		Right = 3
	}
}