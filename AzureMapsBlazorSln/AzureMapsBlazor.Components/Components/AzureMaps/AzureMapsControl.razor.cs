using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Microsoft.JSInterop.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureMapsBlazor.Components.Components.AzureMaps
{
    public partial class AzureMapsControl
    {
        [Inject]
        public IJSRuntime JavascriptRuntime { get; set; }
        [Parameter]
        public string MapsControlId { get; set; }
        [Parameter]
        public string SubscriptionKey { get; set; }


        [Parameter]
        public GeoCoordinates RouteStart { get; set; } = null;

        [Parameter]
        public GeoCoordinates RouteEnd { get; set; } = null;
        [Parameter]
        public GeoCoordinates[] PointsInRoute { get; set; } = null;
        [Inject]
        public AzureMapsControlModule module { get; set; }

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
            await module.InitializeMap(mapControlId: MapsControlId, mapOptions: options); ;
        }

        public async Task RenderLines()
        {
            await module.RenderLines(routeStart:this.RouteStart, routeEnd: this.RouteEnd, pointsInRoute: this.PointsInRoute);
        }
    }
}
