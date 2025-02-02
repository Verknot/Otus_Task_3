using Moq;
using Otus_Task_3;
using Otus_Task_3.Commands;
using Otus_Task_3.Interface;

namespace Tests;

[TestFixture]
public class MoveWithFuelCommandTests
{
    [Test]
    public void Execute_WithSufficientFuel_PerformsMoveAndBurnFuel()
    {
        // Arrange
        var mockGameObj = new Mock<IGameObject>();
        // Используем SetupProperty для отслеживания изменений топлива
        mockGameObj.SetupProperty(x => x.Fuel, 100);
        mockGameObj.SetupGet(x => x.FuelConsumptionRate).Returns(10);
        // Поскольку MoveCommand выводит сообщение, нам не важна его логика
        var command = new MoveWithFuelCommand(mockGameObj.Object);

        // Act
        command.Execute();

        // Assert: топливо должно уменьшиться на 10.
        Assert.AreEqual(90, mockGameObj.Object.Fuel);
    }

    [Test]
    public void Execute_WithInsufficientFuel_ThrowsCommandException_AndDoesNotBurnFuel()
    {
        // Arrange
        var mockGameObj = new Mock<IGameObject>();
        mockGameObj.SetupProperty(x => x.Fuel, 5);
        mockGameObj.SetupGet(x => x.FuelConsumptionRate).Returns(10);
        var command = new MoveWithFuelCommand(mockGameObj.Object);

        // Act & Assert
        Assert.Throws<CommandException>(() => command.Execute());
        // Топливо не изменяется, так как проверка не прошла
        Assert.AreEqual(5, mockGameObj.Object.Fuel);
    }
}