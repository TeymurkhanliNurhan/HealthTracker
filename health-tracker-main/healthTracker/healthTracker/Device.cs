using System.Text.Json;
using System.Text.Json.Serialization;
using Health_tracker;

namespace Health_tracker;


public class Device
{
    private static int IDCounter;
    [JsonInclude]
    private List<User> _users = new List<User>();
    private static List<Device> _devices = new List<Device>();
    private String? version;
    private String? description;

    [JsonInclude]
    public int DeviceID { get; private set; }
    [JsonInclude]
    private string _model;
    [JsonInclude]
    private DeviceRoles _roles;
    [JsonInclude]
    private string _deviceName;

    [JsonIgnore]
    public string DeviceName
    {
        get => _deviceName;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Device name cannot be empty or null.");
            }
            _deviceName = value;
        }
    }

    [JsonIgnore]
    public string Model
    {
        get => _model;
        private set
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("Model cannot be null or empty.");
            }
            _model = value;
        }
    }

    public Device(string model, string deviceName, DeviceRoles roles, string? version, string? description)
    {
        DeviceID = IDCounter++;
        Model = model;
        DeviceName = deviceName;
        _roles = roles;

        AssignVersionAndDescription(roles, version, description);

        ValidateMandatoryAttributes();
        _devices.Add(this);
    }

    public Device(string model, string deviceName, DeviceRoles roles, User user, String version, String description)
        : this(model, deviceName, roles, version, description)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        AddUser(user);
    }

    public List<User> GetUsers() => new List<User>(_users);

    public void AddUser(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        if (!_users.Contains(user))
        {
            _users.Add(user);
            if (!user.GetDevices().Contains(this))
            {
                user.AddDevice(this);
            }
        }
    }

    public bool RemoveUser(User user)
    {
        if (_users.Remove(user))
        {
            if (user.GetDevices().Contains(this))
            {
                user.RemoveDevice(this);
            }
            return true;
        }
        return false;
    }
    private void EnsureRole(DeviceRoles role)
    {
        if (!_roles.HasFlag(role))
        {
            throw new InvalidOperationException($"Device does not have the required role: {role}");
        }
    }
    private void ValidateMandatoryAttributes()
    {
        if (_roles.HasFlag(DeviceRoles.Mobile) && string.IsNullOrEmpty(Model))
        {
            throw new ArgumentException("Mobile devices must have a valid model.");
        }

        if (_roles.HasFlag(DeviceRoles.Wearable))
        {
            Console.WriteLine($"Validating wearable device: {DeviceName}");
        }
    }
    private void AssignVersionAndDescription(DeviceRoles roles, string? version, string? descriptio)
    {
        if (roles.HasFlag(DeviceRoles.Mobile))
        {
            if (version == null)
            {
throw new ArgumentNullException(nameof(version));
            }
      this.version=version;
        }
        if (roles.HasFlag(DeviceRoles.Wearable))
        {
            if (descriptio == null)
            {
                throw new ArgumentNullException(nameof(descriptio));
            }
            this.description=descriptio;
        }
    }

}
