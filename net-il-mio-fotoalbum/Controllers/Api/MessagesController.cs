using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using net_il_mio_fotoalbum.Database;

namespace net_il_mio_fotoalbum.Controllers.Api
{
    [Route("api/usermessage")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly FotoContext _myDb;

        public MessagesController(FotoContext db)
        {
            _myDb = db;
        }

        [HttpPost]
        public IActionResult CreateMessage([FromBody]Message message)
        {
            _myDb.Add(message);

            _myDb.SaveChanges();

            return Ok(message);

        }
    }
}
