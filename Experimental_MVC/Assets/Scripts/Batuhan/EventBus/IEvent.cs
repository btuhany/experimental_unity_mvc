
namespace Assets.Scripts.Batuhan.EventBus
{
    public enum EventCategoryID
    {
        Global = 0,
        UI = 1, //Notifies view and controller
        Input = 2, //Notifies controller
        View = 3, //Notifies view
        Controller = 4,
        Model = 5
    }
    public interface IEvent { }
}
