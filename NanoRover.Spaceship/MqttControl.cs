using nanoFramework.M2Mqtt;
using nanoFramework.M2Mqtt.Messages;
using System;
using System.Text;

namespace NanoRover.Spaceship
{
    public class MqttControl
    {

        public MqttControl() { }

        public bool Connect()
        {
            Console.WriteLine("Connecting to message broker");
            MqttClient mqtt = new MqttClient("192.168.9.164");
            mqtt.ProtocolVersion = MqttProtocolVersion.Version_5;
            var ret = mqtt.Connect("Rover", true);
            if (ret != MqttReasonCode.Success)
            {
                Console.WriteLine($"ERROR connecting: {ret}");
                mqtt.Disconnect();
                return false;
            }
            mqtt.MqttMsgPublishReceived += MqttMsgPublishReceived;
            mqtt.Subscribe
                (
                    new[]
                    {
                        "rover/f",
                        "rover/b",
                        "rover/s",
                        "rover/fl",
                        "rover/fr",
                        "rover/br",
                        "rover/bl",
                    },
                    new MqttQoSLevel[]
                    {
                        MqttQoSLevel.AtMostOnce,
                        MqttQoSLevel.AtMostOnce,
                        MqttQoSLevel.AtMostOnce,
                        MqttQoSLevel.AtMostOnce,
                        MqttQoSLevel.AtMostOnce,
                        MqttQoSLevel.AtMostOnce,
                        MqttQoSLevel.AtMostOnce
                    }
            );
            Console.WriteLine("Connected to message broker");
            return true;
        }

        public event EventHandler MessageReceived;

        protected virtual void OnMessageReceived(EventArgs e)
        {
            var handler = MessageReceived;
            handler?.Invoke(this, e);
        }

        private void MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine($"Message received for {e.Topic}");
            var message = Encoding.UTF8.GetString(e.Message, 0, e.Message.Length);
            if (float.TryParse(message, out var value))
            {

                Console.WriteLine(message);
                OnMessageReceived(new ControlMessageArgs
                {
                    Event = Extensions.FromTopic(e.Topic),
                    Value = value
                });
            }
            else
            {
                Console.WriteLine($"Invalid message {message}");
            }

        }
    }
}
