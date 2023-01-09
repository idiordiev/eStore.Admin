using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Domain.Entities;
using eStore_Admin.Infrastructure.Persistence;
using eStore_Admin.Infrastructure.Persistence.Repositories;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Persistence
{
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
            ICustomerRepository? instance = _unitOfWork.CustomerRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<CustomerRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void CustomerRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            ICustomerRepository? first = _unitOfWork.CustomerRepository;
            ICustomerRepository? second = _unitOfWork.CustomerRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void GamepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IGamepadRepository? instance = _unitOfWork.GamepadRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<GamepadRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void GamepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IGamepadRepository? first = _unitOfWork.GamepadRepository;
            IGamepadRepository? second = _unitOfWork.GamepadRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void GoodsRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IGoodsRepository? instance = _unitOfWork.GoodsRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<GoodsRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void GoodsRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IGoodsRepository? first = _unitOfWork.GoodsRepository;
            IGoodsRepository? second = _unitOfWork.GoodsRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void KeyboardRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IKeyboardRepository? instance = _unitOfWork.KeyboardRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<KeyboardRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void KeyboardRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IKeyboardRepository? first = _unitOfWork.KeyboardRepository;
            IKeyboardRepository? second = _unitOfWork.KeyboardRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void KeyboardSwitchRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IKeyboardSwitchRepository? instance = _unitOfWork.KeyboardSwitchRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<KeyboardSwitchRepository>(),
                "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void KeyboardSwitchRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IKeyboardSwitchRepository? first = _unitOfWork.KeyboardSwitchRepository;
            IKeyboardSwitchRepository? second = _unitOfWork.KeyboardSwitchRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void MousepadRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IMousepadRepository? instance = _unitOfWork.MousepadRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<MousepadRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void MousepadRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IMousepadRepository? first = _unitOfWork.MousepadRepository;
            IMousepadRepository? second = _unitOfWork.MousepadRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void MouseRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IMouseRepository? instance = _unitOfWork.MouseRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<MouseRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void MouseRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IMouseRepository? first = _unitOfWork.MouseRepository;
            IMouseRepository? second = _unitOfWork.MouseRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void OrderItemRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IOrderItemRepository? instance = _unitOfWork.OrderItemRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<OrderItemRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void OrderItemRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IOrderItemRepository? first = _unitOfWork.OrderItemRepository;
            IOrderItemRepository? second = _unitOfWork.OrderItemRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }

        [Test]
        public void OrderRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IOrderRepository? instance = _unitOfWork.OrderRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<OrderRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void OrderRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IOrderRepository? first = _unitOfWork.OrderRepository;
            IOrderRepository? second = _unitOfWork.OrderRepository;

            // Assert
            Assert.That(second, Is.SameAs(first), "UnitOfWork returned different instances.");
        }


        [Test]
        public void ShoppingCartRepository_FirstCall_ReturnsNewInstance()
        {
            // Arrange

            // Act
            IShoppingCartRepository? instance = _unitOfWork.ShoppingCartRepository;

            // Assert
            Assert.That(instance, Is.InstanceOf<ShoppingCartRepository>(), "UnitOfWork returned wrong implementation.");
        }

        [Test]
        public void ShoppingCartRepository_SecondCall_ReturnsSameInstance()
        {
            // Arrange

            // Act
            IShoppingCartRepository? first = _unitOfWork.ShoppingCartRepository;
            IShoppingCartRepository? second = _unitOfWork.ShoppingCartRepository;

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
}