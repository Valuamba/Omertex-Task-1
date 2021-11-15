using BusManager.Application.Contracts;
using BusManager.Application.Contracts.Voyage;
using BusManager.Application.Services.Interfaces;
using BusManager.Presentation.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BusManager.Presentation.Pages.Voyage
{
    public partial class Voyages
    {
        public List<VoyageInfoRequest> AllVoyages { get; set; } = new();
        public MetaData MetaData { get; set; } = new();

        private VoyageParameters _voyageParameters = new();

        [Inject]
        public Services.IVoyageService VoyageSerivce { get; set; }

        [Inject]
        public IJSConsole JSConsole { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetVoyages();
        }

        public async Task SearchVoyage()
        {
            try
            {
                _voyageParameters.PageNumber = 1;
                await GetVoyages();
                this.StateHasChanged();
            }
            catch (Exception ex)
            {
                await JSConsole.LogAsync(ex.Message);
            }
        }

        public async Task Clear()
        {
            _voyageParameters.DepartureDate = null;
            _voyageParameters.From = null;
            _voyageParameters.To = null;
            _voyageParameters.VoyageName = null;
            await GetVoyages();
            this.StateHasChanged();
        }

        private async Task SelectedPage(int page)
        {
            _voyageParameters.PageNumber = page;
            await GetVoyages();
        }

        private async Task GetVoyages()
        {
            var pagingResponse = await VoyageSerivce.GetVoyages(_voyageParameters);
            AllVoyages = pagingResponse.Items;
            MetaData = pagingResponse.MetaData;
        }
    }
}
