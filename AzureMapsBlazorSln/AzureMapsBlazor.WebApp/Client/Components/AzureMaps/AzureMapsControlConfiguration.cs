using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AzureMapsBlazor.WebApp.Client.Components.AzureMaps
{
    public class GeoCoordinates
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public enum MapsLanguage
    {
        [Display(Name = "en-us", ShortName ="en-US")]
        English_US = 1,
        Spanish_SP = 2
    }

    public enum MapsAuthenticationType
    {
        SubscriptionKey = 1,
    }

    public class AuthenticationOptions
    {
        public string AuthType { get; set; } = "subscriptionKey";
        public string SubscriptionKey { get; set; }
    }
    public class AzureMapsControlConfiguration
    {
        public GeoCoordinates Center { get; set; }
        public int Zoom { get; set; }
        public string Language { get; set; } = "en-US";
        public AuthenticationOptions AuthOptions { get; set; }
    }
}
