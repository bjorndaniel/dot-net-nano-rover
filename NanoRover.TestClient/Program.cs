using System.Text;

Console.WriteLine("Hello, World!");
var mqttFactory = new MqttFactory();

using var mqttClient = mqttFactory.CreateMqttClient();
var mqttClientOptions = new MqttClientOptionsBuilder()
    .WithTcpServer("192.168.131.189")
    .Build();

// Setup message handling before connecting so that queued messages
// are also handled properly. When there is no event handler attached all
// received messages get lost.
mqttClient.ApplicationMessageReceivedAsync += e =>
{
    Console.WriteLine("Received application message.");
    //e.DumpToConsole();
    Console.WriteLine(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

    return Task.CompletedTask;
};

await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
    .WithTopicFilter(f => { f.WithTopic("rover/speed"); })
    .Build();

await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);

Console.WriteLine("MQTT client subscribed to topic.");

Console.WriteLine("Press enter to exit.");
Console.ReadLine();
