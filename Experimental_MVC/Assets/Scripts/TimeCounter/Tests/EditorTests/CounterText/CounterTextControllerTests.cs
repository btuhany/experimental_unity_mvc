using NSubstitute;
using NUnit.Framework;
using TimeCounter.Events.ModelEvents;
using TimeCounter.Events.GlobalEvents;
using TimeCounter.Entities.CounterText;
using Batuhan.EventBus;
using Batuhan.CommandManager;
using System;
using Batuhan.MVC.Core;
using TimeCounter.Events.CoreEvents;
using Assets.Scripts.TimeCounter.Helper;

namespace TimeCounter.Tests
{
    [TestFixture]
    public class CounterTextControllerTests
    {
        private ICounterTextModel _model;
        private IViewContextual<ICounterTextContext> _view;
        private ICounterTextContext _context;
        private CounterTextController _controller;

        [SetUp]
        public void SetUp()
        {
            _model = Substitute.For<ICounterTextModel>();
            _view = Substitute.For<IViewContextual<ICounterTextContext>>();
            _context = Substitute.For<ICounterTextContext>();

            _context.Debug.Returns(Substitute.For<IDebugHelper>());
            _context.EventBusModel.Returns(Substitute.For<IEventBus<Model>>());
            _context.EventBusGlobal.Returns(Substitute.For<IEventBus<Global>>());
            _context.EventBusCore.Returns(Substitute.For<IEventBus<Core>>());
            _context.CommandManager.Returns(Substitute.For<ICommandManager>());

            _controller = new CounterTextController(_model, _view, _context);
        }
        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public void Initialize_ShouldSetupModelAndView_AndSubscribeEvents()
        {
            _controller.Initialize();

            _model.Received(1).Setup(_context);
            _view.Received(1).Setup(_context);

            _context.EventBusModel.Received(1).Subscribe(Arg.Any<System.Action<CounterValueUpdatedEvent>>());
            _context.EventBusGlobal.Received(1).Subscribe(Arg.Any<System.Action<SceneInitializedEvent>>());
        }

        [Test]
        public void Dispose_ShouldDisposeModelAndView_AndUnsubscribeEvents()
        {
            _controller.Dispose();

            _model.Received(1).Dispose();
            _view.Received(1).Dispose();

            _context.EventBusModel.Received(1).Unsubscribe<CounterValueUpdatedEvent>(Arg.Any<Action<CounterValueUpdatedEvent>>());
            _context.EventBusGlobal.Received(1).Unsubscribe<SceneInitializedEvent>(Arg.Any<Action<SceneInitializedEvent>>());
        }

    }
}
