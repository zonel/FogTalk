namespace FogTalk.Domain.Entities;

public class Jti
{
    public int Id { get; set; }
    public string JtiValue { get; set; }
    public DateTime BlacklistedAt { get; set; }
}