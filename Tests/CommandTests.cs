using Moq;
using Otus_Task_3.Implementation;
using Otus_Task_3.Interface;

namespace Tests;

[TestFixture]
public class CommandTests
{
    [Test]
    public void LogCommand_Executes_LogServiceCalled()
    {
        // Arrange
        var exception = new Exception("Test exception");
        var logServiceMock = new Mock<ILogService>();
        var logCommand = new LogCommand(exception, logServiceMock.Object);

        // Act
        logCommand.Execute();

        // Assert
        logServiceMock.Verify(ls => ls.Log(It.Is<string>(s => s.Contains("Test exception"))), Times.Once);
    }

    [Test]
    public void RetryCommand_Executes_OriginalCommandExecuted()
    {
        // Arrange
        var commandMock = new Mock<ICommand>();
        var retryCommand = new RetryCommand(commandMock.Object, retryCount: 1);

        // Act
        retryCommand.Execute();

        // Assert
        commandMock.Verify(c => c.Execute(), Times.Once);
    }
}