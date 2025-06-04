using Application.Interfaces.Repositories.Read;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Read;

public class UserReadRepository(OrderFlowDbContext context)
    : BaseReadRepository<User>(context), IUserReadRepository
{

}