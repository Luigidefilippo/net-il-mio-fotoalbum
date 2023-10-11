using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace net_il_mio_fotoalbum.Controllers
{
	[Authorize]
	public class FotoController : Controller
	{
		public IActionResult Index()

		{
			return View("Index");
		}

		public IActionResult Dettagli()
		{
			return View("Dettagli");
		}

		public IActionResult Modifica()
		{
			return View("Modifica");
		}

		public IActionResult Creazione()
		{
			return View("Creazione");
		}
	}
}
