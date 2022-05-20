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
        public Task<ReceiverStatus> GetBasicInfo([FromQuery]string zone = "Main_Zone")
        {
            return _yamahaProxy.GetBasicInfo(zone);
        }

        [HttpGet("config")]
        public Task<ReceiverInformation> GetConfig()
        {
            return _yamahaProxy.GetSystemConfig();
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

        /// <summary>
        /// Set volume to specified level
        /// </summary>
        /// <param name="level">Between -800 and 165</param>
        /// <param name="zone"></param>
        /// <returns></returns>
        [HttpPost("volume/{level}")]
        public Task SetVolume([FromRoute]int level, [FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.SetVolume(zone, level);
        }

        [HttpPost("volume/mute")]
        public Task Mute([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.Mute(zone);
        }

        [HttpPost("volume/unmute")]
        public Task Unmute([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.Unmute(zone);
        }

        [HttpPost("volume/adjustby/{level}")]
        public Task AdjustVolume([FromRoute] int level, [FromQuery] string zone = "Main_Zone")
        {
            return level >= 0 ? _yamahaProxy.TurnVolumeUpBy(zone, level) : _yamahaProxy.TurnVolumeDownBy(zone, level);
        }

        [HttpPost("play")]
        public Task Play([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.Play(zone);
        }

        [HttpPost("pause")]
        public Task Pause([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.Pause(zone);
        }

        [HttpPost("stop")]
        public Task Stop([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.Stop(zone);
        }

        [HttpPost("skip")]
        public Task Skip([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.Skip(zone);
        }

        [HttpPost("rewind")]
        public Task Rewind([FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.Rewind(zone);
        }

        [HttpPost("input/{input}")]
        public Task SetInput([FromRoute] string input, [FromQuery] string zone = "Main_Zone")
        {
            return _yamahaProxy.SetInput(zone, input);
        }
    }
}
