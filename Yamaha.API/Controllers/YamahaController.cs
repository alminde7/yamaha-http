using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yamaha.API.Yamaha;

namespace Yamaha.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YamahaController : ControllerBase
    {
        private readonly YamahaProxy _yamahaProxy;

        public YamahaController(YamahaProxy yamahaProxy)
        {
            if (yamahaProxy is null)
            {
                throw new ArgumentNullException(nameof(yamahaProxy));
            }
            _yamahaProxy = yamahaProxy;
        }

        [HttpGet]
        public Task<BasicInfo> GetBasicInfo([FromQuery]string zone = "Main_Zone")
        {
            return _yamahaProxy.GetBasicInfo(zone);
        }

        [HttpPost("power/on")]
        public Task PowerOn([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.PowerOn(zone);
        }

        [HttpPost("power/off")]
        public Task PowerOff([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.PowerOff(zone);
        }
    }
}
