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
            var lista = ctr.Get();
            Assert.IsNotNull(lista);
        }

        [TestMethod]
        public void GetListWith3Post()
        {
            var ctr = Controller();
            ctr.Post(CreateNewPost());
            ctr.Post(CreateNewPost());
            ctr.Post(CreateNewPost());

            List<PostModel> lista = ctr.Get();

            Assert.IsNotNull(lista);
            Assert.IsTrue(lista.Count >= 3);
        }


        [TestMethod]
        public void GetByIdWithPost()
        {
            var ctr = Controller();
            var post = CreateNewPost();
            ctr.Post(post);

            string id = post.Id;
            var p2 = ctr.Get(id);

            Assert.IsTrue(post.Id == p2.Id);
        }

        [TestMethod]
        public void GetByIdIfExists()
        {
            var ctr = Controller();
            List<PostModel> lista = ctr.Get();
            var onePost = lista.FirstOrDefault();
            string id = onePost.Id;
            var p2 = ctr.Get(id);

            Assert.IsTrue(onePost.Id == p2.Id);
        }

        [TestMethod]
        public void Post()
        {
            var ctr = Controller();
            var post = CreateNewPost();
            ctr.Post(post);

            var id = post.Id;
            var p2 = ctr.Get(id);

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
            var post = ctr.Get().FirstOrDefault();
            

            //Assert.AreEqual(count, 1);

            //Assert.AreEqual(b1.Id, b2.Id);
            //Assert.AreNotEqual(b1.Owner, b3.Owner);
        }

        [TestMethod]
        public void Get()
        {
            var ctr = Controller();

            var posts = ctr.Get();
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
