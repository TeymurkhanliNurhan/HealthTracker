using Health_tracker;

namespace health_tracker_tests;

[TestFixture]
public class UserTest
{
    private const string ValidName1 = "name1";
    private const string ValidName2 = "name2";
    private const string InvalidName = null;
    private const string EmptyName = "";
    private const string ValidUserName1 = "name1";
    private const string ValidUserName2 = "name2";
    private const string InvalidUserName = null;
    private const string EmptyUserName = "";
    private const string ValidPassword1 = "name1";
    private const string ValidPassword2 = "name2";
    private const string InvalidPassword = null;
    private const string EmptyUserPassword = "";
    private static readonly DateTime ValidDateOfBirth1 = DateTime.Now.AddYears(-30);
    private static readonly DateTime ValidDateOfBirth2 = DateTime.Now.AddYears(-25);
    private const float ValidWeight1 = 100f;
    private const float InvalidWeight = -100f;
    private const float ValidWeight2 = 70f;
    private const float ValidHeight1 = 170f;
    private const float ValidHeight2 = 180.2f;
    private const float InvalidHeight = -100f;

    private const string ValidModel1 = "tesa1";
    private const string ValidDeviceName1 = "test2";
    private static readonly DateTime ValidDate1 = DateTime.Now.AddDays(-1);
    private const int ValidStepCount1 = 10000;
    private const int ValidStepCount2 = 10000;
    private const int ValidCaloriesBurnt1 = 500;
    private const float ValidDistance1 = 5.0f;
    private static readonly List<Device> CreationDevicesList = new List<Device>()
    {
        new MobileDevice(ValidModel1, ValidDeviceName1)
    };
    private static readonly List<FitnessData> CreationFitnessDataList = new List<FitnessData>
    {
        new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, ValidDistance1)
    };
    private static readonly List<HealthMetric> CreationHealthMetricList = new List<HealthMetric>
    {
        new HealthMetric(120f, 80f)
    };

    private static readonly List<SleepTracker> CreationSleepTrackerList = new List<SleepTracker>
    {
        new SleepTracker(DateTime.Now, 7, Quality.High)
    };

    private static readonly List<Workout> CreationWorkoutList = new List<Workout>();
    private static readonly List<Meal> CreationMealList = new List<Meal>();
    private static readonly List<Achievement> creationAchievementList = new List<Achievement>();
    private static readonly Diet CreationDiet = new Diet(2);
    
    private FitnessData FitnessData;
    private FitnessData NewFitnessData;
    private FitnessData InvalidFitnessData;
    private List<FitnessData> FitnessDataList;
    private User ValidUserWithAssociations;
    private HealthMetric HealthMetric;
    private List<HealthMetric> HealthMetricList;
    private HealthMetric NewHealthMetric;
    private SleepTracker SleepTracker;
    private SleepTracker NewSleepTracker;
    private List<SleepTracker> SleepTrackerList;
    private Meal Meal;
    private Meal NewMeal;
    private List<Meal> MealList;

    [SetUp]
    public void Setup()
    {
        FitnessData =  new FitnessData(ValidDate1, ValidStepCount1, ValidCaloriesBurnt1, ValidDistance1);
        NewFitnessData = new FitnessData(ValidDate1, ValidStepCount2, ValidCaloriesBurnt1, ValidDistance1);
        FitnessDataList =  new List<FitnessData>
        {
            FitnessData
        };
        InvalidFitnessData = new FitnessData();

        HealthMetric = new HealthMetric(120f, 80f);
        NewHealthMetric = new HealthMetric(130f, 90f);
        HealthMetricList = new List<HealthMetric>
        {
            HealthMetric
        };
        SleepTracker = new SleepTracker(DateTime.Now, 7, Quality.High);
        NewSleepTracker = new SleepTracker(DateTime.Now.AddDays(-1), 8, Quality.Low);
        SleepTrackerList = new List<SleepTracker>
        {
            SleepTracker
        };

        ValidUserWithAssociations = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1, CreationDevicesList, FitnessDataList, CreationHealthMetricList, CreationSleepTrackerList, CreationMealList, creationAchievementList, CreationDiet, CreationWorkoutList);
    }

    [Test]
    public void Should_Correctly_Create_User()
    {
        var user = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);

        Assert.That(user.Username, Is.EqualTo(ValidUserName1));
        Assert.That(user.Weight, Is.EqualTo(ValidWeight1));
        Assert.That(user.Height, Is.EqualTo(ValidHeight1));
        Assert.That(user.Name, Is.EqualTo(ValidName1));
        Assert.That(user.Password, Is.EqualTo(ValidPassword1));
        Assert.That(user.DateOfBirth, Is.EqualTo(ValidDateOfBirth1));
    }
    [Test]
    public void Should_Correctly_Create_User_With_Associations()
    {
        var user = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1, CreationDevicesList, CreationFitnessDataList, CreationHealthMetricList, CreationSleepTrackerList, CreationMealList, creationAchievementList, CreationDiet, CreationWorkoutList);
        Assert.That(user.Username, Is.EqualTo(ValidUserName1));
        Assert.That(user.Weight, Is.EqualTo(ValidWeight1));
        Assert.That(user.Height, Is.EqualTo(ValidHeight1));
        Assert.That(user.Name, Is.EqualTo(ValidName1));
        Assert.That(user.Password, Is.EqualTo(ValidPassword1));
        Assert.That(user.DateOfBirth, Is.EqualTo(ValidDateOfBirth1));

        Assert.That(user.GetFitnessDatas(), Is.EqualTo(CreationFitnessDataList));
        Assert.That(user.GetHealthMetric(), Is.EqualTo(CreationHealthMetricList.FirstOrDefault()));
        Assert.IsNotNull(user.GetHealthMetric());
        Assert.True(user.GetHealthMetric() == CreationHealthMetricList.FirstOrDefault());
        Assert.True(user.GetFitnessDatas().Count == 1);
        Assert.That(user.GetSleepTrackers(), Is.EqualTo(CreationSleepTrackerList));
        Assert.IsNotNull(user.GetSleepTrackers().FirstOrDefault());
        Assert.True(user.GetSleepTrackers().FirstOrDefault() == CreationSleepTrackerList.FirstOrDefault());
        Assert.True(user.GetSleepTrackers().Count == 1);
    }
