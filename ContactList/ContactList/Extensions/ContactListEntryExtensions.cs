using ContactList.Core.Models;
using ContactList.Models;
using System;

namespace ContactList.Extensions
{
    public static class ContactListEntryExtensions
    {
        public static ContactListEntryViewModel ToViewModel(
            this ContactListEntry contactListEntry)
        {
            if (contactListEntry is null)
            {
                throw new ArgumentNullException(nameof(contactListEntry));
            }

            return new ContactListEntryViewModel
            {
                Id = contactListEntry.Id,
                ContactType = contactListEntry.ContactType,
                Name = contactListEntry.Name,
                DateOfBirth = contactListEntry.DateOfBirth,
                Address = contactListEntry.Address,
                PhoneNumber = contactListEntry.PhoneNumber,
                Email = contactListEntry.Email
            };
        }

        public static ContactListEntry ToContactListEntry(
            this ContactListEntryViewModel viewModel)
        {
            if (viewModel is null)
            {
                throw new ArgumentNullException(nameof(viewModel));
            }

            return new ContactListEntry
            {
                Id = viewModel.Id,
                ContactType = viewModel.ContactType,
                Name = viewModel.Name,
                DateOfBirth = viewModel.DateOfBirth,
                Address = viewModel.Address,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email
            };
        }
    }
}
