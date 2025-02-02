using Moq;
using Otus_Task_3;
using Otus_Task_3.Commands;
using Otus_Task_3.Interface;

namespace Tests;

[TestFixture]
public class CheckFuelCommandTests
{
    [Test]
    public void Execute_WithSufficientFuel_DoesNotThrow()
    {
        // Arrange
        var mockGameObj = new Mock<IGameObject>();
        mockGameObj.SetupGet(x => x.Fuel).Returns(100);
        var command = new CheckFuelCommand(mockGameObj.Object, requiredFuel: 10);

        // Act & Assert
        Assert.DoesNotThrow(() => command.Execute());
    }

    [Test]
    public void Execute_WithInsufficientFuel_ThrowsCommandException()
    {
        // Arrange
        var mockGameObj = new Mock<IGameObject>();
        mockGameObj.SetupGet(x => x.Fuel).Returns(5);
        var command = new CheckFuelCommand(mockGameObj.Object, requiredFuel: 10);

        // Act & Assert
        Assert.Throws<CommandException>(() => command.Execute());
    }
}