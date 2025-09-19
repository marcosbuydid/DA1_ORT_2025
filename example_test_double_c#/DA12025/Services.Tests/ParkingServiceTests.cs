using Services;

namespace ParkingServiceTests;

[TestClass]
public class ParkingServiceTests
{
    [TestMethod]
    public void Bill_WhenParkingIsBetweenChargingHours_ThenBillingIsSuccessful()
    {
        //arrange
        //we select an hour between charging hours (example 2:00 PM)
        DateTime fixedDateTime = new DateTime(2025, 4, 9, 14, 0, 0);
        FakeTimeProvider fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        ParkingService parkingService = new ParkingService(fakeTimeProvider);
        //act
        Boolean result = parkingService.Bill();
        //assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void Bill_WhenParkingTimeIsAfterChargingHours_ThenBillingIsNotApplied()
    {
        //arrange
        //we select an hour after charging hours (example 8:00 PM)
        DateTime fixedDateTime = new DateTime(2025, 4, 9, 8, 0, 0);
        FakeTimeProvider fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        ParkingService parkingService = new ParkingService(fakeTimeProvider);
        //act
        Boolean result = parkingService.Bill();
        //assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void Bill_WhenParkingTimeIsBeforeChargingHours_ThenBillingIsNotApplied()
    {
        //arrange
        //we select an hour before charging hours (example 7:00 AM)
        DateTime fixedDateTime = new DateTime(2025, 4, 9, 7, 0, 0);
        FakeTimeProvider fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        ParkingService parkingService = new ParkingService(fakeTimeProvider);
        //act
        Boolean result = parkingService.Bill();
        //assert
        Assert.IsFalse(result);
    }
}