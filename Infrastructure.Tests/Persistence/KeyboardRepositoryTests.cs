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
    public class KeyboardRepositoryTests
    {
        private UnitTestHelper _helper;
        private ApplicationContext _context;
        private IKeyboardRepository _repository;

        [SetUp]
        public void Setup()
        {
            _helper = new UnitTestHelper();
            _context = _helper.GetApplicationContext();
            _repository = new KeyboardRepository(_context);
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredKeyboards(int pageSize, int pageNumber)
        {
            // Arrange
            var expected = _helper.Keyboards.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
            var keyboards = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
            Keyboard? keyboardToChange = keyboards.First(c => c.Id == 5);
            keyboardToChange.Name = changedName;

            // Assert
            Assert.That((await _context.Keyboards.FindAsync(5))?.Name, Is.EqualTo(changedName),
                "Changes has not been saved.");
        }

        [TestCase(1, 1)]
        [TestCase(2, 1)]
        [TestCase(3, 1)]
        [TestCase(1, 2)]
        [TestCase(1, 3)]
        [TestCase(4, 1)]
        public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredKeyboard(int pageSize,
            int pageNumber)
        {
            // Arrange
            var expected = _helper.Keyboards.Where(c => c.Id > 5).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            var pagingParams = new PagingParameters(pageSize, pageNumber);

            // Act
            var actual =
                await _repository.GetByConditionPagedAsync(c => c.Id > 5, pagingParams, false, CancellationToken.None);

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
            var keyboards =
                await _repository.GetByConditionPagedAsync(c => c.Id == 5, pagingParams, true, CancellationToken.None);
            Keyboard? keyboardToChange = keyboards.First(c => c.Id == 5);
            keyboardToChange.Name = changedName;

            // Assert
            Assert.That((await _context.Keyboards.FindAsync(5))?.Name, Is.EqualTo(changedName),
                "Changes has not been saved.");
        }

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public async Task GetByIdAsync_ExistingKeyboard_ReturnsKeyboard(int id)
        {
            // Arrange
            Keyboard expected = _helper.Keyboards.First(c => c.Id == id);

            // Act
            Keyboard? actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.EqualTo(expected), "The actual keyboard is not equal to expected.");
        }

        [TestCase(0)]
        [TestCase(12)]
        public async Task GetByIdAsync_NotExistingKeyboard_ReturnsNull(int id)
        {
            // Arrange

            // Act
            Keyboard? actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

            // Assert
            Assert.That(actual, Is.Null, "The method returned not-null object.");
        }

        [TestCase(5)]
        [TestCase(6)]
        [TestCase(7)]
        [TestCase(8)]
        [TestCase(9)]
        public async Task GetByIdAsync_ExistingKeyboardWithTracking_ReturnsKeyboardAndTracksChanges(int id)
        {
            // Arrange
            const string changedName = "changedName";

            // Act
            Keyboard? keyboard = await _repository.GetByIdAsync(id, true, CancellationToken.None);
            keyboard.Name = changedName;

            // Assert
            Assert.That((await _context.Keyboards.FindAsync(id))?.Name, Is.EqualTo(changedName),
                "Changes has not been tracked.");
        }

        [Test]
        public Task Add_NewKeyboard_AddsKeyboardToContext()
        {
            // Arrange
            var newKeyboard = new Keyboard();

            // Act
            _repository.Add(newKeyboard);
            _context.SaveChanges();

            // Assert
            Assert.That(_context.Keyboards.Count(), Is.EqualTo(6), "The keyboard has not been added to context.");
            return Task.CompletedTask;
        }

        [Test]
        public Task Update_ExistingKeyboard_UpdatedKeyboard()
        {
            // Arrange
            Keyboard keyboardToUpdate = _helper.Keyboards.First(c => c.Id == 5);
            const string changedName = "changedName";
            keyboardToUpdate.Name = changedName;

            // Act
            _repository.Update(keyboardToUpdate);

            // Assert
            Assert.That(_context.Keyboards.Find(5)?.Name, Is.EqualTo(changedName),
                "The keyboard has not been updated.");
            return Task.CompletedTask;
        }

        [Test]
        public async Task Delete_ExistingKeyboard_DeletedKeyboardFromContext()
        {
            // Arrange
            Keyboard keyboardToDelete = _helper.Keyboards.First(c => c.Id == 5);

            // Act
            _repository.Delete(keyboardToDelete);
            await _context.SaveChangesAsync();

            // Assert
            Assert.That(await _context.Keyboards.FindAsync(5), Is.Null, "The keyboard has not been deleted.");
        }
    }
}