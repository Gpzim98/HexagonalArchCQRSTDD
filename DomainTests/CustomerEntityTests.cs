using Domain.Entities;
using Domain.ValueObjects;
using Domain.Enums;
using Xunit;
using Domain.Exceptions;
using System;

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
                CustomerDocument = new CustomerDocument()
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123ABC"
                },
                Email = "a@a.com",
            };

            Assert.True(customer.IsValid());
        }

        [Fact]
        public void ShouldThrowMissingRequiredInformationWhenNameIsNotProvided()
        {
            var customer = new Customer()
            {
                Name = "",
                Surname = "Test",
                CustomerDocument = new CustomerDocument()
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123ABC"
                },
                Email = "a@a.com",
            };
            Action act = () => customer.IsValid();

            var exception = Assert.Throws<MissingRequiredInformationException>(act);
        }

        [Fact]
        public void ShouldThrowMissingRequiredInformationWhenSurnameIsNotProvided()
        {
            var customer = new Customer()
            {
                Name = "Test",
                Surname = "",
                CustomerDocument = new CustomerDocument()
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123ABC"
                },
                Email = "a@a.com",
            };
            Action act = () => customer.IsValid();

            var exception = Assert.Throws<MissingRequiredInformationException>(act);
        }

        [Fact]
        public void ShouldThrowInvalidEmailExceptionWhenEmailIsInvalid()
        {
            var customer = new Customer()
            {
                Name = "Test",
                Surname = "Test",
                CustomerDocument = new CustomerDocument()
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123ABC"
                },
                Email = "",
            };
            Action act = () => customer.IsValid();

            var exception = Assert.Throws<InvalidEmailException>(act);
            Assert.Equal(exception.Message, "User email is invalid");
        }


        [Fact]
        public void ShouldThrowInvalidPersonDocumentIdExceptionWhenDocumentIsInvalid()
        {
            var customer = new Customer()
            {
                Name = "Test",
                Surname = "Test",
                CustomerDocument = new CustomerDocument()
                {
                    DocumentType = DocumentType.DriveLicence,
                    IdNumber = "123"
                },
                Email = "a@a.com",
            };
            Action act = () => customer.IsValid();

            var exception = Assert.Throws<InvalidCustomerDocumentException>(act);
        }

        [Fact]
        public void ShouldThrowInvalidPersonDocumentIdExceptionWhenDocumentTypeIsNotProvided()
        {
            var customer = new Customer()
            {
                Name = "Test",
                Surname = "Test",
                CustomerDocument = new CustomerDocument()
                {
                    IdNumber = "123ABC"
                },
                Email = "a@a.com",
            };
            Action act = () => customer.IsValid();

            var exception = Assert.Throws<InvalidCustomerDocumentException>(act);
        }
    }
}