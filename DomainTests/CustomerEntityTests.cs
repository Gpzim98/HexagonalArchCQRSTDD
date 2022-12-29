using System;
using Xunit;
using Domain.Exceptions;
using Domain;
using Domain.Entities;

namespace DomainTests
{
    public class CustomerEntityTests
    {
        [Fact]
        public void CustomerIsValid()
        {
            var customer = new Customer()
            {
                Name = "Test",
                Surname = "Test",
                CustomerDocument = new ValueObjects.CustomerDocument()
                {
                    DocumentType = Domain.Enums.DocumentType.DriveLicence,
                    IdNumber = "123ABC"
                },
                Email = "a@a.com",
            };

            Assert.True(customer.IsValid());
        }
    }
}