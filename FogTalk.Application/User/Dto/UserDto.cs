using System.ComponentModel.DataAnnotations;

namespace FogTalk.Application.User.Dto;

public class UserDto
{
    [Required(ErrorMessage = "UserName is required")]
    [MaxLength(50, ErrorMessage = "UserName cannot exceed 50 characters")]
    public string UserName { get; set; }

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string Email { get; set; }

    [MaxLength(500, ErrorMessage = "Bio cannot exceed 500 characters")]
    public string Bio { get; set; } = "";

    [RegularExpression(@"^https://.*\.(jpg|jpeg|png|gif)$", ErrorMessage = "Invalid URL for ProfilePicture")]
    public string? ProfilePicture { get; set; } = "https://fogtalk.com/images/default-profile-picture.jpg";
}