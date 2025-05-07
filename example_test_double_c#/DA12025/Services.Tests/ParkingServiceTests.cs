using Services;

namespace ParkingServiceTests;

[TestClass]
public class ParkingServiceTests
{
    [TestMethod]
    public void ShouldBill_WhenParkingIsBetweenChargingHours_ThenItShouldBillNormally()
    {
        //arrange
        //we select an hour between working hours (example 2:00 PM)
        DateTime fixedDateTime = new DateTime(2025, 4, 9, 14, 0, 0);
        FakeTimeProvider fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        ParkingService parkingService = new ParkingService(fakeTimeProvider);
        //act
        Boolean result = parkingService.ShouldBill();
        //assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ShouldBill_WhenParkingTimeIsAfterWorkingHours_ThenItShouldNotBill()
    {
        //arrange
        //we select an hour after working hours (example 8:00 PM)
        DateTime fixedDateTime = new DateTime(2025, 4, 9, 8, 0, 0);
        FakeTimeProvider fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        ParkingService parkingService = new ParkingService(fakeTimeProvider);
        //act
        Boolean result = parkingService.ShouldBill();
        //assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ShouldBill_WhenParkingTimeIsBeforeWorkingHours_ThenItShouldNotBill()
    {
        //arrange
        //we select an hour before working hours (example 7:00 AM)
        DateTime fixedDateTime = new DateTime(2025, 4, 9, 7, 0, 0);
        FakeTimeProvider fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        ParkingService parkingService = new ParkingService(fakeTimeProvider);
        //act
        Boolean result = parkingService.ShouldBill();
        //assert
        Assert.IsFalse(result);
    }
}