using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;
using eStore_Admin.Infrastructure.Persistence;
using eStore_Admin.Infrastructure.Persistence.Repositories;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Persistence
{
    [TestFixture]
    public class OrderItemRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IOrderItemRepository _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new OrderItemRepository(_context);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredOrderItems(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.OrderItems.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var pagingParams = new PagingParameters(pageSize, pageNumber);

            // Act
            var actual = await _repository.GetAllPagedAsync(pagingParams, false, CancellationToken.None);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected");
        }

        [Test]
        public async Task GetAllPaged_WithTracking_TracksChanges()
        {
            // Arrange
            var pagingParams = new PagingParameters();
            const int changedQuantity = 69;

            // Act
            var orderItems = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
            OrderItem? orderItemToChange = orderItems.First(c => c.Id == 1);
            orderItemToChange.Quantity = changedQuantity;

            // Assert
            Assert.That((await _context.OrderItems.FindAsync(1))?.Quantity, Is.EqualTo(changedQuantity),
                "Changes has not been saved.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredOrderItem(int pageSize,
            int pageNumber)
        {
            // Arrange
            var expected = _helper.OrderItems.Where(c => c.Id > 1).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var pagingParams = new PagingParameters(pageSize, pageNumber);

            // Act
            var actual =
                await _repository.GetByConditionPagedAsync(c => c.Id > 1, pagingParams, false, CancellationToken.None);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByConditionPagedAsync_WithTracking_TracksChanges()
        {
            // Arrange
            var pagingParams = new PagingParameters();
            const int changedQuantity = 69;

            // Act
            var orderItems =
                await _repository.GetByConditionPagedAsync(c => c.Id == 1, pagingParams, true, CancellationToken.None);
            OrderItem? orderItemToChange = orderItems.First(c => c.Id == 1);
            orderItemToChange.Quantity = changedQuantity;

            // Assert
            Assert.That((await _context.OrderItems.FindAsync(1))?.Quantity, Is.EqualTo(changedQuantity),
                "Changes has not been saved.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        public async Task GetByIdAsync_ExistingOrderItem_ReturnsOrderItem(int id)
        {
            // Arrange
            OrderItem expected = _helper.OrderItems.First(c => c.Id == id);

            // Act
            OrderItem? actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual order item is not equal to expected.");
        }

        [TestCase(0)]
        [TestCase(122)]
        public async Task GetByIdAsync_NotExistingOrderItem_ReturnsNull(int id)
        {
            // Arrange

            // Act
            OrderItem? actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null object.");
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        [TestCase(16)]
        [TestCase(17)]
        [TestCase(18)]
        public async Task GetByIdAsync_ExistingOrderItemWithTracking_ReturnsOrderItemAndTracksChanges(int id)
        {
            // Arrange
            const int changedQuantity = 69;

            // Act
            OrderItem? orderItem = await _repository.GetByIdAsync(id, true, CancellationToken.None);
            orderItem.Quantity = changedQuantity;

            // Assert
            Assert.That((await _context.OrderItems.FindAsync(id))?.Quantity, Is.EqualTo(changedQuantity),
                "Changes has not been tracked.");
        }

        [Test]
        public Task Add_NewOrderItem_AddsOrderItemToContext()
        {
            // Arrange
            var newOrderItem = new OrderItem();

            // Act
            _repository.Add(newOrderItem);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.OrderItems.Count(), Is.EqualTo(19), "The order item has not been added to context.");
            return Task.CompletedTask;
        }

        [Test]
        public Task Update_ExistingOrderItem_UpdatedOrderItem()
        {
            // Arrange
            OrderItem orderItemToUpdate = _helper.OrderItems.First(c => c.Id == 1);
            const int changedQuantity = 69;
            orderItemToUpdate.Quantity = changedQuantity;

            // Act
            _repository.Update(orderItemToUpdate);

            // Assert
            Assert.That(_context.OrderItems.Find(1)?.Quantity, Is.EqualTo(changedQuantity),
                "The order item has not been updated.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task Delete_ExistingOrderItem_DeletedOrderItemFromContext()
        {
            // Arrange
            OrderItem orderItemToDelete = _helper.OrderItems.First(c => c.Id == 1);

            // Act
            _repository.Delete(orderItemToDelete);
            await _context.SaveChangesAsync();

            // Assert
            Assert.That(await _context.OrderItems.FindAsync(1), Is.Null, "The order item has not been deleted.");
        }
    }
}