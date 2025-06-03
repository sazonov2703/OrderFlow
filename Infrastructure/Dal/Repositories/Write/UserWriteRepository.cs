using Application.Interfaces.Repositories.Write;
using Domain.Entities;

namespace Infrastructure.Dal.Repositories.Write;

public class UserWriteRepository(OrderFlowDbContext context) : BaseWriteRepository<User>(context), IUserWriteRepository
{
    
}