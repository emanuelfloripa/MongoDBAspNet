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
                Datahora = DateTime.Now
            };
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
            ctr.Remove(idBlog, p1.Id);

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
            string idSecao = post.addSecao(CreateSecao()); // ctr.AddSecao(post, CreateSecao());
            ctr.Post(idBlog, post);

            Assert.IsTrue(idSecao.Length > 10);

            var p2 = ctr.Get(idBlog, post.Id);
            var achouSecao = p2.getSecao(idSecao) != null;

            Assert.IsTrue(achouSecao);
        }

        [TestMethod]
        public void GetListSecao()
        {
            var ctrPost = Controller();
            var idBlog = GetOneBlog()._Id.ToString();
            var listPosts = ctrPost.Get(idBlog);
            if (listPosts.Count == 0)
                Assert.Inconclusive("Sem posts no blog selecionado.");

            var post = listPosts.First();
            var countSecoes = post.Secoes.Count;

            SecaoModel secao;
            do
            {
                secao = CreateSecao();
                post.addSecao(secao);
                countSecoes++;
            } while (countSecoes < 5);

            ctrPost.Put(idBlog, post);

            var p2 = ctrPost.Get(idBlog, post.Id);
            Assert.IsTrue(p2.Secoes.Count >= 5);

            var s2 = p2.getSecao(secao.Id);
            Assert.IsNotNull(s2);
            Assert.IsTrue(s2.Id == secao.Id);
        }


        [TestMethod]
        public void UpdateSecao()
        {
            var ctrPost = Controller();
            var idBlog = GetOneBlog()._Id.ToString();
            var listPosts = ctrPost.Get(idBlog);

            var post1 = CreateNewPost();
            var secao1 = CreateSecao();
            post1.addSecao(secao1);
            ctrPost.Post(idBlog, post1);

            var conteudoAlterado = DateTime.Now.ToLongTimeString() + "XXXXXXXXXXXXXXX";
            secao1.Conteudo = conteudoAlterado;
            ctrPost.Put(idBlog, post1);

            var post2 = ctrPost.Get(idBlog, post1.Id);
            var secao2 = post2.getSecao(secao1.Id);

            Assert.AreEqual(secao1.Id, secao2.Id);
        }

        [TestMethod]
        public void AddSecaoAninhadas()
        {
            var ctrPost = Controller();
            var idBlog = GetOneBlog()._Id.ToString();

            var post1 = CreateNewPost();
            var secao1 = CreateSecao();
            var secao11 = CreateSecao();
            var secao12 = CreateSecao();
            var secao121 = CreateSecao();

            // aninhando as seções
            post1.addSecao(secao1);
            secao1.addSecao(secao11);
            secao1.addSecao(secao12);
            secao12.addSecao(secao121);
            ctrPost.Post(idBlog, post1);

            var post2 = ctrPost.Get(idBlog, post1.Id);
            var s1 = post2.getSecao(secao1.Id);
            var s11 = s1.getSecao(secao11.Id);
            Assert.IsNotNull(s11);

            var s12 = s1.getSecao(secao12.Id);
            Assert.IsNotNull(s12);

            var s121 = s12.getSecao(secao121.Id);
            Assert.IsNotNull(s121);
        }

        [TestMethod]
        public void DeleteSecaoPai()
        {
            var ctrPost = Controller();
            var idBlog = GetOneBlog()._Id.ToString();

            var post1 = CreateNewPost();
            var secao1 = CreateSecao();
            var filha1 = CreateSecao();
            var filha2 = CreateSecao();
            var neta1 = CreateSecao();

            // aninhando as seções
            post1.addSecao(secao1);
            secao1.addSecao(filha1);
            secao1.addSecao(filha2);
            filha2.addSecao(neta1);
            ctrPost.Post(idBlog, post1);

            // IDs de referência - São gerados nos métodos addSecao()
            var idPost1 = post1.Id;
            var idSecao1 = secao1.Id;

            // Deletando a seção do post
            var post2 = ctrPost.Get(idBlog, idPost1);
            Assert.AreEqual(post2.Secoes.Count, 1);
            post2.removeSecao(idSecao1);
            ctrPost.Put(idBlog, post2);

            // Verificando em nova leitura
            var post3 = ctrPost.Get(idBlog, idPost1);
            Assert.AreEqual(post3.Secoes.Count, 0);


        }

        [TestMethod]
        public void DeleteSecaoAninhada()
        {
            var ctrPost = Controller();
            var idBlog = GetOneBlog()._Id.ToString();

            var post1 = CreateNewPost();
            var secao1 = CreateSecao();
            var filha1 = CreateSecao();
            var filha2 = CreateSecao();
            var neta1 = CreateSecao();


            // aninhando as seções
            post1.addSecao(secao1);
            secao1.addSecao(filha1);
            secao1.addSecao(filha2);
            filha2.addSecao(neta1);
            ctrPost.Post(idBlog, post1);

            // IDs de referência - São gerados nos métodos addSecao()
            var idPost1 = post1.Id;
            var idSecao1 = secao1.Id;
            var idFilha1 = filha1.Id;
            var idFilha2 = filha2.Id;
            var idNeta1 = neta1.Id;

            // T1 Removendo Neta
            var post2 = ctrPost.Get(idBlog, idPost1);
            var t1_secao1 = post2.getSecao(idSecao1);
            var t1_filha2 = t1_secao1.getSecao(idFilha2);
            t1_filha2.removeSecao(idNeta1);
            Assert.IsTrue(t1_filha2.Secoes.Count == 0);
            ctrPost.Put(idBlog, post2);

            // T2 Conferindo se neta foi removida em nova leitura
            var post3 = ctrPost.Get(idBlog, idPost1);
            var t2_secao1 = post2.getSecao(idSecao1);
            var t2_filha2 = t1_secao1.getSecao(idFilha2);
            Assert.IsTrue(t2_filha2.Secoes.Count == 0);

        }
    }
}
