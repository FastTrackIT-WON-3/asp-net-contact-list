using ContactList.Core.Models;
using ContactList.Infrastructure.Entities;
using System;

namespace ContactList.Infrastructure.Extensions
{
    public static class ContactListEntityExtensions
    {
        public static ContactListEntry ToContactListEntry(this ContactListEntity entity)
        {
            if (entity is null)
            {
                return null;
            }

            return new ContactListEntry
            {
                Id = entity.Id,
                ContactType = entity.ContactType,
                Name = entity.Name,
                DateOfBirth = entity.DateOfBirth,
                Address = entity.Address,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email
            };
        }
    }
}
