using ContactList.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactList.Core.Services
{
    public interface IContactListService
    {
        Task<List<ContactListEntry>> GetAllAsync();

        Task<ContactListEntry> GetByIdAsync(int id);

        Task<ContactListEntry> CreateAsync(ContactListEntry contactListEntry);

        Task<bool> UpdateAsync(int id, ContactListEntry updatedContactListEntry);

        Task<bool> DeleteAsync(int id);
    }
}
