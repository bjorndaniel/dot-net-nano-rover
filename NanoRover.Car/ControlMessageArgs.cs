using System;
namespace NanoRover.Car
{
    public class ControlMessageArgs : EventArgs
    {
        public ControlEvent Event { get; set; }

        public float Value { get; set; }

    }
}
