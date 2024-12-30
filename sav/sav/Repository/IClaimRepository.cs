using sav.Models;

namespace sav.Repository
{
    public interface IClaimRepository
    {
        
        Task<IEnumerable<Claim>> GetAllClaimsAsync();

        
        Task<Claim?> GetClaimByIdAsync(int id);

        
        Task AddClaimAsync(Claim claim);

        
        Task UpdateClaimAsync(Claim claim);

        
        Task DeleteClaimAsync(int id);

        
        Task SaveChangesAsync();
    }
}
