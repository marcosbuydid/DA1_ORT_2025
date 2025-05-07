using Services.Interfaces;

namespace Services;

public class ParkingService
{
    private readonly ITimeProvider _timeProvider;

    public ParkingService(ITimeProvider timeProvider)
    {
        _timeProvider = timeProvider;
    }

    public bool ShouldBill()
    {
        DateTime currentTime = _timeProvider.GetCurrentTime();
        //parking charges only apply between 10:00 AM and 6:00 PM
        return currentTime.Hour >= 10 && currentTime.Hour < 18;
    }
}