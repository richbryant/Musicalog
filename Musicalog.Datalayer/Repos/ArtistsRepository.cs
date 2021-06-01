using LanguageExt;
using LinqToDB;
using Musicalog.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace Musicalog.Datalayer.Repos
{
    public interface IArtistsRepository
    {
        TryOptionAsync<int>  TryDeleteAsync(long id);
        TryOptionAsync<Artist> TryGetAsync(long id);
        TryAsync<List<Artist>> TryGetItemsAsync();
        TryOptionAsync<int> TryInsertAsync(Artist artist);
        TryOptionAsync<int> TryUpdateAsync(long id, Artist artist);
    }

    public class ArtistsRepository : IArtistsRepository
    {
        private readonly MusicalogData _data;

        public ArtistsRepository(MusicalogData data)
            => _data = data;

        public TryOptionAsync<int> TryDeleteAsync(long id) => TryOptionAsync(async () => await DeleteAsync(id));

        public TryOptionAsync<Artist> TryGetAsync(long id) => TryOptionAsync(async () => await GetAsync(id));

        public TryAsync<List<Artist>> TryGetItemsAsync() => TryAsync(async () => await GetAllAsync());

        public TryOptionAsync<int> TryInsertAsync(Artist artist) => TryOptionAsync(async () => await InsertAsync(artist));

        public TryOptionAsync<int> TryUpdateAsync(long id, Artist artist) => TryOptionAsync(async () => await UpdateAsync(id, artist));


        private async Task<List<Artist>> GetAllAsync()
            => await _data.Artists.ToListAsync();

        private async Task<Artist> GetAsync(long id)
            => await _data.Artists.FirstOrDefaultAsync(x => x.Id == id);

        private async Task<int> InsertAsync(Artist artist) 
            => await _data.InsertAsync(artist, "Artists");

        private async Task<int> UpdateAsync(long id, Artist artist) 
            => await _data.Artists
                .Where(x => x.Id == id)
                .Set(a => a.Name, artist.Name)
                .Set(a => a.Image, artist.Image)
                .UpdateAsync();

        private async Task<int> DeleteAsync(long id) 
            => await _data.Artists
                .Where(x => x.Id == id)
                .DeleteAsync();
    }
}
