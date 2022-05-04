using ContactList.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ContactList.Core.Repositories
{
    public interface IContactListRepository
    {
        Task<List<ContactListEntry>> GetAllAsync();

        Task<ContactListEntry> GetByIdAsync(int id);

        Task<ContactListEntry> CreateAsync(ContactListEntry contactListEntry);

        Task<int> UpdateAsync(int id, ContactListEntry updatedContactListEntry);

        Task<int> DeleteAsync(int id);
    }
}