[Test]
public void Should_Add_SleepTracker_Successfully()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    validUser.AddSleepTracker(SleepTracker);
    validUser.AddSleepTracker(NewSleepTracker);
    Assert.That(validUser.GetSleepTrackers(), Has.Count.EqualTo(2));
    Assert.That(validUser.GetSleepTrackers(), Has.Member(SleepTracker));
    Assert.That(validUser.GetSleepTrackers(), Has.Member(NewSleepTracker));
    Assert.IsNotNull(NewSleepTracker.GetUser());
}

[Test]
public void Should_Add_SleepTracker_Unsuccessfully()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    validUser.AddSleepTracker(SleepTracker);
    Assert.That(validUser.GetSleepTrackers(), Has.Count.EqualTo(1));
    Assert.That(validUser.GetSleepTrackers(), Has.Member(SleepTracker));
    Assert.IsNotNull(SleepTracker.GetUser());
}

[Test]
public void Should_Remove_SleepTracker_Successfully()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    validUser.AddSleepTracker(SleepTracker);
    var result = validUser.RemoveSleepTracker(SleepTracker);
    Assert.That(result, Is.True);
    Assert.That(validUser.GetSleepTrackers(), Has.Count.EqualTo(0));
    Assert.IsNull(SleepTracker.GetUser());
}
[Test]
public void Should_Remove_SleepTracker_Unsuccessfully()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    validUser.AddSleepTracker(SleepTracker);
    var result = validUser.RemoveSleepTracker(NewSleepTracker);
    Assert.That(result, Is.False);
    Assert.That(validUser.GetSleepTrackers(), Has.Count.EqualTo(1));
    Assert.IsNotNull(SleepTracker.GetUser());
}
[Test]
public void Should_Edit_SleepTracker_Successfully()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    validUser.AddSleepTracker(SleepTracker);
    var result = validUser.EditSleepTracker(SleepTracker, NewSleepTracker);
    Assert.That(result, Is.True);
    Assert.That(validUser.GetSleepTrackers(), Has.Count.EqualTo(1));
    Assert.That(validUser.GetSleepTrackers(), Has.Member(NewSleepTracker));
    Assert.IsNull(SleepTracker.GetUser());
    Assert.IsNotNull(NewSleepTracker.GetUser());
}

