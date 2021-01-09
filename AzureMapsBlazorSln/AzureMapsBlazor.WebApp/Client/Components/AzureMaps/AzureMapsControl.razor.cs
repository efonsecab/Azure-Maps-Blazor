using AzureMapsBlazor.WebApp.Shared;
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

        [Parameter]
        public GeoCoordinates RouteStart { get; set; } = null;

        [Parameter]
        public GeoCoordinates RouteEnd { get; set; } = null;
        [Parameter]
        public GeoCoordinates[] PointsInRoute { get; set; } = null;
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
