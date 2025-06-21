namespace Domain.ValueObjects;

public class Status : BaseValueObject
{
    #region Constructors

    private Status(string name)
    {
        Name = name;
    }

    #endregion
    
    public string Name { get; private set; }

    public static readonly Status Pending = new Status("Pending");
    
    public static readonly Status Completed = new Status("Completed");
    
    public static IEnumerable<Status> List() => new[] { Pending, Completed };
    public static Status FromName(string name)
    {
        return List().FirstOrDefault(s => s.Name == name) 
               ?? throw new ArgumentException($"Неверный статус: {name}");
    }
}