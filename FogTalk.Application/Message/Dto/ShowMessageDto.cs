namespace FogTalk.Application.Message.Dto;

public class ShowMessageDto
{
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public int SenderId { get; set; }
}