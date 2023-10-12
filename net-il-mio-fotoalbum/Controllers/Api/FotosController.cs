using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using net_il_mio_fotoalbum.Database;
using net_il_mio_fotoalbum.Models;

namespace net_il_mio_fotoalbum.Controllers.Api
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]/[action]")]
    [ApiController]
    public class FotosController : ControllerBase
    {
        private FotoContext _myDb;

        public FotosController(FotoContext db)
        {
            _myDb = db;
        }

        [HttpGet]
        public IActionResult GetFoto(string? filter)
        {

            List<Foto> foto = _myDb.Fotos.Include(foto => foto.Categories).ToList();

            if (filter != null && filter != String.Empty)
            {
                foto = foto.FindAll(f => f.Title.ToLower().Contains(filter.ToLower()));
            }
            return Ok(foto);

        }
    }
}
