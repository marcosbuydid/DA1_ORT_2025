namespace Domain;

public abstract class Vehicle
{
    private int _doorQuantity;
    private string _chassisColor;

    public int DoorQuantity
    {
        get => _doorQuantity;
        set => _doorQuantity = value;
    }

    public string ChassisColor
    {
        get => _chassisColor;
        set => _chassisColor = value;
    }

    public Vehicle(int doorQuantity, string chassisColor)
    {
        DoorQuantity = doorQuantity;
        ChassisColor = chassisColor;
    }

    public abstract void TurnOn();
}