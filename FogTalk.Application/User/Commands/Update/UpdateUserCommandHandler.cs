using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.User.Dto;
using FogTalk.Domain.Exceptions;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.User.Commands.Update;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        var updateUserDto = request.userDto;

        if (HasChanges(user, updateUserDto))
        {
            user.UserName = !string.IsNullOrEmpty(updateUserDto.UserName) ? updateUserDto.UserName : user.UserName;
            user.Email = !string.IsNullOrEmpty(updateUserDto.Email) ? updateUserDto.Email : user.Email;
            user.Bio = !string.IsNullOrEmpty(updateUserDto.Bio) ? updateUserDto.Bio : user.Bio;
            user.ProfilePicture = !string.IsNullOrEmpty(updateUserDto.ProfilePicture) ? updateUserDto.ProfilePicture : user.ProfilePicture;

            await _userRepository.UpdateAsync(user);
        }
        else
        {
            throw new IdempotencyException("No changes in user data.");
        }
    }
    
    private bool HasChanges(Domain.Entities.User existingUser, UpdateUserDto updateUserDto)
    {
        // Compare the properties to determine if there are changes
        bool hasChanges =
            (updateUserDto.UserName != null && existingUser.UserName != updateUserDto.UserName) ||
            (updateUserDto.Email != null && existingUser.Email != updateUserDto.Email) ||
            (updateUserDto.Bio != null && existingUser.Bio != updateUserDto.Bio) ||
            (updateUserDto.ProfilePicture != null && existingUser.ProfilePicture != updateUserDto.ProfilePicture);

        return hasChanges;
    }
}