namespace Domain.Entities;

public class ExternalLogin : BaseEntity<ExternalLogin>
{
    #region Constructors

    /// <summary>
    /// Empty constructor for EF Core
    /// </summary>
    private ExternalLogin()
    {
        
    }

    /// <summary>
    /// Public constructor
    /// </summary>
    /// <param name="provider">Provider</param>
    /// <param name="externalUserId">External user id from jwt</param>
    /// <param name="userId"></param>
    public ExternalLogin(
        User user,
        string provider, 
        string externalUserId
    )
    {
        User = user;
        UserId = user.Id;
        Provider = provider;
        ExternalUserId = externalUserId;

        //ValidateEntity();
        //AddDomainEvent();
    }
    
    #endregion
    
    #region Properties

    /// <summary>
    /// Provider
    /// </summary>
    public string Provider { get; private set; }
    
    /// <summary>
    /// External user id from JWT
    /// </summary>
    public string ExternalUserId { get; private set; }
    
    /// <summary>
    /// Idk
    /// </summary>
    public string AccessToken { get; private set; }
    
    /// <summary>
    /// Idk
    /// </summary>
    public DateTime? TokenExpiresAt { get; private set; }
    
    #region Navigation Properties

    /// <summary>
    /// Navigation property for linking to User
    /// </summary>
    public User User { get; private set; }
    
    /// <summary>
    /// Navigation property for linking to User
    /// </summary>
    public Guid UserId { get; private set; }

    #endregion

    #endregion
}