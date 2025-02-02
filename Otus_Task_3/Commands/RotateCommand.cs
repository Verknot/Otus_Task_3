using Otus_Task_3.Interface;

namespace Otus_Task_3.Commands;

public class RotateCommand : ICommand
{
    private readonly IGameObject _gameObject;
    private readonly float _angleDegrees;

    public RotateCommand(IGameObject gameObject, float angleDegrees)
    {
        _gameObject = gameObject;
        _angleDegrees = angleDegrees;
    }

    public void Execute()
    {
        Console.WriteLine($"Executing RotateCommand. Rotating object by {_angleDegrees} degrees.");
    }
}