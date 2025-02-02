using Moq;
using Otus_Task_3;
using Otus_Task_3.Commands;
using Otus_Task_3.Interface;

namespace Tests;

[TestFixture]
public class MacroCommandTests
{
    [Test]
    public void Execute_AllCommandsExecutedInSequence()
    {
        // Arrange
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        var mockCommand3 = new Mock<ICommand>();

        var macro = new MacroCommand(new List<ICommand> { 
            mockCommand1.Object, 
            mockCommand2.Object, 
            mockCommand3.Object 
        });

        // Act
        macro.Execute();

        // Assert: проверяем, что метод Execute каждого мока вызван ровно 1 раз
        mockCommand1.Verify(x => x.Execute(), Times.Once);
        mockCommand2.Verify(x => x.Execute(), Times.Once);
        mockCommand3.Verify(x => x.Execute(), Times.Once);
    }

    [Test]
    public void Execute_WhenACommandThrows_StopsExecutionAndThrows()
    {
        // Arrange
        var mockCommand1 = new Mock<ICommand>();
        var mockCommand2 = new Mock<ICommand>();
        var mockCommand3 = new Mock<ICommand>();

        mockCommand2.Setup(x => x.Execute()).Throws(new CommandException("Test failure"));

        var macro = new MacroCommand(new List<ICommand> { 
            mockCommand1.Object, 
            mockCommand2.Object, 
            mockCommand3.Object 
        });

        // Act & Assert
        Assert.Throws<CommandException>(() => macro.Execute());

        // Проверяем, что первая команда выполнена, вторая – выполнена (и выбросила исключение),
        // а третья не вызывается
        mockCommand1.Verify(x => x.Execute(), Times.Once);
        mockCommand2.Verify(x => x.Execute(), Times.Once);
        mockCommand3.Verify(x => x.Execute(), Times.Never);
    }
}