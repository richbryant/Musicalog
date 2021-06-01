using LinqToDB.Mapping;
using Musicalog.Shared.Models;

namespace Musicalog.Datalayer
{
    public static class Mappings
    {
        private static MappingSchema schema = null;

        public static MappingSchema GetSchema()
        {
            if (schema == null)
            {
                schema = MappingSchema.Default;
                var mapper = schema.GetFluentMappingBuilder();

                mapper.Entity<Album>()
                    .HasTableName("Albums")
                    .Property(x => x.Id)
                        .HasColumnName(nameof(Album.Id))
                        .IsIdentity()
                        .IsPrimaryKey()
                    .Property(x => x.Name)
                        .HasColumnName(nameof(Album.Name))
                    .Property(x => x.ArtistId)
                        .HasColumnName("Artist")
                    .Property(x => x.LabelId)
                        .HasColumnName("Label")
                    .Property(x => x.Image)
                        .HasColumnName(nameof(Album.Image))
                    .Property(x => x.ImageSource)
                        .SkipOnEntityFetch()
                        .HasSkipOnInsert()
                        .HasSkipOnUpdate()
                    .Property(x => x.Artist)
                        .HasAttribute(new AssociationAttribute { ThisKey = nameof(Album.ArtistId), OtherKey = nameof(Artist.Id) })
                    .Property(x => x.Label)
                        .HasAttribute(new AssociationAttribute { ThisKey = nameof(Album.LabelId), OtherKey = nameof(Label.Id) });


                mapper.Entity<Artist>()
                    .HasTableName("Artists")
                    .Property(x => x.Id)
                        .HasColumnName(nameof(Artist.Id))
                        .IsIdentity()
                        .IsPrimaryKey()
                    .Property(x => x.Name)
                        .HasColumnName(nameof(Artist.Name))
                    .Property(x => x.Image)
                        .HasColumnName(nameof(Artist.Image))
                        .Property(x => x.ImageSource)
                        .SkipOnEntityFetch()
                        .HasSkipOnInsert()
                        .HasSkipOnUpdate();

                mapper.Entity<Label>()
                    .HasTableName("Labels")
                    .Property(x => x.Id)
                        .HasColumnName(nameof(Label.Id))
                        .IsIdentity()
                        .IsPrimaryKey()
                    .Property(x => x.Name)
                        .HasColumnName(nameof(Label.Name));
            }

            return schema;
        }
    }
}
