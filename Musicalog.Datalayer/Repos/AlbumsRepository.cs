using LanguageExt;
using LinqToDB;
using Musicalog.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace Musicalog.Datalayer.Repos
{
    public interface IAlbumsRepository
    {
        TryOptionAsync<int> TryGetAsync();
        TryOptionAsync<Album> TryGetAsync(long id);
        TryAsync<List<Album>> TryGetAsync(FilterParameters p);
        TryAsync<Unit> TryInsertAsync(Album album);
        TryAsync<Unit> TryUpdateAsync(long id, Album album);
        TryAsync<Unit> TryDeleteAsync(long id);
    }

    public class AlbumsRepository : IAlbumsRepository
    {
        private readonly MusicalogData _data;

        public AlbumsRepository(MusicalogData data)
            => _data = data;

        public TryOptionAsync<int> TryGetAsync() => TryOptionAsync(async () => await GetAsync());

        public TryAsync<List<Album>> TryGetAsync(FilterParameters p) => TryAsync(async () => await GetAsync(p));

        public TryOptionAsync<Album> TryGetAsync(long id) => TryOptionAsync(async () => await GetAsync(id));

        public TryAsync<Unit> TryInsertAsync(Album album) => TryAsync(async () => await InsertAsync(album));

        public TryAsync<Unit> TryUpdateAsync(long id, Album album) => TryAsync(async () => await UpdateAsync(id, album));

        public TryAsync<Unit> TryDeleteAsync(long id) => TryAsync(async () => await DeleteAsync(id));

        private async Task<int> GetAsync() => await _data.Albums.CountAsync();

        private async Task<List<Album>> GetAsync(FilterParameters p)
        {  
            var list = _data.Albums
                    .LoadWith(x => x.Artist)
                    .LoadWith(x => x.Label)
                    .Skip((p.PageNumber - 1) * p.PageSize)
                    .Take(p.PageSize);
                    

            if(p.OrderByArtist) list = list.OrderBy(x => x.Artist.Name);
            if(p.OrderByLabel) list = list.OrderBy(x => x.Label.Name);
            //stockcount check here

            var result = await list.ToListAsync();

            return result;
        }

        private async Task<Album> GetAsync(long id)
            => await _data.Albums
                .LoadWith(x => x.Artist)
                .LoadWith(x => x.Label)
                .FirstOrDefaultAsync(x => x.Id == id);

        private async Task<Unit> InsertAsync(Album album)
        {
            await _data.InsertAsync(album, "Albums");
            return Unit.Default;
        }

        private async Task<Unit> UpdateAsync(long id, Album album)
        {
            await _data.Albums
                .Where(x => x.Id == id)
                .Set(a => a.Name, album.Name)
                .Set(a => a.ArtistId, album.ArtistId)
                .Set(a => a.LabelId, album.LabelId)
                .Set(a => a.Image, album.Image)
                .UpdateAsync();

            return Unit.Default;
        }

        private async Task<Unit> DeleteAsync(long id)
        {
            await _data.Albums
                .Where(x => x.Id == id)
                .DeleteAsync();

            return Unit.Default;
        }
    }
}
