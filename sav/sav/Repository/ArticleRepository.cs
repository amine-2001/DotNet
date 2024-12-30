using Microsoft.EntityFrameworkCore;
using sav.Models;


namespace sav.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        readonly ApplicationDbContext _context;

        public ArticleRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Article>> GetAllArticlesAsync()
        {
            return await _context.Article.ToListAsync();
        }

        public async Task<Article?> GetArticleByIdAsync(int id)
        {
            return await _context.Article.FindAsync(id);
        }

        public async Task AddArticleAsync(Article article)
        {
            await _context.Article.AddAsync(article);
        }

        public async Task UpdateArticleAsync(Article article)
        {
            _context.Article.Update(article);
        }

        public async Task DeleteArticleAsync(int id)
        {
            var article = await GetArticleByIdAsync(id);
            if (article != null)
            {
                _context.Article.Remove(article);
            }
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
