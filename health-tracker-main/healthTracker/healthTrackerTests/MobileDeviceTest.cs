using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class MobileDeviceTest
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
    public void Should_Correctly_Create_MobileDevice()
    {
        var mobileDevice = new MobileDevice(ValidModel1, ValidDeviceName1);

        Assert.That(mobileDevice.MobileDeviceName, Is.EqualTo(ValidDeviceName1));
        Assert.That(mobileDevice.Model, Is.EqualTo(ValidModel1));
    }

    [Test]
    public void Should_Throw_Exception_Empty_Or_Null_MobileDeviceName()
    {
        Assert.Throws<ArgumentException>(() => new MobileDevice(ValidModel1, InvalidDeviceName));
        Assert.Throws<ArgumentException>(() => new MobileDevice(ValidModel1, EmptyDeviceName));
    }
    
    [Test]
    public void Should_Throw_Exception_Empty_Or_Null_MobileModelName()
    {
        Assert.Throws<ArgumentNullException>(() => new MobileDevice(EmptyModel, ValidDeviceName1));
        Assert.Throws<ArgumentNullException>(() => new MobileDevice(InvalidModel, ValidDeviceName2));
    }
    
    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var mobileDevice1 = new MobileDevice(ValidModel1, ValidDeviceName1);
        var mobileDevice2 = new MobileDevice(ValidModel2, ValidDeviceName2);
        var allMobileDevices = MobileDevice.GetAllInstances();

        Assert.Contains(mobileDevice1, allMobileDevices);
        Assert.Contains(mobileDevice2, allMobileDevices);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        MobileDevice.InitializeClass();
        MobileDevice.WriteClass();

        Assert.NotNull(MobileDevice.GetAllInstances());
    }
}