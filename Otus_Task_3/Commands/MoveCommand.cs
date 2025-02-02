using Otus_Task_3.Interface;

namespace Otus_Task_3.Commands;

public class MoveCommand : ICommand
{
    private readonly IGameObject _gameObject;
    public MoveCommand(IGameObject gameObject)
    {
        _gameObject = gameObject;
    }

    public void Execute()
    {
        Console.WriteLine("Executing MoveCommand. Moving object with velocity: " + _gameObject.Velocity);
    }
}