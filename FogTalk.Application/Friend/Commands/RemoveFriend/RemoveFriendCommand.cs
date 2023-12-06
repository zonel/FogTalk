using System.Windows.Input;
using ICommand = FogTalk.Application.Abstraction.Messaging.ICommand;

namespace FogTalk.Application.Friend.Commands.Delete;

public record RemoveFriendCommand(int userId, int userToDeleteId, CancellationToken Token) : ICommand;