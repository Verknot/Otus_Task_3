namespace Otus_Task_3.Interface;

public interface IRetryableCommand : ICommand
{
    /// <summary>
    /// Количество уже выполненных повторов.
    /// </summary>
    int RetryCount { get; }

    /// <summary>
    /// Исходная (оригинальная) команда, которую необходимо выполнить.
    /// </summary>
    ICommand OriginalCommand { get; }
}