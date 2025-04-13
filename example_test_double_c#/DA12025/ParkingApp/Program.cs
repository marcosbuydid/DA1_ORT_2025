using Services;

namespace ParkingApp;

class Program
{
    static void Main(string[] args)
    {
        SystemTimeProvider timeProvider = new SystemTimeProvider();
        ParkingService parkingService = new ParkingService(timeProvider);

        bool shouldBill = parkingService.ShouldBill();

        Console.WriteLine(shouldBill
            ? "Parking service should bill normally"
            : "Parking service is outside working hours");
    }
}