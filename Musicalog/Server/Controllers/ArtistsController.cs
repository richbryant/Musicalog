using Microsoft.AspNetCore.Mvc;
using Musicalog.Datalayer.Repos;
using Musicalog.Shared.Models;
using System.Threading.Tasks;

namespace Musicalog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistsRepository _repo;

        public ArtistsController(IArtistsRepository repo)
            => _repo = repo;

        // GET: api/<ArtistsController>
        [HttpGet]
        public async Task<IActionResult> Get()
            => await _repo.TryGetItemsAsync()
                        .ToActionResult();

        // GET api/<ArtistsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
            => await _repo.TryGetAsync(id)
                    .ToActionResult();

        // POST api/<ArtistsController>
        [HttpPost]
        public async Task<IActionResult> Post(Artist artist)
            => await _repo.TryInsertAsync(artist)
                .ToActionResult();

        // PUT api/<ArtistsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Artist artist)
            => await _repo.TryUpdateAsync(id, artist)
                .ToActionResult();

        // DELETE api/<ArtistsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
            => await _repo.TryDeleteAsync(id)
                .ToActionResult();
    }
}
