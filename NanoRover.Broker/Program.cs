Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .Enrich.FromLogContext()
           .WriteTo.Console()
           .CreateLogger();

var mqttFactory = new MqttFactory();

// The port for the default endpoint is 1883.
// The default endpoint is NOT encrypted!
// Use the builder classes where possible.
var mqttServerOptions = new MqttServerOptionsBuilder()
    .WithDefaultEndpoint()
    .Build();

// The port can be changed using the following API (not used in this example).
new MqttServerOptionsBuilder()
    .WithDefaultEndpoint()
    .WithDefaultEndpointPort(1234)
    .Build();

using var mqttServer = mqttFactory.CreateMqttServer(mqttServerOptions);
await mqttServer.StartAsync();

Console.WriteLine("Press Enter to exit.");
Console.ReadLine();

// Stop and dispose the MQTT server if it is no longer needed!
await mqttServer.StopAsync();
