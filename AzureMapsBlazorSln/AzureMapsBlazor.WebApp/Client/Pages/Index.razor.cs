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
        public string FromLatitude { get; set; }
        public string FromLongitude { get; set; }
        public string ToLatitude { get; set; }
        public string ToLongitude { get; set; }

        public async Task Search()
        {
            var response = await Http.GetAsync("api/AzureMaps/GetSubscriptionKey", HttpCompletionOption.ResponseContentRead);
            if (response.IsSuccessStatusCode)
            {
                SubscriptionKey = await response.Content.ReadAsStringAsync();

                GetFastestRouteModel model = new GetFastestRouteModel()
                {
                    StartPoint = new GeoCoordinates()
                    {
                        Latitude = double.Parse(FromLatitude),
                        Longitude = double.Parse(FromLongitude)
                    },
                    EndPoint = new GeoCoordinates()
                    {
                        Latitude = double.Parse(ToLatitude),
                        Longitude = double.Parse(ToLongitude)
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
