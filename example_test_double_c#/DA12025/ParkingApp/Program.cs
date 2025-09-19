using Services;

namespace ParkingApp;

class Program
{
    static void Main(string[] args)
    {
        SystemTimeProvider timeProvider = new SystemTimeProvider();
        ParkingService parkingService = new ParkingService(timeProvider);

        bool billResult = parkingService.Bill();

        Console.WriteLine(billResult
            ? "Parking billed successfully"
            : "Parking is outside charging hours. Billing does not apply.");
    }
}