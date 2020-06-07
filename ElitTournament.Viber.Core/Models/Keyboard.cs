using ElitTournament.Viber.Core.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ElitTournament.Viber.Core.Models.Message
{
	public class Keyboard
	{
		[JsonProperty("Type")]
		private const string Type = "keyboard";

		[JsonProperty("Buttons")]
		public ICollection<Button> Buttons { get; set; }

		[JsonProperty("DefaultHeight")]
		public bool DefaultHeight { get; set; }

		[JsonProperty("BgColor")]
		public string BackgroundColor { get; set; }

		[JsonProperty("CustomDefaultHeight")]
		public int? CustomDefaultHeight { get; set; }

		[JsonProperty("HeightScale")]
		public int? HeightScale { get; set; }

		[JsonProperty("ButtonsGroupColumns")]
		public int? ButtonsGroupColumns { get; set; }

		[JsonProperty("ButtonsGroupRows")]
		public int? ButtonsGroupRows { get; set; }

		[JsonProperty("InputFieldState")]
		public KeyboardInputFieldState? InputFieldState { get; set; }
	}
}
