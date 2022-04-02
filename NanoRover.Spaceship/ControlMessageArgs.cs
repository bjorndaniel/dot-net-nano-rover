using System;

namespace NanoRover.Spaceship
{
    public class ControlMessageArgs : EventArgs
    {
        public ControlEvent Event { get; set; }

        public float Value { get; set; }

    }
}
