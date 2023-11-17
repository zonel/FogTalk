namespace FogTalk.Domain.Entities;

public class Notification
{
    public int Id { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsRead { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}