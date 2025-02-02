using Otus_Task_3.Interface;

namespace Otus_Task_3.Commands;

public class BurnFuelCommand : ICommand
{
    private readonly IGameObject _gameObject;
    private readonly double _burnAmount;

    public BurnFuelCommand(IGameObject gameObject, double burnAmount)
    {
        _gameObject = gameObject;
        _burnAmount = burnAmount;
    }

    public void Execute()
    {
        _gameObject.Fuel -= _burnAmount;
        Console.WriteLine("BurnFuelCommand executed. Burned fuel: " + _burnAmount);
    }  
}