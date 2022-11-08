using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Application.Tests.Unit.EqualityComparers;
using AutoMapper;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Application.Interfaces.Persistence;
using eStore_Admin.Application.Requests.Customers.Queries.GetAllPaged;
using eStore_Admin.Application.Requests.Customers.Queries.GetByFilterPaged;
using eStore_Admin.Application.Requests.Customers.Queries.GetById;
using eStore_Admin.Application.Responses;
using eStore_Admin.Application.Utility;
using eStore_Admin.Domain.Entities;
using Moq;
using NUnit.Framework;
using Tests.Common;

namespace Application.Tests.Unit;

[TestFixture]
public class CustomerQueriesTests
{
    private Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IMapper> _mapperMock;
    private UnitTestHelper _helper;

    public CustomerQueriesTests()
    {
        _mapperMock = new Mock<IMapper>();
        _mapperMock.Setup(x => x.Map<IEnumerable<CustomerResponse>>(It.IsAny<IEnumerable<Customer>>())).Returns<IEnumerable<Customer>>(
            source =>
            {
                var responseList = new List<CustomerResponse>();
                foreach (var customer in source)
                {
                    var response = new CustomerResponse
                    {
                        Id = customer.Id,
                        IsDeleted = customer.IsDeleted,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                        Country = customer.Country,
                        City = customer.City,
                        Address = customer.Address,
                        PostalCode = customer.PostalCode
                    };
                    responseList.Add(response);
                }

                return responseList;
            });
        _mapperMock.Setup(x => x.Map<CustomerResponse>(It.IsAny<Customer>())).Returns<Customer>(source =>
        {
            if (source is null)
                return null;

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
        _helper = new UnitTestHelper();
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetAllCustomersPagedQuery_EmptyDb_ReturnsEmptyCollection(int pageSize, int pageNumber)
    {
        // Arrange
        _unitOfWorkMock
            .Setup(x => x.CustomerRepository.GetAllPagedAsync(It.IsAny<PagingParameters>(), false, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Customer>());
        var query = new GetAllCustomersPagedQuery { PagingParameters = new PagingParameters(pageSize, pageNumber) };
        var handler = new GetAllCustomersPagedQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        CollectionAssert.IsEmpty(result, "The result collection is not empty.");
        _unitOfWorkMock.Verify(
            x => x.CustomerRepository.GetAllPagedAsync(It.Is<PagingParameters>(p => p.PageSize == pageSize && p.PageNumber == pageNumber),
                false, It.IsAny<CancellationToken>()), Times.Once);
    }

    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetAllCustomersPagedQuery_NotEmptyDb_ReturnsCollectionOfCustomerResponse(int pageSize, int pageNumber)
    {
        // Arrange
        _unitOfWorkMock
            .Setup(x => x.CustomerRepository.GetAllPagedAsync(It.IsAny<PagingParameters>(), false, It.IsAny<CancellationToken>()))
            .ReturnsAsync((PagingParameters pagingParameters, bool _, CancellationToken _) => _helper.Customers
                .Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize).Take(pagingParameters.PageSize));
        var query = new GetAllCustomersPagedQuery { PagingParameters = new PagingParameters(pageSize, pageNumber) };
        var handler = new GetAllCustomersPagedQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var expected = _helper.Customers.Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(c => new CustomerResponse
        {
            Id = c.Id,
            IsDeleted = c.IsDeleted,
            FirstName = c.FirstName,
            LastName = c.LastName,
            Email = c.Email,
            PhoneNumber = c.PhoneNumber,
            Country = c.Country,
            City = c.City,
            Address = c.Address,
            PostalCode = c.PostalCode
        });

        // Act
        var actual = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new CustomerResponseEqualityComparer()),
            "The actual collection is not equal to expected.");
    }


    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(3, 1)]
    [TestCase(1, 2)]
    [TestCase(1, 3)]
    [TestCase(4, 1)]
    public async Task GetCustomersByFilterPagedQuery_NotEmptyDb_ReturnsCollectionOfCustomerResponses(int pageSize, int pageNumber)
    {
        // Arrange
        _unitOfWorkMock
            .Setup(x => x.CustomerRepository.GetByConditionPagedAsync(It.IsAny<Expression<Func<Customer, bool>>>(),
                It.IsAny<PagingParameters>(), false, It.IsAny<CancellationToken>())).ReturnsAsync(
                (Expression<Func<Customer, bool>> condition, PagingParameters pagingParameters, bool _, CancellationToken _) => _helper
                    .Customers.Where(condition.Compile()).Skip((pagingParameters.PageNumber - 1) * pagingParameters.PageSize)
                    .Take(pagingParameters.PageSize));
        var predicateFactoryMock = new Mock<IPredicateFactory<Customer, CustomerFilterModel>>();
        predicateFactoryMock.Setup(x => x.CreateExpression(It.IsAny<CustomerFilterModel>())).Returns(c => c.Id > 1);
        var query = new GetCustomerByFilterPagedQuery { PagingParameters = new PagingParameters(pageSize, pageNumber) };
        var handler = new GetCustomerByFilterPagedQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object, predicateFactoryMock.Object);
        var expected = _helper.Customers.Where(c => c.Id > 1).Skip((pageNumber - 1) * pageSize).Take(pageSize).Select(c =>
            new CustomerResponse
            {
                Id = c.Id,
                IsDeleted = c.IsDeleted,
                FirstName = c.FirstName,
                LastName = c.LastName,
                Email = c.Email,
                PhoneNumber = c.PhoneNumber,
                Country = c.Country,
                City = c.City,
                Address = c.Address,
                PostalCode = c.PostalCode
            });

        // Act
        var actual = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expected).Using(new CustomerResponseEqualityComparer()),
            "The actual collection is not equal to expected.");
    }

    [TestCase(1)]
    [TestCase(2)]
    public async Task GetCustomerByIdQuery_ExistingCustomer_ReturnsCustomerResponse(int customerId)
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>(), false, It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, bool _, CancellationToken _) => _helper.Customers.First(c => c.Id == id));
        var query = new GetCustomerByIdQuery(customerId);
        var handler = new GetCustomerByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);
        var expectedCustomer = _helper.Customers.First(c => c.Id == customerId);
        var expectedResponse = new CustomerResponse
        {
            Id = expectedCustomer.Id,
            IsDeleted = expectedCustomer.IsDeleted,
            FirstName = expectedCustomer.FirstName,
            LastName = expectedCustomer.LastName,
            Email = expectedCustomer.Email,
            PhoneNumber = expectedCustomer.PhoneNumber,
            Country = expectedCustomer.Country,
            City = expectedCustomer.City,
            Address = expectedCustomer.Address,
            PostalCode = expectedCustomer.PostalCode
        };

        // Act
        var actual = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.That(actual, Is.EqualTo(expectedResponse).Using(new CustomerResponseEqualityComparer()),
            "The actual response is not equal to expected.");
    }

    [Test]
    public async Task GetCustomerByIdQuery_NotExistingCustomer_ReturnsNull()
    {
        // Arrange
        _unitOfWorkMock.Setup(x => x.CustomerRepository.GetByIdAsync(It.IsAny<int>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer)null);
        var query = new GetCustomerByIdQuery(1);
        var handler = new GetCustomerByIdQueryHandler(_unitOfWorkMock.Object, _mapperMock.Object);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.IsNull(result, "Method returned not-null object.");
    }
}