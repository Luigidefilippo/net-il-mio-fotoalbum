using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using net_il_mio_fotoalbum.Database;

namespace net_il_mio_fotoalbum.Controllers.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private FotoContext _myDb;

        public MessagesController(FotoContext db)
        {
            _myDb = db;
        }

        [HttpPost]
        public IActionResult CreateMessage(Message message)
        {
            _myDb.Add(message);

            _myDb.SaveChanges();

            return Ok(message);

        }
    }
}
