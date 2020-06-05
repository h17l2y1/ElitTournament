using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ElitTournament.Viber.Core.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OpenUrlType
	{
		[EnumMember(Value = "internal")]
		Internal = 1,

		[EnumMember(Value = "external")]
		External = 2
	}
}