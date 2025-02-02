using System.Numerics;
using Otus_Task_3.Interface;

namespace Otus_Task_3.Commands;

public class ChangeVelocityCommand : ICommand
{
    private readonly IGameObject _gameObject;
    private readonly float _angleDegrees;

    public ChangeVelocityCommand(IGameObject gameObject, float angleDegrees)
    {
        _gameObject = gameObject;
        _angleDegrees = angleDegrees;
    }

    public void Execute()
    {
        if (_gameObject.Velocity != Vector2.Zero)
        {
            float radians = MathF.PI * _angleDegrees / 180f;
            float cos = MathF.Cos(radians);
            float sin = MathF.Sin(radians);
            Vector2 v = _gameObject.Velocity;
            Vector2 newVelocity = new Vector2(
                v.X * cos - v.Y * sin,
                v.X * sin + v.Y * cos
            );
            _gameObject.Velocity = newVelocity;
            Console.WriteLine("ChangeVelocityCommand executed. New velocity: " + newVelocity);
        }
        else
        {
            Console.WriteLine("ChangeVelocityCommand: Object is stationary; velocity remains zero.");
        }
    }
}