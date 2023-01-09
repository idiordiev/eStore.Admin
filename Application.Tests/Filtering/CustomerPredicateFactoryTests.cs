using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using eStore_Admin.Application.Filtering.Factories;
using eStore_Admin.Application.Filtering.Models;
using eStore_Admin.Application.Interfaces.Filtering;
using eStore_Admin.Domain.Entities;
using NUnit.Framework;
using Tests.Common;

namespace Application.Tests.Unit.Filtering
{
    [TestFixture]
    public class CustomerPredicateFactoryTests
    {
        private IPredicateFactory<Customer, CustomerFilterModel> _predicateFactory;
        private UnitTestHelper _helper;

        public CustomerPredicateFactoryTests()
        {
            _predicateFactory = new CustomerPredicateFactory();
            _helper = new UnitTestHelper();
        }

        [SetUp]
        public void SetUp()
        {
            _helper = new UnitTestHelper();
        }

        [Test]
        public void CreateExpression_ZeroParameters_ReturnsCorrectPredicate()
        {
            // Arrange
            var filter = new CustomerFilterModel();
            Expression<Func<Customer, bool>> expected = c => true;
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase(new [] { true })]
        [TestCase(new [] { false })]
        [TestCase(new [] { true, false })]
        public void CreateExpression_IsDeletedValues_ReturnsCorrectPredicate(ICollection<bool> values)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                IsDeletedValues = new List<bool>(values)
            };
            Expression<Func<Customer, bool>> expected = c => values.Contains(c.IsDeleted);
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }

        [TestCase("")]
        [TestCase("F")]
        [TestCase("T")]
        [TestCase("Fitr")]
        [TestCase("First")]
        [TestCase("First1")]
        [TestCase("First2")]
        [TestCase("First2     ")]
        [TestCase("    First2")]
        [TestCase("   First2  ")]
        [TestCase("fiRst1")]
        [TestCase("Firstqkjfsn")]
        public void CreateExpression_FirstName_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                FirstName = value
            };
            Expression<Func<Customer, bool>> expected = c => c.FirstName.Contains(value.Trim());
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase("")]
        [TestCase("L")]
        [TestCase("g")]
        [TestCase("Last")]
        [TestCase("LaSt")]
        [TestCase("Last1")]
        [TestCase("Last2")]
        [TestCase("Last2   ")]
        [TestCase("   Last2")]
        [TestCase("     Last2     ")]
        [TestCase("Lastsdaljshdlkja")]
        public void CreateExpression_LastName_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                LastName = value
            };
            Expression<Func<Customer, bool>> expected = c => c.LastName.Contains(value.Trim());
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase("")]
        [TestCase("e")]
        [TestCase("E")]
        [TestCase("J")]
        [TestCase("email")]
        [TestCase("EmaIl")]
        [TestCase("email1@mail.com")]
        [TestCase("email2@mail.com")]
        [TestCase("     email2@mail.com")]
        [TestCase("email2@mail.com      ")]
        [TestCase("     email2@mail.com     ")]
        [TestCase("email1@mail.comdasdbajkshgdj")]
        public void CreateExpression_Email_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                Email = value
            };
            Expression<Func<Customer, bool>> expected = c => c.Email.Contains(value.Trim());
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase("")]
        [TestCase("P")]
        [TestCase("t")]
        [TestCase("Phone")]
        [TestCase("phOnE")]
        [TestCase("PhoneNumber1")]
        [TestCase("PhoneNumber2")]
        [TestCase("     PhoneNumber2")]
        [TestCase("PhoneNumber2    ")]
        [TestCase("    PhoneNumber2   ")]
        [TestCase("PhoneNumber2falkjhfskl")]
        public void CreateExpression_PhoneNumber_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                PhoneNumber = value
            };
            Expression<Func<Customer, bool>> expected = c => c.PhoneNumber.Contains(value.Trim());
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase("")]
        [TestCase("C")]
        [TestCase("h")]
        [TestCase("Country")]
        [TestCase("cOuNtRy")]
        [TestCase("Country1")]
        [TestCase("Country2")]
        [TestCase("     Country2")]
        [TestCase("Country2      ")]
        [TestCase("    Country2    ")]
        [TestCase("Country3dkajbskjdhgasjd")]
        public void CreateExpression_Country_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                Country = value
            };
            Expression<Func<Customer, bool>> expected = c => c.Country.Contains(value.Trim());
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase("")]
        [TestCase("C")]
        [TestCase("V")]
        [TestCase("City")]
        [TestCase("cItY")]
        [TestCase("City1")]
        [TestCase("City2")]
        [TestCase("    City2")]
        [TestCase("City2     ")]
        [TestCase("    City2    ")]
        [TestCase("City123129873")]
        public void CreateExpression_City_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                City = value
            };
            Expression<Func<Customer, bool>> expected = c => c.City.Contains(value.Trim());
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase("")]
        [TestCase("Add")]
        [TestCase("hdf")]
        [TestCase("Address")]
        [TestCase("AddReSS")]
        [TestCase("Address1")]
        [TestCase("Address2")]
        [TestCase("Address2     ")]
        [TestCase("     Address2")]
        [TestCase("    Address2     ")]
        [TestCase("Address2fjashdkjfhgasd")]
        public void CreateExpression_Address_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                Address = value
            };
            Expression<Func<Customer, bool>> expected = c => c.Address.Contains(value.Trim());
            
            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
        
        [TestCase("")]
        [TestCase("p")]
        [TestCase("k")]
        [TestCase("Postal")]
        [TestCase("PoStAL")]
        [TestCase("Postal1")]
        [TestCase("Postal2")]
        [TestCase("     Postal2")]
        [TestCase("Postal2     ")]
        [TestCase("    Postal2   ")]
        [TestCase("Postal2FDanlksfjh")]
        public void CreateExpression_PostalCode_ReturnsCorrectPredicate(string value)
        {
            // Arrange
            var filter = new CustomerFilterModel
            {
                PostalCode = value
            };
            Expression<Func<Customer, bool>> expected = c => c.PostalCode.Contains(value.Trim());

            // Act
            var actual = _predicateFactory.CreateExpression(filter);

            // Assert
            var expectedList = _helper.Customers.Where(expected.Compile());
            var actualList = _helper.Customers.Where(actual.Compile());
            CollectionAssert.AreEquivalent(expectedList, actualList, "The actual predicate is not equal to expected.");
        }
    }
}