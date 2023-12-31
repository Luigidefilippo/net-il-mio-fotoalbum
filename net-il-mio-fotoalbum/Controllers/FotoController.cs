﻿using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
		public IActionResult Modifica(int id)
		{
            Foto? fotoDaModificare = _myDb.Fotos.Where(f => f.Id == id).Include(f => f.Categories).FirstOrDefault();

            if (fotoDaModificare == null)
            {
                return NotFound($"Non è stato possibile modificare il post con :  {id} ");

            }
            else
            {

                List<Category> eventiNelDb = _myDb.Categories.ToList();
                List<SelectListItem> eventiSelezionati = new List<SelectListItem>();

                foreach (Category evento in eventiNelDb)
                {
                    eventiSelezionati.Add(new SelectListItem
                    {
                        Text = evento.Name,
                        Value = evento.Id.ToString(),
                        Selected = fotoDaModificare.Categories.Any(e => e.Id == evento.Id),
                    });
                }

                FotoFormModel model = new FotoFormModel
                {
                    Foto = fotoDaModificare,

                    Categories = eventiSelezionati
                };

                return View("Modifica", model);
            }
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Modifica(int id, FotoFormModel data)
        {

            if (!ModelState.IsValid)
            {


                List<Category> eventiNelDb = _myDb.Categories.ToList();
                List<SelectListItem> eventoSelezionato = new List<SelectListItem>();

                foreach (Category evento in eventiNelDb)
                {
                    eventoSelezionato.Add(new SelectListItem
                    {
                        Text = evento.Name,
                        Value = evento.Id.ToString(),
                    });
                }

                data.Categories = eventoSelezionato;

                return View("Modifica", data);
            }

            Foto? fotoDaModificare = _myDb.Fotos.Where(f => f.Id == id).Include(f => f.Categories).FirstOrDefault();

            if (fotoDaModificare != null)
            {
                fotoDaModificare.Title = data.Foto.Title;
                fotoDaModificare.Description = data.Foto.Description;
                fotoDaModificare.Pathimg = data.Foto.Pathimg;

                if (data.eventoIdSelezionato != null)
                {
                    foreach (string eventoSelezionato in data.eventoIdSelezionato)
                    {
                        int intEventoSelzionato = int.Parse(eventoSelezionato);

                        Category? eventoInDb = _myDb.Categories.Where(e => e.Id == intEventoSelzionato).FirstOrDefault();

                        if (eventoInDb != null)
                        {
                            fotoDaModificare.Categories.Add(eventoInDb);
                        }
                    }
                }

                if (data.ImageFormFile != null)
                {

                    MemoryStream stream = new MemoryStream();

                    data.ImageFormFile.CopyTo(stream);
                    fotoDaModificare.Imagefile = stream.ToArray();
                }

                _myDb.SaveChanges();

                return RedirectToAction("Index");

            }
            else
            {
                return NotFound("Post da Aggiornare non trovato");
            }
        
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

            this.SetImageFile(data);

            _myDb.Fotos.Add(data.Foto);
            _myDb.SaveChanges();

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "SUPERADMIN, ADMIN")]
        [HttpPost]
        public IActionResult Cancella(int id)
        {
            Foto? postDaCancellare = _myDb.Fotos.Where(f => f.Id == id).FirstOrDefault();

            if (postDaCancellare != null)
            {
                _myDb.Fotos.Remove(postDaCancellare);
                _myDb.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound("Nessuna Foto da cancellare");
            }
        }

        private void SetImageFile(FotoFormModel formData)
        {
            if (formData.ImageFormFile == null)
            {
                return;
            }

            MemoryStream stream = new MemoryStream();

            formData.ImageFormFile.CopyTo(stream);
            formData.Foto.Imagefile = stream.ToArray();


        }

    }
}
