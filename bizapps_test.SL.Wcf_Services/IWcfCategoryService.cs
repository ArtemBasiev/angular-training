using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using bizapps_test.SL.Wcf_Services.DataContracts;

namespace bizapps_test.SL.Wcf_Services
{
    [ServiceContract]
    public interface IWcfCategoryService
    {
        [OperationContract]
        int CreateCategory(CategoryDC categoryDC);

        [OperationContract]
        int UpdateCategory(CategoryDC categoryDC);

        [OperationContract]
        int DeleteCategory(CategoryDC categoryDC);

        [OperationContract]
        IEnumerable<CategoryDC> GetAllCategories();

        [OperationContract]
        IEnumerable<CategoryDC> GetPostCategories(int postId);

        [OperationContract]
        IEnumerable<CategoryDC> GetBlogCategories(string userName);

        [OperationContract]
        CategoryDC GetCategoryById(int categoryId);
    }

}
