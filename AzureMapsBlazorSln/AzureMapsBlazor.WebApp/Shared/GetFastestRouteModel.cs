using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzureMapsBlazor.WebApp.Shared
{
    public class GetFastestRouteModel
    {
        public GeoCoordinates StartPoint { get; set; }
        public GeoCoordinates EndPoint { get; set; }

        public GeoCoordinates[] WayPoints { get; set; }
    }
}
