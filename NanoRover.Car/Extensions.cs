namespace NanoRover.Car
{
    public static class Extensions
    {

        public static ControlEvent FromTopic(string topic)
        {
            return topic switch
            {
                "rover/f" => ControlEvent.Forward,
                "rover/b" => ControlEvent.Backward,
                "rover/br" => ControlEvent.BackwardRight,
                "rover/bl" => ControlEvent.BackwardLeft,
                "rover/fl" => ControlEvent.ForwardLeft,
                "rover/fr" => ControlEvent.ForwardRight,
                _ => ControlEvent.Stop,
            };
        }
    }
}
