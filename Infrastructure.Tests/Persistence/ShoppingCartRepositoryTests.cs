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
    public class ShoppingCartRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IShoppingCartRepository _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new ShoppingCartRepository(_context);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredShoppingCarts(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.ShoppingCarts.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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

            // Act
            var shoppingCarts = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
            var shoppingCartToChange = shoppingCarts.First(c => c.Id == 1);
            shoppingCartToChange.IsDeleted = true;
            
            // Assert
            Assert.That((await _context.ShoppingCarts.FindAsync(1))?.IsDeleted, Is.True, "Changes has not been saved.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredShoppingCart(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.ShoppingCarts.Where(c => c.Id > 1).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var pagingParams = new PagingParameters(pageSize, pageNumber);
            
            // Act
            var actual = await _repository.GetByConditionPagedAsync(c => c.Id > 1, pagingParams, false, CancellationToken.None);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByConditionPagedAsync_WithTracking_TracksChanges()
        {
            // Arrange
            var pagingParams = new PagingParameters();
            
            // Act
            var shoppingCarts = await _repository.GetByConditionPagedAsync(c => c.Id == 1, pagingParams, true, CancellationToken.None);
            var shoppingCartToChange = shoppingCarts.First(c => c.Id == 1);
            shoppingCartToChange.IsDeleted = true;
            
            // Assert
            Assert.That((await _context.ShoppingCarts.FindAsync(1))?.IsDeleted, Is.True, "Changes has not been saved.");
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GetByIdAsync_ExistingShoppingCart_ReturnsShoppingCart(int id)
        {
            // Arrange
            var expected = _helper.ShoppingCarts.First(c => c.Id == id);
            
            // Act
            var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual shopping cart is not equal to expected.");
        }

        [TestCase(0)]
        [TestCase(12)]
        public async Task GetByIdAsync_NotExistingShoppingCart_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null object.");
        }

        [TestCase(1)]
        [TestCase(2)]
        public async Task GetByIdAsync_ExistingShoppingCartWithTracking_ReturnsShoppingCartAndTracksChanges(int id)
        {
            // Arrange
            
            // Act
            var shoppingCart = await _repository.GetByIdAsync(id, true, CancellationToken.None);
            shoppingCart.IsDeleted = true;

            // Assert
            Assert.That((await _context.ShoppingCarts.FindAsync(id))?.IsDeleted, Is.True, "Changes has not been tracked.");
        }

        [Test]
        public Task Add_NewShoppingCart_AddsShoppingCartToContext()
        {
            // Arrange
            var newShoppingCart = new ShoppingCart();

            // Act
            _repository.Add(newShoppingCart);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.ShoppingCarts.Count(), Is.EqualTo(3), "The shopping cart has not been added to context.");
            return Task.CompletedTask;
        }

        [Test]
        public Task Update_ExistingShoppingCart_UpdatedShoppingCart()
        {
            // Arrange
            var shoppingCartToUpdate = _helper.ShoppingCarts.First(c => c.Id == 1);
            shoppingCartToUpdate.IsDeleted = true;

            // Act
            _repository.Update(shoppingCartToUpdate);

            // Assert
            Assert.That(_context.ShoppingCarts.Find(1)?.IsDeleted, Is.True, "The shopping cart has not been updated.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task Delete_ExistingShoppingCart_DeletedShoppingCartFromContext()
        {
            // Arrange
            var shoppingCartToDelete = _helper.ShoppingCarts.First(c => c.Id == 1);

            // Act
            _repository.Delete(shoppingCartToDelete);
            await _context.SaveChangesAsync();

            // Assert
            Assert.That(await _context.ShoppingCarts.FindAsync(1), Is.Null, "The shopping cart has not been deleted.");
        }
    }
}