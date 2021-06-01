using Microsoft.AspNetCore.Components;
using Musicalog.Shared.Models;

namespace Musicalog.Client.Shared
{
    public partial class AlbumCard
    {
        [Parameter]
        public EventCallback<long> OpenDetails { get; set; }

        [Parameter]
        public Album Album { get; set; }
    }
}
