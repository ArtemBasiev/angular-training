using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Models
{
    public class PostViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Input post title")]
        [StringLength(50, ErrorMessage = "Title must be no longer than 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Input post content")]
        [DataType(DataType.MultilineText)]
        public string Body { get; set; }

        public string CreationDate { get; set; }

        [DataType(DataType.Upload)]
        public string PostImage { get; set; }

        public int UserId { get; set; }

        public CategoryViewModel[] Categories { get; set; }
    }
}