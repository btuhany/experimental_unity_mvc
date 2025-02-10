using Assets.Scripts.LoggerExample.MVC.Entities.Counter;
using Zenject;

namespace Assets.Scripts.LoggerExample.MVC.Entities.Circle
{
    internal class CircleFactory : IFactory<CircleController>
    {
        readonly DiContainer _container;
        readonly CircleView _circleViewPrefab;
        readonly CircleContext _circleContext;

        [Inject]
        public CircleFactory(DiContainer container, CircleView circleViewPrefab, CircleContext circleContext)
        {
            _container = container;
            _circleViewPrefab = circleViewPrefab;
            _circleContext = circleContext;
        }
        public CircleController Create()
        {
            //can bind model in install process
            var model = _container.Instantiate<CircleModel>();

            var view = _container.InstantiatePrefabForComponent<CircleView>(_circleViewPrefab);

            var controller = _container.Instantiate<CircleController>(
                new object[] { model, view, _circleContext }
            );

            return controller;
        }
    }
}
