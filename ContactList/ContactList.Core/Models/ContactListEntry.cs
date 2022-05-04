using ContactList.Core.Enums;
using System;

namespace ContactList.Core.Models
{
    public class ContactListEntry
    {
        public int Id { get; set; }

        public ContactType ContactType { get; set; }

        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }
    }
}
