using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using bizapps_test.BLL.Interfaces;
using bizapps_test.BLL.DTO;

namespace bizapps_test.SL.API_Services.Controllers
{
    public class ValuesController : ApiController
    {
        [Ninject.Inject]
        public IPostService PostService { get; set; }


        // GET api/values
        public IEnumerable<PostDto> GetUserPosts()
        {
            //djwkfkwfkwfkwefj
            return PostService.GetUserPostsByUserName("admin");
        }

        // GET api/values/5
        public PostDto GetPost(int id)
        {

            return PostService.GetPost(id);
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
      
        public void DeletePost(int id)
        {
            PostService.DeletePost(new PostDto
            {
                Id = id,
            });

        }
    }
}
