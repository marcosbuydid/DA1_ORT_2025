namespace Domain;

public class Car : Vehicle
{
    public Car(int doorQuantity, string chassisColor) : base(doorQuantity, chassisColor)
    {
    }

    public override void TurnOn()
    {
        Console.WriteLine($"Turning on car with {DoorQuantity} doors and chassis color {ChassisColor}");
    }
}