namespace FogTalk.Application.Friend.Dto;

/// <summary>
/// Dto used to show friend requests.
/// </summary>
public class ShowFriendDto
{
    public string UserName { get; set; } 
    public string Bio { get; set; }
    public string? ProfilePicture { get; set; }
}