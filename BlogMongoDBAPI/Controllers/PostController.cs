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
        public List<PostModel> Get(string idBlog)
        {
            return _service.Get(idBlog);
            return null;
        }

        // GET: api/Post/5
        public PostModel Get(string idBlog, string id)
        {
            return _service.Get(idBlog, id);
        }

        // POST: api/Post
        public void Post(string idBlog, PostModel p)
        {
            _service.Insert(idBlog, p);
        }

        // PUT: api/Post/5
        public void Put(string idBlog, PostModel post)
        {
            _service.Update(idBlog, post);
        }

        // DELETE: api/Post/5
        public void Delete(string idBlog, string id)
        {
            _service.Remove(id);
        }

    }
}
