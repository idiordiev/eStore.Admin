using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Domain.Entities;
using eStore.Admin.Infrastructure.Persistence;
using eStore.Admin.Infrastructure.Persistence.Repositories;
using eStore.Admin.Tests.Common;
using Moq;
using NUnit.Framework;

namespace eStore.Admin.Infrastructure.Tests.Persistence;

[TestFixture]
public class UnitOfWorkTests
{
    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private IUnitOfWork _unitOfWork;

    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _unitOfWork = new UnitOfWork(_context);
    }

    [Test]
    public void CustomerRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.CustomerRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<CustomerRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void CustomerRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.CustomerRepository;
        var second = _unitOfWork.CustomerRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void GamepadRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.GamepadRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<GamepadRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void GamepadRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.GamepadRepository;
        var second = _unitOfWork.GamepadRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void GoodsRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.GoodsRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<GoodsRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void GoodsRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.GoodsRepository;
        var second = _unitOfWork.GoodsRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void KeyboardRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.KeyboardRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<KeyboardRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void KeyboardRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.KeyboardRepository;
        var second = _unitOfWork.KeyboardRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void KeyboardSwitchRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.KeyboardSwitchRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<KeyboardSwitchRepository>(),
            "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void KeyboardSwitchRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.KeyboardSwitchRepository;
        var second = _unitOfWork.KeyboardSwitchRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void MousepadRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.MousepadRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<MousepadRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void MousepadRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.MousepadRepository;
        var second = _unitOfWork.MousepadRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void MouseRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.MouseRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<MouseRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void MouseRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.MouseRepository;
        var second = _unitOfWork.MouseRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void OrderItemRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.OrderItemRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<OrderItemRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void OrderItemRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.OrderItemRepository;
        var second = _unitOfWork.OrderItemRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void OrderRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.OrderRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<OrderRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void OrderRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.OrderRepository;
        var second = _unitOfWork.OrderRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }


    [Test]
    public void ShoppingCartRepository_FirstCall_ReturnsNewInstance()
    {
        // Arrange

        // Act
        var instance = _unitOfWork.ShoppingCartRepository;

        // Assert
        Assert.That(instance, Is.InstanceOf<ShoppingCartRepository>(), "UnitOfWork returned wrong implementation.");
    }

    [Test]
    public void ShoppingCartRepository_SecondCall_ReturnsSameInstance()
    {
        // Arrange

        // Act
        var first = _unitOfWork.ShoppingCartRepository;
        var second = _unitOfWork.ShoppingCartRepository;

        // Assert
        Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
    }

    [Test]
    public void Dispose_FirstCall_DisposesContext()
    {
        // Arrange
        var context = new Mock<ApplicationContext>();
        context.Setup(x => x.Dispose());
        IUnitOfWork unitOfWork = new UnitOfWork(context.Object);

        // Act
        unitOfWork.Dispose();

        // Assert
        context.Verify(x => x.Dispose(), Times.Once);
    }

    [Test]
    public void Dispose_SecondCall_DisposeMethodCalledOnce()
    {
        // Arrange
        var context = new Mock<ApplicationContext>();
        context.Setup(x => x.Dispose());
        IUnitOfWork unitOfWork = new UnitOfWork(context.Object);

        // Act
        unitOfWork.Dispose();
        unitOfWork.Dispose();

        // Assert
        context.Verify(x => x.Dispose(), Times.Once);
    }

    [Test]
    public void Save_AddedCustomer_SavesNewCustomer()
    {
        // Arrange
        var customer = new Customer();

        // Act
        _unitOfWork.CustomerRepository.Add(customer);
        _unitOfWork.Save();

        // Assert
        Assert.That(_context.Customers.Count(), Is.EqualTo(3), "The changes has not been saved.");
    }

    [Test]
    public async Task SaveAsync_AddedCustomer_SavesNewCustomer()
    {
        // Arrange
        var customer = new Customer();

        // Act
        _unitOfWork.CustomerRepository.Add(customer);
        await _unitOfWork.SaveAsync(CancellationToken.None);

        // Assert
        Assert.That(_context.Customers.Count(), Is.EqualTo(3), "The changes has not been saved.");
    }
}