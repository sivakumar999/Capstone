using System.ComponentModel.DataAnnotations;

namespace BlogTrackerApplication.Models
{
    public class LoginInfo
    {
        [Required(ErrorMessage = "Please Enter Your EmailId")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please Enter Your Password")]
        public string Password { get; set; }
    }
}
