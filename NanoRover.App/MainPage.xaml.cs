using MQTTnet;
using MQTTnet.Client;

namespace NanoRover.App;

public partial class MainPage : ContentPage
{
    private MqttClient _mqttClient;
    private MqttApplicationMessageBuilder _builder;
    private MqttClientOptions _mqttClientOptions;
    private readonly string _controller = "Xbox Wireless Controller";

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
            .WithTcpServer("192.168.131.189")
            .Build();
        await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        //var connector = new BluetoothConnector();//DependencyService.Get<BluetoothConnector>();
        //var connectedDevices = connector.GetConnectedDevices();
        //foreach (var c in connectedDevices)
        //{
        //    var x = c;
        //    _ = "";
        //}
        //connector.Connect(_controller);

    }


    private async void Button_Clicked(object sender, EventArgs e)
    {
        var message = _builder
            .WithTopic("rover/f")
            .WithPayload("1")
            .Build();
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }
        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        var message = _builder
        .WithTopic("rover/s")
        .WithPayload("1")
        .Build();
        if (!_mqttClient.IsConnected)
        {
            await _mqttClient.ConnectAsync(_mqttClientOptions, CancellationToken.None);
        }
        await _mqttClient.PublishAsync(message, CancellationToken.None);
    }
}
