using sav.Models;

namespace sav.Repository
{
    public interface IClientRepository
    {
        Task<IEnumerable<Client>> GetAllClientsAsync(); 
        Task<Client> GetClientByIdAsync(int clientId);  
        Task AddClientAsync(Client client);            
        Task UpdateClientAsync(Client client);         
        Task DeleteClientAsync(int clientId);          
        Task<bool> SaveChangesAsync();                 
    }
}
