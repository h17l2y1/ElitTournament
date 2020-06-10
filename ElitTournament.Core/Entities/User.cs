using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElitTournament.Core.Entities
{
	public class User
	{
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		public string ClientId { get; set; }

		public bool IsViber { get; set; }

		public bool IsTelegram { get; set; }

		public string PrimaryDeviceOS { get; set; }

		public string ViberVersion { get; set; }

		public int Mcc { get; set; }

		public int Mnc { get; set; }

		public string DeviceType { get; set; }

		public string Country { get; set; }

		public string Language { get; set; }

		public double ApiVersion { get; set; }

		public string Name { get; set; }

		public string Avatar { get; set; }

		public string Username { get; set; }

	}
}
