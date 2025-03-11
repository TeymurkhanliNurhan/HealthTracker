using Health_tracker;
namespace health_tracker_tests;

[TestFixture]
public class AchievementTest
{
    private const string ValidTitle1 = "test1";
    private const string ValidTitle2 = "test2";
    private const string ValidDescription1 = "description1";
    private const string ValidDescription2 = "description2";
    private const string InvalidTitle = null;
    private const string EmptyTitle = "";
    private const string InvalidDescription = null;
    private const string EmptyDescription = "";
    private User ValidUser1;
    private User ValidUser2;
    private List<User> ValidUsers;

    private static readonly DateTime FutureDate = DateTime.Now.AddDays(1);

    [SetUp]
    public void SetUp()
    {
        ValidUser1 = new User("user1", new DateTime(), 20, 20, "username1", "password1");
        ValidUser2 = new User("user2", new DateTime(), 10, 10, "username2", "password2");
        ValidUsers = new List<User> { ValidUser1, ValidUser2 };
    }

    [Test]
    public void Should_Correctly_Create_Achievement()
    {
        var achievement = new Achievement(ValidTitle1, ValidDescription1);

        Assert.That(achievement.Title, Is.EqualTo(ValidTitle1));
        Assert.That(achievement.Description, Is.EqualTo(ValidDescription1));
    }

    [Test]
    public void Should_Throw_Exception_Title_Is_Null_Or_Empty()
    {
        Assert.Throws<ArgumentException>(() => new Achievement(InvalidTitle, ValidDescription1));
        Assert.Throws<ArgumentException>(() => new Achievement(EmptyTitle, ValidDescription1));
    }

    [Test]
    public void Should_Throw_Exception_Description_Is_Null_Or_Empty()
    {
        Assert.Throws<ArgumentException>(() => new Achievement(ValidTitle1, InvalidDescription));
        Assert.Throws<ArgumentException>(() => new Achievement(ValidTitle1, EmptyDescription));
    }

    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var achievement1 = new Achievement(ValidTitle1, ValidDescription1);
        var achievement2 = new Achievement(ValidTitle2, ValidDescription2);
        var allAchievements = Achievement.GetAllInstances();

        Assert.Contains(achievement1, allAchievements);
        Assert.Contains(achievement2, allAchievements);
    }

    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        Achievement.InitializeClass();
        Achievement.WriteClass();

        Assert.NotNull(Achievement.GetAllInstances());
    }

    [Test]
    public void Should_Correctly_Create_Achievement_With_Users()
    {
        Achievement achievement = new Achievement(ValidTitle1, ValidDescription1, ValidUsers);
        List<User> achievementUsers = new List<User>();
        
        foreach (User_Achievement userAchievement in achievement.GetUsers())
        {
            achievementUsers.Add(userAchievement.GetUser());
        }
        
        Assert.Contains(ValidUser1, achievementUsers);
        Assert.Contains(ValidUser2, achievementUsers);
    }

    [Test]
    public void Should_Correctly_Delete_User_From_Achievement()
    {
        Achievement achievement = new Achievement(ValidTitle1, ValidDescription1, ValidUsers);
        achievement.RemoveUser(ValidUser1);
        
        List<User> achievementUsers = new List<User>();
        foreach (User_Achievement userAchievement in achievement.GetUsers())
        {
            achievementUsers.Add(userAchievement.GetUser());
        }
        
        Assert.IsFalse(achievementUsers.Contains(ValidUser1));
        Assert.Contains(ValidUser2, achievementUsers);
    }

    [Test]
    public void Should_Correctly_Edit_User_From_Achievement()
    {
        Achievement achievement = new Achievement(ValidTitle1, ValidDescription1, ValidUsers);
        achievement.RemoveUser(ValidUser1);
        achievement.EditUser(ValidUser2, ValidUser1);
        
        List<User> achievementUsers = new List<User>();
        foreach (User_Achievement userAchievement in achievement.GetUsers())
        {
            achievementUsers.Add(userAchievement.GetUser());
        }
        
        Assert.IsFalse(achievementUsers.Contains(ValidUser2));
        Assert.Contains(ValidUser1, achievementUsers);
    }

    [Test]
    public void Should_Not_Edit_User_From_Achievement_If_User_Does_Not_Exist()
    {
        Achievement achievement = new Achievement(ValidTitle1, ValidDescription1, ValidUsers);
        achievement.RemoveUser(ValidUser1);
        achievement.RemoveUser(ValidUser2);
        achievement.EditUser(ValidUser2, ValidUser1);
        
        List<User> achievementUsers = new List<User>();
        foreach (User_Achievement userAchievement in achievement.GetUsers())
        {
            achievementUsers.Add(userAchievement.GetUser());
        }
        
        Assert.IsFalse(achievementUsers.Contains(ValidUser1));
        Assert.IsFalse(achievementUsers.Contains(ValidUser2));    
    }
}