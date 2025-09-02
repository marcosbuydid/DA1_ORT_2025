namespace Domain;

public class Van : Vehicle
{
    public Van(int doorQuantity, string chassisColor) : base(doorQuantity, chassisColor)
    {
    }

    public override void TurnOn()
    {
        Console.WriteLine($"Turning on van with {DoorQuantity} doors and chassis color {ChassisColor}");
    }
}