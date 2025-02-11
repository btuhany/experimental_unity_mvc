namespace Batuhan.EventBus
{
    public interface IEvent //TODOBY generic event category
    {
        public EventCategoryID CategoryID { get; }
    }
}
