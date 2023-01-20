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
    public class MouseRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IMouseRepository _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new MouseRepository(_context);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredMouses(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.Mouses.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
            var mouses = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
            Mouse? mouseToChange = mouses.First(c => c.Id == 10);
            mouseToChange.Name = changedName;

            // Assert
            Assert.That((await _context.Mouses.FindAsync(10))?.Name, Is.EqualTo(changedName),
                "Changes has not been saved.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredMouse(int pageSize,
            int pageNumber)
        {
            // Arrange
            var expected = _helper.Mouses.Where(c => c.Id > 10).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var pagingParams = new PagingParameters(pageSize, pageNumber);

            // Act
            var actual =
                await _repository.GetByConditionPagedAsync(c => c.Id > 10, pagingParams, false, CancellationToken.None);

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
            var mouses =
                await _repository.GetByConditionPagedAsync(c => c.Id == 10, pagingParams, true, CancellationToken.None);
            Mouse? mouseToChange = mouses.First(c => c.Id == 10);
            mouseToChange.Name = changedName;

            // Assert
            Assert.That((await _context.Mouses.FindAsync(10))?.Name, Is.EqualTo(changedName),
                "Changes has not been saved.");
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task GetByIdAsync_ExistingMouse_ReturnsMouse(int id)
        {
            // Arrange
            Mouse expected = _helper.Mouses.First(c => c.Id == id);

            // Act
            Mouse? actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual mouse is not equal to expected.");
        }

        [TestCase(0)]
        [TestCase(142)]
        public async Task GetByIdAsync_NotExistingMouse_ReturnsNull(int id)
        {
            // Arrange

            // Act
            Mouse? actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null object.");
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task GetByIdAsync_ExistingMouseWithTracking_ReturnsMouseAndTracksChanges(int id)
        {
            // Arrange
            const string changedName = "changedName";

            // Act
            Mouse? mouse = await _repository.GetByIdAsync(id, true, CancellationToken.None);
            mouse.Name = changedName;

            // Assert
            Assert.That((await _context.Mouses.FindAsync(id))?.Name, Is.EqualTo(changedName),
                "Changes has not been tracked.");
        }

        [Test]
        public Task Add_NewMouse_AddsMouseToContext()
        {
            // Arrange
            var newMouse = new Mouse();

            // Act
            _repository.Add(newMouse);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Mouses.Count(), Is.EqualTo(4), "The mouse has not been added to context.");
            return Task.CompletedTask;
        }

        [Test]
        public Task Update_ExistingMouse_UpdatedMouse()
        {
            // Arrange
            Mouse mouseToUpdate = _helper.Mouses.First(c => c.Id == 10);
            const string changedName = "changedName";
            mouseToUpdate.Name = changedName;

            // Act
            _repository.Update(mouseToUpdate);

            // Assert
            Assert.That(_context.Mouses.Find(10)?.Name, Is.EqualTo(changedName), "The mouse has not been updated.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task Delete_ExistingMouse_DeletedMouseFromContext()
        {
            // Arrange
            Mouse mouseToDelete = _helper.Mouses.First(c => c.Id == 10);

            // Act
            _repository.Delete(mouseToDelete);
            await _context.SaveChangesAsync();

            // Assert
            Assert.That(await _context.Mouses.FindAsync(10), Is.Null, "The mouse has not been deleted.");
        }
    }
}