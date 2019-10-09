using System.ComponentModel.DataAnnotations;


namespace bizapps_test.MVC.Models
{
    public class ChangePasswordViewModel
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password must be set")]
        public string UserPassword { get; set; }

        [Required(ErrorMessage = "Repeat password must be set")]
        [Compare("UserPassword", ErrorMessage = "Passwords are not equal")]
        public string RepeatPassword { get; set; }
    }
}