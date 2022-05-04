using ContactList.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace ContactList.Models
{
    public class ContactListEntryViewModel
    {
        public int Id { get; set; }

        public ContactType ContactType { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime? DateOfBirth { get; set; }

        [MaxLength(500)]
        public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        [MaxLength(50)]
        public string PhoneNumber { get; set; }

        [DataType(DataType.EmailAddress)]
        [MaxLength(50)]
        public string Email { get; set; }
    }
}
