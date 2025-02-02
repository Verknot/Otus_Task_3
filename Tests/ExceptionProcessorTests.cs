using Moq;
using Otus_Task_3;
using Otus_Task_3.Implementation;
using Otus_Task_3.Interface;

namespace Tests;

public class ExceptionProcessorTests
{
    [Test]
    public void Process_WithRetryStrategy_FirstException_EnqueuesRetryCommand()
    {
        var queueMock = new Mock<ICommandQueue>();
        var logServiceMock = new Mock<ILogService>();

        var exceptionProcessor = new ExceptionProcessor(queueMock.Object, logServiceMock.Object, maxRetries: 1);
        
        var commandMock = new Mock<ICommand>();

        var testException = new Exception("Test");

        exceptionProcessor.Process(testException, commandMock.Object);
        
        queueMock.Verify(q =>
                q.Enqueue(It.Is<IRetryableCommand>(cmd =>
                    cmd.RetryCount == 1 &&
                    ((RetryCommand)cmd).OriginalCommand == commandMock.Object)),
            Times.Once);
    }

    [Test]
    public void Process_WithRetryStrategy_SecondException_EnqueuesLogCommand()
    {

        var queueMock = new Mock<ICommandQueue>();
        var logServiceMock = new Mock<ILogService>();
        var exceptionProcessor = new ExceptionProcessor(queueMock.Object, logServiceMock.Object, maxRetries: 1);
        
        IRetryableCommand retryCommand = new RetryCommand(new Mock<ICommand>().Object, retryCount: 1);

        var testException = new Exception("Test");
        
        exceptionProcessor.Process(testException, retryCommand);
        
        queueMock.Verify(q =>
                q.Enqueue(It.Is<LogCommand>(cmd => cmd != null)),
            Times.Once);
    }

    [Test]
    public void Process_WithTwoRetriesStrategy_FirstAndSecondException_EnqueuesRetryCommand()
    {
        var queueMock = new Mock<ICommandQueue>();
        var logServiceMock = new Mock<ILogService>();

        var exceptionProcessor = new ExceptionProcessor(queueMock.Object, logServiceMock.Object, maxRetries: 2);
        
        var originalCommand = new Mock<ICommand>().Object;
        var testException1 = new Exception("First failure");
        
        exceptionProcessor.Process(testException1, originalCommand);
        
        queueMock.Verify(q =>
                q.Enqueue(It.Is<IRetryableCommand>(cmd =>
                    cmd.RetryCount == 1 &&
                    ((RetryCommand)cmd).OriginalCommand == originalCommand)),
            Times.Once);
        
        IRetryableCommand retryCommand = new RetryCommand(originalCommand, retryCount: 1);
        var testException2 = new Exception("Second failure");
        
        exceptionProcessor.Process(testException2, retryCommand);
        
        queueMock.Verify(q =>
                q.Enqueue(It.Is<IRetryableCommand>(cmd =>
                    cmd.RetryCount == 2 &&
                    cmd is FailedRetryCommand &&
                    ((FailedRetryCommand)cmd).OriginalCommand == originalCommand)),
            Times.Once);
    }

    [Test]
    public void Process_WithTwoRetriesStrategy_ThirdException_EnqueuesLogCommand()
    {
        var queueMock = new Mock<ICommandQueue>();
        var logServiceMock = new Mock<ILogService>();
        var exceptionProcessor = new ExceptionProcessor(queueMock.Object, logServiceMock.Object, maxRetries: 2);
        
        IRetryableCommand retryCommand = new RetryCommand(new Mock<ICommand>().Object, retryCount: 2);

        var testException = new Exception("Third failure");
        
        exceptionProcessor.Process(testException, retryCommand);

        queueMock.Verify(q =>
                q.Enqueue(It.Is<LogCommand>(cmd => cmd != null)),
            Times.Once);
    }
}