using System.Numerics;

namespace Otus_Task_3.Interface;

public interface IGameObject
{
    double Fuel { get; set; }
    /// <summary>
    /// Количество топлива, расходуемое при движении.
    /// </summary>
    double FuelConsumptionRate { get; }
    
    /// <summary>
    /// Мгновенный вектор скорости объекта.
    /// </summary>
    Vector2 Velocity { get; set; }
}