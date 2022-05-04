using ContactList.Core.Models;
using ContactList.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactList.Core.Services
{
    public class ContactListService : IContactListService
    {
        private readonly IContactListRepository _repository;

        public ContactListService(IContactListRepository repository)
        {
            _repository = repository 
                          ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<ContactListEntry>> GetAllAsync()
        {
            List<ContactListEntry> contactListEntries = await _repository.GetAllAsync();
            return contactListEntries;
        }

        public async Task<ContactListEntry> GetByIdAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            ContactListEntry entry = await _repository.GetByIdAsync(id);

            return entry;
        }

        public async Task<ContactListEntry> CreateAsync(ContactListEntry contactListEntry)
        {
            if (contactListEntry is null)
            {
                throw new ArgumentNullException(nameof(contactListEntry));
            }

            if (contactListEntry.ContactType == Enums.ContactType.Company)
            {
                contactListEntry.DateOfBirth = null;
            }

            ContactListEntry newContactListEntry = await _repository.CreateAsync(contactListEntry);

            return newContactListEntry;
        }

        public async Task<bool> UpdateAsync(int id, ContactListEntry updatedContactListEntry)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            if (updatedContactListEntry is null)
            {
                throw new ArgumentNullException(nameof(updatedContactListEntry));
            }

            int affectedRows = await _repository.UpdateAsync(id, updatedContactListEntry);

            return affectedRows == 1;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id < 0)
            {
                throw new ArgumentException($"Parameter {nameof(id)} must have a valid positive value");
            }

            int affectedRows = await _repository.DeleteAsync(id);

            return affectedRows == 1;
        }
    }
}
