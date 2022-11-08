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
    public class GoodsRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IGoodsRepository _repository;
        
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new GoodsRepository(_context);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredGoods(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.Goods.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
            const string changedName = "changedName";
            
            // Act
            var goods = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
            var goodToChange = goods.First(c => c.Id == 5);
            goodToChange.Name = changedName;
            
            // Assert
            Assert.That((await _context.Goods.FindAsync(5))?.Name, Is.EqualTo(changedName), "Changes has not been saved.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredGood(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.Goods.Where(c => c.Id > 13).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var pagingParams = new PagingParameters(pageSize, pageNumber);
            
            // Act
            var actual = await _repository.GetByConditionPagedAsync(c => c.Id > 13, pagingParams, false, CancellationToken.None);

            // Assert
            CollectionAssert.AreEqual(expected, actual, "The actual collection is not equal to expected.");
        }

        [Test]
        public async Task GetByConditionPagedAsync_WithTracking_TracksChanges()
        {
            // Arrange
            var pagingParams = new PagingParameters();
            const string changedName = "changedName";
            
            // Act
            var goods = await _repository.GetByConditionPagedAsync(c => c.Id == 13, pagingParams, true, CancellationToken.None);
            var goodToChange = goods.First(c => c.Id == 13);
            goodToChange.Name = changedName;
            
            // Assert
            Assert.That((await _context.Goods.FindAsync(13))?.Name, Is.EqualTo(changedName), "Changes has not been saved.");
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
        public async Task GetByIdAsync_ExistingGood_ReturnsGood(int id)
        {
            // Arrange
            var expected = _helper.Goods.First(c => c.Id == id);
            
            // Act
            var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual good is not equal to expected.");
        }

        [TestCase(0)]
        [TestCase(172)]
        public async Task GetByIdAsync_NotExistingGood_ReturnsNull(int id)
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
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingGoodWithTracking_ReturnsGoodAndTracksChanges(int id)
        {
            // Arrange
            const string changedName = "changedName";
            
            // Act
            var good = await _repository.GetByIdAsync(id, true, CancellationToken.None);
            good.Name = changedName;

            // Assert
            Assert.That((await _context.Goods.FindAsync(id))?.Name, Is.EqualTo(changedName), "Changes has not been tracked.");
        }

        [Test]
        public Task Add_NewGood_AddsGoodToContext()
        {
            // Arrange
            var newGood = new Keyboard();

            // Act
            _repository.Add(newGood);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Goods.Count(), Is.EqualTo(16), "The good has not been added to context.");
            return Task.CompletedTask;
        }

        [Test]
        public Task Update_ExistingGood_UpdatedGood()
        {
            // Arrange
            var goodToUpdate = _helper.Goods.First(c => c.Id == 13);
            const string changedName = "changedName";
            goodToUpdate.Name = changedName;

            // Act
            _repository.Update(goodToUpdate);

            // Assert
            Assert.That(_context.Goods.Find(13)?.Name, Is.EqualTo(changedName), "The good has not been updated.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task Delete_ExistingGood_DeletedGoodFromContext()
        {
            // Arrange
            var goodToDelete = _helper.Goods.First(c => c.Id == 13);

            // Act
            _repository.Delete(goodToDelete);
            await _context.SaveChangesAsync();

            // Assert
            Assert.That(await _context.Goods.FindAsync(13), Is.Null, "The good has not been deleted.");
        }
    }
}