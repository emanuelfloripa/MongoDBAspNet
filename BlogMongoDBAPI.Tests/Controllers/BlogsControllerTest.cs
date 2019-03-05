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
            var id1 = b1.Id;
            var b2 = controller.Get(id1);
            Assert.IsTrue(b2.Id == b1.Id);
        }

        [TestMethod]
        public void Post()
        {
            BlogController controller = new BlogController();
            var blog = CreateNewBlog();
            controller.Post(blog);
        }

        private BlogViewModels CreateNewBlog()
        {
            return new BlogViewModels
            {
                Owner = "Teste" + DateTime.Now.ToString("yyMMdd-hhmmss"),
                Description = "descrição " + DateTime.Now.ToString("yyMMdd-hhmmss")
            };
        }

        [TestMethod]
        public void Put()
        {
            BlogController controller = new BlogController();

            var b1 = CreateNewBlog();
            //crio um cara para existir um e pegar o id
            controller.Post(b1);
            var id1 = b1.Id;

            var b2 = CreateNewBlog();
            var owner = "zzzzzzzz" + DateTime.Now.ToLongTimeString();
            b2.Owner = owner;
            b2.Description = "zzzzzzzz" + DateTime.Now.ToLongTimeString();

            var count = controller.Put(id1, b2);
            Assert.AreEqual(count, 1);

            var b3 = controller.Get(id1);
            Assert.AreEqual(b1.Id, b2.Id);
            Assert.AreNotEqual(b1.Owner, b3.Owner);
        }

        [TestMethod]
        public void Delete()
        {
            BlogController controller = new BlogController();

            var blog = CreateNewBlog();
            //blog.Id = "999";

            controller.Post(blog);
            Assert.IsNotNull(blog.Id);
            var id1 = blog.Id;

            controller.Delete(id1);

            var b2 = controller.Get(id1);
            Assert.IsNull(b2);
        }
    }
}
