using System.ComponentModel.DataAnnotations;

namespace Assign.PL.Models.Account
{
    public class ForgetPasswordViewModel
    {
        [Required(ErrorMessage = "Email is Required!!")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
    }
}
