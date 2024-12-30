using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using sav.Models;
using sav.ViewModels;


namespace sav.Controllers
{
    public class ClaimController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClaimController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Afficher le formulaire pour ajouter un Claim
        public IActionResult Create()
        {
            // Charger tous les articles disponibles
            ViewBag.Articles = new SelectList(_context.Article, "ArticleId", "Name");
            ViewBag.Clients = new SelectList(_context.Client, "ClientId", "Name");// Assumez que "Nom" est la désignation
            return View();
        }

        // Ajouter un nouveau Claim
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClaimViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Vérification de l'article
                    var article = await _context.Article.FindAsync(model.ArticleId);
                    if (article == null)
                    {
                        ModelState.AddModelError("ArticleId", "L'article sélectionné n'existe pas.");
                        return View(model);
                    }

                    // Créer et sauvegarder le Claim
                    var claim = new Claim
                    {
                        ClientId = model.ClientId,
                        ArticleId = model.ArticleId,
                        Description = model.Description,
                        Status = model.Status,
                        Date = DateTime.Now,
                        
                    };

                    _context.Claim.Add(claim);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Votre réclamation a été ajoutée avec succès.";
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de l'ajout du Claim : {ex.Message}");
                    ModelState.AddModelError("", "Une erreur s'est produite. Veuillez réessayer.");
                }
            }
            return View(model);
        }
        // Afficher la liste des réclamations
        public async Task<IActionResult> Index()
        {
            // Inclure les données des Clients et Articles associés pour les afficher
            var claims = await _context.Claim
                .Include(c => c.Client)
                .Include(c => c.Article)
                .ToListAsync();

            return View(claims);
        }

        // Modifier un Claim (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim.FindAsync(id);
            if (claim == null)
            {
                return NotFound();
            }

            // Charger les listes pour les articles et clients
            ViewBag.Articles = new SelectList(_context.Article, "ArticleId", "Name", claim.ArticleId);
            ViewBag.Clients = new SelectList(_context.Client, "ClientId", "Name", claim.ClientId);

            var model = new ClaimViewModel
            {
                
                ArticleId = claim.ArticleId,
                ClientId = claim.ClientId,
                Description = claim.Description,
                Status = claim.Status,
                
            };

            return View(model);
        }

        // Modifier un Claim (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ClaimViewModel model)
        {
            

            if (ModelState.IsValid)
            {
                try
                {
                    var claim = await _context.Claim.FindAsync(id);
                    if (claim == null)
                    {
                        return NotFound();
                    }

                    claim.ArticleId = model.ArticleId;
                    claim.ClientId = model.ClientId;
                    claim.Description = model.Description;
                    claim.Status = model.Status;

                    _context.Update(claim);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "Réclamation modifiée avec succès.";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur lors de la modification : {ex.Message}");
                    ModelState.AddModelError("", "Une erreur s'est produite. Veuillez réessayer.");
                }
            }

            // Recharger les listes en cas d'erreur
            ViewBag.Articles = new SelectList(_context.Article, "ArticleId", "Name", model.ArticleId);
            ViewBag.Clients = new SelectList(_context.Client, "ClientId", "Name", model.ClientId);
            return View(model);
        }

        // Supprimer un Claim (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var claim = await _context.Claim
                .Include(c => c.Client)
                .Include(c => c.Article)
                .FirstOrDefaultAsync(m => m.ClaimId == id);

            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }

        // Supprimer un Claim (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var claim = await _context.Claim.FindAsync(id);
            if (claim != null)
            {
                _context.Claim.Remove(claim);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Réclamation supprimée avec succès.";
            }

            return RedirectToAction(nameof(Index));
        }

    }
}
