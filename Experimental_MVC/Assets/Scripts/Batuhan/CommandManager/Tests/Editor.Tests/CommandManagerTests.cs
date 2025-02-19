using System;
using System.Collections.Generic;
using NUnit.Framework;
using Batuhan.CommandManager.TestCommands;

namespace Batuhan.CommandManager.EditorTests
{
    [TestFixture]
    public class CommandManagerTests
    {
        private CommandManager _commandManager;

        [SetUp]
        public void Setup()
        {
            _commandManager = new CommandManager();
        }

        [TearDown]
        public void TearDown()
        {
            _commandManager.Dispose();
        }

        #region Basic Functionality Tests
        [Test]
        public void ExecuteCommand_ShouldCallExecuteCallbackWithoutBindings()
        {
            var command = new TestCommand();

            _commandManager.ExecuteCommand(command);

            Assert.IsTrue(command.ExecuteCalled, "Execute callback was invoked.");
        }
        [Test]
        public void UndoCommand_ShouldCallUndoCallbackWithoutBindings()
        {
            var command = new TestCommand();

            _commandManager.UndoCommand(command);

            Assert.IsTrue(command.UndoCalled, "Undo callback was invoked.");
        }
        [Test]
        public void ExecuteCommand_ShouldCallExecuteCallbackAndOnExecute()
        {
            // Arrange
            bool executeCallbackCalled = false;
            var command = new TestCommand();
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { executeCallbackCalled = true; },
                undo: (cmd) => { }
            );
            _commandManager.AddListener(binding);

            // Act
            _commandManager.ExecuteCommand(command);

            // Assert
            Assert.IsTrue(executeCallbackCalled, "Execute callback was invoked.");
            Assert.IsTrue(command.ExecuteCalled, "Command's OnExecute was called.");
        }

        [Test]
        public void UndoCommand_ShouldCallUndoCallbackAndOnUndo()
        {
            // Arrange
            bool undoCallbackCalled = false;
            var command = new TestCommand();
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: (cmd) => { undoCallbackCalled = true; }
            );
            _commandManager.AddListener(binding);

            // Act
            _commandManager.UndoCommand(command);

