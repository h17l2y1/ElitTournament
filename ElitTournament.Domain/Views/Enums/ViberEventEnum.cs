namespace ElitTournament.Domain.Views.Enums
{
    public enum ViberEventEnum
    {
        webhook,
        subscribed,
        unsubscribed,
        conversation_started,
        delivered,
        seen,
        failed,
        message
    }

    public enum EventTypes
    {
        delivered,
        seen,
        failed,
        subscribed,
        unsubscribed,
        conversation_started
    }
}
