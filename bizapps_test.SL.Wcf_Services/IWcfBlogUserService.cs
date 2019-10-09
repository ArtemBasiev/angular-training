using System.Collections.Generic;
using System.ServiceModel;
using bizapps_test.SL.Wcf_Services.DataContracts;

namespace bizapps_test.SL.Wcf_Services
{
    [ServiceContract]
    public interface IWcfBlogUserService
    {
       [OperationContract]
       int CreateBlogUser(BlogUserDC bloguserDC);

        [OperationContract]
        int UpdateBlogUser(BlogUserDC bloguserDC);

        [OperationContract]
        int DeleteBlogUser(BlogUserDC bloguserDC);


        [OperationContract]
        BlogUserDC GetBlogUserById(int userId);

        [OperationContract]
        BlogUserDC GetBlogUserByName(string userName);

    }
}
