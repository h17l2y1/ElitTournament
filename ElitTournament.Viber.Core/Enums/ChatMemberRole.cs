using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ElitTournament.Viber.Core.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ChatMemberRole
	{
		Admin = 1,
		Participant = 2
	}
}