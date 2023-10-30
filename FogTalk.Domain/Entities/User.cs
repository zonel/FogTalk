namespace FogTalk.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; } 
    public string Password { get; set; }
    public string Email { get; set; } 
    public string Bio { get; set; } 
    public string? ProfilePicture { get; set; } 
    public DateTime? CreatedAt { get; set; }

    //public int ReceivedMessagesId { get; set; }
    
    public virtual ICollection<Message> SentMessages { get; set; } 
    public virtual ICollection<Message> ReceivedMessages { get; set; } 
    public virtual ICollection<Chat> Chats { get; set; } 
    public virtual ICollection<User> Friends { get; set; } 
    public virtual ICollection<User> FriendRequests { get; set; } 
}