using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bizapps_test.MVC.Models
{
   
    public class BlogUserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User name")]
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, ErrorMessage = "Login must be no longer than 50 characters")]
        public string UserName { get; set; }

        [Display(Name = "User password")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Field must be set")]
        [StringLength(50, ErrorMessage = "Password must be no longer than 50 characters")]
        public string UserPassword { get; set; }

        public bool IsAdmin {get; set; }
    }
}