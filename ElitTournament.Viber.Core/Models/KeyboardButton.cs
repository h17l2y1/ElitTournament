using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;

namespace ElitTournament.Viber.Core.Models
{
	public class KeyboardButton
	{
		[JsonProperty("ActionType")]
		public KeyboardActionType? ActionType { get; set; }

		[JsonProperty("ActionBody")]
		public string ActionBody { get; set; }

		[JsonProperty("Text")]
		public string Text { get; set; }

		[JsonProperty("TextSize")]
		public TextSize? TextSize { get; set; }

		[JsonProperty("Columns")]
		public int? Columns { get; set; }

		[JsonProperty("Rows")]
		public int? Rows { get; set; }

		[JsonProperty("BgColor")]
		public string BackgroundColor { get; set; }

		[JsonProperty("Silent")]
		public bool? Silent { get; set; }

		[JsonProperty("BgMediaType")]
		public BackgroundMediaType? BackgroundMediaType { get; set; }

		[JsonProperty("BgMedia")]
		public string BackgroundMedia { get; set; }

		[JsonProperty("BgLoop")]
		public bool? BackgroundLoop { get; set; }

		[JsonProperty("Image")]
		public string Image { get; set; }

		[JsonProperty("TextVAlign")]
		public TextVerticalAlign? TextVerticalAlign { get; set; }

		[JsonProperty("TextHAlign")]
		public TextHorizontalAlign? TextHorizontalAlign { get; set; }

		[JsonProperty("TextPaddings")]
		public int[] TextPaddings { get; set; }

		[JsonProperty("TextOpacity")]
		public int? TextOpacity { get; set; }

		[JsonProperty("OpenURLType")]
		public OpenUrlType? UrlOpenType { get; set; }

		[JsonProperty("OpenURLMediaType")]
		public OpenUrlMediaType? UrlMediaType { get; set; }

		[JsonProperty("TextBgGradientColor")]
		public string TextBackgroundGradientColor { get; set; }
	}
}
