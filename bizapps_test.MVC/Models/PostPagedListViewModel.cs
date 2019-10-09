using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;

namespace bizapps_test.MVC.Models
{
    public class PostPagedListViewModel
    {
        public IPagedList<PostViewModel> PostList { get; set; }

        public int PageButtonCount { get; set; }

        public int SelectedPage { get; set; }
    }
}