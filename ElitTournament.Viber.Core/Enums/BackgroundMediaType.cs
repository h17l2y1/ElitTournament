using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ElitTournament.Viber.Core.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum BackgroundMediaType
	{
		[EnumMember(Value = "picture")]
		Picture = 1,

		[EnumMember(Value = "gif")]
		Gif = 2
	}
}
