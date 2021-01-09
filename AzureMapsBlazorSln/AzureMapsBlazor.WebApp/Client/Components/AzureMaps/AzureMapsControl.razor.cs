using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureMapsBlazor.WebApp.Client.Components.AzureMaps
{
    public partial class AzureMapsControl
    {
        [Inject]
        public IJSRuntime JavascriptRuntime { get; set; }
        [Parameter]
        public string MapsControlId { get; set; }
        [Parameter]
        public string SubscriptionKey { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
        }

        private GeoCoordinates RouteStart { get; set; } = new GeoCoordinates()
        {
            Latitude = 9.94552,
            Longitude = -84.11618
        };

        private GeoCoordinates RouteEnd { get; set; } = new GeoCoordinates()
        {
            Latitude = 9.94458,
            Longitude = -84.11675
        };

        private GeoCoordinates[] PointsInRoute { get; set; } = new GeoCoordinates[]
        {
            new GeoCoordinates()
            {
                Latitude=9.94501,
                Longitude=-84.11649
            },
            new GeoCoordinates()
            {
                Latitude=9.94478,
                Longitude=-84.11663
            }
        };
        public async Task InitializeMap()
        {
            AzureMapsControlConfiguration options = new AzureMapsControlConfiguration()
            {
                Center = RouteStart,
                Language = "en-US",
                Zoom = 12,
                AuthOptions = new AuthenticationOptions()
                {
                    AuthType = "subscriptionKey",
                    SubscriptionKey = this.SubscriptionKey
                }
            };
            await JavascriptRuntime.InvokeVoidAsync("InitMap", MapsControlId, options);
            this.StateHasChanged();
        }

        public async Task RenderLines()
        {
            await JavascriptRuntime.InvokeVoidAsync("RenderLine",
                RouteStart,
                RouteEnd,
                PointsInRoute
                );
            this.StateHasChanged();
        }
    }
}
