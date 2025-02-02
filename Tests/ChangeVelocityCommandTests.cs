using System.Numerics;
using Moq;
using Otus_Task_3.Commands;
using Otus_Task_3.Interface;

namespace Tests;

[TestFixture]
public class ChangeVelocityCommandTests
{
    [Test]
    public void Execute_WhenObjectIsMoving_ChangesVelocityByRotation()
    {
        // Arrange
        var mockGameObj = new Mock<IGameObject>();
        // Устанавливаем исходное значение скорости (1,0)
        mockGameObj.SetupProperty(x => x.Velocity, new Vector2(1, 0));
        var command = new ChangeVelocityCommand(mockGameObj.Object, angleDegrees: 90);

        // Act
        command.Execute();

        // Assert: при повороте на 90 градусов ожидаем скорость примерно (0,1)
        var expected = new Vector2(0, 1);
        Assert.AreEqual(expected.X, mockGameObj.Object.Velocity.X, 0.001);
        Assert.AreEqual(expected.Y, mockGameObj.Object.Velocity.Y, 0.001);
    }

    [Test]
    public void Execute_WhenObjectIsStationary_VelocityRemainsZero()
    {
        // Arrange
        var mockGameObj = new Mock<IGameObject>();
        mockGameObj.SetupProperty(x => x.Velocity, Vector2.Zero);
        var command = new ChangeVelocityCommand(mockGameObj.Object, angleDegrees: 45);

        // Act
        command.Execute();

        // Assert
        Assert.AreEqual(Vector2.Zero, mockGameObj.Object.Velocity);
    }
}