using Musicalog.Shared.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Musicalog.Client.Services
{
    public interface IDataService
    {
        [Get("/Albums/Count/")]
        Task<int> GetAlbumCount();

        [Headers("Content-Type: application/json")]
        [Get("/Albums/")]
        Task<List<Album>> GetAlbums([Query] FilterParameters filterParameters);

        [Get("/Albums/{id}")]
        Task<Album> GetAlbum(long id);

        [Put("/Albums")]
        Task<int> UpdateAlbum(Album album);

        [Get("/Artists")]
        Task<List<Artist>> GetArtists();

        [Post("/Artists")]
        Task<int> PostArtist(Artist artist);

        [Get("/Labels")]
        Task<List<Label>> GetLabels();

        [Post("/Labels")]
        Task<int> PostLabel(Label label);
    }
}
