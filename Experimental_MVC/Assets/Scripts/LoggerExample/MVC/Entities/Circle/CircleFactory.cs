using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal class CircleFactory : IFactory<CircleController>
    {
        readonly DiContainer _container;
        readonly CircleView _circleViewPrefab;

        [Inject]
        public CircleFactory(DiContainer container, CircleView circleViewPrefab)
        {
            _container = container;
            _circleViewPrefab = circleViewPrefab;
        }
        public CircleController Create()
        {
            //can bind model in install process
            var model = _container.Resolve<CircleModel>();
            var context = _container.Resolve<ICircleContext>();

            var view = _container.InstantiatePrefabForComponent<CircleView>(_circleViewPrefab);
            view.SetContext(context);

            var controller = _container.Instantiate<CircleController>(
                new object[] { model, view, context }
            );

            return controller;
        }
    }
}
