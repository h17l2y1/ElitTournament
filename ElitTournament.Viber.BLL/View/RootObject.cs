namespace ElitTournament.Viber.BLL.View
{
    public class RootObject1
    {
        public int status { get; set; }
        public string status_message { get; set; }
        public string[] event_types { get; set; }
    }

    public class RootObject
    {
        public string Event { get; set; }
        public long Timestamp { get; set; }
        public string Chat_hostname { get; set; }
        public long Message_token { get; set; }
        public Sender Sender { get; set; }
        public Message Message { get; set; }
        public bool Silent { get; set; }
    }

    public class Sender
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Language { get; set; }
        public string Country { get; set; }
        public int Api_version { get; set; }
    }

    public class Message
    {
        public string Text { get; set; }
        public string Type { get; set; }
    }
}
