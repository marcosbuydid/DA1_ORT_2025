namespace Domain.Tests;

[TestClass]
public class ContractorTests
{
    [TestMethod]
    public void NewContractor_WhenConstructorIsNotNull_ThenContractorIsCreated()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Hilary", 35, 3000, 60, 180);
        //assert
        Assert.IsNotNull(contractor);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewContractor_WhenNameIsNull_ThenThrowException()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor(null, 35, 3000, 60, 180);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewContractor_WhenNameIsEmpty_ThenThrowException()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("", 35, 3000, 60, 180);
    }

    [TestMethod]
    public void NewContractor_WhenNameIsNotNullOrEmpty_ThenNameShouldBeAssigned()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophia", 45, 2800, 60, 180);
        //assert
        Assert.AreEqual("Sophia", contractor.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewContractor_WhenAgeIsBelowEighteen_ThenThrowException()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophie", 15, 2800, 60, 180);
    }

    [TestMethod]
    public void NewContractor_WhenAgeIsAboveEighteen_ThenAgeShouldBeAssigned()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophie", 25, 2800, 60, 180);
        //assert
        Assert.AreEqual(25, contractor.Age);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewContractor_WhenBaseSalaryIsCeroOrNegative_ThenThrowException()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophie", 25, 0, 60, 180);
    }

    [TestMethod]
    public void NewContractor_WhenBaseSalaryIsAboveCero_ThenBaseSalaryShouldBeAssigned()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophie", 25, 2800, 60, 180);
        //assert
        Assert.AreEqual(2800, contractor.BaseSalary);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewContractor_WhenHourlyRateIsCeroOrBelow_ThenThrowException()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophie", 25, 2800, -10, 180);
    }

    [TestMethod]
    public void NewContractor_WhenHourlyRateIsAboveCero_ThenHourlyRateShouldBeAssigned()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophie", 25, 2800, 60, 180);
        //assert
        Assert.AreEqual(60, contractor.HourlyRate);
    }

    [TestMethod]
    public void NewContractor_WhenWorkedHoursAreGiven_ThenWorkedHoursShouldBeAssigned()
    {
        //arrange
        Contractor contractor;
        //act
        contractor = new Contractor("Sophie", 25, 2800, 60, 180);
        //assert
        Assert.AreEqual(180, contractor.WorkedHours);
    }

    [TestMethod]
    public void Contractor_WhenInvokeCalculateSalary_ThenReturnSuccessfully()
    {
        //arrange
        Contractor contractor = new Contractor("Sophie", 25, 2800, 60, 180);
        //act
        decimal result = contractor.CalculateSalary();
        //assert
        Assert.AreEqual(10800, result);
    }
}