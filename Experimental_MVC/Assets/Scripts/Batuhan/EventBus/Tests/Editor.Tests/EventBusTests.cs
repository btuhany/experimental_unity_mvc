using System;
using NSubstitute;
using NUnit.Framework;

namespace Batuhan.EventBus.EditorTests
{
    [TestFixture]
    public class EventBusTests
    {
        public class TestCategory : IEventCategory
        {
            public int ID => 1;

            public bool CanBeDisposed => true;
        }

        public class TestEvent : IEvent
        {
            public int CategoryID => 1;
            public string Message { get; set; }
        }
        public class AnotherTestEvent : IEvent
        {
            public int CategoryID => 1;
            public int Data { get; set; }
        }
        public class InvalidCategoryTestEvent : IEvent
        {
            public int CategoryID => 2;
        }

        // NSubstitute mock
        public interface ITestEventHandler
        {
            void HandleEvent(TestEvent evt);
        }
        public interface IAnotherTestEventHandler
        {
            void HandleEvent(AnotherTestEvent evt);
        }

        private EventBus<TestCategory> _eventBus;
        private TestCategory _category;

        [SetUp]
        public void Setup()
        {
            _category = new TestCategory();
            _eventBus = new EventBus<TestCategory>(_category);
        }

        [TearDown]
        public void Teardown()
        {
            _eventBus.Dispose();
        }

        [Test]
        public void SubscribeAndPublish_ShouldInvokeCallback()
        {
            // Arrange
            var handler = Substitute.For<ITestEventHandler>();
            _eventBus.Subscribe<TestEvent>(handler.HandleEvent);
            var testEvent = new TestEvent { Message = "Hello" };

            // Act
            _eventBus.Publish(testEvent);

            // Assert
            handler.Received(1).HandleEvent(Arg.Is<TestEvent>(x => x.Message == "Hello"));
        }

        [Test]
        public void Unsubscribe_ShouldNotInvokeCallback()
        {
            // Arrange
            var handler = Substitute.For<ITestEventHandler>();
            Action<TestEvent> callback = handler.HandleEvent;
            _eventBus.Subscribe<TestEvent>(callback);
            _eventBus.Unsubscribe<TestEvent>(callback);
            var testEvent = new TestEvent { Message = "Test" };

            // Act
            _eventBus.Publish(testEvent);

            // Assert
            handler.DidNotReceive().HandleEvent(Arg.Any<TestEvent>());
        }

        [Test]
        public void Publish_WithInvalidCategory_ShouldThrowException()
        {
            // Arrange
            var invalidEvent = new InvalidCategoryTestEvent();

            // Act & Assert
            Assert.Throws<Exception>(() => _eventBus.Publish(invalidEvent));
        }

        [Test]
        public void Subscribe_WithInvalidCategory_ShouldThrowException()
        {
            // Arrange
            Action<InvalidCategoryTestEvent> callback = (x) => { };

            // Act & Assert
            Assert.Throws<Exception>(() => _eventBus.Subscribe<InvalidCategoryTestEvent>(callback));
        }

        [Test]
        public void Publish_EventHandlerThrows_ShouldWrapAndThrowException()
        {
            // Arrange
            Action<TestEvent> callback = (evt) => { throw new InvalidOperationException("Handler error"); };
            _eventBus.Subscribe<TestEvent>(callback);
            var testEvent = new TestEvent();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => _eventBus.Publish(testEvent));
            Assert.That(ex.Message, Does.Contain("Exception in event handler"));
            Assert.That(ex.Message, Does.Contain("Handler error"));
        }

        [Test]
        public void CleanUp_ShouldRemoveEmptyBindings()
        {
            _eventBus.Subscribe<TestEvent>(_ => { });
            _eventBus.Unsubscribe<TestEvent>(_ => { });
            _eventBus.CleanUp();

            Assert.DoesNotThrow(() => _eventBus.Publish(new TestEvent()));
        }

        [Test]
        public void AddingSameActionMultipleTimes_ShouldNotDuplicate()
        {
            var handler = Substitute.For<ITestEventHandler>();
            Action<TestEvent> callback = handler.HandleEvent;

            _eventBus.Subscribe<TestEvent>(callback);
            _eventBus.Subscribe<TestEvent>(callback); // Second subscription should be ignored
            var testEvent = new TestEvent { Message = "Duplicate Test" };

            _eventBus.Publish(testEvent);

            handler.Received(1).HandleEvent(Arg.Any<TestEvent>());
        }

