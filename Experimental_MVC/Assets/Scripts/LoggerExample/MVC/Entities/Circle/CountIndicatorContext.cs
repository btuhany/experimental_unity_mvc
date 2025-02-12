using Batuhan.MVC.Core;
using System;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal interface ICountIndicatorContext : IContext
    {
        public Action ChangeColorEvent { get; set; }
    }
    internal class CountIndicatorContext : ICountIndicatorContext
    {
        public Action ChangeColorEvent { get; set; }
    }
}
