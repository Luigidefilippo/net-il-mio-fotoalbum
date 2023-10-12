using Microsoft.AspNetCore.Mvc;
using net_il_mio_fotoalbum.Database;

namespace net_il_mio_fotoalbum.Controllers
{
    public class MessageController : Controller
    {
        private FotoContext _myDb;

        public MessageController(FotoContext db)
        {
            _myDb = db;
        }

        public IActionResult Index()
        {
            var messages = _myDb.Messages.ToArray();
            return View(messages);
        }

        public IActionResult Send()
        {
            return View();
        }
    }
}
