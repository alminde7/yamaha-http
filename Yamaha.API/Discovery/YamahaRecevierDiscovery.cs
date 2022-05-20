using Rssdp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Yamaha.API.Discovery
{
    public class YamahaRecevierDiscovery
    {
        public YamahaRecevierDiscovery()
        {

        }

        public async Task<List<SsdpDevice>> FindAvailableReceivers()
        {
            var availableDevices = new List<SsdpDevice>();    
            using (var deviceLocator = new SsdpDeviceLocator())
            {
                var foundDevices = await deviceLocator.SearchAsync("urn:schemas-upnp-org:device:MediaRenderer:1");
                //var foundDevices1 = await deviceLocator.SearchAsync();

                foreach (var foundDevice in foundDevices)
                {
                    try
                    {
                        // Can retrieve the full device description easily though.
                        var fullDevice = await foundDevice.GetDeviceInfo();
                        if (fullDevice.Manufacturer.Contains("Yamaha"))
                        {
                            availableDevices.Add(fullDevice);
                        }
                    }
                    catch
                    {
                        // Ignore
                        continue;
                    }
                }
            }
            return availableDevices;
        }
    }
}
