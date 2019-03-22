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
    public class BlogControllerTest
    {



        [TestMethod]
        public void Get()
        {
            // Arrange
            BlogController controller = new BlogController();

            var blogs = controller.Get();
            Assert.IsNotNull(blogs);
            Assert.IsTrue(blogs.Count() > 0);

            var b1 = blogs.First();
            Assert.IsTrue(b1.Owner.Length > 0);
        }

        [TestMethod]
        public void GetById()
        {
            BlogController controller = new BlogController();

            var blogs = controller.Get();
            Assert.IsTrue(blogs.Count() > 0);

            var b1 = blogs.First();
            var id1 = b1._Id;
            var b2 = controller.Get(id1.ToString());
            Assert.IsTrue(b2._Id == b1._Id);
        }

        [TestMethod]
        public void Post()
        {
            BlogController controller = new BlogController();
            var blog = CreateNewBlog();
            controller.Post(blog);
        }

        public static BlogModel CreateNewBlog()
        {
            return new BlogModel
            {
                Owner = "Teste" + DateTime.Now.ToString("yyMMdd-hhmmss"),
                Description = "descrição " + DateTime.Now.ToString("yyMMdd-hhmmss"),
                Password = "!@#$!@#!@#$%!#@$%!#@$#@$"
                //Posts = new List<PostModel> {
                //    new PostModel{Datahora= DateTime.Now, Secoes = 
                //        new List<SecaoModel>{ },Titulo = "new post" + DateTime.Now.ToString("yyMMdd-hhmmss") }}
            };
        }

        [TestMethod]
        public void PostController()
        {
            PostController post = new PostController();
            Assert.IsNotNull(post);
        }

        [TestMethod]
        public void Put()
        {
            BlogController controller = new BlogController();

            var b1 = CreateNewBlog();
            //crio um cara para existir um e pegar o id
            controller.Post(b1);
            var id1 = b1._Id;

            var b2 = CreateNewBlog();
            var owner = "zzzzzzzz" + DateTime.Now.ToLongTimeString();
            b2.Owner = owner;
            b2.Description = "zzzzzzzz" + DateTime.Now.ToLongTimeString();

            var count = controller.Put(id1.ToString(), b2);
            Assert.AreEqual(count, 1);

            var b3 = controller.Get(id1.ToString());
            Assert.AreEqual(b1._Id, b2._Id);
            Assert.AreNotEqual(b1.Owner, b3.Owner);
        }

        [TestMethod]
        public void Delete()
        {
            BlogController controller = new BlogController();

            var blog = CreateNewBlog();
            //blog.Id = "999";

            controller.Post(blog);
            Assert.IsNotNull(blog._Id);
            var id1 = blog._Id;

            controller.Remove(id1.ToString());

            var b2 = controller.Get(id1.ToString());
            Assert.IsNull(b2);
        }
    }
}
