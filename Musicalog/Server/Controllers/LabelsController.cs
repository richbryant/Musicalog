using Microsoft.AspNetCore.Mvc;
using Musicalog.Datalayer.Repos;
using Musicalog.Shared.Models;
using System.Threading.Tasks;

namespace Musicalog.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LabelsController : ControllerBase
    {
        private readonly ILabelsRepository _repo;

        public LabelsController(ILabelsRepository repo)
            => _repo = repo;

        // GET: api/<LabelsController>
        [HttpGet]
        public async Task<IActionResult> Get()
            => await _repo.TryGetAsync()
                        .ToActionResult();

        // GET api/<LabelsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
            => await _repo.TryGetAsync(id)
                    .ToActionResult();

        // POST api/<LabelsController>
        [HttpPost]
        public async Task<IActionResult> Post(Label label)
            => await _repo.TryInsertAsync(label)
                .ToActionResult();

        // PUT api/<LabelsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, Label label)
            => await _repo.TryUpdateAsync(id, label)
                .ToActionResult();

        // DELETE api/<LabelsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
            => await _repo.TryDeleteAsync(id)
                .ToActionResult();
    }
}
