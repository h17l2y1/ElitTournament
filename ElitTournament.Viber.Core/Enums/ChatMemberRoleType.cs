using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ElitTournament.Viber.Core.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum ChatMemberRoleType
	{
		None = 0,
		Admin = 1,
		Participant = 2
	}
}
