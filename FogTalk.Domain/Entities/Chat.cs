namespace FogTalk.Domain.Entities;

public class Chat
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsGroupChat { get; set; } = false;
    public DateTime CreatedAt { get; set; }

    public virtual ICollection<User> Participants { get; set; } 
    public virtual ICollection<Message> Messages { get; set; } 
}