        [Test]
        public void MultipleBindings_ShouldWorkIndependently()
        {
            var handler1 = Substitute.For<ITestEventHandler>();
            var handler2 = Substitute.For<IAnotherTestEventHandler>();

            _eventBus.Subscribe<TestEvent>(handler1.HandleEvent);
            _eventBus.Subscribe<AnotherTestEvent>(handler2.HandleEvent);

            _eventBus.Publish(new TestEvent { Message = "TestEvent" });
            _eventBus.Publish(new AnotherTestEvent { Data = 42 });

            handler1.Received(1).HandleEvent(Arg.Any<TestEvent>());
            handler2.Received(1).HandleEvent(Arg.Any<AnotherTestEvent>());
        }

        [Test]
        public void UnsubscribingOneBinding_ShouldNotAffectOthers()
        {
            var handler1 = Substitute.For<ITestEventHandler>();
            var handler2 = Substitute.For<IAnotherTestEventHandler>();

            _eventBus.Subscribe<TestEvent>(handler1.HandleEvent);
            _eventBus.Subscribe<AnotherTestEvent>(handler2.HandleEvent);

            _eventBus.Unsubscribe<TestEvent>(handler1.HandleEvent);

            _eventBus.Publish(new TestEvent { Message = "Should not trigger" });
            _eventBus.Publish(new AnotherTestEvent { Data = 99 });

            handler1.DidNotReceive().HandleEvent(Arg.Any<TestEvent>());
            handler2.Received(1).HandleEvent(Arg.Any<AnotherTestEvent>());
        }

        [Test]
        public void PublishingEventWithNoSubscribers_ShouldNotThrow()
        {
            var testEvent = new TestEvent { Message = "No one is listening" };

            Assert.DoesNotThrow(() => _eventBus.Publish(testEvent));
        }

        [Test]
        public void EventBinding_DifferentBindingsOfSameMethod_ShouldNotBeEqual()
        {
            Action<TestEvent> action1 = HandleTestEvent;
            Action<TestEvent> action2 = HandleTestEvent;

            var binding1 = new EventBinding<TestEvent>(action1);
            var binding2 = new EventBinding<TestEvent>(action2);

            Assert.AreNotEqual(binding1, binding2);
        }

        [Test]
        public void EventBinding_DifferentLambdas_ShouldNotBeEqual()
        {
            var binding1 = new EventBinding<TestEvent>(e => Console.WriteLine(e.Message));
            var binding2 = new EventBinding<TestEvent>(e => Console.WriteLine(e.Message));

            Assert.AreNotEqual(binding1.OnEvent, binding2.OnEvent);
        }

        [Test]
        public void Subscribe_WithSameMethodGroup_ShouldNotDuplicate()
        {
            var handler = Substitute.For<ITestEventHandler>();
            _eventBus.Subscribe<TestEvent>(handler.HandleEvent);
            _eventBus.Subscribe<TestEvent>(handler.HandleEvent);

            _eventBus.Publish(new TestEvent { Message = "Test" });

            handler.Received(1).HandleEvent(Arg.Any<TestEvent>());
        }

        [Test]
        public void Subscribe_SameMethodAsLambda_ShouldBeDifferentBindings()
        {
            var handler = Substitute.For<ITestEventHandler>();

            _eventBus.Subscribe<TestEvent>(handler.HandleEvent);
            _eventBus.Subscribe<TestEvent>(e => handler.HandleEvent(e)); // Different lambda expression

            _eventBus.Publish(new TestEvent { Message = "Test" });

            handler.Received(2).HandleEvent(Arg.Any<TestEvent>()); // Should receive twice
        }

        [Test]
        public void Subscribe_Unsubscribe_Lambda_ShouldOnlyRemoveThatInstance()
        {
            var handler = Substitute.For<ITestEventHandler>();
            Action<TestEvent> lambda1 = e => handler.HandleEvent(e);
            Action<TestEvent> lambda2 = e => handler.HandleEvent(e);

            _eventBus.Subscribe<TestEvent>(lambda1);
            _eventBus.Subscribe<TestEvent>(lambda2);
            _eventBus.Unsubscribe<TestEvent>(lambda1);

            _eventBus.Publish(new TestEvent { Message = "Test" });

            handler.Received(1).HandleEvent(Arg.Any<TestEvent>()); // Only one should remain
        }

        private void HandleTestEvent(TestEvent evt)
        {
            // Dummy method for equality testing
        }
    }
}
