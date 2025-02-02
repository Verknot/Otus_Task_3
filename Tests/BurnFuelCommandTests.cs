using Moq;
using Otus_Task_3.Commands;
using Otus_Task_3.Interface;

namespace Tests;

[TestFixture]
public class BurnFuelCommandTests
{
    [Test]
    public void Execute_BurnsFuelCorrectly()
    {
        // Arrange
        var mockGameObj = new Mock<IGameObject>();
        // Для проверки изменяемости свойства используем SetupProperty
        mockGameObj.SetupProperty(x => x.Fuel, 100);
        var command = new BurnFuelCommand(mockGameObj.Object, burnAmount: 10);

        // Act
        command.Execute();

        // Assert
        Assert.AreEqual(90, mockGameObj.Object.Fuel);
    }
}