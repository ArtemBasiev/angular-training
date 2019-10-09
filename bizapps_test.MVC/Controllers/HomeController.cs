using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using bizapps_test.MVC.Models;
using bizapps_test.MVC.Interfaces;
using PagedList;
using bizapps_test.MVC.Filters;


namespace bizapps_test.MVC.Controllers
{
    public class HomeController : Controller
    {
        [Ninject.Inject]
        public IPostService PostService { get; set; }

        [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }

        #region Action Index Methods
        [CustomExceptionHandler]
        public ViewResult Index(string categoryId , int page=1)
        {
            List<PostViewModel> postViewModelList = GetPostList(categoryId);
            DecoratePostListForView(postViewModelList);

            int pageSize = 3;

            PostPagedListViewModel postPagedListViewModel = new PostPagedListViewModel
            {
                PostList = postViewModelList.ToPagedList(page, pageSize),
                SelectedPage = page,
                PageButtonCount = (int)Math.Ceiling((decimal)postViewModelList.Count / (decimal)pageSize)
            };

            return View(postPagedListViewModel);       
        }


        private List<PostViewModel> GetPostList(string categoryId)
        {
            if (string.IsNullOrEmpty(categoryId))
            {
               return PostService.GetUserPostsByUserName("admin").ToList();
            }
            else
            {
                if (categoryId == "withoutcategory")
                  return   PostService.GetPostsByUserNameWithoutCategory("admin").ToList();
                else
                  return PostService.GetPostsByUserNameAndCategory("admin", Convert.ToInt32(categoryId)).ToList();
            }
        }


        private void DecoratePostListForView(List<PostViewModel> postViewModelList)
        {
            foreach (PostViewModel postViewModel in postViewModelList)
            {
                postViewModel.CreationDate = (Convert.ToDateTime(postViewModel.CreationDate)).ToString("D");
                postViewModel.PostImage = @"/Images/" + postViewModel.PostImage.Trim();
                postViewModel.Categories = CategoryService.GetPostCategories(postViewModel.Id).ToArray();
            }
        }
        #endregion


        [CustomExceptionHandler]
        public PartialViewResult CategorySideBar()
        {
            return PartialView(CategoryService.GetBlogCategories("admin").ToList());
        }



        [CustomExceptionHandler]
        public PartialViewResult CategoryUpdateList()
        {
            return PartialView(CategoryService.GetAllCategories().ToList());
        }

    }
}