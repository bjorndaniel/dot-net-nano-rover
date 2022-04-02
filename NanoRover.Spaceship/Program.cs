using System;
using System.Device.Gpio;
using System.Threading;

namespace NanoRover.Spaceship
{
    public class Program
    {
        private static MotorControl _motorControl;
        private static WiFiControl _wifiControl;
        private static MqttControl _mqttControl;
        private static GpioController _gpioController;

        public static void Main()
        {
            _gpioController = new GpioController();
            _gpioController.OpenPin(2, PinMode.Output);
            _motorControl = new MotorControl();
            _wifiControl = new WiFiControl();
            _mqttControl = new MqttControl();
            if (_wifiControl.Connect())
            {
                Blink(2, 1000);
                if (_mqttControl.Connect())
                {
                    _mqttControl.MessageReceived += MessageReceived;
                    Blink(4, 1000);
                }
            }
            Thread.Sleep(Timeout.Infinite);
        }

        private static void Blink(int nrOfTimes, int intervalMS)
        {
            for (int i = 0; i < nrOfTimes; i++)
            {
                _gpioController.Write(2, PinValue.High);
                Thread.Sleep(intervalMS);
                _gpioController.Write(2, PinValue.Low);
                Thread.Sleep(intervalMS);
            }
        }

        private static void MessageReceived(object sender, EventArgs e)
        {
            var args = (ControlMessageArgs)e;
            if (args != null)
            {
                switch (args.Event)
                {
                    case ControlEvent.Backward:
                        _motorControl.Backward(args.Value);
                        break;
                    case ControlEvent.Forward:
                        _motorControl.Forward(args.Value);
                        break;
                    case ControlEvent.ForwardLeft:
                        _motorControl.ForwardLeft(args.Value);
                        break;
                    case ControlEvent.ForwardRight:
                        _motorControl.ForwardRight(args.Value);
                        break;
                    case ControlEvent.BackwardLeft:
                        _motorControl.BackwardLeft(args.Value);
                        break;
                    case ControlEvent.BackwardRight:
                        _motorControl.BackwardRight(args.Value);
                        break;
                    default:
                        _motorControl.Stop();
                        break;
                }
            }
        }
    }
}