            // Assert
            Assert.IsTrue(undoCallbackCalled, "Undo callback was not invoked.");
            Assert.IsTrue(command.UndoCalled, "Command's OnUndo was not called.");
        }

        [Test]
        public void UndoLastExecutedCommand_ShouldCallUndoForLastCommand()
        {
            // Arrange
            bool undoCallbackCalled = false;
            var command = new TestCommand();
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: (cmd) => { undoCallbackCalled = true; }
            );
            _commandManager.AddListener(binding);
            _commandManager.ExecuteCommand(command);

            // Act
            _commandManager.UndoLastExecutedCommand();

            // Assert
            Assert.IsTrue(undoCallbackCalled, "The Undo callback for the last executed command was not invoked.");
            Assert.IsTrue(command.UndoCalled, "The last executed command's OnUndo was not called.");
        }

        [Test]
        public void RemoveListener_ShouldPreventCallbackInvocation()
        {
            // Arrange
            bool executeCallbackCalled = false;
            var command = new TestCommand();
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { executeCallbackCalled = true; },
                undo: (cmd) => { }
            );
            _commandManager.AddListener(binding);
            _commandManager.RemoveListener(binding);

            // Act
            _commandManager.ExecuteCommand(command);

            // Assert
            Assert.IsFalse(executeCallbackCalled, "Callback was invoked even after listener removal.");
        }

        [Test]
        public void RemoveListenerFromExecuteCallback_ShouldPreventCallbackInvocation()
        {
            // Arrange
            bool executeCallbackCalled = false;
            var command = new TestCommand();
            Action<TestCommand> executeCallback = (cmd) => { executeCallbackCalled = true; };
            var binding = new CommandBinding<TestCommand>(
                execute: executeCallback,
                undo: (cmd) => { }
            );
            _commandManager.AddListener(binding);
            _commandManager.RemoveListenerFromExecuteCallback(executeCallback);

            // Act
            _commandManager.ExecuteCommand(command);

            // Assert
            Assert.IsFalse(executeCallbackCalled, "Callback was invoked even after removing listener by execute callback.");
        }

        [Test]
        public void ExecuteCommand_ShouldInvokeBindingsInPriorityOrder()
        {
            // Arrange
            var executionOrder = new List<int>();
            var command = new TestCommand();

            // binding2 has a higher priority (2) and should be invoked first.
            var binding1 = new CommandBinding<TestCommand>(
                execute: (cmd) => { executionOrder.Add(1); },
                undo: (cmd) => { executionOrder.Add(1); },
                priority: 1
            );
            var binding2 = new CommandBinding<TestCommand>(
                execute: (cmd) => { executionOrder.Add(2); },
                undo: (cmd) => { executionOrder.Add(2); },
                priority: 2
            );

            _commandManager.AddListener(binding1);
            _commandManager.AddListener(binding2);

            // Act
            _commandManager.ExecuteCommand(command);

            // Assert
            Assert.AreEqual(2, executionOrder.Count, "Not all callbacks were executed.");
            Assert.AreEqual(2, executionOrder[0], "Higher priority callback was not executed first.");
            Assert.AreEqual(1, executionOrder[1], "Lower priority callback was not executed second.");
        }

        #endregion

        #region Edge Case Tests

        [Test]
        public void ExecuteCommand_WithNullExecuteCallback_ShouldStillCallOnExecute()
        {
            // Arrange
            var command = new TestCommand();
            // Create a binding with a null execute callback.
            var binding = new CommandBinding<TestCommand>(
                execute: null,
                undo: (cmd) => { }
            );
            _commandManager.AddListener(binding);

            // Act
            _commandManager.ExecuteCommand(command);

            // Assert
            Assert.IsTrue(command.ExecuteCalled, "OnExecute should be called even if execute callback is null.");
        }

        [Test]
        public void UndoCommand_WithNullUndoCallback_ShouldStillCallOnUndo()
        {
            // Arrange
            var command = new TestCommand();
            // Create a binding with a null undo callback.
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: null
            );
            _commandManager.AddListener(binding);

            // Act
            _commandManager.UndoCommand(command);

            // Assert
            Assert.IsTrue(command.UndoCalled, "OnUndo should be called even if undo callback is null.");
        }

        [Test]
        public void ExecuteCommand_NoListeners_DoesNotThrow()
        {
            // Arrange
            var command = new TestCommand();

            // Act & Assert: No exception should be thrown.
            Assert.DoesNotThrow(() => _commandManager.ExecuteCommand(command));
            // Since no binding is registered, OnExecute will not be invoked.
            Assert.IsFalse(command.ExecuteCalled, "OnExecute should not be called when no listener is registered.");
        }

        [Test]
        public void UndoCommand_NoListeners_DoesNotThrow()
        {
            // Arrange
            var command = new TestCommand();

            // Act & Assert: No exception should be thrown.
            Assert.DoesNotThrow(() => _commandManager.UndoCommand(command));
            // Since no binding is registered, OnUndo will not be invoked.
            Assert.IsFalse(command.UndoCalled, "OnUndo should not be called when no listener is registered.");
        }

        [Test]
        public void UndoLastExecutedCommand_WithNoExecutedCommands_DoesNotThrow()
        {
            // Act & Assert: Calling UndoLastExecutedCommand on an empty stack should not throw.
            Assert.DoesNotThrow(() => _commandManager.UndoLastExecutedCommand());
        }

        [Test]
        public void UndoLastExecutedCommand_WithMultipleCommands_OnlyUndoesLastOne()
        {
            // Arrange
            var firstCommand = new TestCommand();
            var secondCommand = new TestCommand();

            // Flags to verify which command gets undone.
            var firstUndone = false;
            var secondUndone = false;
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: (cmd) => {
                    if (cmd == firstCommand)
                        firstUndone = true;
                    if (cmd == secondCommand)
                        secondUndone = true;
                }
            );
            _commandManager.AddListener(binding);

            // Act
            _commandManager.ExecuteCommand(firstCommand);
            _commandManager.ExecuteCommand(secondCommand);
            _commandManager.UndoLastExecutedCommand();

            // Assert: Only the second command should have been undone.
            Assert.IsFalse(firstUndone, "First command should not be undone by UndoLastExecutedCommand.");
            Assert.IsTrue(secondUndone, "Second command should be undone by UndoLastExecutedCommand.");
        }

        [Test]
        public void RemoveListenerFromExecuteCallback_NonExistentCallback_DoesNotRemoveExistingListener()
        {
            // Arrange
            bool executeCallbackCalled = false;
            Action<TestCommand> validCallback = (cmd) => { executeCallbackCalled = true; };
            Action<TestCommand> nonExistentCallback = (cmd) => { /* no-op */ };

            var binding = new CommandBinding<TestCommand>(
                execute: validCallback,
                undo: (cmd) => { }
            );
            _commandManager.AddListener(binding);

            // Act
            _commandManager.RemoveListenerFromExecuteCallback(nonExistentCallback);
            _commandManager.ExecuteCommand(new TestCommand());

            // Assert: Since the non-matching callback was provided, the valid listener should remain.
            Assert.IsTrue(executeCallbackCalled, "Existing listener was incorrectly removed when a non-matching callback was provided.");
        }

        [Test]
        public void AddListener_SameListenerTwice_CallbackInvokedTwice()
        {
            // Arrange
            int callCount = 0;
            Action<TestCommand> executeCallback = (cmd) => { callCount++; };

            var binding = new CommandBinding<TestCommand>(
                execute: executeCallback,
                undo: (cmd) => { }
            );
            // Add the same binding instance twice.
            _commandManager.AddListener(binding);
            _commandManager.AddListener(binding);

            // Act
            _commandManager.ExecuteCommand(new TestCommand());

            // Assert: The callback should be invoked twice.
            Assert.AreEqual(2, callCount, "Callback was not invoked twice when the same listener was added twice.");
        }

        [Test]
        public void RemoveListener_OnlyRemovesOneInstance_WhenAddedTwice()
        {
            // Arrange
            int callCount = 0;
            Action<TestCommand> executeCallback = (cmd) => { callCount++; };

            var binding = new CommandBinding<TestCommand>(
                execute: executeCallback,
                undo: (cmd) => { }
            );
            _commandManager.AddListener(binding);
            _commandManager.AddListener(binding);

            // Act: Remove one instance.
            _commandManager.RemoveListener(binding);
            _commandManager.ExecuteCommand(new TestCommand());

            // Assert: The callback should be invoked only once.
            Assert.AreEqual(1, callCount, "Only one instance of the listener should remain after removal of one instance.");
        }

        public void Dispose_ClearsListenersAndExecutedCommands()
        {
            // Arrange
            bool callbackCalled = false;
            var command = new TestCommand();
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { callbackCalled = true; },
                undo: (cmd) => { }
            );
            _commandManager.AddListener(binding);
            _commandManager.ExecuteCommand(command);

            // Act
            _commandManager.Dispose();

            // Try to execute and undo commands after disposing.
            Assert.DoesNotThrow(() => _commandManager.ExecuteCommand(command));
            Assert.DoesNotThrow(() => _commandManager.UndoCommand(command));

            // Since Dispose cleared the listeners and the executed commands stack,
            // any further command execution should not invoke callbacks.
            callbackCalled = false;
            _commandManager.UndoLastExecutedCommand();
            Assert.IsFalse(callbackCalled, "After disposal, no callbacks should be invoked.");
        }

        #endregion
    }

}

