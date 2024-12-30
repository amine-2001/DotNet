using Microsoft.EntityFrameworkCore;
using sav.Models;
using System;

namespace sav.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Client.ToListAsync();
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _context.Client.FirstOrDefaultAsync(c => c.ClientId == clientId);
        }

        public async Task AddClientAsync(Client client)
        {
            await _context.Client.AddAsync(client);
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Client.Update(client);
        }

        public async Task DeleteClientAsync(int clientId)
        {
            var client = await GetClientByIdAsync(clientId);
            if (client != null)
            {
                _context.Client.Remove(client);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
