using Health_tracker;
using NUnit.Framework;

namespace health_tracker_tests;

[TestFixture]
public class DoctorTest
{
    private const string ValidName1 = "name1";
    private const string ValidName2 = "name2";
    private const string InvalidName = null;
    private const string EmptyName = "";
    private static readonly DateTime ValidDateOfBirth1 = DateTime.Now.AddYears(-30);
    private static readonly DateTime ValidDateOfBirth2 = DateTime.Now.AddYears(-25);
    private const Specialization ValidSpecialization1 = Specialization.Psychologist;
    private const Specialization ValidSpecialization2 = Specialization.Dietitian;
    private static readonly List<Meal> ValidMeals = new List<Meal>();
    private Doctor doctor1;
    private Doctor doctor2;
    private Diet diet1;
    private Diet diet2;
 [Test]
    public void Should_Correctly_Create_Doctor()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
 
        Assert.That(doctor.Name, Is.EqualTo(ValidName1));
        Assert.That(doctor.DateOfBirth, Is.EqualTo(ValidDateOfBirth1));
    }
 
    [Test]
    public void Should_Throw_Exception_Name_Is_Null_Or_Empty()
    {
        Assert.Throws<ArgumentException>(() => new Doctor(InvalidName, ValidDateOfBirth1, ValidSpecialization1));
        Assert.Throws<ArgumentException>(() => new Doctor(EmptyName, ValidDateOfBirth1, ValidSpecialization1));
    }
    [Test]
    public void Should_Correctly_Create_Doctor_With_Diets()
    {
        var diet1 = new Diet(2000);
        var diet2 = new Diet(1800);
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1, new List<Diet> { diet1, diet2 });
        Assert.That(doctor.Name, Is.EqualTo(ValidName1));
        Assert.That(doctor.DateOfBirth, Is.EqualTo(ValidDateOfBirth1));
        Assert.That(doctor.GetDietCount(), Is.EqualTo(2));
        Assert.That(doctor.HasDiet(diet1), Is.True);
        Assert.That(doctor.HasDiet(diet2), Is.True);
    }
    [Test]
    public void Should_Add_Diet_Successfully()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var diet = new Diet(2000);
        doctor.AddDiet(diet);
        Assert.That(doctor.GetDietCount(), Is.EqualTo(1));
        Assert.That(doctor.HasDiet(diet), Is.True);
        Assert.That(diet.GetDoctor(), Is.EqualTo(doctor));
    }
    [Test]
    public void Should_Not_Add_Null_Diet()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        Assert.Throws<ArgumentNullException>(() => doctor.AddDiet(null));
    }
    [Test]
    public void Should_Not_Add_Duplicate_Diet()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var diet = new Diet(2000);
        doctor.AddDiet(diet);
        doctor.AddDiet(diet);
        Assert.That(doctor.GetDietCount(), Is.EqualTo(1));
        Assert.That(doctor.HasDiet(diet), Is.True);
    }
    [Test]
    public void Should_Remove_Diet_Successfully()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var diet = new Diet(2000);
        doctor.AddDiet(diet);
        var result = doctor.RemoveDiet(diet);
        Assert.That(result, Is.True);
        Assert.That(doctor.GetDietCount(), Is.EqualTo(0));
        Assert.That(diet.GetDoctor(), Is.Null);
    }
    [Test]
    public void Should_Not_Remove_Null_Diet()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var result = doctor.RemoveDiet(null);
        Assert.That(result, Is.False);
    }
    [Test]
    public void Should_Not_Remove_Nonexistent_Diet()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var diet = new Diet(2000);
        var result = doctor.RemoveDiet(diet);
        Assert.That(result, Is.False);
    }
    [Test]
    public void Should_Edit_Diet_Successfully()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var diet1 = new Diet(2000);
        var diet2 = new Diet(1800);
        doctor.AddDiet(diet1);
        var result = doctor.EditDiet(diet1, diet2);
        Assert.That(result, Is.True);
        Assert.That(doctor.GetDietCount(), Is.EqualTo(1));
        Assert.That(doctor.HasDiet(diet2), Is.True);
        Assert.That(diet2.GetDoctor(), Is.EqualTo(doctor));
        Assert.That(doctor, Is.Not.EqualTo(diet1.GetDoctor()));
    }
    [Test]
    public void Should_Not_Edit_With_Null_Diets()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var diet1 = new Diet(2000);
        var diet2 = new Diet(1800);
        var result = doctor.EditDiet(null, diet2);
        Assert.That(result, Is.False);
        result = doctor.EditDiet(diet1, null);
        Assert.That(result, Is.False);
    }
    [Test]
    public void Should_Not_Edit_If_Original_Diet_Not_Found()
    {
        var doctor = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var diet1 = new Diet(2000);
        var diet2 = new Diet(1800);
        var result = doctor.EditDiet(diet1, diet2);
        Assert.That(result, Is.False);
        Assert.That(doctor.GetDietCount(), Is.EqualTo(0));
    }
    [Test]
    public void Should_Correctly_Return_All_Instances()
    {
        var doctor1 = new Doctor(ValidName1, ValidDateOfBirth1, ValidSpecialization1);
        var doctor2 = new Doctor(ValidName2, ValidDateOfBirth2, ValidSpecialization2);
        var allDoctors = Doctor.GetAllInstances();
 
        Assert.Contains(doctor1, allDoctors);
        Assert.Contains(doctor2, allDoctors);
    }
 
    [Test]
    public void Should_Correctly_Initialize_And_Write_Data()
    {
        Doctor.InitializeClass();
        Doctor.WriteClass();
 
        Assert.NotNull(Doctor.GetAllInstances());
    }
}