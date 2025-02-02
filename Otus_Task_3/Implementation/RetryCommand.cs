using Otus_Task_3.Interface;

namespace Otus_Task_3.Implementation;

public class RetryCommand : IRetryableCommand
{
    public ICommand OriginalCommand { get; }
    public int RetryCount { get; }

    public RetryCommand(ICommand originalCommand, int retryCount)
    {
        OriginalCommand = originalCommand;
        RetryCount = retryCount;
    }

    public void Execute()
    {
        // Можно добавить дополнительную логику при повторе.
        OriginalCommand.Execute();
    }
}