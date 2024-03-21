using Domain.Entities;
using Domain.ValueObjects;

namespace PublicWebSite
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public CustomerDocument DocumentId { get; set; }

        public static Customer MapToDomain(CustomerDTO customer)
        {
            return new Customer()
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                CustomerDocument = customer.DocumentId
            };
        }

        public static CustomerDTO MapToDTO(Customer customer)
        {
            return new CustomerDTO()
            {
                Id = customer.Id,
                Name = customer.Name,
                Surname = customer.Surname,
                Email = customer.Email,
                DocumentId = new CustomerDocument()
                {
                    IdNumber = customer.CustomerDocument.IdNumber,
                    DocumentType = customer.CustomerDocument.DocumentType,
                }
            };
        }
    }

    public class UserDTO
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public List<string> Permissions { get; set; }
        public List<string> Roles { get; set; }
    }

    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
