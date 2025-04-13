using Services;
using Services.Interfaces;

namespace ParkingServiceTests;

public class FakeTimeProvider : ITimeProvider
{
    private readonly DateTime _fixedTime;

    public FakeTimeProvider(DateTime fixedTime)
    {
        _fixedTime = fixedTime;
    }

    public DateTime GetCurrentTime()
    {
        return _fixedTime;
    }
}