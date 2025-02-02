using Otus_Task_3.Interface;

namespace Otus_Task_3.Commands;

public class MoveWithFuelCommand : ICommand
{
    private readonly MacroCommand _macroCommand;

    public MoveWithFuelCommand(IGameObject gameObject)
    {
        // Требуемое количество топлива равно скорости расхода топлива.
        double requiredFuel = gameObject.FuelConsumptionRate;
        var commands = new List<ICommand>
        {
            new CheckFuelCommand(gameObject, requiredFuel),
            new MoveCommand(gameObject),
            new BurnFuelCommand(gameObject, gameObject.FuelConsumptionRate)
        };
        _macroCommand = new MacroCommand(commands);
    }

    public void Execute()
    {
        _macroCommand.Execute();
    }
}