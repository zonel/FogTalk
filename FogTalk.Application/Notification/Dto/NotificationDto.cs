namespace FogTalk.Application.Notification.Dto;

public class NotificationDto
{
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
}