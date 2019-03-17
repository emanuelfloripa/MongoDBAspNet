using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlogMongoDBAPI;
using BlogMongoDBAPI.Controllers;
using BlogMongoDBAPI.Models;

namespace BlogMongoDBAPI.Tests.Controllers
{
    [TestClass]
    public class PostControllerTest
    {
        //create, get, get 1, post, put, addSecao, RemoveSecao, updateSecao



        /// <summary>
        /// Obtém o PostController do BlogController
        /// </summary>
        /// <returns></returns>
        private PostController Controller()
        {
            var blog = new BlogController();
            return blog.PostController;
        }

        /// <summary>
        /// Busca ou cria um blog.
        /// </summary>
        /// <returns></returns>
        private BlogModel GetOneBlog()
        {
            BlogController ctr = new BlogController();
            var blog = ctr.Get().FirstOrDefault();
            if (blog == null)
            {
                blog = Tests.Controllers.BlogControllerTest.CreateNewBlog();
                ctr.Post(blog);
            }
            return blog;
        }

        [TestMethod]
        public void GetListNotNull()
        {
            var ctr = Controller();
            var blog = GetOneBlog();
            var lista = ctr.Get(blog.Id);
            Assert.IsNotNull(lista);
        }

        [TestMethod]
        public void GetListWith3Post()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog().Id;
            ctr.Post(idBlog, CreateNewPost());
            ctr.Post(idBlog, CreateNewPost());
            ctr.Post(idBlog, CreateNewPost());

            List<PostModel> lista = ctr.Get(idBlog);

            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Count >= 3);
        }


        [TestMethod]
        public void GetByIdWithPost()
        {
            var ctr = Controller();
            var post = CreateNewPost();
            var idBlog = GetOneBlog().Id;
            ctr.Post(idBlog, post);

            string id = post.Id;
            var p2 = ctr.Get(idBlog, id);

            Assert.IsTrue(post.Id == p2.Id);
        }

        [TestMethod]
        public void GetByIdIfExists()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog().Id;
            List<PostModel> lista = ctr.Get(idBlog);
            var onePost = lista.FirstOrDefault();
            string id = onePost.Id;
            var p2 = ctr.Get(idBlog, id);

            Assert.IsTrue(onePost.Id == p2.Id);
        }

        [TestMethod]
        public void Post()
        {
            var ctr = Controller();
            var post = CreateNewPost();
            var idBlog = GetOneBlog().Id;
            ctr.Post(idBlog, post);

            var id = post.Id;
            var p2 = ctr.Get(idBlog, id);

            Assert.IsTrue(post.Id == p2.Id);
        }

        private PostModel CreateNewPost()
        {
            return new PostModel
            {
                Titulo = "Teste" + DateTime.Now.ToString("yyMMdd-hhmmss"),
                Datahora = DateTime.Now,                
                Secoes = CreateSecoes()
            };
        }

        private List<SecaoModel> CreateSecoes()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Put()
        {
            var ctr = Controller();

            var blog = GetOneBlog();
            var idBlog = blog.Id;

            while (blog.Posts.Count < 2)
            {
                var p = CreateNewPost();
                ctr.Post(idBlog, p);
            }

            var post = ctr.Get(idBlog).FirstOrDefault();

            var newTitulo = "novo título alterado" + DateTime.Now.ToLongTimeString();
            post.Titulo = newTitulo;
            ctr.Put(idBlog, post);

            var p2 = ctr.Get(idBlog).FirstOrDefault();
            Assert.AreEqual(p2.Titulo, newTitulo);
        }

        [TestMethod]
        public void Get()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog().Id;
            var posts = ctr.Get(idBlog);
            Assert.IsNotNull(posts);
            Assert.IsTrue(posts.Count() > 0);

            var b1 = posts.First();
            //Assert.IsTrue(b1.Owner.Length > 0);
            Assert.Inconclusive();
        }

        [TestMethod]
        public void Delete()
        {
            //Assert.IsNotNull(post.Id);

            //Assert.IsNull(b2);
            Assert.Inconclusive();
        }
    }
}
