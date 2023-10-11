using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers
{
	[Authorize]
	public class FotoController : Controller
	{
		private FotoContext _myDb;

		public FotoController(FotoContext db) 
		{
			_myDb = db;
		}
		public IActionResult Index()

		{
			List<Foto> foto = _myDb.Fotos.Include(foto => foto.Categories).ToList<Foto>();
			return View("Index", foto);
		}

		public IActionResult Dettagli(int id)
		{
			Foto? fotoTrovata = _myDb.Fotos.Where(foto => foto.Id == id).Include(foto => foto.Categories).FirstOrDefault();
			return View("Dettagli", fotoTrovata);
		}

		[Authorize(Roles = "SUPERADMIN , ADMIN")]
		public IActionResult Modifica()
		{
			return View("Modifica");
		}

		[Authorize(Roles = "SUPERADMIN, ADMIN")]
		[HttpGet]
		public IActionResult Creazione()
		{
            List<SelectListItem> eventiSelezionati = new List<SelectListItem>();
            List<Category> eventiNelDb = _myDb.Categories.ToList();

            foreach (Category evento in eventiNelDb)
            {
                eventiSelezionati.Add(new SelectListItem
                {
                    Text = evento.Name,
                    Value = evento.Id.ToString(),
                });
            }

            FotoFormModel model = new FotoFormModel
            {
                Foto = new Foto(),

                Categories = eventiSelezionati
            };
            return View("Creazione", model);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Creazione(FotoFormModel data)
        {
            if (!ModelState.IsValid)
            {

                List<SelectListItem> eventiSelezionati = new List<SelectListItem>();
                List<Category> eventiNelDb = _myDb.Categories.ToList();

                foreach (Category eventi in eventiNelDb)
                {
                    eventiSelezionati.Add(new SelectListItem
                    {
                        Text = eventi.Name,
                        Value = eventi.Id.ToString(),
                    });
                }

                data.Categories = eventiSelezionati;

                return View("Creazione", data);
            }

            data.Foto.Categories = new List<Category>();

            if (data.eventoIdSelezionato != null)
            {
                foreach (string eventoSelezionato in data.eventoIdSelezionato)
                {
                    int intEventoSelezionato = int.Parse(eventoSelezionato);

                    Category? eventoInDb = _myDb.Categories.Where(i => i.Id == intEventoSelezionato).FirstOrDefault();
                    if (eventoInDb != null)
                    {
                        data.Foto.Categories.Add(eventoInDb);
                    }
                }
            }

            _myDb.Fotos.Add(data.Foto);
            _myDb.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SUPERADMIN, ADMIN")]
        public IActionResult Cancella(int id)
        {
            return View("Index");
        }
    }
}
