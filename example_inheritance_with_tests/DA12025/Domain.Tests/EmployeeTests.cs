namespace Domain.Tests;

[TestClass]
public class EmployeeTests
{
    [TestMethod]
    public void NewEmployee_WhenConstructorIsNotNull_ThenEmployeeIsCreated()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("Nick", 35, 4000, 2000);
        //assert
        Assert.IsNotNull(employee);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewEmployee_WhenNameIsNull_ThenThrowException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee(null, 35, 4000, 2000);
    }
    
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewEmployee_WhenNameIsEmpty_ThenThrowException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("", 35, 4000, 2000);
    }

    [TestMethod]
    public void NewEmployee_WhenNameIsNotNullOrEmpty_ThenNameShouldBeAssigned()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 35, 1000, 3000);
        //assert
        Assert.AreEqual("John", employee.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewEmployee_WhenAgeIsBelowEighteen_ThenThrowException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 17, 4000, 2000);
    }

    [TestMethod]
    public void NewEmployee_WhenAgeIsAboveEighteen_ThenAgeShouldBeAssigned()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 19, 5000, 3000);
        //assert
        Assert.AreEqual(19, employee.Age);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NewEmployee_WhenBaseSalaryIsCeroOrNegative_ThenThrowException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 27, 0, 2000);
    }

    [TestMethod]
    public void NewEmployee_WhenBaseSalaryIsAboveCero_ThenBaseSalaryShouldBeAssigned()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 19, 5000, 3000);
        //assert
        Assert.AreEqual(5000, employee.BaseSalary);
    }

    [TestMethod]
    public void NewEmployee_WhenAnualBondIsGiven_ThenAnualBondShouldBeAssigned()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 19, 5000, 3000);
        //assert
        Assert.AreEqual(3000, employee.AnualBond);
    }

    [TestMethod]
    public void Employee_WhenInvokeCalculateSalary_ThenReturnSuccessfully()
    {
        //arrange
        Employee employee = new Employee("John", 19, 5000, 3000);
        //act
        decimal result = employee.CalculateSalary();
        //assert
        Assert.AreEqual(8000, result);
    }
}