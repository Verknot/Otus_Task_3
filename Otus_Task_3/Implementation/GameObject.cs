using System.Numerics;
using Otus_Task_3.Interface;

namespace Otus_Task_3.Implementation;

public class GameObject : IGameObject
{
    public double Fuel { get; set; }
    public double FuelConsumptionRate { get; private set; }
    public Vector2 Velocity { get; set; }

    public GameObject(double fuel, double fuelConsumptionRate, Vector2 velocity)
    {
        Fuel = fuel;
        FuelConsumptionRate = fuelConsumptionRate;
        Velocity = velocity;
    }
}