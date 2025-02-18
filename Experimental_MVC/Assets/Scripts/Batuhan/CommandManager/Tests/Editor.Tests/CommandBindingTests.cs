using System;
using NUnit.Framework;
using Batuhan.CommandManager.TestCommands;

namespace Batuhan.CommandManager.EditorTests
{
    [TestFixture]
    public class CommandBindingTests
    {
        [Test]
        public void Execute_ShouldInvokeExecuteCallbackAndCallOnExecute()
        {
            // Arrange
            bool executeCallbackCalled = false;
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { executeCallbackCalled = true; },
                undo: null
            );
            var command = new TestCommand();

            // Act
            binding.Execute(command);

            // Assert
            Assert.IsTrue(executeCallbackCalled, "Execute callback should have been invoked.");
            Assert.IsTrue(command.ExecuteCalled, "Command's OnExecute should have been called.");
        }

        [Test]
        public void Undo_ShouldInvokeUndoCallbackAndCallOnUndo()
        {
            // Arrange
            bool undoCallbackCalled = false;
            var binding = new CommandBinding<TestCommand>(
                execute: null,
                undo: (cmd) => { undoCallbackCalled = true; }
            );
            var command = new TestCommand();

            // Act
            binding.Undo(command);

            // Assert
            Assert.IsTrue(undoCallbackCalled, "Undo callback should have been invoked.");
            Assert.IsTrue(command.UndoCalled, "Command's OnUndo should have been called.");
        }

        [Test]
        public void Execute_WithNullExecuteCallback_ShouldStillCallOnExecute()
        {
            // Arrange
            var binding = new CommandBinding<TestCommand>(
                execute: null,
                undo: null
            );
            var command = new TestCommand();

            // Act
            binding.Execute(command);

            // Assert
            Assert.IsTrue(command.ExecuteCalled, "OnExecute should be called even if execute callback is null.");
        }

        [Test]
        public void Undo_WithNullUndoCallback_ShouldStillCallOnUndo()
        {
            // Arrange
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: null
            );
            var command = new TestCommand();

            // Act
            binding.Undo(command);

            // Assert
            Assert.IsTrue(command.UndoCalled, "OnUndo should be called even if undo callback is null.");
        }

        [Test]
        public void Execute_WithInvalidCommandType_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: (cmd) => { }
            );
            var invalidCommand = new OtherCommand();

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => binding.Execute(invalidCommand));
            Assert.AreEqual("Invalid command type", ex.Message);
        }

        [Test]
        public void Undo_WithInvalidCommandType_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: (cmd) => { }
            );
            var invalidCommand = new OtherCommand();

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => binding.Undo(invalidCommand));
            Assert.AreEqual("Invalid command type", ex.Message);
        }

        [Test]
        public void ExecuteEquals_ShouldReturnTrueForSameCallback()
        {
            // Arrange
            Action<TestCommand> callback = (cmd) => { };
            var binding = new CommandBinding<TestCommand>(
                execute: callback,
                undo: null
            );

            // Act & Assert
            Assert.IsTrue(binding.ExecuteEquals(callback), "ExecuteEquals should return true when the same callback is provided.");
        }

        [Test]
        public void ExecuteEquals_ShouldReturnFalseForDifferentCallback()
        {
            // Arrange
            Action<TestCommand> callback = (cmd) => { };
            Action<TestCommand> anotherCallback = (cmd) => { };
            var binding = new CommandBinding<TestCommand>(
                execute: callback,
                undo: null
            );

            // Act & Assert
            Assert.IsFalse(binding.ExecuteEquals(anotherCallback), "ExecuteEquals should return false for a different callback.");
        }

        [Test]
        public void PriorityProperty_ShouldReturnCorrectPriority()
        {
            // Arrange
            int expectedPriority = 10;
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: null,
                priority: expectedPriority
            );

            // Act & Assert
            Assert.AreEqual(expectedPriority, binding.Priority, "Priority property did not return the expected value.");
        }

        [Test]
        public void CommandTypeProperty_ShouldReturnCorrectType()
        {
            // Arrange
            var binding = new CommandBinding<TestCommand>(
                execute: (cmd) => { },
                undo: null
            );

            // Act & Assert
            Assert.AreEqual(typeof(TestCommand), binding.CommandType, "CommandType property did not return the expected type.");
        }
    }
}
