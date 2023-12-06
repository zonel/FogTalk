using FogTalk.Application.Abstraction.Messaging;

namespace FogTalk.Application.Friend.Commands.Update;

public record RespondToFriendRequestCommand(int RequestedUserId, int RequestingUserId, bool Accepted, CancellationToken cancellationToken) : ICommand;