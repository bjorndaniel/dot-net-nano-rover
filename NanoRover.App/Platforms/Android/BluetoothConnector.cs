using Android.Bluetooth;
using Android.Runtime;

namespace NanoRover.App;
public partial class BluetoothConnector
{
    private const string SspUdid = "00001101-0000-1000-8000-00805f9b34fb";
    private BluetoothAdapter _adapter;
    private Callback _callback;

    public partial void Connect(string deviceName)
    {
        _adapter = BluetoothAdapter.DefaultAdapter;
        var device = _adapter.BondedDevices.FirstOrDefault(d => d.Name == deviceName);
        //device.ConnectGatt(App.Current.BindingContext, true, () =>
        //{

        //});
        _callback = new Callback();
        device.ConnectGatt(Android.App.Application.Context, true, _callback);
        //var _socket = device.CreateRfcommSocketToServiceRecord(UUID.FromString(SspUdid));
        //_socket.Connect();

    }

    public partial List<string> GetConnectedDevices()
    {
        _adapter = BluetoothAdapter.DefaultAdapter;
        if (_adapter == null)
        {
            throw new Exception("No Bluetooth adapter found.");
        }
        if (_adapter.IsEnabled)
        {
            if (_adapter.BondedDevices.Count > 0)
            {
                return _adapter.BondedDevices.Select(d => d.Name).ToList();
            }
        }
        else
        {
            Console.Write("Bluetooth is not enabled on device");
        }
        return new List<string>();
    }
}
public class Callback : BluetoothGattCallback
{
    public override void OnCharacteristicRead(BluetoothGatt gatt, BluetoothGattCharacteristic characteristic, [GeneratedEnum] GattStatus status)
    {
        _ = "";
    }
}
