using Application.Interfaces;
using Application.Interfaces.Repositories.Read;
using Application.Interfaces.Repositories.Write;
using Application.UseCases.Users.Commands.Requests;
using Domain.Entities;
using MediatR;

namespace Application.UseCases.Users.Commands.Handlers;

public class CreateUserCommandHandler(
    IUserReadRepository userReadRepository,
    IUserWriteRepository userWriteRepository,
    IPasswordHasher passwordHasher
    ) : IRequestHandler<CreateUserCommand, Guid>
{
    public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    { 
        var existingUserByEmail = await userReadRepository.FindAsync(
            x => x.Email == request.Email, 
            cancellationToken);
        
        if (existingUserByEmail is not null)
            throw new InvalidOperationException($"User with email '{request.Email}' already exists.");
        
        var existingUserByUsername = await userReadRepository.FindAsync(
            x => x.Username == request.Username, 
            cancellationToken);
        
        if (existingUserByUsername is not null)
            throw new InvalidOperationException($"User with username '{request.Username}' already exists.");
        
        var hashedPassword = passwordHasher.HashPassword(request.Password);

        var user = new User(
            request.Username, 
            request.Email, 
            hashedPassword
            );
        
        await userWriteRepository.AddAsync(user, cancellationToken);
        
        await userWriteRepository.SaveChangesAsync(cancellationToken);
        
        return user.Id;
    }
}