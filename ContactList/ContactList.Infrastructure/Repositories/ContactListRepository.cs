using ContactList.Core.Exceptions;
using ContactList.Core.Models;
using ContactList.Core.Repositories;
using ContactList.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactList.Infrastructure.Repositories
{
    public class ContactListRepository : IContactListRepository
    {
        private readonly DatabaseContext _dbContext;

        public ContactListRepository(DatabaseContext dbContext)
        {
            _dbContext = dbContext 
                         ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<List<ContactListEntry>> GetAllAsync()
        {
            var contactListEntities = await _dbContext.ContactListEntry
                                                      .ToListAsync();

            return contactListEntities.Select(entity => entity.ToContactListEntry())
                                      .ToList();
        }

        public async Task<ContactListEntry> GetByIdAsync(int id)
        {
            var contactListEntity = await _dbContext.ContactListEntry
                                                    .FirstOrDefaultAsync(c => c.Id == id);

            return contactListEntity.ToContactListEntry();
        }

        public async Task<ContactListEntry> CreateAsync(ContactListEntry contactListEntry)
        {
            var contactListEntity = new Entities.ContactListEntity()
            {
                Name = contactListEntry.Name,
                ContactType = contactListEntry.ContactType,
                DateOfBirth = contactListEntry.DateOfBirth,
                Address = contactListEntry.Address,
                PhoneNumber = contactListEntry.PhoneNumber,
                Email = contactListEntry.Email
            };

            _dbContext.Add(contactListEntity);

            await _dbContext.SaveChangesAsync();

            return contactListEntity.ToContactListEntry();
        }

        public async Task<int> UpdateAsync(int id, ContactListEntry updatedContactListEntry)
        {
            var contactListEntity = await _dbContext.ContactListEntry
                                                    .FirstOrDefaultAsync(c => c.Id == id);

            if (contactListEntity is not null)
            {
                contactListEntity.Name = updatedContactListEntry.Name;
                contactListEntity.ContactType = updatedContactListEntry.ContactType;
                contactListEntity.DateOfBirth = updatedContactListEntry.DateOfBirth;
                contactListEntity.Address = updatedContactListEntry.Address;
                contactListEntity.PhoneNumber = updatedContactListEntry.PhoneNumber;
                contactListEntity.Email = updatedContactListEntry.Email;

                try
                {
                    _dbContext.Update(contactListEntity);

                    int affectedRows = await _dbContext.SaveChangesAsync();

                    return affectedRows;
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    contactListEntity = await _dbContext.ContactListEntry
                                                        .FirstOrDefaultAsync(c => c.Id == id);

                    if (contactListEntity is null)
                    {
                        throw new EntryDoesNotExistException();
                    }
                    else
                    {
                        throw new EntryUpdateErrorException("Unexpected error while updating contact list entry", ex);
                    }
                }

                
            }

            return 0;
        }

        public async Task<int> DeleteAsync(int id)
        {
            var contactListEntity = await _dbContext.ContactListEntry
                                                    .FirstOrDefaultAsync(c => c.Id == id);

            if (contactListEntity is not null)
            {
                _dbContext.Remove(contactListEntity);

                int affectedRows = await _dbContext.SaveChangesAsync();

                return affectedRows;
            }

            return 0;
        }
    }
}
