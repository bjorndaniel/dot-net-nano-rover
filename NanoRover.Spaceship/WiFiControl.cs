using nanoFramework.Networking;
using System;
using System.Device.WiFi;
using System.Threading;

namespace NanoRover.Spaceship
{
    public class WiFiControl
    {
        public bool Connect() //TODO: Add connection retries if disconnected
        {
            Console.WriteLine("Connecting to WIFI");
            CancellationTokenSource cs = new(120000);
            var success = WiFiNetworkHelper.ConnectDhcp("", "", WiFiReconnectionKind.Automatic, token: cs.Token);
            if (!success)
            {
                Console.WriteLine("Could not get ip address");
                Console.WriteLine($"ex: {WiFiNetworkHelper.Status}");

                if (WiFiNetworkHelper.HelperException != null)
                {
                    Console.WriteLine($"ex: {WiFiNetworkHelper.HelperException}");
                }
                return false;
            }
            Console.WriteLine("Connected to WIFI");
            return true;
        }
    }
}
