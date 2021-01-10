using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureMapsBlazor.Components
{
    public class AzureMapsControlModule : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public AzureMapsControlModule(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
               "import", "./_content/AzureMapsBlazor.Components/azureMapsInterop.js").AsTask());
        }
        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }

        internal async Task InitializeMap(string mapControlId, AzureMapsControlConfiguration mapOptions)
        {
            var module = await this.moduleTask.Value;
            await module.InvokeVoidAsync("InitMap", mapControlId, mapOptions);
        }

        internal async Task RenderLines(GeoCoordinates routeStart, GeoCoordinates routeEnd, GeoCoordinates[] pointsInRoute)
        {
            var module = await this.moduleTask.Value;
            await module.InvokeVoidAsync("RenderLine",
                routeStart,
                routeEnd,
                pointsInRoute
                );
        }
    }
}
