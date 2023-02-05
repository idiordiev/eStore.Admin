using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;
using eStore_Admin.Infrastructure.Persistence;
using eStore_Admin.Infrastructure.Persistence.Repositories;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Persistence;

[TestFixture]
public class KeyboardSwitchRepositoryTests
{
    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private IKeyboardSwitchRepository _repository;

    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _repository = new KeyboardSwitchRepository(_context);
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredKeyboardSwitches(int pageSize,
        int pageNumber)
    {
        // Arrange
        var expected = _helper.KeyboardSwitches.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
        var keyboardSwitches = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
        KeyboardSwitch keyboardSwitchToChange = keyboardSwitches.First(c => c.Id == 1);
        keyboardSwitchToChange.Name = changedName;

        // Assert
        Assert.That((await _context.KeyboardSwitches.FindAsync(1))?.Name, Is.EqualTo(changedName),
            "Changes has not been saved.");
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredKeyboardSwitch(
        int pageSize, int pageNumber)
    {
        // Arrange
        var expected = _helper.KeyboardSwitches.Where(c => c.Id > 1).Skip(pageSize * (pageNumber - 1))
            .Take(pageSize);
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
        const string changedName = "changedName";

        // Act
        var keyboardSwitches =
            await _repository.GetByConditionPagedAsync(c => c.Id == 1, pagingParams, true, CancellationToken.None);
        KeyboardSwitch keyboardSwitchToChange = keyboardSwitches.First(c => c.Id == 1);
        keyboardSwitchToChange.Name = changedName;

        // Assert
        Assert.That((await _context.KeyboardSwitches.FindAsync(1))?.Name, Is.EqualTo(changedName),
            "Changes has not been saved.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    public async Task GetByIdAsync_ExistingKeyboardSwitch_ReturnsKeyboardSwitch(int id)
    {
        // Arrange
        KeyboardSwitch expected = _helper.KeyboardSwitches.First(c => c.Id == id);

        // Act
        KeyboardSwitch actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected), "The actual keyboard switch is not equal to expected.");
    }

    [TestCase(0)]
    [TestCase(12)]
    public async Task GetByIdAsync_NotExistingKeyboardSwitch_ReturnsNull(int id)
    {
        // Arrange

        // Act
        KeyboardSwitch actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null object.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    [TestCase(5)]
    public async Task GetByIdAsync_ExistingKeyboardSwitchWithTracking_ReturnsKeyboardSwitchAndTracksChanges(int id)
    {
        // Arrange
        const string changedName = "changedName";

        // Act
        KeyboardSwitch keyboardSwitch = await _repository.GetByIdAsync(id, true, CancellationToken.None);
        keyboardSwitch.Name = changedName;

        // Assert
        Assert.That((await _context.KeyboardSwitches.FindAsync(id))?.Name, Is.EqualTo(changedName),
            "Changes has not been tracked.");
    }

    [Test]
    public Task Add_NewKeyboardSwitch_AddsKeyboardSwitchToContext()
    {
        // Arrange
        var newKeyboardSwitch = new KeyboardSwitch();

        // Act
        _repository.Add(newKeyboardSwitch);
        _context.SaveChanges();

        // Assert
        Assert.That(_context.KeyboardSwitches.Count(), Is.EqualTo(6),
            "The keyboard switch has not been added to context.");
        return Task.CompletedTask;
    }

    [Test]
    public Task Update_ExistingKeyboardSwitch_UpdatedKeyboardSwitch()
    {
        // Arrange
        KeyboardSwitch keyboardSwitchToUpdate = _helper.KeyboardSwitches.First(c => c.Id == 1);
        const string changedName = "changedName";
        keyboardSwitchToUpdate.Name = changedName;

        // Act
        _repository.Update(keyboardSwitchToUpdate);

        // Assert
        Assert.That(_context.KeyboardSwitches.Find(1)?.Name, Is.EqualTo(changedName),
            "The keyboard switch has not been updated.");
        return Task.CompletedTask;
    }

    [Test]
    public async Task Delete_ExistingKeyboardSwitch_DeletedKeyboardSwitchFromContext()
    {
        // Arrange
        KeyboardSwitch keyboardSwitchToDelete = _helper.KeyboardSwitches.First(c => c.Id == 1);

        // Act
        _repository.Delete(keyboardSwitchToDelete);
        await _context.SaveChangesAsync();

        // Assert
        Assert.That(await _context.KeyboardSwitches.FindAsync(1), Is.Null,
            "The keyboard switch has not been deleted.");
    }
}