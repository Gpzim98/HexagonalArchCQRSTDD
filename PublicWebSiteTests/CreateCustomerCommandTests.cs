using Xunit;
using PublicWebSite;
using Moq;

namespace PublicWebSiteTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Should_Create_Customer()
        {
            var customerDto = new CustomerDTO()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test",
                Email = "test@test.com",
                DocumentId = new Domain.ValueObjects.CustomerDocument()
                {
                    IdNumber = "1234",
                    DocumentType = Domain.Enums.DocumentType.DriveLicence
                }
            };

            var createCustomerMock = new Mock<ICreateCustomer>();
            createCustomerMock.Setup(
                c => c.CreateCustomerAsync(It.IsAny<CustomerDTO>())).Returns(
                Task.FromResult(CustomerDTO.MapToDomain(customerDto)));

            var handler = new CreateCustomerCommandHandler(createCustomerMock.Object);
            var command = new CreateCustomerCommand();

            command.CustomerDTO = customerDto;

            var result = await handler.Handle(command, CancellationToken.None);

            Assert.True(result.Success);
            Assert.Equal("Customer Created Successfully", result.Message);
            Assert.Equal(1, result.Data.Id);
        }

        [Fact]
        public async Task Should_Return_INVALID_PERSON_ID_When_Document_Is_Not_Valid()
        {
            // Arrange
            var createCustomerMock = new Mock<ICreateCustomer>();

            var handler = new CreateCustomerCommandHandler(createCustomerMock.Object);
            var command = new CreateCustomerCommand();

            var customerDto = new CustomerDTO()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test",
                Email = "test@test.com",
                DocumentId = new Domain.ValueObjects.CustomerDocument()
                {
                    IdNumber = "1",
                    DocumentType = Domain.Enums.DocumentType.DriveLicence
                }
            };

            command.CustomerDTO = customerDto;

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("Invalid Customer Document", result.Message);
            Assert.Equal(ErrorCodes.INVALID_PERSON_ID, result.ErrorCode);
        }

        [Fact]
        public async Task Should_Return_MISSING_REQUIRED_INFORMATION_When_Customer_Info_Is_Not_Valid()
        {
            // Arrange
            var createCustomerMock = new Mock<ICreateCustomer>();

            var handler = new CreateCustomerCommandHandler(createCustomerMock.Object);
            var command = new CreateCustomerCommand();

            var customerDto = new CustomerDTO()
            {
                Id = 1,
                Name = "",
                Surname = "Test",
                Email = "test@test.com",
                DocumentId = new Domain.ValueObjects.CustomerDocument()
                {
                    IdNumber = "1234",
                    DocumentType = Domain.Enums.DocumentType.DriveLicence
                }
            };

            command.CustomerDTO = customerDto;

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("User is missing required information", result.Message);
            Assert.Equal(ErrorCodes.MISSING_REQUIRED_INFORMATION, result.ErrorCode);
        }

        [Fact]
        public async Task Should_Return_INVALID_EMAIL_When_Customer_Email_Is_Not_Valid()
        {
            // Arrange
            var createCustomerMock = new Mock<ICreateCustomer>();

            var handler = new CreateCustomerCommandHandler(createCustomerMock.Object);
            var command = new CreateCustomerCommand();

            var customerDto = new CustomerDTO()
            {
                Id = 1,
                Name = "Test",
                Surname = "Test",
                Email = "",
                DocumentId = new Domain.ValueObjects.CustomerDocument()
                {
                    IdNumber = "1234",
                    DocumentType = Domain.Enums.DocumentType.DriveLicence
                }
            };

            command.CustomerDTO = customerDto;

            //Act
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.Success);
            Assert.Equal("User email is not valid", result.Message);
            Assert.Equal(ErrorCodes.INVALID_EMAIL, result.ErrorCode);
        }
    }
}