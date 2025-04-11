namespace Domain.Tests;

[TestClass]
public class ProjectTests
{
    [TestMethod]
    public void NewProject_WhenConstructorIsNotNull_ThenProjectIsCreated()
    {
        //arrange
        Project project;
        //act
        project = new Project("Software Migration", 60000, new DateTime(2025, 03, 01));
        //assert
        Assert.IsNotNull(project);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProject_WhenNameIsNull_ThenThrowException()
    {
        //arrange
        Project project;
        //act
        project = new Project(null, 60000, new DateTime(2025, 03, 01));
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProject_WhenNameIsEmpty_ThenThrowException()
    {
        //arrange
        Project project;
        //act
        project = new Project("", 60000, new DateTime(2025, 03, 01));
    }

    [TestMethod]
    public void NewProject_WhenNameIsNotNullOrEmpty_ThenNameShouldBeAssigned()
    {
        //arrange
        Project project;
        //act
        project = new Project("Upgrade Libraries", 30000, new DateTime(2025, 1, 01));
        //assert
        Assert.AreEqual("Upgrade Libraries", project.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProject_WhenBudgetIsCeroOrNegative_ThenThrowException()
    {
        //arrange
        Project project;
        //act
        project = new Project("Software Upgrade", 0, new DateTime(2025, 02, 01));
    }

    [TestMethod]
    public void NewProject_WhenBudgetIsPositive_ThenBudgetShouldBeAssigned()
    {
        //arrange
        Project project;
        //act
        project = new Project("Software Upgrade", 25000, new DateTime(2025, 03, 11));
        Assert.AreEqual(25000, project.Budget);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProject_WhenStartDateIsAfterToday_ThenThrowException()
    {
        //arrange
        Project project;
        //act
        project = new Project("Software Upgrade", 15000, new DateTime(2026, 09, 01));
    }

    [TestMethod]
    public void NewProject_WhenStartDateIsEarlierThanToday_ThenStartDateShouldBeAssigned()
    {
        //arrange
        Project project;
        //act
        project = new Project("Software Upgrade", 15000, new DateTime(2025, 04, 01));
    }

    [TestMethod]
    public void NewProject_WhenAddingContributors_ThenStaffListIsNotEmpty()
    {
        //arrange
        Project project = new Project("Software Upgrade", 25000, new DateTime(2025, 01, 14));
        Employee employee = new Employee("John", 19, 5000, 3000);
        Contractor contractor = new Contractor("Sophia", 45, 2800, 60, 180);
        //act
        project.AddContributors(employee);
        project.AddContributors(contractor);
        //assert
        Assert.AreEqual(2, project.StaffList.Count);
        Assert.IsTrue(project.StaffList.Contains(employee));
        Assert.IsTrue(project.StaffList.Contains(contractor));
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewProject_WhenShowContributorsSalaryAndThereAreNoContributors_ThenThrowException()
    {
        //arrange
        Project project = new Project("Software Upgrade", 15000, new DateTime(2022, 09, 01));
        //act
        project.ShowContributorsSalary();
    }
    
    [TestMethod]
    public void NewProject_WhenShowContributorsSalaryAndThereAreContributors_ThenProyectDetailsAndSalaryIsShowed()
    {
        //arrange
        Project project = new Project("Software Libraries Upgrade", 35000, new DateTime(2025, 04, 10));
        Employee employee = new Employee("John", 19, 5000, 3000);
        Contractor contractor = new Contractor("Sophia", 45, 2800, 60, 180);
        project.AddContributors(employee);
        var stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        //act
        project.ShowContributorsSalary();
        //assert
        var consoleOutput = stringWriter.ToString();
        Assert.AreEqual("Proyect: Software Libraries Upgrade, Budget: $35000\r\nJohn has a salary of: $8000\r\n", consoleOutput);
    }
}