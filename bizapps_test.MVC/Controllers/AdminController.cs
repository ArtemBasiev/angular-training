using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Security;
using System.Web.Mvc;
using bizapps_test.MVC.Models;
using bizapps_test.MVC.Util;
using bizapps_test.MVC.Interfaces;
using System.Web;
using bizapps_test.MVC.Filters;


namespace bizapps_test.MVC.Controllers
{
    public class AdminController : Controller
    {
        [Ninject.Inject]
        public IPostService PostService { get; set; }

        [Ninject.Inject]
        public ICategoryService CategoryService { get; set; }


        [CustomExceptionHandler]
        public ActionResult Post(PostViewModel post)
        {
            PostViewModel newpost = PostService.GetPost(post.Id);

            PostViewMoreModel postViewMore = new PostViewMoreModel
            {
                Post = new PostViewModel
                {
                    Id= newpost.Id,
                    Title = newpost.Title,
                    Body = HttpUtility.HtmlDecode(newpost.Body),
                    PostImage = @"/Images/" + newpost.PostImage.Trim()           
                },
                BlogUser = null
            };

            postViewMore.Post.Categories = GetCategoryList(post.Id);

            return View(postViewMore);
        }


        [ChildActionOnly]
        [HttpGet]
        public ActionResult ChangePostLink(PostViewModel postViewModel)
        {
            return PartialView(postViewModel);
        }


        [CustomExceptionHandler]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateUpdatePost(PostViewModel postViewModel = null)
        {
            if (postViewModel == null)
            {
                postViewModel = new PostViewModel();
                postViewModel.Categories = GetCategoryList();
            }

             postViewModel.Categories = MapedPostCategories(mapFromCategories: CategoryService.GetPostCategories(postViewModel.Id).ToList(), mapToCategories: GetCategoryList().ToList());

             return PartialView(postViewModel);
        }


        private CategoryViewModel[] MapedPostCategories(List<CategoryViewModel> mapFromCategories, List<CategoryViewModel> mapToCategories)
        {
            foreach (CategoryViewModel categoryViewModel in mapToCategories)
            {
                foreach (CategoryViewModel postCategory in mapFromCategories)
                {
                    if (postCategory.Id == categoryViewModel.Id)
                    {
                        categoryViewModel.IsChecked = true;
                    }
                }
            }
            return mapToCategories.ToArray();
        }


        private CategoryViewModel[] GetCategoryList(int postId = 0)
        {
            IEnumerable<CategoryViewModel> categoryViewModelList = new List<CategoryViewModel>();
            if (postId == 0)
            {
                categoryViewModelList = CategoryService.GetAllCategories();
            }
            else
            {
                categoryViewModelList = CategoryService.GetPostCategories(postId);
            }
           
            foreach (CategoryViewModel categoryViewModel in categoryViewModelList)
            {
                categoryViewModel.IsChecked = false;

            }

            return categoryViewModelList.ToArray();
        }


        [CustomExceptionHandler]
        [Authorize(Roles="Admin")]
        [HttpPost]
        public ActionResult CreateUpdatePostDo(PostViewModel postViewModel)
        {
                RemoveCategoryValidationErrors();

               if (postViewModel.Categories is null)
               {
                   postViewModel.Categories = new CategoryViewModel[0];
               }

              if (string.IsNullOrEmpty(postViewModel.PostImage))
              {
                postViewModel.PostImage = "Default.jpeg";
              }
 
            if (ModelState.IsValid)
                    {
                        try
                        {
                            List<CategoryViewModel> postCategories = new List<CategoryViewModel>();

                            foreach (var categoryItem in postViewModel.Categories)
                            {
                                if (categoryItem.IsChecked)
                                {
                                    postCategories.Add(new CategoryViewModel { Id = categoryItem.Id });
                                }
                            }

                            if (postViewModel.Id == 0)
                            {
                                postViewModel.Body = HttpUtility.HtmlEncode(postViewModel.Body);

                                PostService.CreatePost(postViewModel, 1,  postCategories);

                                return RedirectToAction("Index", "Home");
                            }
                            else
                            {
                                postViewModel.Body = HttpUtility.HtmlEncode(postViewModel.Body);
                                PostService.UpdatePost(postViewModel, postCategories);

                                return RedirectToAction("Post", "Admin", postViewModel);
                            }
                        }
                        catch (ApplicationException)
                        {
                            return RedirectToAction("CreateUpdatePost", "Admin", postViewModel);
                        }
                    }
                    else
                    {
                        return RedirectToAction("CreateUpdatePost", "Admin");
                    }
        }


        private void RemoveCategoryValidationErrors()
        {
            foreach (string key in ModelState.Keys.ToArray())
            {
                if (key.Contains("CategoryName"))
                {
                    ModelState.Remove(key);
                }
            }
        }


        [CustomExceptionHandler]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeletePost(PostViewModel postViewModel)
        {
                    try
                    {
                        if (postViewModel.Id != 0)
                        {
                            PostService.DeletePost(postViewModel);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            return RedirectToAction("CreateUpdatePost", "Admin", postViewModel);
                         }
                    }
                    catch (ApplicationException)
                    {
                        return RedirectToAction("CreateUpdatePost", "Admin", postViewModel);
                    }
        }



        [CustomExceptionHandler]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult CreateCategory(CategoryViewModel categoryViewModel = null)
        {
            if (categoryViewModel == null)
            {
                categoryViewModel = new CategoryViewModel();
            }
            return PartialView(categoryViewModel);
        }



        [CustomExceptionHandler]
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult UpdateDeleteCategory(CategoryViewModel categoryViewModel)
        {
            return PartialView(categoryViewModel);
        }



        [CustomExceptionHandler]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult CreateUpdateCategoryDo(CategoryViewModel categoryViewModel)
        {
                if (ModelState.IsValid)
                {
                    try
                    {
                        if (categoryViewModel.Id == 0)
                        {
                            CategoryService.CreateCategory(categoryViewModel);

                            return RedirectToAction("CreateCategory", "Admin");
                        }
                        else
                        {
                            CategoryService.UpdateCategory(categoryViewModel);

                            return RedirectToAction("CreateCategory", "Admin");
                        }
                    }
                    catch (Exception)
                    {
                        return RedirectToAction("CreateCategory", "Admin");
                    }
                }
                else
                {
                    return RedirectToAction("CreateCategory", "Admin");
                }
        }



        [CustomExceptionHandler]
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult DeleteCategory(CategoryViewModel categoryViewModel)
        {
                try
                {
                    if (categoryViewModel.Id != 0)
                    {
                        CategoryService.DeleteCategory(categoryViewModel);
                        return RedirectToAction("CreateCategory", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("CreateCategory", "Admin", categoryViewModel);
                    }
                }
                catch (Exception)
                {
                    return RedirectToAction("CreateCategory", "Admin", categoryViewModel);
                }
        }
    }
}