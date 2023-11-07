using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;
using FogTalk.Domain.Exceptions;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Chat.Commands.Update;

public class UpdateChatCommandHandler : ICommandHandler<UpdateChatCommand>
{
    private readonly IChatRepository _chatRepository;

    public UpdateChatCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task Handle(UpdateChatCommand request, CancellationToken cancellationToken)
    {
        var chatId = request.chat.Id;
        var userId = request.userId;
        
        var currentChat = await _chatRepository.GetChatForUserByIdAsync(chatId, userId);
        var updateChatDto = request.chat;

        if (HasChanges(currentChat, updateChatDto))
        {
            currentChat.Name = !string.IsNullOrEmpty(updateChatDto.Name) ? updateChatDto.Name : currentChat.Name;
            await _chatRepository.UpdateAsync(currentChat);
        }
        else
        {
            throw new IdempotencyException("No changes in user data.");
        }
    }
    
    private bool HasChanges(Domain.Entities.Chat existingChat, UpdateChatDto updateChatDto)
    {
        // Compare the properties to determine if there are changes
        bool hasChanges =
            (updateChatDto.Name != null && existingChat.Name != updateChatDto.Name);
        return hasChanges;
    }
}