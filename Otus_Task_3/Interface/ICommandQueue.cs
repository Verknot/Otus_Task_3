namespace Otus_Task_3.Interface;

public interface ICommandQueue
{
    void Enqueue(ICommand command);
}