
using System.ComponentModel.DataAnnotations;

namespace bizapps_test.MVC.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "You must set category name")]
        public string CategoryName { get; set; }

        public bool IsChecked { get; set; } = false;
    }
}