// Assurez-vous que ceci pointe vers votre DbContext
using sav.Models;
namespace sav.Repositories
{
    public class SparePartRepository : ISparePartRepository
    {
        readonly ApplicationDbContext _context;

        public SparePartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SparePart> GetAll()
        {
            return _context.SparePart.ToList();
        }

        public SparePart GetById(int id)
        {
            return _context.SparePart.FirstOrDefault(sp => sp.SparePartId == id);
        }

        public void Add(SparePart sparePart)
        {
            _context.SparePart.Add(sparePart);
        }

        public void Update(SparePart sparePart)
        {
            _context.SparePart.Update(sparePart);
        }

        public void Delete(int id)
        {
            var sparePart = GetById(id);
            if (sparePart != null)
            {
                _context.SparePart.Remove(sparePart);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
