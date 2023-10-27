using FogTalk.Domain.Enums;

namespace FogTalk.Domain.Entities;

public class Message
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime Timestamp { get; set; }
    public MessageStatus MessageStatus { get; set; }

    public int SenderId { get; set; }
    public int ReceiverId { get; set; }
    public int ChatId { get; set; }

    public User Sender { get; set; }
    public User Receiver { get; set; }
    public Chat ReceivingChat  { get; set; } 
}