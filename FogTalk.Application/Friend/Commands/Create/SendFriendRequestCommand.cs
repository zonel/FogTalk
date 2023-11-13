using System.Windows.Input;
using ICommand = FogTalk.Application.Abstraction.Messaging.ICommand;

namespace FogTalk.Application.Friend.Commands.Create;

public record SendFriendRequestCommand(int currentUserId, int receivingUserId) : ICommand;