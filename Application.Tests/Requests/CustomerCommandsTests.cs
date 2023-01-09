using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Tests.Unit.EqualityComparers;
using AutoMapper;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Interfaces.Services;
using eStore_Admin.Application.RequestDTOs;
using eStore_Admin.Application.Requests.Customers.Commands.Delete;
using eStore_Admin.Application.Requests.Customers.Commands.Edit;
using eStore_Admin.Application.Responses;
using eStore_Admin.Domain.Entities;
using Moq;
using NUnit.Framework;

namespace Application.Tests.Unit.Requests
{
    [TestFixture]
    public class CustomerCommandsTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ILoggingService> _loggerMock;
        private Mock<IMapper> _mapperMock;

        public CustomerCommandsTests()
        {
            _mapperMock = new Mock<IMapper>();
            _mapperMock.Setup(x => x.Map(It.IsAny<CustomerDto>(), It.IsAny<Customer>()))
                .Callback<CustomerDto, Customer>(
                    (source, destination) =>
                    {
                        destination.IsDeleted = source.IsDeleted;
                        destination.FirstName = source.FirstName;
                        destination.LastName = source.LastName;
                        destination.Email = source.Email;
                        destination.PhoneNumber = source.PhoneNumber;
                        destination.Country = source.Country;
                        destination.City = source.City;
                        destination.Address = source.Address;
                        destination.PostalCode = source.PostalCode;
                    });
            _mapperMock.Setup(x => x.Map<CustomerResponse>(It.IsAny<Customer>())).Returns<Customer>(source =>
            {
                var destination = new CustomerResponse
                {
                    Id = source.Id,
                    IsDeleted = source.IsDeleted,
                    FirstName = source.FirstName,
                    LastName = source.LastName,
                    Email = source.Email,
                    PhoneNumber = source.PhoneNumber,
                    Country = source.Country,
                    City = source.City,
                    Address = source.Address,
                    PostalCode = source.PostalCode
                };

                return destination;
            });
        }

        [SetUp]
        public void Setup()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILoggingService>();
        }

        [Test]
        public async Task DeleteCustomerCommand_ExistingCustomer_DeletesCustomerAndReturnsTrue()
        {
            // Arrange
            _unitOfWorkMock.Setup(x =>
                    x.CustomerRepository.GetByIdAsync(1, It.IsAny<bool>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new Customer { Id = 1 });
            var command = new DeleteCustomerCommand(1);
            var handler = new DeleteCustomerCommandHandler(_unitOfWorkMock.Object, _loggerMock.Object);

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

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
            var handler = new DeleteCustomerCommandHandler(_unitOfWorkMock.Object, _loggerMock.Object);

            // Act
            bool result = await handler.Handle(command, CancellationToken.None);

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
            var command = new EditCustomerCommand(1) { Customer = new CustomerDto { FirstName = newName } };
            var handler =
                new EditCustomerCommandHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);
            var expected = new CustomerResponse
            {
                Id = 1,
                FirstName = newName
            };

            // Act
            CustomerResponse result = await handler.Handle(command, CancellationToken.None);

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
            var command = new EditCustomerCommand(1) { Customer = new CustomerDto { FirstName = newName } };
            var handler =
                new EditCustomerCommandHandler(_mapperMock.Object, _unitOfWorkMock.Object, _loggerMock.Object);

            // Act

            // Assert
            Assert.ThrowsAsync<KeyNotFoundException>(
                async () => { await handler.Handle(command, CancellationToken.None); },
                "The method didn't throw KeyNotFoundException.");
            return Task.CompletedTask;
        }
    }
}