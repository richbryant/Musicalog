using Microsoft.AspNetCore.Mvc;
using Musicalog.Datalayer.Repos;
using Musicalog.Shared.Models;
using System.Threading.Tasks;

namespace Musicalog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumsRepository _repo;

        public AlbumsController(IAlbumsRepository repo)
            => _repo = repo;

        // GET /api/AlbumsController
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FilterParameters filter)
            => await _repo.TryGetAsync(filter)
                .ToActionResult();

        [HttpGet("Count")]
        public async Task<IActionResult> Get()
            => await _repo.TryGetAsync()
                        .ToActionResult();

        // GET api/<AlbumsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            HttpContext.Response.ContentType = "application/json";
            return await _repo.TryGetAsync(id)
                                .ToActionResult();
        }

        [HttpGet("{pagesize}/{pagenumber}")]
        public async Task<IActionResult> Get(int pagesize, int pagenumber)
            => await _repo.TryGetAsync(new FilterParameters { PageSize = pagesize, PageNumber = pagenumber })
                .ToActionResult();


        // POST api/<AlbumsController>
        [HttpPost]
        public async Task Post(Album album)
            => await _repo.TryInsertAsync(album)
                          .ToActionResult();


        // PUT api/<AlbumsController>/5
        [HttpPut("{id}")]
        public async Task Put(long id, Album album)
            => await _repo.TryUpdateAsync(id, album)
                          .ToActionResult();

        // DELETE api/<AlbumsController>/5
        [HttpDelete("{id}")]
        public async Task Delete(long id)
            => await _repo.TryDeleteAsync(id)
                          .ToActionResult();
    }
}
