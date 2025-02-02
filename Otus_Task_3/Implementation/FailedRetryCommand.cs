using Otus_Task_3.Interface;

namespace Otus_Task_3.Implementation;

public class FailedRetryCommand : IRetryableCommand
{
    public ICommand OriginalCommand { get; }
    public int RetryCount { get; }

    public FailedRetryCommand(ICommand originalCommand, int retryCount)
    {
        OriginalCommand = originalCommand;
        RetryCount = retryCount;
    }

    public void Execute()
    {
        // Поведение может быть таким же, как у RetryCommand,
        // или можно добавить логику, сигнализирующую о неудаче.
        OriginalCommand.Execute();
    }
}