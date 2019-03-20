using BlogMongoDBAPI.Models;
using BlogMongoDBAPI.Services;
using MongoDB.Bson;
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
            _service = new PostBlogService(conn);
        }

        // GET: api/Post
        public List<PostModel> Get(string idBlog)
        {
            return _service.Get(new ObjectId( idBlog));
        }

        // GET: api/Post/5
        public PostModel Get(string idBlog, string id)
        {
            return _service.Get(new ObjectId(idBlog), id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBlog"></param>
        /// <param name="p"></param>
        /// <returns>ID da postagem inserida</returns>
        public string Post(string idBlog, PostModel p)
        {
            return _service.Insert(idBlog, p);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBlog"></param>
        /// <param name="post"></param>
        /// <returns>Update realizado TRUE</returns>
        public bool Put(string idBlog, PostModel post)
        {
            return _service.Update(idBlog, post);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBlog"></param>
        /// <param name="idPost"></param>
        /// <returns>TRUE se achou e conseguiu deletar o registro</returns>
        public bool Remove(string idBlog, string idPost)
        {
            return _service.Remove(idPost);
        }

        //public string AddSecao(PostModel post, SecaoModel secao)
        //{
        //    return _service.AddSecao(post, secao);
        //}
    }
}
