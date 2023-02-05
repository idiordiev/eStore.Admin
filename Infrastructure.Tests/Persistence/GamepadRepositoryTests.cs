using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;
using eStore_Admin.Infrastructure.Persistence;
using eStore_Admin.Infrastructure.Persistence.Repositories;
using NUnit.Framework;
using Tests.Common;

namespace Infrastructure.Tests.Persistence;

[TestFixture]
public class GamepadRepositoryTests
{
    private UnitTestHelper _helper;
    private ApplicationContext _context;
    private IGamepadRepository _repository;

    [SetUp]
    public void Setup()
    {
        _helper = new UnitTestHelper();
        _context = _helper.GetApplicationContext();
        _repository = new GamepadRepository(_context);
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetAllPagedAsync_ValidPagingParams_ReturnsRequiredGamepads(int pageSize, int pageNumber)
    {
        // Arrange
        var expected = _helper.Gamepads.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
        var gamepads = await _repository.GetAllPagedAsync(pagingParams, true, CancellationToken.None);
        Gamepad gamepadToChange = gamepads.First(c => c.Id == 1);
        gamepadToChange.Name = changedName;

        // Assert
        Assert.That((await _context.Gamepads.FindAsync(1))?.Name, Is.EqualTo(changedName),
            "Changes has not been saved.");
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetByConditionPagedAsync_ValidPagingParamsAndCondition_ReturnsRequiredGamepad(int pageSize,
        int pageNumber)
    {
        // Arrange
        var expected = _helper.Gamepads.Where(c => c.Id > 1).Skip(pageSize * (pageNumber - 1)).Take(pageSize);
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
        var gamepads =
            await _repository.GetByConditionPagedAsync(c => c.Id == 1, pagingParams, true, CancellationToken.None);
        Gamepad gamepadToChange = gamepads.First(c => c.Id == 1);
        gamepadToChange.Name = changedName;

        // Assert
        Assert.That((await _context.Gamepads.FindAsync(1))?.Name, Is.EqualTo(changedName),
            "Changes has not been saved.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task GetByIdAsync_ExistingGamepad_ReturnsGamepad(int id)
    {
        // Arrange
        Gamepad expected = _helper.Gamepads.First(c => c.Id == id);

        // Act
        Gamepad actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected), "The actual gamepad is not equal to expected.");
    }

    [TestCase(0)]
    [TestCase(12)]
    public async Task GetByIdAsync_NotExistingGamepad_ReturnsNull(int id)
    {
        // Arrange

        // Act
        Gamepad actual = await _repository.GetByIdAsync(id, false, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.Null, "The method returned not-null object.");
    }

    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(4)]
    public async Task GetByIdAsync_ExistingGamepadWithTracking_ReturnsGamepadAndTracksChanges(int id)
    {
        // Arrange
        const string changedName = "changedName";

        // Act
        Gamepad gamepad = await _repository.GetByIdAsync(id, true, CancellationToken.None);
        gamepad.Name = changedName;

        // Assert
        Assert.That((await _context.Gamepads.FindAsync(id))?.Name, Is.EqualTo(changedName),
            "Changes has not been tracked.");
    }

    [Test]
    public Task Add_NewGamepad_AddsGamepadToContext()
    {
        // Arrange
        var newGamepad = new Gamepad();

        // Act
        _repository.Add(newGamepad);
        _context.SaveChanges();

        // Assert
        Assert.That(_context.Gamepads.Count(), Is.EqualTo(5), "The gamepad has not been added to context.");
        return Task.CompletedTask;
    }

    [Test]
    public Task Update_ExistingGamepad_UpdatedGamepad()
    {
        // Arrange
        Gamepad gamepadToUpdate = _helper.Gamepads.First(c => c.Id == 1);
        const string changedName = "changedName";
        gamepadToUpdate.Name = changedName;

        // Act
        _repository.Update(gamepadToUpdate);

        // Assert
        Assert.That(_context.Gamepads.Find(1)?.Name, Is.EqualTo(changedName), "The gamepad has not been updated.");
        return Task.CompletedTask;
    }

    [Test]
    public async Task Delete_ExistingGamepad_DeletedGamepadFromContext()
    {
        // Arrange
        Gamepad gamepadToDelete = _helper.Gamepads.First(c => c.Id == 1);

        // Act
        _repository.Delete(gamepadToDelete);
        await _context.SaveChangesAsync();

        // Assert
        Assert.That(await _context.Gamepads.FindAsync(1), Is.Null, "The gamepad has not been deleted.");
    }
}