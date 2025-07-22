namespace Domain.ValueObjects;

public class WorkspaceRole : BaseValueObject
{
    private WorkspaceRole(string value)
    {
        Value = value;
    }
    
    public string Value { get; }
    
    public static WorkspaceRole Owner => new("Owner");
    public static WorkspaceRole Admin => new("Admin");
    public static WorkspaceRole Member => new("Member");
    public static WorkspaceRole Guest => new("Guest");

    public static WorkspaceRole From(string roleValue)
    {
        var role = new WorkspaceRole(roleValue);
        
        if (!IsValid(role))
        {
            throw new InvalidOperationException($"Invalid workspace role: {roleValue}");
        }
        
        return role;
    }
    
    public static implicit operator string(WorkspaceRole role) => role.Value;
    
    private static bool IsValid(WorkspaceRole role)
    {
        return role.Value is "Owner" or "Admin" or "Member" or "Guest";
    }
    
    public bool HasAtLeastRole(WorkspaceRole requiredRole)
    {
        var roleHierarchy = new Dictionary<string, int>
        {
            { Owner.Value, 4 },
            { Admin.Value, 3 },
            { Member.Value, 2 },
            { Guest.Value, 1 }
        };

        return roleHierarchy[Value] >= roleHierarchy[requiredRole.Value];
    }

    protected IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
