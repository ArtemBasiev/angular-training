using PagedList;

namespace bizapps_test.MVC.Models
{
    public class MainPageModel
    {
        public IPagedList<PostViewModel> PagedList { get; set; }

        public BlogUserViewModel BlogUser { get; set; }
    }
}