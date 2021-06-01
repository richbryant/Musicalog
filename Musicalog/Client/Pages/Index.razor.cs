using Microsoft.AspNetCore.Components;
using Musicalog.Client.Services;
using Musicalog.Shared.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Musicalog.Client.Pages
{
    public partial class Index
    {
        private List<Album> Albums = new List<Album>();
        int pageSize = 10;

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IDataService DataService { get; set; }

        public FilterParameters Filter { get; set; } = new FilterParameters();

        public int PageCount => AlbumsCount / pageSize;

        public int PageNumber { get; set; }

        public bool OrderByArtist { get; set; }
        public bool OrderByLabel { get; set; }
        public bool OrderByStockCount { get; set; }

        public int AlbumsCount { get; set; }

        public void OpenAlbum(long id = 0)
        {
            NavigationManager.NavigateTo($"Album/{id}/");
        }

        public async Task SetPage(int pageNum)
        {
            PageNumber = pageNum;
            LoadFilter();
            await LoadAlbumsAsync();
        }

        public async Task LoadAlbumsAsync()
        {
            Console.WriteLine($"Filter new PageSize ={pageSize}");
            LoadFilter();
            Albums = await DataService.GetAlbums(Filter);
            Console.WriteLine($"Filter new PageSize ={pageSize}");
        }



        protected override async Task OnInitializedAsync()
        {
            AlbumsCount = await DataService.GetAlbumCount();
            LoadFilter();
            Albums = await DataService.GetAlbums(Filter);
        }

        private void LoadFilter()
        {
            Filter = new FilterParameters
            {
                MaxPageSize = AlbumsCount,
                PageNumber = PageNumber > 0 ? PageNumber : 1,
                PageSize = pageSize > 0 ? pageSize : 10,
                OrderByArtist = OrderByArtist,
                OrderByLabel = OrderByLabel
            };
            Console.WriteLine($"MaxPages = {Filter.MaxPages}");
        }
    }
}
