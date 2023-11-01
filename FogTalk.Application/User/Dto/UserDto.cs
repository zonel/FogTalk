namespace FogTalk.Application.User.Dto;

public class UserDto
{
    public string UserName { get; set; } 
    public string Password { get; set; }
    public string Email { get; set; }
    public string Bio { get; set; } = "";
    public string? ProfilePicture { get; set; } = "https://fogtalk.com/images/default-profile-picture.jpg";
}