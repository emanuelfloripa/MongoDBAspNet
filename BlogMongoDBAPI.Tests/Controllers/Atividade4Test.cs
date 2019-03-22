using BlogMongoDBAPI.Controllers;
using BlogMongoDBAPI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogMongoDBAPI.Tests.Controllers
{
    [TestClass]
    public class Atividade4Test
    {
        /// <summary>
        /// 1.O sistema deverá registrar o conteúdo de blogs a serem publicados na internet. 
        /// 2.Um Blog terá unicamente um usuário “proprietário”. 
        /// 3.Um Blog pode conter uma descrição simples indicando sua finalidade.
        /// 8. O sistema deve suportar que um usuário anônimo crie uma nova conta. 
        /// </summary>
        [TestMethod]
        public void T1_CadastrarBlogComUmUsuario()
        {
            var ownerName = "Teste1 VariasPostagens";
            var login = $"emanuel1_{DateTime.Now.ToString("hhMMss")}";
            var password = "1234";
            var description = "Blog teste para validação do projeto";
            BlogController blogCtr = new BlogController();

            BlogModel blog = new BlogModel
            {
                OwnerName = ownerName,
                Login = login,
                Password = password,
                Description = description,
            };

            var idBlog = blogCtr.Post(blog);

            // V A L I D A Ç Ã O ///////////////////////

            var leituraBlog = blogCtr.Get(idBlog);

            Assert.IsNotNull(leituraBlog);
            Assert.AreEqual(ownerName, leituraBlog.OwnerName);
            Assert.AreEqual(login, leituraBlog.Login);
            Assert.AreEqual(password, leituraBlog.Password);
            Assert.AreEqual(description, leituraBlog.Description);
        }

        /// <summary>
        /// 4. Um blog pode conter “0-n” postagens (posts). 
        /// 5. Uma postagem pode conter uma estrutura dinâmica, contendo minimamente o título da postagem, o conteúdo, e a data e hora de publicação. 
        /// </summary>
        [TestMethod]
        public void T2_VariasPostagens()
        {
            // REQ4

            var ownerName = "Teste2 VariasPostagens";
            var login = $"emanuel2_{DateTime.Now.ToString("hhMMss")}";
            var password = "1234";
            var description = "T2 Blog teste para validação do projeto";
            BlogController blogCtr = new BlogController();

            var idBlog = blogCtr.Post(
                new BlogModel
                {
                    OwnerName = ownerName,
                    Login = login,
                    Password = password,
                    Description = description
                });

            PostController postCtr = new PostController();

            for (int i = 0; i < 100; i++)
            {
                postCtr.Post(idBlog,
                    new PostModel
                    {
                        IdBlog = idBlog,
                        Datahora = DateTime.Now,
                        Titulo = "T3" + DateTime.Now.ToLongTimeString()
                    });
            }

            // V A L I D A Ç Ã O ///////////////////////

            var listaPosts = postCtr.Get(idBlog);

            Assert.IsNotNull(listaPosts);
            Assert.AreEqual(100, listaPosts.Count);
            Assert.AreEqual(idBlog, listaPosts[99].IdBlog);
        }

        /// <summary>
        /// 6. O conteúdo de uma postagem pode opcionalmente ser fracionado um “seções”, 
        ///    devendo estas estar ordenadas, e conter um “subtítulo” e seu conteúdo. 
        /// 7. O conteúdo de uma seção, também pode conter outras “subseções”. 
        /// </summary>
        [TestMethod]
        public void T3_SecoesAninhadas()
        {
            var ownerName = "Teste3 VariasPostagens";
            var login = $"emanuel3_{DateTime.Now.ToString("hhMMss")}";
            var password = "1234";
            var description = "T3 Blog teste para validação do projeto";
            BlogController blogCtr = new BlogController();
            var idBlog = blogCtr.Post(
                new BlogModel
                {
                    OwnerName = ownerName,
                    Login = login,
                    Password = password,
                    Description = description
                });

            PostController postCtr = new PostController();

            var postagem = new PostModel
            {
                IdBlog = idBlog,
                Datahora = DateTime.Now,
                Titulo = $"T3_Post[{DateTime.Now.ToLongTimeString()}]"
            };

            // 4 filhas com 3 netas em cada uma
            for (int i = 0; i < 4; i++)
            {
                postagem.addSecao(new SecaoModel
                {
                    SubTitulo = $"T3_SecaoFilha[{i}]",
                    Conteudo = "Teste3 com algum texto qualquer"
                });

                for (int j = 0; j < 3; j++)
                {
                    postagem.Secoes[i].addSecao(new SecaoModel
                    {
                        SubTitulo = $"T3_SecaoNeta[{i},{j}]",
                        Conteudo = "Teste3 com algum texto qualquer"
                    });
                }
            }

            var idPost = postCtr.Post(idBlog, postagem);

            // V A L I D A Ç Ã O ///////////////////////

            // várias leituras diferentes
            var secao_0_1 = postCtr.Get(idBlog, idPost).Secoes[0].Secoes[1];
            var secao_2_2 = postCtr.Get(idBlog, idPost).Secoes[2].Secoes[2];
            var secao_3_0 = postCtr.Get(idBlog, idPost).Secoes[3].Secoes[0];
            // Conferindo os títulos gerados
            Assert.IsNotNull(secao_0_1);
            Assert.AreEqual("T3_SecaoNeta[0,1]", secao_0_1.SubTitulo);
            Assert.AreEqual("T3_SecaoNeta[2,2]", secao_2_2.SubTitulo);
            Assert.AreEqual("T3_SecaoNeta[3,0]", secao_3_0.SubTitulo);

        }

        /// <summary>
        /// 9. O sistema deve suportar que o usuário anônimo, com conta criada, se autentique (login e senha). 
        /// </summary>
        [TestMethod]
        public void T4_Login()
        {
            var ownerName = "Teste4 VariasPostagens";
            var login = "emanuel4";
            var password = "1234";
            var description = "T4 Blog teste para validação do projeto";
            BlogController blogCtr = new BlogController();
            var idBlog = blogCtr.Post(new BlogModel
            {
                OwnerName = ownerName,
                Login = login,
                Password = password,
                Description = description
            });

            //var tokenInvalido = blogCtr.Login(idBlog, "emanuel4", "senhaInvalida");
            //var token = blogCtr.Login(idBlog, "emanuel4", "1234");



        }
    }
}
