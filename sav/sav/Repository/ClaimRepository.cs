using Microsoft.EntityFrameworkCore;
using sav.Models;
using System;

namespace sav.Repository
{
    public class ClaimRepository : IClaimRepository
    {
        private readonly ApplicationDbContext _context;

        public ClaimRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Récupérer toutes les réclamations
        public async Task<IEnumerable<Claim>> GetAllClaimsAsync()
        {
            return await _context.Claim
                .Include(c => c.Client)
                .Include(c => c.Article)
                .ToListAsync();
        }

        // Récupérer une réclamation par son ID
        public async Task<Claim?> GetClaimByIdAsync(int id)
        {
            return await _context.Claim
                .Include(c => c.Client)
                .Include(c => c.Article)
                .FirstOrDefaultAsync(c => c.ClaimId == id);
        }

        // Ajouter une nouvelle réclamation
        public async Task AddClaimAsync(Claim claim)
        {
            await _context.Claim.AddAsync(claim);
        }

        // Mettre à jour une réclamation existante
        public async Task UpdateClaimAsync(Claim claim)
        {
            _context.Claim.Update(claim);
        }

        // Supprimer une réclamation par son ID
        public async Task DeleteClaimAsync(int id)
        {
            var claim = await GetClaimByIdAsync(id);
            if (claim != null)
            {
                _context.Claim.Remove(claim);
            }
        }

        // Enregistrer les modifications dans la base de données
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
