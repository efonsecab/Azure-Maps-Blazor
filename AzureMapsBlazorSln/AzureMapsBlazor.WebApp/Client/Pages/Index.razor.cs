using AzureMapsBlazor.WebApp.Shared;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AzureMapsBlazor.WebApp.Client.Pages
{
    public partial class Index
    {
        [Inject]
        public HttpClient Http { get; set; }
        public string SubscriptionKey { get; set; }
        public GetFastestRouteModel FastestRoute { get; private set; }
        private bool CanRenderMap { get; set; } = false;

        [Parameter]
        public GeoCoordinates RouteStart { get; set; } = null;

        [Parameter]
        public GeoCoordinates RouteEnd { get; set; } = null;
        [Parameter]
        public GeoCoordinates[] PointsInRoute { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            var response = await Http.GetAsync("api/AzureMaps/GetSubscriptionKey", HttpCompletionOption.ResponseContentRead);
            if (response.IsSuccessStatusCode)
            {
                SubscriptionKey = await response.Content.ReadAsStringAsync();

                GetFastestRouteModel model = new GetFastestRouteModel()
                {
                    StartPoint = new GeoCoordinates()
                    {
                        Latitude = 9.9356284,
                        Longitude = -84.1483647
                    },
                    EndPoint = new GeoCoordinates()
                    {
                        Latitude = 9.9983731,
                        Longitude = -84.1306463
                    }
                };
                response = await Http.PostAsJsonAsync<GetFastestRouteModel>("api/AzureMaps/GetFastestRoute", model);
                if (response.IsSuccessStatusCode)
                {
                    this.FastestRoute = await response.Content.ReadFromJsonAsync<GetFastestRouteModel>();
                    this.RouteStart = this.FastestRoute.StartPoint;
                    this.RouteEnd = this.FastestRoute.EndPoint;
                    this.PointsInRoute = this.FastestRoute.WayPoints;
                    CanRenderMap = true;
                }
            }
        }
    }
}
