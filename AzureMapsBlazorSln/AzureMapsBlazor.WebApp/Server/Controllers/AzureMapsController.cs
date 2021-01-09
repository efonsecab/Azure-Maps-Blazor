using AzureMapsBlazor.WebApp.Server.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public AzureMapsController(AzureMapsConfiguration azureMapsConfiguration)
        {
            this.azureMapsConfiguration = azureMapsConfiguration;
        }

        [HttpGet("[action]")]
        public string GetSubscriptionKey()
        {
            return this.azureMapsConfiguration.SubscriptionKey;
        }
    }
}
