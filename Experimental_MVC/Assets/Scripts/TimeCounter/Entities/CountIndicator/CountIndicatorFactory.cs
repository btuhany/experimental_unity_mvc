using Zenject;

namespace TimeCounter.Entities.CountIndicator
{
    internal class CountIndicatorFactory : IFactory<CountIndicatorController>
    {
        readonly DiContainer _container;
        readonly CountIndicatorView _circleViewPrefab;

        [Inject]
        public CountIndicatorFactory(DiContainer container, CountIndicatorView circleViewPrefab)
        {
            _container = container;
            _circleViewPrefab = circleViewPrefab;
        }
        public CountIndicatorController Create()
        {
            //can bind model in install process
            var model = _container.Resolve<CountIndicatorModel>();
            var context = _container.Resolve<ICountIndicatorContext>();
            var view = _container.InstantiatePrefabForComponent<CountIndicatorView>(_circleViewPrefab);

            var controller = _container.Instantiate<CountIndicatorController>(
                new object[] { model, view, context }
            );

            return controller;
        }
    }
}
