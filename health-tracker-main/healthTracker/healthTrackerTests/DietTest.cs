using Health_tracker;
using NUnit.Framework;

namespace health_tracker_tests;

[TestFixture]
public class DietTest
{
    private const double ValidCalories1 = 2000.0;
    private const double ValidCalories2 = 1500.0;
    private const double InvalidCalories = -500.0;
    private const double ZeroCalories = 0.0;
    private static readonly List<Meal> ValidMeals = new List<Meal>();
    private const string validDoctorName1 = "Doctor1";
    private static readonly DateTime validDoctorBirthDate1 = DateTime.Now.AddYears(-30);
    private const Specialization validDoctorSpecialization = Specialization.Dietitian;
    private Diet diet1;
    private Diet diet2;
    private Doctor doctor1;
    private Doctor doctor2;
    [SetUp]
    public void Setup()
    {
        doctor1 = new Doctor(validDoctorName1, DateTime.Now, Specialization.Dietitian);
        doctor2 = new Doctor(validDoctorName1, DateTime.Now, Specialization.Dietitian);
        diet1 = new Diet(ValidCalories1, ValidMeals, doctor1, new User());
        diet2 = new Diet(ValidCalories1, ValidMeals, doctor2, new User());
    }
    [Test]
    public void Should_Correctly_Create_Diet()
    {
        var diet = new Diet(ValidCalories1, ValidMeals, doctor1, new User());
 
        Assert.That(diet.CaloriesTarget, Is.EqualTo(ValidCalories1));
        Assert.That(diet.GetDoctor(), Is.EqualTo(doctor1));
    }
 
    [Test]
    public void Should_Throw_Exception_Calories_Target_Invalid()
    {
        Assert.Throws<ArgumentException>(() => new Diet(InvalidCalories, ValidMeals, doctor1, new User()));
        Assert.Throws<ArgumentException>(() => new Diet(ZeroCalories, ValidMeals, doctor2, new User()));
    }
 
    [Test]
    public void Should_Create_Diet_With_Doctor_And_User()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var user = new User();
        var diet = new Diet(ValidCalories1, ValidMeals, doctor, user);
        Assert.That(diet.GetDoctor(), Is.EqualTo(doctor));
        Assert.That(doctor.HasDiet(diet), Is.True);
        Assert.That(diet.GetUsers(), Contains.Item(user));
        Assert.That(user.GetDiet(), Is.EqualTo(diet));
    }
 [Test]
    public void Should_Not_Add_Same_User_Twice()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var user1 = new User();
        var diet = new Diet(ValidCalories1, ValidMeals, doctor,user1);
        diet.AddUser(user1);
        diet.AddUser(user1);
        Assert.That(diet.GetUsers().Count, Is.EqualTo(1));
    }
    [Test]
    public void Should_Remove_User_From_Diet()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var user1 = new User();
        var diet = new Diet(ValidCalories1, ValidMeals, doctor,user1);
        diet.AddUser(user1);
        var result = diet.RemoveUser(user1);
        Assert.That(result, Is.True);
        Assert.That(diet.GetUsers(), Does.Not.Contain(user1));
        Assert.That(user1.GetDiet(), Is.Null);
    }
    [Test]
    public void Should_Return_False_When_Removing_Nonexistent_User()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var user1 = new User();
        var diet = new Diet(ValidCalories1, ValidMeals, doctor, user1);
        var result = diet.RemoveUser(new User()); 
        Assert.That(result, Is.False);
    }
    [Test]
    public void Should_Get_All_Users_Associated_With_Diet()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var user1 = new User();
        var user2 = new User();
        var diet = new Diet(ValidCalories1, ValidMeals, doctor,user1);
        diet.AddUser(user1);
        diet.AddUser(user2);
        var users = diet.GetUsers();
        Assert.That(users, Contains.Item(user1));
        Assert.That(users, Contains.Item(user2));
        Assert.That(users.Count, Is.EqualTo(2));
    }
 
    [Test]
    public void Should_Throw_Exception_When_Adding_Null_User_To_Existing_Diet()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var user = new User();
        var diet = new Diet(ValidCalories1, ValidMeals, doctor, user);
        Assert.Throws<ArgumentNullException>(() => diet.AddUser(null));
    }
    [Test]
    public void Should_Set_Doctor_Successfully()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var diet = new Diet(ValidCalories1, ValidMeals, doctor1, new User());
        diet.SetDoctor(doctor);
        Assert.That(diet.GetDoctor(), Is.EqualTo(doctor));
        Assert.That(doctor.HasDiet(diet), Is.True);
    }
 
    [Test]
    public void Should_Remove_Doctor_Successfully()
    {
        var doctor = new Doctor(validDoctorName1, validDoctorBirthDate1, validDoctorSpecialization);
        var diet = new Diet(ValidCalories1, ValidMeals, doctor1, new User());
        diet.SetDoctor(doctor);
        diet.RemoveDoctor();
        Assert.That(diet.GetDoctor(), Is.Null);
        Assert.That(doctor.HasDiet(diet), Is.False);
    }
    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var allDiets = Diet.GetAllInstances();
        Assert.Contains(diet1, allDiets);
        Assert.Contains(diet2, allDiets);
    }
 
    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        Diet.InitializeClass();
        Diet.WriteClass();
 
        Assert.NotNull(Diet.GetAllInstances());
    }
}