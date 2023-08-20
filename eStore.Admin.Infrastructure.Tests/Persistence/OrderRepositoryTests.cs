using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Utility;
using eStore.Admin.Domain.Entities;
using eStore.Admin.Infrastructure.Persistence;
using eStore.Admin.Infrastructure.Persistence.Repositories;
using eStore.Admin.Infrastructure.Tests.EqualityComparers;
using eStore.Admin.Tests.Common;
using NUnit.Framework;

namespace eStore.Admin.Infrastructure.Tests.Persistence;

[TestFixture]
public class OrderRepositoryTests
{
    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private IOrderRepository _repository;

    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _repository = new OrderRepository(_context);
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredOrders(int pageSize, int pageNumber)
    {
        // Arrange
        var expected = _helper.Orders.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        var pagingParams = new PagingParameters(pageSize, pageNumber);

        // Act
        var actual = await _repository.GetAllPagedAsync(pagingParams, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new OrderEqualityComparer()),
            "The actual collection is not equal to expected");
    }

    [Test]
    public async Task GetAllPaged_WithTracking_TracksChanges()
    {
        // Arrange
        var pagingParams = new PagingParameters();
        const string changedAddress = "changedAddress";

        // Act
        var orders = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
        var orderToChange = orders.First(c => c.Id == 1);
        orderToChange.ShippingAddress = changedAddress;

        // Assert
        Assert.That((await _context.Orders.FindAsync(1))?.ShippingAddress, Is.EqualTo(changedAddress),
            "Changes has not been saved.");
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredOrder(int pageSize,
        int pageNumber)
    {
        // Arrange
        var expected = _helper.Orders.Where(c => c.Id > 1).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        var pagingParams = new PagingParameters(pageSize, pageNumber);

        // Act
        var actual = await _repository.GetByConditionPagedAsync(c => c.Id > 1, pagingParams, false,
            CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new OrderEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [Test]
    public async Task GetByConditionPagedAsync_WithTracking_TracksChanges()
    {
        // Arrange
        var pagingParams = new PagingParameters();
        const string changedAddress = "changedAddress";

        // Act
        var orders = await _repository.GetByConditionPagedAsync(c => c.Id == 1, pagingParams, true,
            CancellationToken.None);
        var orderToChange = orders.First(c => c.Id == 1);
        orderToChange.ShippingAddress = changedAddress;

        // Assert
        Assert.That((await _context.Orders.FindAsync(1))?.ShippingAddress, Is.EqualTo(changedAddress),
            "Changes has not been saved.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public async Task GetByIdAsync_ExistingOrder_ReturnsOrder(int id)
    {
        // Arrange
        var expected = _helper.Orders.First(c => c.Id == id);

        // Act
        var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new OrderEqualityComparer()),
            "The actual order is not equal to expected.");
    }

    [TestCase(0)]
    [TestCase(12)]
    public async Task GetByIdAsync_NotExistingOrder_ReturnsNull(int id)
    {
        // Arrange

        // Act
        var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null object.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public async Task GetByIdAsync_ExistingOrderWithTracking_ReturnsOrderAndTracksChanges(int id)
    {
        // Arrange
        const string changedAddress = "changedAddress";

        // Act
        var order = await _repository.GetByIdAsync(id, true, CancellationToken.None);
        order.ShippingAddress = changedAddress;

        // Assert
        Assert.That((await _context.Orders.FindAsync(id))?.ShippingAddress, Is.EqualTo(changedAddress),
            "Changes has not been tracked.");
    }

    [Test]
    public Task Add_NewOrder_AddsOrderToContext()
    {
        // Arrange
        var newOrder = new Order();

        // Act
        _repository.Add(newOrder);
        _context.SaveChanges();

        // Assert
        Assert.That(_context.Orders.Count(), Is.EqualTo(7), "The order has not been added to context.");
        return Task.CompletedTask;
    }

    [Test]
    public Task Update_ExistingOrder_UpdatedOrder()
    {
        // Arrange
        var orderToUpdate = _helper.Orders.First(c => c.Id == 1);
        const string changedAddress = "changedAddress";
        orderToUpdate.ShippingAddress = changedAddress;

        // Act
        _repository.Update(orderToUpdate);

        // Assert
        Assert.That(_context.Orders.Find(1)?.ShippingAddress, Is.EqualTo(changedAddress),
            "The order has not been updated.");
        return Task.CompletedTask;
    }

    [Test]
    public async Task Delete_ExistingOrder_DeletedOrderFromContext()
    {
        // Arrange
        var orderToDelete = _helper.Orders.First(c => c.Id == 1);

        // Act
        _repository.Delete(orderToDelete);
        await _context.SaveChangesAsync();

        // Assert
        Assert.That(await _context.Orders.FindAsync(1), Is.Null, "The order has not been deleted.");
    }


    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetAllWithOrderItemsPagedAsync_ValidPagingParams_ReturnsRequiredOrders(int pageSize,
        int pageNumber)
    {
        // Arrange
        var expected = _helper.Orders.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
        var expectedOrderItems = expected.Select(o => o.OrderItems);
        var pagingParams = new PagingParameters(pageSize, pageNumber);

        // Act
        var actual = await _repository.GetAllWithOrderItemsPagedAsync(pagingParams, false, CancellationToken.None);
        var actualList = actual.ToList();

        // Assert
        Assert.That(actualList, Is.EqualTo(expected).Using(new OrderEqualityComparer()),
            "The actual collection is not equal to expected");
        Assert.That(actualList.Select(o => o.OrderItems), Is.EqualTo(expectedOrderItems)
            .Using(new OrderItemEqualityComparer()), "The actual order items are not equivalent to expected.");
    }

    [Test]
    public async Task GetAllWithOrderItemsPagedAsync_WithTracking_TracksChanges()
    {
        // Arrange
        var pagingParams = new PagingParameters();
        const string changedAddress = "changedAddress";

        // Act
        var orders = await _repository.GetAllWithOrderItemsPagedAsync(pagingParams, true, CancellationToken.None);
        var orderToChange = orders.First(c => c.Id == 1);
        orderToChange.ShippingAddress = changedAddress;

        // Assert
        Assert.That((await _context.Orders.FindAsync(1))?.ShippingAddress, Is.EqualTo(changedAddress),
            "Changes has not been saved.");
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetByConditionWithOrderItemsPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredOrder(
        int pageSize, int pageNumber)
    {
        // Arrange
        var expected = _helper.Orders.Where(c => c.Id > 1).Skip(pageSize * (pageNumber - 1)).Take(pageSize)
            .ToList();
        var expectedOrderItems = expected.Select(o => o.OrderItems);
        var pagingParams = new PagingParameters(pageSize, pageNumber);

        // Act
        var actual = await _repository.GetByConditionWithOrderItemsPagedAsync(c => c.Id > 1, pagingParams, false,
            CancellationToken.None);
        var actualList = actual.ToList();

        // Assert
        Assert.That(actualList, Is.EqualTo(expected).Using(new OrderEqualityComparer()),
            "The actual collection is not equal to expected");
        Assert.That(actualList.Select(o => o.OrderItems), Is.EqualTo(expectedOrderItems)
            .Using(new OrderItemEqualityComparer()), "The actual order items are not equivalent to expected.");
    }

    [Test]
    public async Task GetByConditionWithOrderItemsPagedAsync_WithTracking_TracksChanges()
    {
        // Arrange
        var pagingParams = new PagingParameters();
        const string changedAddress = "changedAddress";

        // Act
        var orders = await _repository.GetByConditionWithOrderItemsPagedAsync(c => c.Id == 1, pagingParams, true,
            CancellationToken.None);
        var orderToChange = orders.First(c => c.Id == 1);
        orderToChange.ShippingAddress = changedAddress;

        // Assert
        Assert.That((await _context.Orders.FindAsync(1))?.ShippingAddress, Is.EqualTo(changedAddress),
            "Changes has not been saved.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public async Task GetByIdWithOrderItemsAsync_ExistingOrder_ReturnsOrder(int id)
    {
        // Arrange
        var expected = _helper.Orders.First(c => c.Id == id);
        var expectedOrderItems = _helper.OrderItems.Where(oi => oi.OrderId == id);

        // Act
        var actual = await _repository.GetByIdWithOrderItemsAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new OrderEqualityComparer()),
            "The actual collection is not equal to expected");
        Assert.That(actual.OrderItems, Is.EqualTo(expectedOrderItems)
            .Using(new OrderItemEqualityComparer()), "The actual order items are not equivalent to expected.");
    }

    [TestCase(0)]
    [TestCase(12)]
    public async Task GetByIdWithOrderItemsAsync_NotExistingOrder_ReturnsNull(int id)
    {
        // Arrange

        // Act
        var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null object.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    [TestCase(6)]
    public async Task GetByIdWithOrderItemsAsync_ExistingOrderWithTracking_ReturnsOrderAndTracksChanges(int id)
    {
        // Arrange
        const string changedAddress = "changedAddress";

        // Act
        var order = await _repository.GetByIdAsync(id, true, CancellationToken.None);
        order.ShippingAddress = changedAddress;

        // Assert
        Assert.That((await _context.Orders.FindAsync(id))?.ShippingAddress, Is.EqualTo(changedAddress),
            "Changes has not been tracked.");
    }
}