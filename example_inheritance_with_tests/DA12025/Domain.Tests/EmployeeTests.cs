namespace Domain.Tests;

[TestClass]
public class EmployeeTests
{
    [TestMethod]
    public void CreateNewEmployee_WhenConstructorHasValidData_ThenEmployeeIsCreated()
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
    public void CreateNewEmployee_WhenNameIsNull_ThenThrowsException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee(null, 35, 4000, 2000);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void CreateNewEmployee_WhenNameIsEmpty_ThenThrowsException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("", 35, 4000, 2000);
    }

    [TestMethod]
    public void CreateNewEmployee_WhenNameIsNotNullOrEmpty_ThenNameIsAssigned()
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
    public void CreateNewEmployee_WhenAgeIsBelowEighteen_ThenThrowsException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 17, 4000, 2000);
    }

    [TestMethod]
    public void CreateNewEmployee_WhenAgeIsAboveOrEqualToEighteen_ThenAgeIsAssigned()
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
    public void CreateNewEmployee_WhenBaseSalaryIsCeroOrNegative_ThenThrowsException()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 27, 0, 2000);
    }

    [TestMethod]
    public void CreateNewEmployee_WhenBaseSalaryIsAboveCero_ThenBaseSalaryIsAssigned()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 19, 5000, 3000);
        //assert
        Assert.AreEqual(5000, employee.BaseSalary);
    }

    [TestMethod]
    public void CreateNewEmployee_WhenAnualBondIsGiven_ThenAnualBondIsAssigned()
    {
        //arrange
        Employee employee;
        //act
        employee = new Employee("John", 19, 5000, 3000);
        //assert
        Assert.AreEqual(3000, employee.AnualBond);
    }

    [TestMethod]
    public void CalculateSalary_WhenCalled_ThenSalaryIsReturned()
    {
        //arrange
        Employee employee = new Employee("John", 19, 5000, 3000);
        //act
        decimal result = employee.CalculateSalary();
        //assert
        Assert.AreEqual(8000, result);
    }
}