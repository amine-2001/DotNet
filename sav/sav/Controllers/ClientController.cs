using Microsoft.AspNetCore.Mvc;
using sav.Models;
using sav.Repository;

namespace sav.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // GET: /Client
        // Liste de tous les clients
        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return View(clients);
        }

        // GET: /Client/Details/{id}
        // Affiche les détails d'un client
        public async Task<IActionResult> Details(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // GET: /Client/Create
        // Affiche le formulaire pour ajouter un client
        public IActionResult Create()
        {
            return View(new Client());
        }

        // POST: /Client/Create
        // Ajoute un nouveau client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client)
        {
            if (ModelState.IsValid)
            {
                await _clientRepository.AddClientAsync(client);
                await _clientRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: /Client/Edit/{id}
        // Affiche le formulaire pour modifier un client
        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: /Client/Edit/{id}
        // Met à jour les informations d'un client
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client)
        {
            if (id != client.ClientId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _clientRepository.UpdateClientAsync(client);
                await _clientRepository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(client);
        }

        // GET: /Client/Delete/{id}
        // Affiche la confirmation de suppression d'un client
        public async Task<IActionResult> Delete(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }

        // POST: /Client/Delete/{id}
        // Supprime un client
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
            await _clientRepository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
