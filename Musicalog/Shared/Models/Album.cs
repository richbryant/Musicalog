using System;

namespace Musicalog.Shared.Models
{
    public class Album
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long ArtistId { get; set; }
        public long LabelId { get; set; }
        public byte[] Image { get; set; }
        public string ImageSource => Image != null ? $"data:image/png;base64, {Convert.ToBase64String(Image)}" : string.Empty;
        public virtual Artist Artist { get; set; } = new Artist();
        public virtual Label Label { get; set; } = new Label();
    }
}
