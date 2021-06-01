using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;
using Musicalog.Shared.Models;

namespace Musicalog.Datalayer
{
    public class MusicalogData : DataConnection
    {
        private static MappingSchema mappingSchema = null;

        public MusicalogData(LinqToDbConnectionOptions<MusicalogData> options) : base(options)
        {
            if (mappingSchema != null)
            {
                return;
            }
            mappingSchema = Mappings.GetSchema();

        }

        public ITable<Album> Albums => GetTable<Album>();
        public ITable<Artist> Artists => GetTable<Artist>();
        public ITable<Label> Labels => GetTable<Label>();
    }
}
