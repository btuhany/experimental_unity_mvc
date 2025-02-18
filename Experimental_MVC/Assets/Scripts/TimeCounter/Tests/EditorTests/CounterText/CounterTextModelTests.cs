using NUnit.Framework;
using NSubstitute;
using TimeCounter.Entities.CounterText;
using TimeCounter.Events.ModelEvents;
using TimeCounter.Data;
using Batuhan.EventBus;
using Batuhan.RuntimeCopyScriptableObjects;
using System.Reflection;

[TestFixture]
public class CounterTextModelTests
{
    private const string DATA_SO_FIELD_NAME = "_dataSO";
    private ICounterTextModel _counterTextModel;
    private ICounterTextContext _mockContext;
    private IEventBus<Model> _mockEventBusModel;
    private RuntimeClonableSOManager _mockClonableSOManager;
    private CounterTextModelDataSO _mockCounterTextDataSO;

    private static CounterTextModelDataSO GetDataSOWithReflection(ICounterTextModel model)
    {
        var fieldInfo = typeof(CounterTextModel).GetField(DATA_SO_FIELD_NAME, BindingFlags.NonPublic | BindingFlags.Instance);
        var dataSO = (CounterTextModelDataSO)fieldInfo.GetValue(model);
        return dataSO;
    }

    [SetUp]
    public void Setup()
    {
        // Mock dependencies
        _mockContext = Substitute.For<ICounterTextContext>();
        _mockEventBusModel = Substitute.For<IEventBus<Model>>();
        _mockClonableSOManager = Substitute.For<RuntimeClonableSOManager>();
        _mockCounterTextDataSO = Substitute.For<CounterTextModelDataSO>();

        // Set up the initial counter value and count speed
        _mockCounterTextDataSO.CounterValue = 0;
        _mockCounterTextDataSO.CountSpeed = 1.0f;

        // Mock the data SO manager to return the mocked data SO
        //_mockClonableSOManager.CreateModelDataSOInstance(Arg.Any<CounterTextModelDataSO>()).Returns(_mockCounterTextDataSO);

        // Mock context to return the event bus
        _mockContext.EventBusModel.Returns(_mockEventBusModel);

        // Initialize the CounterTextModel
        _counterTextModel = new CounterTextModel();
        _counterTextModel.CreateData(_mockCounterTextDataSO, _mockClonableSOManager);
        _counterTextModel.Setup(_mockContext);
    }

    [TearDown]
    public void TearDown()
    {
        _mockClonableSOManager.DeleteAllRuntimeSOAssets();
    }

    [Test]
    public void IncreaseCounter_WhenValueIncreases_ShouldPublishEvent()
    {
        // Arrange
        var initialValue = _mockCounterTextDataSO.CounterValue;
        var incrementValue = 5;

        // Act
        _counterTextModel.IncreaseCounter(incrementValue);

        // Act

        var dataSO = GetDataSOWithReflection(_counterTextModel);

        // Assert
        Assert.AreEqual(initialValue + incrementValue, dataSO.CounterValue);
        _mockEventBusModel.Received(1).Publish(Arg.Is<CounterValueUpdatedEvent>(e => e.UpdatedValue == initialValue + incrementValue));
    }

    [Test]
    public void IncreaseCounter_NegativeValue_ShouldNotPublishEvent()
    {
        // Arrange
        var initialValue = _mockCounterTextDataSO.CounterValue;
        var incrementValue = -10; 

        // Act
        _counterTextModel.IncreaseCounter(incrementValue);

        // Assert
        _mockEventBusModel.DidNotReceive().Publish(Arg.Any<CounterValueUpdatedEvent>());
    }

    [Test]
    public void IncreaseCounter_NegativeValue_ShouldNotChangeTheValue()
    {
        // Arrange
        var initialValue = _mockCounterTextDataSO.CounterValue;
        var incrementValue = -10;

        // Act
        _counterTextModel.IncreaseCounter(incrementValue);

        // Assert
        Assert.AreEqual(initialValue, _mockCounterTextDataSO.CounterValue);
    }

    [Test]
    public void IncreaseCounter_WhenNewValueIsZero_ShouldPublishEvent()
    {
        // Arrange
        var dataSO = GetDataSOWithReflection(_counterTextModel);
        dataSO.CounterValue = -5; // Start with negative value
        var incrementValue = 5;

        // Act
        _counterTextModel.IncreaseCounter(incrementValue);

        // Assert
        
        Assert.AreEqual(0, dataSO.CounterValue);
        _mockEventBusModel.Received(1).Publish(Arg.Is<CounterValueUpdatedEvent>(e => e.UpdatedValue == 0));
    }
}
