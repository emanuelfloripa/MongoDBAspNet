using BlogMongoDBAPI.Models;
using BlogMongoDBAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BlogMongoDBAPI.Controllers
{
    public class PostController : ApiController
    {
        private PostBlogService _service;

        public PostController(string conn)
        {
            //var conn = System.Configuration.ConfigurationManager.ConnectionStrings[Consts.ConnStringName].ConnectionString;
            //_service = new PostBlogService(conn);
        }

        // GET: api/Post
        public List<PostModel> Get()
        {
            //return _service.Get(idBlog);
            return null;
        }

        // GET: api/Post/5
        public PostModel Get(string id)
        {
            return null;
        }

        // POST: api/Post
        public void Post(PostModel p)
        {
            _service.Post(p);
        }

        // PUT: api/Post/5
        public void Put(PostModel post)
        {
            _service.Put(post);
        }

        // DELETE: api/Post/5
        public void Delete(string id)
        {
            _service.Delete(id);
        }

    }
}
