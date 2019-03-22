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
    //[RoutePrefix("api/Blog/{:idBlog}/Post")]
    public class PostController : ApiController
    {
        private PostBlogService _service;

        public PostController(string conn)
        {
            _service = new PostBlogService(conn);
        }

        /// <summary>
        /// Retorna um
        /// </summary>
        /// <param name="idBlog"></param>
        /// <returns></returns>
        [Route("api/Blog/{idBlog}/Post")]
        public List<PostModel> Get([FromUri] string idBlog)
        {
            return _service.Get(idBlog);
        }

        /// <summary>
        /// Retorna um registro de postagem
        /// </summary>
        /// <param name="idBlog">ID do blog.</param>
        /// <param name="id">ID da postagem.</param>
        /// <returns>JSON de um PostModel</returns>
        [Route("api/Blog/{idBlog}/Post/{id}")]
        public PostModel Get([FromUri] string idBlog,[FromUri] string id)
        {
            return _service.Get(idBlog, id);
        }

        /// <summary>
        /// Insere um registro de postagem no Blog
        /// </summary>
        /// <param name="idBlog">Id do blog.</param>
        /// <param name="post">Conteúdo da postagem.</param>
        /// <returns>ID da postagem inserida</returns>
        [Route("api/Blog/{idBlog}/Post")]
        public string Post([FromUri] string idBlog, [FromBody] PostModel post)
        {
            return _service.Insert(idBlog, post);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBlog"></param>
        /// <param name="post"></param>
        /// <returns>Update realizado TRUE</returns>
        [Route("api/Blog/{idBlog}/Post")]
        public bool Put([FromUri] string idBlog, [FromBody] PostModel post)
        {
            return _service.Update(idBlog, post);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idBlog"></param>
        /// <param name="idPost"></param>
        /// <returns>TRUE se achou e conseguiu deletar o registro</returns>
        [Route("api/Blog/{idBlog}/Post/{idPost}")]
        public bool Remove([FromUri] string idBlog,[FromUri] string idPost)
        {
            return _service.Remove(idPost);
        }

    }
}
