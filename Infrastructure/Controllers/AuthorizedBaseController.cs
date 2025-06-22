using System.Security.Claims;

namespace Infrastructure.Controllers;

public class AuthorizedBaseController : BaseController
{
    protected Guid UserId
    {
        get
        {
            var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdString, out var userId))
                throw new UnauthorizedAccessException("User ID not found or invalid");
            return userId;
        }
    }
}