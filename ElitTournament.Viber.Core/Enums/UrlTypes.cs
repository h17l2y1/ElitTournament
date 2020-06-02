using System.Runtime.Serialization;

namespace ElitTournament.Viber.Core.Enums
{
	public enum UrlTypes
	{
		[EnumMember(Value = "none")]
		None = 0,

		[EnumMember(Value = "set_webhook")]
		set_webhook = 1,

		[EnumMember(Value = "get_account_info")]
		get_account_info = 2,

		[EnumMember(Value = "send_message")]
		send_message = 3
	}
}

