using Otus_Task_3.Interface;

namespace Otus_Task_3;

public class CommandQueue : ICommandQueue
{
    private readonly Queue<ICommand> _queue = new Queue<ICommand>();

    public void Enqueue(ICommand command)
    {
        _queue.Enqueue(command);
    }

    /// <summary>
    /// Извлекает и выполняет команды из очереди.
    /// Каждая команда выполняется в блоке try-catch.
    /// </summary>
    public void ProcessAll(ExceptionProcessor exceptionProcessor)
    {
        while (_queue.Count > 0)
        {
            ICommand command = _queue.Dequeue();
            try
            {
                command.Execute();
            }
            catch (Exception ex)
            {
                // Перехватываем базовое исключение и делегируем обработку.
                exceptionProcessor.Process(ex, command);
            }
        }
    }
}