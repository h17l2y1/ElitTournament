using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ElitTournament.Viber.Core.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum KeyboardInputFieldState
	{
		[EnumMember(Value = "regular")]
		Regular = 1,

		[EnumMember(Value = "minimized")]
		Minimized = 2,

		[EnumMember(Value = "hidden")]
		Hidden = 3
	}
}