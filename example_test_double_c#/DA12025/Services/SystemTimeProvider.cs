using Services.Interfaces;

namespace Services;

public class SystemTimeProvider: ITimeProvider
{
    public DateTime GetCurrentTime()
    {
        return DateTime.Now;
    }
}