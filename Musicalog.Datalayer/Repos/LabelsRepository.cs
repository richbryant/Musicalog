using LanguageExt;
using LinqToDB;
using Musicalog.Shared.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace Musicalog.Datalayer.Repos
{
    public interface ILabelsRepository
    {
        TryAsync<List<Label>> TryGetAsync();
        TryOptionAsync<Label> TryGetAsync(long id);
        TryOptionAsync<int> TryInsertAsync(Label label);
        TryOptionAsync<int> TryUpdateAsync(long id, Label label);
        TryOptionAsync<int> TryDeleteAsync(long id);
    }


    public class LabelsRepository : ILabelsRepository
    {
        private readonly MusicalogData _data;

        public LabelsRepository(MusicalogData data)
            => _data = data;

        public TryAsync<List<Label>> TryGetAsync() => TryAsync(async () => await GetAsync());
        public TryOptionAsync<Label> TryGetAsync(long id) => TryOptionAsync(async () => await GetAsync(id));
        public TryOptionAsync<int> TryInsertAsync(Label label) => TryOptionAsync(async () => await InsertAsync(label));
        public TryOptionAsync<int> TryUpdateAsync(long id, Label label) => TryOptionAsync(async () => await UpdateAsync(id, label));
        public TryOptionAsync<int> TryDeleteAsync(long id) => TryOptionAsync(async () => await DeleteAsync(id));


        private async Task<List<Label>> GetAsync() => await _data.Labels.ToListAsync();

        private async Task<Label> GetAsync(long id) => await _data.Labels.FirstOrDefaultAsync(x => x.Id == id);

        private async Task<int> InsertAsync(Label label) => await _data.InsertAsync(label, "Labels");

        private async Task<int> UpdateAsync(long id, Label label) 
            => await _data.Labels
                .Where(x => x.Id == id)
                .Set(a => a.Name, label.Name)
                .UpdateAsync();

        private async Task<int> DeleteAsync(long id) 
            => await _data.Labels
                .Where(x => x.Id == id)
                .DeleteAsync();
    }
}
