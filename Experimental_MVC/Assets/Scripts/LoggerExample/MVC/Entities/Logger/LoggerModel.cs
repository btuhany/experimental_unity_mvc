using Assets.Scripts.Batuhan.Core.MVC.Base;
using Batuhan.Core.MVC;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Logger
{
    internal class LoggerModel : BaseModel
    {
        private int _counter = 0;
        private const string _logPrefix = "(CounterLoggerModel) ";
        public override void Initialize(IContext context)
        {
            base.Initialize(context);
            _counter = 0;
        }


        //TODO Implement Observable Pattern
        public void IncreaseCounter(int value = 1)
        {
            _counter+= value;
        }
    }
}
