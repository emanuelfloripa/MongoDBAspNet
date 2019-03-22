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

        /// <summary>
        /// Retorna a lista de Blogs disponíveis.
        /// </summary>
        /// <returns>JSON array de BlogModel.</returns>
        public IEnumerable<BlogModel> Get()
        {
            return _blogService.Get();
        }

        /// <summary>
        /// Retorna um Blog
        /// </summary>
        /// <param name="id">ID do blog.</param>
        /// <returns>JSON de BlogModel</returns>
        public BlogModel Get(string id)
        {
            return _blogService.Get(id);
        }

        /// <summary>
        /// Insere um registro de Blog
        /// </summary>
        /// <param name="value"></param>
        /// <returns>ID do Blog adicionado.</returns>
        public string Post([FromBody]BlogModel value)
        {
            return _blogService.Insert(value);
        }

        /// <summary>
        /// Atualiza um registro de Blog.
        /// </summary>
        /// <param name="id">ID do blog</param>
        /// <param name="blog">JSON BlogModel - Conteúdo do Blog</param>
        /// <returns>1 se o blog foi atualizado com sucesso.</returns>
        public int Put(string id, [FromBody]BlogModel blog)
        {
            return _blogService.Update(id, blog);

        }

        /// <summary>
        /// Remove um registro de Blog.
        /// </summary>
        /// <param name="id">ID do Blog a ser removido.</param>
        public void Remove(string id)
        {
            _blogService.Remove(id);
        }
    }
}

