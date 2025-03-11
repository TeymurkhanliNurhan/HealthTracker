using System.Text.Json.Serialization;

namespace Health_tracker;

public abstract class Person
{
    [JsonInclude]
    private string _name;
    [JsonIgnore]
    public string Name
    {
        get => _name;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Name cannot be null or empty.");
            }
            _name = value;
        }
    }
    [JsonInclude]
    private DateTime _dateOfBirth;
    [JsonIgnore]
    public DateTime DateOfBirth
    {
        get => _dateOfBirth;
        private set
        {
            if (value > DateTime.Now)
            {
                throw new ArgumentException("Date of Birth cannot be in the future.");
            }
            _dateOfBirth = value;
            _age = DateTime.Now.Year - value.Year;
        }
    }

    [JsonInclude] 
    private int _age;
    [JsonIgnore]
    public int Age { get => _age;}

    protected Person()
    {
    }
    protected Person(string name, DateTime dateOfBirth)
    {
        this.Name = name;
        this.DateOfBirth = dateOfBirth;
        _age = DateTime.Now.Year - dateOfBirth.Year;
    }
}