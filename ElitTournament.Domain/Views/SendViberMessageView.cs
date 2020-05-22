using System.Collections.Generic;

namespace ElitTournament.Domain.Views
{
    public class SendViberMessageView
    {
        public string Receiver { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public ViberKeyboard Keyboard { get; set; }
    }

    public class Button
    {
        public string ActionType { get; set; }
        public string ActionBody { get; set; }
        public string Text { get; set; }
        public string TextSize { get; set; }
    }

    public class ViberKeyboard
    {
        public string Type { get; set; }
        public bool DefaultHeight { get; set; }
        public List<Button> Buttons { get; set; } = new List<Button>();
    }
}
