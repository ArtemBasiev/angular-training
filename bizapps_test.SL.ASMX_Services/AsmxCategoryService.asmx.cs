using System;
using System.Collections.Generic;
using bizapps_test.BLL.DTO;
using bizapps_test.BLL.Interfaces;
using bizapps_test.SL.ASMX_Services.SoapEntities;
using bizapps_test.SL.ASMX_Services.Interfaces;
using System.Web.Services;
using Ninject;
using System.ServiceModel;
using Ninject.Web.Common.WebHost;
using System.Web;

namespace bizapps_test.SL.ASMX_Services
{
    [WebService(Namespace = "http://category.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class AsmxCategoryService : WebService, IAsmxCategoryService
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }

        public AsmxCategoryService()
            : this(((NinjectHttpApplication)HttpContext.Current.ApplicationInstance).Kernel.Get<ICategoryService>())
        {          
        }

        public AsmxCategoryService(ICategoryService categoryService)
        {
            CategoryService = categoryService;
        }
       

        [WebMethod]
        public int CreateCategory(CategorySoap categorySoap)
        {
            try
            {
                return CategoryService.CreateCategory(new CategoryDto
                {
                    CategoryName = categorySoap.CategoryName
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public int UpdateCategory(CategorySoap categorySoap)
        {
            try
            {
                return CategoryService.UpdateCategory(new CategoryDto
                {
                    Id = categorySoap.Id,
                    CategoryName = categorySoap.CategoryName
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public int DeleteCategory(CategorySoap categorySoap)
        {
            try
            {
                return CategoryService.DeleteCategory(new CategoryDto
                {
                    Id = categorySoap.Id
                });
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public CategorySoap[] GetAllCategories()
        {
            try
            {
                List<CategorySoap> categorySoapList = new List<CategorySoap>();
                IEnumerable<CategoryDto> categoryDtoList = CategoryService.GetAllCategories();
                foreach (CategoryDto categoryDto in categoryDtoList)
                {
                    categorySoapList.Add(new CategorySoap
                    {
                        Id = categoryDto.Id,
                        CategoryName = categoryDto.CategoryName
                    });
                }
                return categorySoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public CategorySoap[] GetPostCategories(int postId)
        {
            try
            {
                List<CategorySoap> categorySoapList = new List<CategorySoap>();
                IEnumerable<CategoryDto> categoryDtoList = CategoryService.GetPostCategories(postId);
                foreach (CategoryDto categoryDto in categoryDtoList)
                {
                    categorySoapList.Add(new CategorySoap
                    {
                        Id = categoryDto.Id,
                        CategoryName = categoryDto.CategoryName
                    });
                }
                return categorySoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public CategorySoap[] GetBlogCategories(string userName)
        {
            try
            {
                List<CategorySoap> categorySoapList = new List<CategorySoap>();
                IEnumerable<CategoryDto> categoryDtoList = CategoryService.GetBlogCategories(userName);
                foreach (CategoryDto categoryDto in categoryDtoList)
                {
                    categorySoapList.Add(new CategorySoap
                    {
                        Id = categoryDto.Id,
                        CategoryName = categoryDto.CategoryName
                    });
                }
                return categorySoapList.ToArray();
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        [WebMethod]
        public CategorySoap GetCategoryById(int categoryId)
        {
            try
            {
                CategoryDto categoryDto = CategoryService.GetCategoryById(categoryId);
                return new CategorySoap
                {
                    Id = categoryDto.Id,
                    CategoryName = categoryDto.CategoryName
                };
            }
            catch (ApplicationException ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
