namespace Domain.Tests;

[TestClass]
public class ProyectTests
{
    [TestMethod]
    public void NewProyect_WhenConstructorIsNotNull_ThenProyectIsCreated()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect("Software Migration", 60000, new DateTime(2025, 03, 01));
        //assert
        Assert.IsNotNull(proyect);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProyect_WhenNameIsNull_ThenThrowException()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect(null, 60000, new DateTime(2025, 03, 01));
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProyect_WhenNameIsEmpty_ThenThrowException()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect("", 60000, new DateTime(2025, 03, 01));
    }

    [TestMethod]
    public void NewProyect_WhenNameIsNotNullOrEmpty_ThenNameIsValid()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect("Upgrade Libraries", 30000, new DateTime(2025, 1, 01));
        //assert
        Assert.AreEqual("Upgrade Libraries", proyect.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProyect_WhenBudgetIsCeroOrNegative_ThenThrowException()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect("Software Upgrade", 0, new DateTime(2025, 02, 01));
    }

    [TestMethod]
    public void NewProyect_WhenBudgetIsPositive_ThenBudgetShouldBeAssigned()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect("Software Upgrade", 25000, new DateTime(2025, 03, 11));
        Assert.AreEqual(25000, proyect.Budget);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProyect_WhenStartDateIsAfterToday_ThenThrowException()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect("Software Upgrade", 15000, new DateTime(2026, 09, 01));
    }

    [TestMethod]
    public void NewProyect_WhenStartDateIsEarlierThanToday_ThenStartDateShouldBeAssigned()
    {
        //arrange
        Proyect proyect;
        //act
        proyect = new Proyect("Software Upgrade", 15000, new DateTime(2025, 04, 01));
    }

    [TestMethod]
    public void NewProyect_WhenAddingContributors_ThenStaffListIsNotEmpty()
    {
        //arrange
        Proyect proyect = new Proyect("Software Upgrade", 25000, new DateTime(2025, 01, 14));
        Employee employee = new Employee("John", 19, 5000, 3000);
        Contractor contractor = new Contractor("Sophia", 45, 2800, 60, 180);
        //act
        proyect.AddContributors(employee);
        proyect.AddContributors(contractor);
        //assert
        Assert.AreEqual(2, proyect.StaffList.Count);
        Assert.IsTrue(proyect.StaffList.Contains(employee));
        Assert.IsTrue(proyect.StaffList.Contains(contractor));
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProyect_WhenShowContributorsSalaryAndThereAreNoContributors_ThenThrowException()
    {
        //arrange
        Proyect proyect = new Proyect("Software Upgrade", 15000, new DateTime(2022, 09, 01));
        //act
        proyect.ShowContributorsSalary();
    }
    
    [TestMethod]
    public void NewProyect_WhenShowContributorsSalaryAndThereAreContributors_ThenProyectDetailsAndSalaryIsShowed()
    {
        //arrange
        Proyect proyect = new Proyect("Software Libraries Upgrade", 35000, new DateTime(2025, 04, 10));
        Employee employee = new Employee("John", 19, 5000, 3000);
        Contractor contractor = new Contractor("Sophia", 45, 2800, 60, 180);
        proyect.AddContributors(employee);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        //act
        proyect.ShowContributorsSalary();
        //assert
        var consoleOutput = stringWriter.ToString();
        Assert.AreEqual("Proyect: Software Libraries Upgrade, Budget: $35000\r\nJohn has a salary of: $8000\r\n", consoleOutput);
    }
}