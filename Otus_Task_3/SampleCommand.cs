using Otus_Task_3.Interface;

namespace Otus_Task_3;

public class SampleCommand : ICommand
{
    private readonly Action _action;

    public SampleCommand(Action action)
    {
        _action = action;
    }

    public void Execute()
    {
        _action();
    }
}