using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public CustomerDocument CustomerDocument { get; set; }

        private void ValidateState()
        {
            if (CustomerDocument == null ||
                string.IsNullOrEmpty(CustomerDocument.IdNumber) ||
                CustomerDocument.IdNumber.Length <= 3 ||
                CustomerDocument.DocumentType == 0)
            {
                throw new InvalidCustomerDocumentException();
            }

            if (string.IsNullOrEmpty(Name) ||
                string.IsNullOrEmpty(Surname))
            {
                throw new MissingRequiredInformationException();
            }

            if (string.IsNullOrEmpty(this.Email))
            {
                throw new InvalidEmailException("User email is invalid");
            }
        }
        public bool IsValid()
        {
            this.ValidateState();
            return true;
        }

    }
}