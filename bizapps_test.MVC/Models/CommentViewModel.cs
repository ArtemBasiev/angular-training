using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bizapps_test.MVC.Models;

namespace bizapps_test.MVC.Models
{
   
    public class CommentViewModel
    {
        public int Id { get; set; }

       
        [Required(ErrorMessage = "Comments can't be blank!")]
        [StringLength(500, ErrorMessage = "Comments can't be longer than 500 characters!")]
        [DataType(DataType.MultilineText)]
        public string CommentText { get; set; }

        public string UserName { get;  set; }

        public string CreationDate { get;  set; }

        public int ParentId { get; set; }

        public int PostId { get; set; }

        public string ActiveUser { get; set; }
    }
}