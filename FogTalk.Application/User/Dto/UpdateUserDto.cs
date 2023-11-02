namespace FogTalk.Application.User.Dto;

public class UpdateUserDto
{
    public string UserName { get; set; } 
    public string Email { get; set; }
    public string Bio { get; set; }
    public string? ProfilePicture { get; set; }
}