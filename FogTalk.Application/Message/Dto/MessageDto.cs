using FogTalk.Domain.Enums;

namespace FogTalk.Application.Message.Dto;

public class MessageDto
{
    public MessageDto(string content)
    {
        Content = content;
    }
    
    public string Content { get; set; }
}