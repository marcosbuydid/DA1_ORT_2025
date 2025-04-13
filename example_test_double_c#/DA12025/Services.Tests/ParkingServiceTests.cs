using Services;

namespace ParkingServiceTests;

[TestClass]
public class ParkingServiceTests
{
    [TestMethod]
    public void ShouldChargeForParking_WhenParkingIsBetweenChargingHours_ThenItShouldBillNormally()
    {
        //arrange
        //we select an hour between working hours (for example 2:00 PM)
        var fixedDateTime = new DateTime(2025, 4, 9, 14, 0, 0);
        var fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        var parkingService = new ParkingService(fakeTimeProvider);
        //act
        var result = parkingService.ShouldBill();
        //assert
        Assert.IsTrue(result);
    }

    [TestMethod]
    public void ShouldChargeForParking_WhenParkingTimeIsAfterWorkingHours_ThenItShouldNotBill()
    {
        //arrange
        //we select an hour after working hours (for example 8:00 PM)
        var fixedDateTime = new DateTime(2025, 4, 9, 8, 0, 0);
        var fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        var parkingService = new ParkingService(fakeTimeProvider);
        //act
        var result = parkingService.ShouldBill();
        //assert
        Assert.IsFalse(result);
    }

    [TestMethod]
    public void ShouldChargeForParking_WhenParkingTimeIsBeforeWorkingHours_ThenItShouldNotBill()
    {
        //arrange
        //we select an hour before working hours (for example 7:00 AM)
        var fixedDateTime = new DateTime(2025, 4, 9, 7, 0, 0);
        var fakeTimeProvider = new FakeTimeProvider(fixedDateTime);
        var parkingService = new ParkingService(fakeTimeProvider);
        //act
        var result = parkingService.ShouldBill();
        //assert
        Assert.IsFalse(result);
    }
}