[Test]
public void Should_Not_Edit_SleepTracker_When_Old_Tracker_Is_Null()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    Assert.Throws<ArgumentNullException>(() => validUser.EditSleepTracker(null, NewSleepTracker));
}

[Test]
public void Should_Not_Edit_SleepTracker_When_New_Tracker_Is_Null()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    Assert.Throws<ArgumentNullException>(() => validUser.EditSleepTracker(SleepTracker, null));
}
[Test]
public void Should_Add_HealthMetric_Successfully()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    validUser.AddHealthMetric(NewHealthMetric);
    Assert.That(validUser.GetHealthMetric(), Is.EqualTo(NewHealthMetric));
    Assert.IsNotNull(NewHealthMetric.GetUser());
}

[Test]
public void Should_Add_HealthMetric_Unsuccessfully()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    validUser.AddHealthMetric(HealthMetric);
    Assert.That(validUser.GetHealthMetric(), Is.EqualTo(HealthMetric));
    Assert.IsNotNull(HealthMetric.GetUser());
}



[Test]
public void Should_Remove_HealthMetric_Successfully()
{
    var removedHealthMetric = ValidUserWithAssociations.GetHealthMetric();
    var result = ValidUserWithAssociations.RemoveHealthMetric();
    Assert.That(result, Is.True);
    Assert.IsNull(ValidUserWithAssociations.GetHealthMetric());
    Assert.IsNull(removedHealthMetric.GetUser());
}

[Test]
public void Should_Remove_HealthMetric_Unsuccessfully_When_None_Exists()
{
    var result = new User().RemoveHealthMetric();
    Assert.That(result, Is.False);
    Assert.IsNull(new User().GetHealthMetric());
}

[Test]
public void Should_Edit_HealthMetric_Successfully()
{
    var newHealthMetric = new HealthMetric(120f, 80f);
    var result = ValidUserWithAssociations.EditHealthMetric(newHealthMetric);
    Assert.That(result, Is.True);
    Assert.That(ValidUserWithAssociations.GetHealthMetric(), Is.EqualTo(newHealthMetric));
    Assert.That(HealthMetric.GetUser(), Is.Null);
    Assert.IsNotNull(newHealthMetric.GetUser());
}

