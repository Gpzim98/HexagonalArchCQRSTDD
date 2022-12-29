using Domain.Enums;

namespace Domain.ValueObjects
{
    public class CustomerDocument
    {
        public string IdNumber { get; set; }
        public DocumentType DocumentType { get; set; }
    }
}
