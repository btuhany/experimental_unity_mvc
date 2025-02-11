using Batuhan.Core.MVC;
using System;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal interface ICircleContext : IContext
    {
        public Action ChangeColorEvent { get; set; }
    }
    internal class CircleContext : ICircleContext
    {
        public Action ChangeColorEvent { get; set; }
    }
}
