using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureMapsBlazor.WebApp.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public HttpClient Http { get; set; }
        public string SubscriptionKey { get; set; }
        private bool CanRenderMap { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            var response = await Http.GetAsync("api/AzureMaps/GetSubscriptionKey", HttpCompletionOption.ResponseContentRead);
            if (response.IsSuccessStatusCode)
            {
                SubscriptionKey = await response.Content.ReadAsStringAsync();
                CanRenderMap = true;
            }
        }
    }
}
