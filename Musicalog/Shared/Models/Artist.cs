using System;

namespace Musicalog.Shared.Models
{
    public class Artist
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string ImageSource => Image != null ? $"data:image/png;base64, {Convert.ToBase64String(Image)}" : string.Empty;
    }
}
