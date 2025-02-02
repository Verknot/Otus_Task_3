using Otus_Task_3.Implementation;
using Otus_Task_3.Interface;

namespace Otus_Task_3;

public class ExceptionProcessor
{
    private readonly ICommandQueue _queue;
    private readonly ILogService _logService;

    /// <summary>
    /// Максимальное количество повторов до записи в лог.
    /// Например, для стратегии "при первом исключении повторить, при повторном – лог" maxRetries = 1,
    /// а для стратегии "повторить два раза, потом лог" maxRetries = 2.
    /// </summary>
    public int MaxRetries { get; }

    public ExceptionProcessor(ICommandQueue queue, ILogService logService, int maxRetries)
    {
        _queue = queue;
        _logService = logService;
        MaxRetries = maxRetries;
    }

    public void Process(Exception ex, ICommand command)
    {
        int currentRetries = 0;

        ICommand originalCommand = command;
        if (command is IRetryableCommand retryable)
        {
            currentRetries = retryable.RetryCount;
            originalCommand = retryable.OriginalCommand;
        }

        if (currentRetries < MaxRetries)
        {
            IRetryableCommand retryCommand;

            if (MaxRetries == 2 && currentRetries == 1)
            {
                retryCommand = new FailedRetryCommand(originalCommand, currentRetries + 1);
            }
            else
            {
                retryCommand = new RetryCommand(originalCommand, currentRetries + 1);
            }

            _queue.Enqueue(retryCommand);
        }
        else
        {
            _queue.Enqueue(new LogCommand(ex, _logService));
        }
    }
}