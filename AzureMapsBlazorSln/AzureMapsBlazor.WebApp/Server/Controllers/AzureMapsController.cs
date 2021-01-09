using AzureMapsBlazor.WebApp.Server.Configuration;
using AzureMapsBlazor.WebApp.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTI.Microservices.Library.Models.AzureMapsService.GetOptimizedRoute;
using PTI.Microservices.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureMapsBlazor.WebApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureMapsController : ControllerBase
    {
        private AzureMapsConfiguration azureMapsConfiguration;
        private AzureMapsService azureMapsService;

        public AzureMapsController(AzureMapsConfiguration azureMapsConfiguration,
            PTI.Microservices.Library.Services.AzureMapsService azureMapsService
            )
        {
            this.azureMapsConfiguration = azureMapsConfiguration;
            this.azureMapsService = azureMapsService;
        }

        [HttpGet("[action]")]
        public string GetSubscriptionKey()
        {
            return this.azureMapsConfiguration.SubscriptionKey;
        }

        [HttpPost("[action]")]
        public async Task<GetFastestRouteModel> GetFastestRoute(GetFastestRouteModel model)
        {
            var fastestRoute = await this.azureMapsService.GetOptimizedRouteAsync(
                startingPoint: new PTI.Microservices.Library.Models.Shared.GeoCoordinates()
                {
                    Latitude = model.StartPoint.Latitude,
                    Longitude = model.StartPoint.Longitude
                },
                finalPoint: new PTI.Microservices.Library.Models.Shared.GeoCoordinates()
                {
                    Latitude = model.EndPoint.Latitude,
                    Longitude = model.EndPoint.Longitude
                },
                pointsInRoute: null);
            GetFastestRouteModel result = new GetFastestRouteModel();
            var points = fastestRoute?.routes?.First()?.legs?.First().points;
            var startPoint = points?.First();
            var endPoint = points?.Last();
            var wayPoints = points?.Except(new Point[] { startPoint, endPoint });
            result.StartPoint = new GeoCoordinates()
            {
                Latitude = startPoint.latitude,
                Longitude = startPoint.longitude
            };
            result.EndPoint = new GeoCoordinates()
            {
                Latitude = endPoint.latitude,
                Longitude = endPoint.longitude
            };
            if (wayPoints?.Count() > 0)
            {
                result.WayPoints = wayPoints.Select(p => new GeoCoordinates 
                {
                    Latitude=p.latitude,
                    Longitude=p.longitude
                }).ToArray();
            }
            return result;
        }
    }
}
