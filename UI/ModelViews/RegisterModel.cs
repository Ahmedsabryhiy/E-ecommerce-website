using LapShop.Rserouses;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace UI.ModelViews
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please enter your first name")]
        public string FirstName { get; set; }=string.Empty;
        [Required(ErrorMessage = "Please enter yourlast name")]
        public string LastName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your name")]
        [EmailAddress(ErrorMessage="Please enter a valid email")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "Please enter your Password")]
       
        public string Password { get; set; } = string.Empty;
        
    }
}
