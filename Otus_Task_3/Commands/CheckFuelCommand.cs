using Otus_Task_3.Interface;

namespace Otus_Task_3.Commands;

public class CheckFuelCommand : ICommand
{
    private readonly IGameObject _gameObject;
    private readonly double _requiredFuel;

    public CheckFuelCommand(IGameObject gameObject, double requiredFuel)
    {
        _gameObject = gameObject;
        _requiredFuel = requiredFuel;
    }

    public void Execute()
    {
        if (_gameObject.Fuel < _requiredFuel)
        {
            throw new CommandException("Not enough fuel");
        }
        Console.WriteLine("CheckFuelCommand passed.");
    }
}