using nanoFramework.Hardware.Esp32;
using System.Device.Pwm;

namespace NanoRover.Car
{
    public class MotorControl
    {
        private const int _leftForwardPin = 19;
        private const int _leftBackwardPin = 18;
        private const int _rightForwardPin = 16;
        private const int _rightBackwardPin = 17;
        private static PwmChannel _leftForward;
        private static PwmChannel _leftBackward;
        private static PwmChannel _rightForward;
        private static PwmChannel _rightBackward;

        public MotorControl()
        {
            ConfigPins();
        }

        public void Forward(float speed)
        {
            _leftForward.DutyCycle = speed;
            _rightForward.DutyCycle = speed;
            _leftBackward.DutyCycle = 0;
            _rightBackward.DutyCycle = 0;
        }

        public void Backward(float speed)
        {
            _leftBackward.DutyCycle = speed;
            _rightBackward.DutyCycle = speed;
            _leftForward.DutyCycle = 0;
            _rightForward.DutyCycle = 0;
        }

        public void Stop()
        {
            _leftBackward.DutyCycle = 0;
            _rightBackward.DutyCycle = 0;
            _leftForward.DutyCycle = 0;
            _rightForward.DutyCycle = 0;
        }

        public void ForwardLeft(float speed)
        {
            _leftForward.DutyCycle = speed / 2;
            _rightForward.DutyCycle = speed;
            _leftBackward.DutyCycle = 0;
            _rightBackward.DutyCycle = 0;
        }

        public void ForwardRight(float speed)
        {
            _leftForward.DutyCycle = speed;
            _rightForward.DutyCycle = speed / 2;
            _leftBackward.DutyCycle = 0;
            _rightBackward.DutyCycle = 0;
        }

        public void BackwardLeft(float speed)
        {
            _leftForward.DutyCycle = 0;
            _rightForward.DutyCycle = 0;
            _leftBackward.DutyCycle = speed;
            _rightBackward.DutyCycle = speed / 2;
        }

        public void BackwardRight(float speed)
        {
            _leftForward.DutyCycle = 0;
            _rightForward.DutyCycle = 0;
            _leftBackward.DutyCycle = speed / 2;
            _rightBackward.DutyCycle = speed;
        }

        private void ConfigPins()
        {
            Configuration.SetPinFunction(_leftForwardPin, DeviceFunction.PWM1);
            Configuration.SetPinFunction(_leftBackwardPin, DeviceFunction.PWM2);
            Configuration.SetPinFunction(_rightForwardPin, DeviceFunction.PWM3);
            Configuration.SetPinFunction(_rightBackwardPin, DeviceFunction.PWM4);
            _leftForward = PwmChannel.CreateFromPin(_leftForwardPin, 40000, 0);
            _leftBackward = PwmChannel.CreateFromPin(_leftBackwardPin, 40000, 0);
            _rightForward = PwmChannel.CreateFromPin(_rightForwardPin, 40000, 0);
            _rightBackward = PwmChannel.CreateFromPin(_rightBackwardPin, 40000, 0);
            _leftForward.Start();
            _leftBackward.Start();
            _rightForward.Start();
            _rightBackward.Start();
        }
    }
}
