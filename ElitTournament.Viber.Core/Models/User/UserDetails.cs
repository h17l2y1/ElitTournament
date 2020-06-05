using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class UserDetails : UserModel
	{
		[JsonProperty("primary_device_os")]
		public string PrimaryDeviceOS { get; set; }

		[JsonProperty("viber_version")]
		public string ViberVersion { get; set; }

		[JsonProperty("mcc")]
		public int Mcc { get; set; }

		[JsonProperty("mnc")]
		public int Mnc { get; set; }

		[JsonProperty("device_type")]
		public string DeviceType { get; set; }
	}
}
