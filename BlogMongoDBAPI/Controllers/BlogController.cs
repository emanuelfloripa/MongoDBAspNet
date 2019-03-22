using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogMongoDBAPI.Models;
using BlogMongoDBAPI.Services;

namespace BlogMongoDBAPI.Controllers
{
    //[Authorize]
    [RoutePrefix("api/Blog")]
    public class BlogController : ApiController
    {

        private readonly BlogService _blogService;

        public BlogController()
        {
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings[Consts.ConnStringName].ConnectionString;
            _blogService = new BlogService(conn);            
        }

        public IEnumerable<BlogModel> Get()
        {
            return _blogService.Get();
        }

        public BlogModel Get(string id)
        {
            return _blogService.Get(id);
        }

        public void Post([FromBody]BlogModel value)
        {
            _blogService.Insert(value);
        }

        public int Put(string id, [FromBody]BlogModel value)
        {
            return _blogService.Update(id, value);

        }

        public void Delete(string id)
        {
            _blogService.Remove(id);
        }
    }
}

