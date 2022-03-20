using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;
using System;
using System.Text;

namespace NanoRover.Car
{
    public class MqttControl
    {
        public MqttControl()
        {

        }

        public void Connect()
        {
            Console.WriteLine("Connecting to message broker");
            MqttClient mqtt = new MqttClient("192.168.131.189");
            mqtt.ProtocolVersion = MqttProtocolVersion.Version_5;
            var ret = mqtt.Connect("nanoTestDevice", true);
            if (ret != MqttReasonCode.Success)
            {
                Console.WriteLine($"ERROR connecting: {ret}");
                mqtt.Disconnect();
                return;
            }
            mqtt.MqttMsgPublishReceived += MqttMsgPublishReceived;
            mqtt.Subscribe(new[] { "rover/speed" }, new MqttQoSLevel[] { MqttQoSLevel.AtLeastOnce });
            Console.WriteLine("Connected to message broker");
        }

        private void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine($"Message received for {e.Topic}");
            var message = Encoding.UTF8.GetString(e.Message, 0, e.Message.Length);
            Console.WriteLine(message);
        }
    }
}
