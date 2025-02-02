using Otus_Task_3.Interface;

namespace Otus_Task_3.Implementation;

public class LogCommand : ICommand
{
    private readonly Exception _exception;
    private readonly ILogService _logService;

    public LogCommand(Exception exception, ILogService logService)
    {
        _exception = exception;
        _logService = logService;
    }

    public void Execute()
    {
        _logService.Log($"[LogCommand] Exception: {_exception}");
    }
}