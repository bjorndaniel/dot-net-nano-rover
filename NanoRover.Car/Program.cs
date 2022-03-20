using System.Threading;

namespace NanoRover.Car
{
    public class Program
    {
        private static MotorControl _motorControl;
        private static WiFiControl _wifiControl;
        private static MqttControl _mqttControl;

        public static void Main()
        {
            _motorControl = new MotorControl();
            _wifiControl = new WiFiControl();
            _mqttControl = new MqttControl();
            if (_wifiControl.Connect())
            {
                _mqttControl.Connect();
            }
            //_motorControl.Forward(0.5f);
            //Thread.Sleep(1500);
            //_motorControl.ForwardLeft(0.5f);
            //Thread.Sleep(1500);
            //_motorControl.Stop();

            Thread.Sleep(Timeout.Infinite);
        }

    }
}