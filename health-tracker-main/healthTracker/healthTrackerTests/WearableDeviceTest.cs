using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class WearableDeviceTest
{
    private const string ValidModel1 = "tesa1";
    private const string ValidModel2 = "tesa1";
    private const string ValidDeviceName1 = "test2";
    private const string ValidDeviceName2 = "test2";
    private const string EmptyDeviceName = "";
    private const string InvalidDeviceName = null;
    private const string InvalidModel = null;
    private const string EmptyModel = "";

    [Test]
    public void Should_Correctly_Create_WearableDevice()
    {
        var WearableDevice = new WearableDevice(ValidModel1, ValidDeviceName1);

        Assert.That(WearableDevice.WearableDeviceName, Is.EqualTo(ValidDeviceName1));
        Assert.That(WearableDevice.Model, Is.EqualTo(ValidModel1));
    }

    [Test]
    public void Should_Throw_Exception_Empty_Or_Null_WearableDeviceName()
    {
        Assert.Throws<ArgumentException>(() => new WearableDevice(ValidModel1, InvalidDeviceName));
        Assert.Throws<ArgumentException>(() => new WearableDevice(ValidModel1, EmptyDeviceName));
    }
    
    [Test]
    public void Should_Throw_Exception_Empty_Or_Null_MobileModelName()
    {
        Assert.Throws<ArgumentNullException>(() => new WearableDevice(EmptyModel, ValidDeviceName1));
        Assert.Throws<ArgumentNullException>(() => new WearableDevice(InvalidModel, ValidDeviceName2));
    }
    
    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var WearableDevice1 = new WearableDevice(ValidModel1, ValidDeviceName1);
        var WearableDevice2 = new WearableDevice(ValidModel2, ValidDeviceName2);
        var allWearableDevices = WearableDevice.GetAllInstances();

        Assert.Contains(WearableDevice1, allWearableDevices);
        Assert.Contains(WearableDevice2, allWearableDevices);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        WearableDevice.InitializeClass();
        WearableDevice.WriteClass();

        Assert.NotNull(WearableDevice.GetAllInstances());
    }
}