using Blazorise;
using Microsoft.AspNetCore.Components;
using Musicalog.Client.Services;
using Musicalog.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Label = Musicalog.Shared.Models.Label;

namespace Musicalog.Client.Pages
{
    public partial class AlbumDetails
    {
        [Parameter]
        public long Id { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public bool NewAlbum { get; set; }

        public Album Album { get; set; } = new Album();

        public List<Artist> Artists { get; set; } = new List<Artist>();
        public List<Label> Labels { get; set; } = new List<Label>();


        protected override async Task OnInitializedAsync()
        {
            Artists = await DataService.GetArtists();
            Labels = await DataService.GetLabels();
        }

        protected override async Task OnParametersSetAsync()
        {
            if (Id == 0)
            {
                NewAlbum = true;
            }
            if (!NewAlbum)
            {
                Console.WriteLine("Getting album");
                Album = await DataService.GetAlbum(Id);
                Console.WriteLine(Album.Name);
            }
            else
            {
                Console.WriteLine("NewAlbum");
            }
        }

        private Modal labelModal;
        private Modal artistModal;

        private Label newLabel = new Label();
        private Artist newArtist = new Artist();

        private async Task SubmitLabel()
        {
            var response = await DataService.PostLabel(newLabel);
            Console.WriteLine($"received response {response}");
            if (response == 0) return;
            Labels = await DataService.GetLabels();
            HideLabelModal();
            StateHasChanged();
        }

        private async Task SubmitArtist()
        {
            var response = await DataService.PostArtist(newArtist);
            Console.WriteLine($"received response {response}");
            if (response == 0) return;
            Artists = await DataService.GetArtists();
            HideArtistModal();
            StateHasChanged();
        }

        private void ShowLabelModal()
        {
            labelModal.Show();
        }

        private void HideLabelModal()
        {
            labelModal.Hide();
        }

        private void ShowArtistsModal()
        {
            artistModal.Show();
        }

        private void HideArtistModal()
        {
            artistModal.Hide();
        }

        private async Task ChangeArtistPicture(FileChangedEventArgs e)
        {
            var files = e.Files;
            var file = files[0];
            using var stream = new MemoryStream();
            await file.WriteToStreamAsync(stream);
            newArtist.Image = stream.ToArray();
            StateHasChanged();
        }

        private async Task ChangeAlbumPicture(FileChangedEventArgs e)
        {
            var files = e.Files;
            var file = files[0];
            using var stream = new MemoryStream();
            await file.WriteToStreamAsync(stream);
            Album.Image = stream.ToArray();
            StateHasChanged();
        }

        private async Task UpdateAlbum()
        {
            if (!NewAlbum)
            {
                var response = await DataService.UpdateAlbum(Album);
            }
            NavigationManager.NavigateTo("/");
        }

        private void CancelOut()
        {
            NavigationManager.NavigateTo("/");
        }

    }
}
