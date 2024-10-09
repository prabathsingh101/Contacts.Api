using Contacts.Api.Models;

namespace Contacts.Api.Services
{
    public interface IContacts
    {
        Task<List<ContactModels>> GetAllAsync();

        Task<ContactModels?> GetByIdAsync(int id);

        Task<ContactModels> CreateAsync(ContactModels contact);

        Task<ContactModels?> UpdateAsync(int id, ContactModels contact);

        Task<ContactModels?> DeleteAsync(int id);
    }
}
