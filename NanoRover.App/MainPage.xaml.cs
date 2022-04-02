using MQTTnet;
using MQTTnet.Client;
using System.Numerics;

namespace NanoRover.App;

public partial class MainPage : ContentPage
{
    private MqttClient _mqttClient;
    private MqttApplicationMessageBuilder _builder;
    private MqttClientOptions _mqttClientOptions;
    private readonly string _controller = "Xbox Wireless Controller";
    private (Vector3 speed, DateTime time) _lastSpeed;
    public MainPage()
    {
        InitializeComponent();
        var mqttFactory = new MqttFactory();
        _mqttClient = mqttFactory.CreateMqttClient();
        _builder = new MqttApplicationMessageBuilder();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        _mqttClientOptions = new MqttClientOptionsBuilder()
            .WithTcpServer("192.168.9.164")
            .Build();
        await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
        Accelerometer.Start(SensorSpeed.Game);
    }


    private void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
    {
        //var currentX = Math.Round(e.Reading.Acceleration.X, 1);
        //var currentY = Math.Round(e.Reading.Acceleration., 1);
        //var currentZ = Math.Round(e.Reading.Acceleration.X, 1);
        var current = new Vector3(
                (float)Math.Round(e.Reading.Acceleration.X, 1),
                (float)Math.Round(e.Reading.Acceleration.Y, 1),
                (float)Math.Round(e.Reading.Acceleration.Z, 1)
            );
        if (Math.Abs(current.Y - _lastSpeed.speed.Y) > 0.5 && DateTime.Now.Subtract(_lastSpeed.time).TotalMilliseconds > 1500)
        {

            if (current.Y > 0.25)
            {
                if (_lastSpeed.speed.Y <= -0.25)
                {
                    return;
                }
                BackClicked(sender, null);
                _lastSpeed.speed.Y = current.Y;
                _lastSpeed.time = DateTime.Now;
                Console.WriteLine($"X:{current.X} Y: {current.Y} Z: {current.Z} BACK");
            }
            else if (current.Y < -0.25)
            {
                if (_lastSpeed.speed.Y >= 0.25)
                {
                    return;
                }
                Console.WriteLine($"X:{current.X} Y: {current.Y} Z: {current.Z} FORWARD");
                ForwardClicked(sender, null);
                _lastSpeed.speed.Y = current.Y;
                _lastSpeed.time = DateTime.Now;
            }
            else
            {
                //if (_lastSpeed.speed.Y < 0.25 || _lastSpeed.speed.Y > -0.25)
                //{
                //    return;
                //}
                Console.WriteLine($"X:{current.X} Y: {current.Y} Z: {current.Z} STOP");
                StopClicked(sender, null);
                _lastSpeed.speed.Y = current.Y;
                _lastSpeed.time = DateTime.Now;
            }
        }

        //e.Reading.Acceleration.X;
        //e.Reading.Acceleration.Y;

    }

    private async void ForwardClicked(object sender, EventArgs e)
    {
        var message = _builder
            .WithTopic("rover/f")
            .WithPayload("1")
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
            .Build();
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }
        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }

    private async void StopClicked(object sender, EventArgs e)
    {
        var message = _builder
        .WithTopic("rover/s")
        .WithPayload("1")
        .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
        .Build();
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }
        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }

    private async void Button_Clicked_2(object sender, EventArgs e)
    {
        var message = _builder
    .WithTopic("rover/fl")
    .WithPayload("1")
    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
    .Build();
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }
        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }

    private async void Button_Clicked_3(object sender, EventArgs e)
    {
        var message = _builder
    .WithTopic("rover/fr")
    .WithPayload("1")
    .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
    .Build();
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }
        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }

    private async void BackClicked(object sender, EventArgs e)
    {
        var message = _builder
            .WithTopic("rover/b")
            .WithPayload("1")
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.ExactlyOnce)
            .Build();
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }
        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }
}