[Test]
public void Should_Not_Edit_HealthMetric_When_Existing_HealthMetric_Is_Null()
{
    var newHealthMetric = new HealthMetric(120f, 80f);
    var newUser = new User();
    Assert.Throws<InvalidOperationException>(() => newUser.EditHealthMetric(newHealthMetric));
}
[Test]
public void Should_Not_Edit_HealthMetric_When_New_HealthMetric_Is_Null()
{
    var validUser = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
    Assert.Throws<ArgumentNullException>(() => validUser.EditHealthMetric(null));
}
    [Test]
    public void Should_Add_FitnessData_Successfully()
    {
        ValidUserWithAssociations.AddFitnessData(NewFitnessData);

        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.Member(FitnessData));
        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.Member(NewFitnessData));

        Assert.IsNotNull(FitnessData.GetUser());
        Assert.IsNotNull(NewFitnessData.GetUser());
    }

    [Test]
    public void Should_Add_FitnessData_Unsuccessfully()
    {
        ValidUserWithAssociations.AddFitnessData(FitnessData);

        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.Count.EqualTo(1).And.Contain(FitnessData));

        Assert.IsNotNull(FitnessData.GetUser());
    }

    [Test]
    public void Should_Remove_FitnessData_Successfully()
    {
        var result = ValidUserWithAssociations.RemoveFitnessData(FitnessData);

        Assert.That(result, Is.True);
        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.No.Member(FitnessData));
        Assert.IsNull(FitnessData.GetUser());
    }

    [Test]
    public void Should_Remove_FitnessData_Unsuccessfully()
    {
        var result = ValidUserWithAssociations.RemoveFitnessData(InvalidFitnessData);

        Assert.That(result, Is.False);
        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.Member(FitnessData));
    }

    [Test]
    public void Should_Edit_FitnessData_Successfully()
    {
        var result = ValidUserWithAssociations.EditFitnessData(FitnessData, NewFitnessData);

        Assert.That(result, Is.True);
        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.No.Member(FitnessData));
        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.Member(NewFitnessData));
    }

    [Test]
    public void Should_Edit_FitnessData_Unsuccessfully()
    {
        var result = ValidUserWithAssociations.EditFitnessData(InvalidFitnessData, InvalidFitnessData);

        Assert.That(result, Is.False);
        Assert.That(ValidUserWithAssociations.GetFitnessDatas(), Has.Member(FitnessData));
    }


    [Test]
    public void Should_Throw_Exception_Null_Ot_Invalid_Username()
    {
        Assert.Throws<ArgumentException>(() => new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1,
            InvalidUserName, ValidPassword1));
        Assert.Throws<ArgumentException>(() => new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1,
            EmptyUserName, ValidPassword1));
    }

    [Test]
    public void Should_Throw_Exception_Null_Ot_Invalid_Name()
    {
        Assert.Throws<ArgumentException>(() => new User(InvalidName, ValidDateOfBirth1, ValidWeight1, ValidHeight1,
            ValidUserName1, ValidPassword1));
        Assert.Throws<ArgumentException>(() => new User(EmptyName, ValidDateOfBirth1, ValidWeight1, ValidHeight1,
            ValidUserName2, ValidPassword1));
    }


    [Test]
    public void Should_Throw_Exception_Null_Or_Invalid_Password()
    {
        Assert.Throws<ArgumentException>(() => new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1,
            ValidUserName1, InvalidPassword));
        Assert.Throws<ArgumentException>(() => new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1,
            ValidUserName2, EmptyUserPassword));
    }

    [Test]
    public void Should_Throw_Exception_Invalid_Weight()
    {
        Assert.Throws<ArgumentException>(() => new User(ValidName1, ValidDateOfBirth1, InvalidWeight, ValidHeight1,
            ValidUserName1, ValidPassword1));
    }

    [Test]
    public void Should_Throw_Exception_Invalid_Height()
    {
        Assert.Throws<ArgumentException>(() => new User(ValidName1, ValidDateOfBirth1, ValidWeight1, InvalidHeight,
            ValidUserName1, ValidPassword1));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var user1 = new User(ValidName1, ValidDateOfBirth1, ValidWeight1, ValidHeight1, ValidUserName1, ValidPassword1);
        var user2 = new User(ValidName2, ValidDateOfBirth2, ValidWeight2, ValidHeight2, ValidUserName2, ValidPassword2);
        var allUsers = User.GetAllInstances();

        Assert.Contains(user1, allUsers);
        Assert.Contains(user2, allUsers);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        User.InitializeClass();
        User.WriteClass();

        Assert.NotNull(User.GetAllInstances());
    }
    
    [Test]
    public void TRUE_TEST()
    {
        bool joke = true;
        Assert.That(joke, Is.True);
    }
}