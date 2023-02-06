using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;
using eStore_Admin.Infrastructure.Persistence;
using eStore_Admin.Infrastructure.Persistence.Repositories;
using Infrastructure.Tests.EqualityComparers;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Persistence;

[TestFixture]
public class CustomerRepositoryTests
{
    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private ICustomerRepository _repository;

    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _repository = new CustomerRepository(_context);
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredCustomers(int pageSize, int pageNumber)
    {
        // Arrange
        var expected = _helper.Customers.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        var pagingParams = new PagingParameters(pageSize, pageNumber);

        // Act
        var actual = await _repository.GetAllPagedAsync(pagingParams, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new CustomerEqualityComparer()),
            "The actual collection is not equal to expected");
    }

    [Test]
    public async Task GetAllPaged_WithTracking_TracksChanges()
    {
        // Arrange
        var pagingParams = new PagingParameters();
        const string changedName = "changedName";

        // Act
        var customers = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
        Customer customerToChange = customers.First(c => c.Id == 1);
        customerToChange.FirstName = changedName;

        // Assert
        Assert.That((await _context.Customers.FindAsync(1))?.FirstName, Is.EqualTo(changedName),
            "Changes has not been saved.");
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredCustomer(int pageSize,
        int pageNumber)
    {
        // Arrange
        var expected = _helper.Customers.Where(c => c.Id > 1).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        var pagingParams = new PagingParameters(pageSize, pageNumber);

        // Act
        var actual = await _repository.GetByConditionPagedAsync(c => c.Id > 1, pagingParams, false,
            CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new CustomerEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetByConditionPagedAsync_WithTracking_TracksChanges()
    {
        // Arrange
        var pagingParams = new PagingParameters();
        const string changedName = "changedName";

        // Act
        var customers = await _repository.GetByConditionPagedAsync(c => c.Id == 1, pagingParams, true,
            CancellationToken.None);
        Customer customerToChange = customers.First(c => c.Id == 1);
        customerToChange.FirstName = changedName;

        // Assert
        Assert.That((await _context.Customers.FindAsync(1))?.FirstName, Is.EqualTo(changedName),
            "Changes has not been saved.");
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task GetByIdAsync_ExistingCustomer_ReturnsCustomer(int id)
    {
        // Arrange
        Customer expected = _helper.Customers.First(c => c.Id == id);

        // Act
        Customer actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new CustomerEqualityComparer()),
            "The actual customer is not equal to expected.");
    }

    [TestCase(0)]
    [TestCase(12)]
    public async Task GetByIdAsync_NotExistingCustomer_ReturnsNull(int id)
    {
        // Arrange

        // Act
        Customer actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null object.");
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task GetByIdAsync_ExistingCustomerWithTracking_ReturnsCustomerAndTracksChanges(int id)
    {
        // Arrange
        const string changedName = "changedName";

        // Act
        Customer customer = await _repository.GetByIdAsync(id, true, CancellationToken.None);
        customer.FirstName = changedName;

        // Assert
        Assert.That((await _context.Customers.FindAsync(id))?.FirstName, Is.EqualTo(changedName),
            "Changes has not been tracked.");
    }

    [Test]
    public Task Add_NewCustomer_AddsCustomerToContext()
    {
        // Arrange
        var newCustomer = new Customer();

        // Act
        _repository.Add(newCustomer);
        _context.SaveChanges();

        // Assert
        Assert.That(_context.Customers.Count(), Is.EqualTo(3), "The customer has not been added to context.");
        return Task.CompletedTask;
    }

    [Test]
    public Task Update_ExistingCustomer_UpdatedCustomer()
    {
        // Arrange
        Customer customerToUpdate = _helper.Customers.First(c => c.Id == 1);
        const string changedName = "changedName";
        customerToUpdate.FirstName = changedName;

        // Act
        _repository.Update(customerToUpdate);

        // Assert
        Assert.That(_context.Customers.Find(1)?.FirstName, Is.EqualTo(changedName),
            "The customer has not been updated.");
        return Task.CompletedTask;
    }

    [Test]
    public async Task Delete_ExistingCustomer_DeletedCustomerFromContext()
    {
        // Arrange
        Customer customerToDelete = _helper.Customers.First(c => c.Id == 1);

        // Act
        _repository.Delete(customerToDelete);
        await _context.SaveChangesAsync();

        // Assert
        Assert.That(await _context.Customers.FindAsync(1), Is.Null, "The customer has not been deleted.");
    }
}