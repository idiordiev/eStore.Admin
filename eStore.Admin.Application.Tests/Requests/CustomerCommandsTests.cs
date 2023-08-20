using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using eStore.Admin.Application.Interfaces.Persistence;
using eStore.Admin.Application.Interfaces.Services;
using eStore.Admin.Application.RequestDTOs;
using eStore.Admin.Application.Requests.Customers.Commands;
using eStore.Admin.Application.Responses;
using eStore.Admin.Application.Tests.EqualityComparers;
using eStore.Admin.Domain.Entities;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace eStore.Admin.Application.Tests.Requests;

[TestFixture]
public class CustomerCommandsTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private IMapper _mapper;

    public CustomerCommandsTests()
    {
        _mapper = new Mapper(new MapperConfiguration(x =>
        {
            x.AddMaps(typeof(CustomerCommandsTests));
        }));
    }

    [SetUp]
    public void Setup()
    {
        _unitOfWorkMock = new Mock<IUnitOfWork>();
    }

    [Test]
    public async Task DeleteCustomerCommand_ExistingCustomer_DeletesCustomerAndReturnsTrue()
    {
        // Arrange
        _unitOfWorkMock.Setup(x =>
                x.CustomerRepository.GetByIdAsync(1, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Customer { Id = 1 });
        var command = new DeleteCustomerCommand(1);
        var handler = new DeleteCustomerCommandHandler(_unitOfWorkMock.Object,
            new Mock<ILogger<DeleteCustomerCommandHandler>>().Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.True, "The handler returned wrong value.");
        _unitOfWorkMock.Verify(x => x.CustomerRepository.Delete(It.Is<Customer>(c => c.Id == 1)), Times.Once);
        _unitOfWorkMock.Verify(x => x.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public async Task DeleteCustomerCommand_NotExistingCustomer_ReturnsFalse()
    {
        // Arrange
        _unitOfWorkMock.Setup(x =>
                x.CustomerRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer)null);
        var command = new DeleteCustomerCommand(1);
        var handler = new DeleteCustomerCommandHandler(_unitOfWorkMock.Object,
            new Mock<ILogger<DeleteCustomerCommandHandler>>().Object);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.False, "The handler returned wring value.");
    }

    [Test]
    public async Task EditCustomerCommand_ExistingCustomer_UpdatesCustomerAndSavesToContext()
    {
        // Arrange
        _unitOfWorkMock.Setup(x =>
                x.CustomerRepository.GetByIdAsync(1, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new Customer { Id = 1 });

        const string newName = "newName";
        var command = new EditCustomerCommand(1)
        {
            Customer = new CustomerDto
            {
                FirstName = newName
            }
        };
        var handler = new EditCustomerCommandHandler(_mapper, _unitOfWorkMock.Object,
            new Mock<ILogger<EditCustomerCommandHandler>>().Object);
        var expected = new CustomerResponse
        {
            Id = 1,
            FirstName = newName
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.That(result, Is.EqualTo(expected).Using(new CustomerResponseEqualityComparer()),
            "The actual response is not equal to expected.");
        _unitOfWorkMock.Verify(x => x.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Test]
    public Task EditCustomerCommand_NotExistingCustomer_ThrowsKeyNotFoundException()
    {
        // Arrange
        _unitOfWorkMock.Setup(x =>
                x.CustomerRepository.GetByIdAsync(1, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer)null);
        const string newName = "newName";
        var command = new EditCustomerCommand(1)
        {
            Customer = new CustomerDto
            {
                FirstName = newName
            }
        };
        var handler = new EditCustomerCommandHandler(_mapper, _unitOfWorkMock.Object,
            new Mock<ILogger<EditCustomerCommandHandler>>().Object);

        // Act

        // Assert
        Assert.ThrowsAsync<KeyNotFoundException>(
            async () => { await handler.Handle(command, CancellationToken.None); },
            "The method didn't throw KeyNotFoundException.");
        return Task.CompletedTask;
    }
}