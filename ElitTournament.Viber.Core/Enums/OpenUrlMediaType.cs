using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;

namespace ElitTournament.Viber.Core.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum OpenUrlMediaType
	{
		[EnumMember(Value = "not-media")]
		NotMedia = 1,

		[EnumMember(Value = "video")]
		Video = 2,

		[EnumMember(Value = "gif")]
		Gif = 3,

		[EnumMember(Value = "picture")]
		Picture = 4,
	}
}