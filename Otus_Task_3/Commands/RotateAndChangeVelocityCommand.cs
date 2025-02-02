using Otus_Task_3.Interface;

namespace Otus_Task_3.Commands;

public class RotateAndChangeVelocityCommand : ICommand
{
    private readonly MacroCommand _macroCommand;

    public RotateAndChangeVelocityCommand(IGameObject gameObject, float angleDegrees)
    {
        var commands = new List<ICommand>
        {
            new RotateCommand(gameObject, angleDegrees),
            new ChangeVelocityCommand(gameObject, angleDegrees)
        };
        _macroCommand = new MacroCommand(commands);
    }

    public void Execute()
    {
        _macroCommand.Execute();
    }
}