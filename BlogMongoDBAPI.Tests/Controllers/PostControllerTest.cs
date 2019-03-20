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
            var lista = ctr.Get(blog._Id.ToString());
            Assert.IsNotNull(lista);
        }

        [TestMethod]
        public void GetListWith3Post()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog()._Id.ToString();
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
            var idBlog = GetOneBlog()._Id.ToString();
            var idPost = ctr.Post(idBlog, post);

            var p2 = ctr.Get(idBlog, idPost);

            Assert.IsTrue(post.Id == idPost);
            Assert.IsTrue(post.Id == p2.Id);
        }

        [TestMethod]
        public void GetByIdIfExists()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog()._Id.ToString();
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
            var idBlog = GetOneBlog()._Id.ToString();
            var idPost = ctr.Post(idBlog, CreateNewPost());

            Assert.IsTrue(idPost.Length > 0);

            var p2 = ctr.Get(idBlog, idPost);

            Assert.IsTrue(idPost == p2.Id);
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

        private List<SecaoModel> CreateSecoes(int quantidade = 1)
        {
            var secoes = new List<SecaoModel>();
            for (int i = 0; i < quantidade; i++)
            {
                secoes.Add(CreateSecao());
            };
            return secoes;
        }

        private SecaoModel CreateSecao()
        {
            return new SecaoModel
            {
                Conteudo = DateTime.Now.ToShortTimeString() + "Conteúdo da seção",
                SubTitulo = "subtítulo seção" + DateTime.Now.ToShortTimeString()
            };
        }

        [TestMethod]
        public void Put()
        {
            var ctr = Controller();

            var blog = GetOneBlog();
            var idBlog = blog._Id.ToString();
            var i = blog.Posts.Count;
            while (i < 2)
            {
                var p = CreateNewPost();
                ctr.Post(idBlog, p);
                i++;
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
            var idBlog = GetOneBlog()._Id.ToString();
            var posts = ctr.Get(idBlog);

            Assert.IsNotNull(posts);

            if (posts.Count == 0)
                Assert.Inconclusive("O Blog obtido não possui posts para esse teste.");

            var b1 = posts.First();
            Assert.IsTrue(b1.Titulo.Length > 0);
        }

        [TestMethod]
        public void Delete()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog()._Id.ToString();
            var posts = ctr.Get(idBlog);

            var postCount = posts.Count;
            if (postCount == 0)
                Assert.Inconclusive("O Blog obtido não tem posts para esse teste.");

            var p1 = posts.First();
            var id1 = p1.Id;
            ctr.Delete(idBlog, p1.Id);

            Assert.IsNotNull(p1);

            var posts2 = ctr.Get(idBlog);
            Assert.IsTrue(postCount - 1 == posts2.Count);

            if (posts2.Count > 0)
            {
                // verificando se ainda existe alguem com o ID do cara deletado
                var p2 = posts2.Find(pp => pp.Id == id1);
                Assert.IsNull(p2, "O cara deletado ainda existe na lista lida do banco");
            }
        }

        [TestMethod]
        public void AddSecao()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog()._Id.ToString();

            var post = CreateNewPost();
            ctr.Post(idBlog, post);
            string idSecao = ctr.AddSecao(post, CreateSecao());

            Assert.IsTrue(idSecao.Length > 10);

            var p2 = ctr.Get(idBlog, post.Id);
            var achouSecao = p2.Secoes.Find(s => s.Id == idSecao) != null;

            Assert.IsTrue(achouSecao);
        }

        [TestMethod]
        public void GetListSecao()
        {
            var ctr = Controller();
            var idBlog = GetOneBlog()._Id.ToString();
            var listPosts = ctr.Get(idBlog);
            if (listPosts.Count == 0)
                Assert.Inconclusive("Sem posts no blog selecionado.");

            var post = listPosts.First();
            var countSecoes = post.Secoes.Count;

            while (countSecoes < 5)
            {
                ctr.AddSecao(post, CreateSecao());
                countSecoes++;
            }

            var p2 = ctr.Get(idBlog, post.Id);
            Assert.IsTrue(p2.Secoes.Count >= 5);


            Assert.Inconclusive("doing");
        }

        [TestMethod]
        public void GetOneSecao()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void UpdateSecao()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void AddSecaoAninhadas()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void GetSecaoAninhadas()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteSecaoPai()
        {
            Assert.Inconclusive();
        }

        [TestMethod]
        public void DeleteSecaoAninhada()
        {
            Assert.Inconclusive();
        }
    }
}
