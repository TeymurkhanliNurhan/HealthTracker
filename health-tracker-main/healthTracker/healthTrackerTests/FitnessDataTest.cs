using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class FitnessDataTest
{
    private static readonly DateTime ValidDate1 = DateTime.Now.AddDays(-1);
    private static readonly DateTime ValidDate2 = DateTime.Now.AddDays(-2);
    private const int ValidStepCount1 = 10000;
    private const int ValidStepCount2 = 15000;
    private const int ValidCaloriesBurnt1 = 500;
    private const int ValidCaloriesBurnt2 = 700;
    private const float ValidDistance1 = 5.0f;
    private const float ValidDistance2 = 7.5f;
    private const int InvalidNegativeStepCount = -100;
    private const int InvalidNegativeCaloriesBurnt = -100;
    private const float InvalidNegativeDistance = -100f;
    
    private const string ValidName1 = "name1";
    private const string ValidUserName1 = "name1";
    private const string ValidPassword1 = "name1";
    private static readonly DateTime ValidDateOfBirth1 = DateTime.Now.AddYears(-30);
    private const float ValidWeight1 = 100f;
    private const float ValidHeight1 = 170f;
    private const string ValidModel1 = "tesa1";
    private const string ValidDeviceName1 = "test2";
    private static readonly MobileDevice MobileDevice = new MobileDevice(ValidModel1, ValidDeviceName1);
    private static readonly User CreationUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);

    private FitnessData ValidFitnessDataWithoutUser;
    private FitnessData ValidFitnessDataWithUser;
    private User InvalidUser;
    
    [SetUp]
    public void Setup()
    {
        ValidFitnessDataWithoutUser =  new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, ValidDistance1);
        ValidFitnessDataWithUser =  new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, ValidDistance1, CreationUser);
        InvalidUser = new User();
    }
    
    [Test]
    public void Should_Correctly_Create_FitnessData()
    {
        var fitnessData = new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, ValidDistance1);

        Assert.That(fitnessData.StepCount, Is.EqualTo(ValidStepCount1));
        Assert.That(fitnessData.CaloriesBurnt, Is.EqualTo(ValidCaloriesBurnt1));
        Assert.That(fitnessData.DistanceTravelled, Is.EqualTo(ValidDistance1));
    }
    
    [Test]
    public void Should_Correctly_Create_FitnessData_With_User()
    {
        var fitnessData = new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, ValidDistance1, CreationUser);

        Assert.That(fitnessData.StepCount, Is.EqualTo(ValidStepCount1));
        Assert.That(fitnessData.CaloriesBurnt, Is.EqualTo(ValidCaloriesBurnt1));
        Assert.That(fitnessData.DistanceTravelled, Is.EqualTo(ValidDistance1));
        Assert.That(fitnessData.GetUser(), Is.EqualTo(CreationUser));
    }
    
    [Test]
    public void Should_Correctly_Set_User()
    {
        ValidFitnessDataWithoutUser.SetUser(CreationUser);
        
        Assert.That(ValidFitnessDataWithoutUser.GetUser(), Is.EqualTo(CreationUser));
    }

    [Test] public void Should_Correctly_Remove_User()
    {
        ValidFitnessDataWithUser.RemoveUser();
        
        Assert.That(ValidFitnessDataWithoutUser.GetUser(), Is.EqualTo(null));
    }

    [Test]
    public void Should_Throw_Exception_Negative_StepCount()
    {
        Assert.Throws<ArgumentException>(() => new FitnessData(ValidDate1, InvalidNegativeStepCount, ValidCaloriesBurnt1, ValidDistance1));
    }

    [Test]
    public void Should_Throw_Exception_Negative_CaloriesBurnt()
    {
        Assert.Throws<ArgumentException>(() => new FitnessData(ValidDate1, ValidStepCount1, InvalidNegativeCaloriesBurnt, ValidDistance1));
    }

    [Test]
    public void Should_Throw_Exception_Negative_DistanceTravelled()
    {
        Assert.Throws<ArgumentException>(() => new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, InvalidNegativeDistance));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var fitnessData1 = new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, ValidDistance1);
        var fitnessData2 = new FitnessData(ValidDate2, ValidStepCount2, ValidCaloriesBurnt2, ValidDistance2);
        var allFitnessData = FitnessData.GetAllInstances();

        Assert.Contains(fitnessData1, allFitnessData);
        Assert.Contains(fitnessData2, allFitnessData);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        FitnessData.InitializeClass();
        FitnessData.WriteClass();

        Assert.NotNull(FitnessData.GetAllInstances());
    }
}