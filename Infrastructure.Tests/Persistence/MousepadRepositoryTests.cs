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
    public class MousepadRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IMousepadRepository _repository;
        
        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new MousepadRepository(_context);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredMousepads(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.Mousepads.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
            var mousepads = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
            var mousepadToChange = mousepads.First(c => c.Id == 13);
            mousepadToChange.Name = changedName;
            
            // Assert
            Assert.That((await _context.Mousepads.FindAsync(13))?.Name, Is.EqualTo(changedName), "Changes has not been saved.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredMousepad(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.Mousepads.Where(c => c.Id > 13).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
            var mousepads = await _repository.GetByConditionPagedAsync(c => c.Id == 13, pagingParams, true, CancellationToken.None);
            var mousepadToChange = mousepads.First(c => c.Id == 13);
            mousepadToChange.Name = changedName;
            
            // Assert
            Assert.That((await _context.Mousepads.FindAsync(13))?.Name, Is.EqualTo(changedName), "Changes has not been saved.");
        }

        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingMousepad_ReturnsMousepad(int id)
        {
            // Arrange
            var expected = _helper.Mousepads.First(c => c.Id == id);
            
            // Act
            var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual mousepad is not equal to expected.");
        }

        [TestCase(0)]
        [TestCase(12)]
        public async Task GetByIdAsync_NotExistingMousepad_ReturnsNull(int id)
        {
            // Arrange

            // Act
            var actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null object.");
        }

        [TestCase(13)]
        [TestCase(14)]
        [TestCase(15)]
        public async Task GetByIdAsync_ExistingMousepadWithTracking_ReturnsMousepadAndTracksChanges(int id)
        {
            // Arrange
            const string changedName = "changedName";
            
            // Act
            var mousepad = await _repository.GetByIdAsync(id, true, CancellationToken.None);
            mousepad.Name = changedName;

            // Assert
            Assert.That((await _context.Mousepads.FindAsync(id))?.Name, Is.EqualTo(changedName), "Changes has not been tracked.");
        }

        [Test]
        public Task Add_NewMousepad_AddsMousepadToContext()
        {
            // Arrange
            var newMousepad = new Mousepad();

            // Act
            _repository.Add(newMousepad);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Mousepads.Count(), Is.EqualTo(4), "The mousepad has not been added to context.");
            return Task.CompletedTask;
        }

        [Test]
        public Task Update_ExistingMousepad_UpdatedMousepad()
        {
            // Arrange
            var mousepadToUpdate = _helper.Mousepads.First(c => c.Id == 13);
            const string changedName = "changedName";
            mousepadToUpdate.Name = changedName;

            // Act
            _repository.Update(mousepadToUpdate);

            // Assert
            Assert.That(_context.Mousepads.Find(13)?.Name, Is.EqualTo(changedName), "The mousepad has not been updated.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task Delete_ExistingMousepad_DeletedMousepadFromContext()
        {
            // Arrange
            var mousepadToDelete = _helper.Mousepads.First(c => c.Id == 13);

            // Act
            _repository.Delete(mousepadToDelete);
            await _context.SaveChangesAsync();

            // Assert
            Assert.That(await _context.Mousepads.FindAsync(13), Is.Null, "The mousepad has not been deleted.");
        }
    }
}