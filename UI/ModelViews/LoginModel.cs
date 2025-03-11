using System.ComponentModel.DataAnnotations;

namespace   UI.ModelViews
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        [EmailAddress(ErrorMessage = "Please enter a valid email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter a valid Password")]
     
        public string Password { get; set; } = string.Empty;
        public string? ReturnUrl { get; set; } = string.Empty;
    }
}
