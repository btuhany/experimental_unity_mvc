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
            var model = _container.Instantiate<CircleModel>();

            // Prefab'dan view nesnesini instantiate ediyoruz
            var view = _container.InstantiatePrefabForComponent<CircleView>(_circleViewPrefab);

            // Controller'ı model, view ve context ile birlikte instantiate ediyoruz
            var controller = _container.Instantiate<CircleController>(
                new object[] { model, view, _circleContext }
            );

            // Controller initialize ediliyor
            controller.Initialize();

            return controller;
        }
    }
}
