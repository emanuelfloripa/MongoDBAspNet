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
        /// </summary>
        [TestMethod]
        public void CadastrarBlogComUmUsuario()
        {


            var ownerName = "Emanuel Espíndola";
            var login = "emanuel";
            var password = "123";
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

            //Validação
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
        public void VariasPostagens()
        {
            // REQ4

            var ownerName = "Emanuel Espíndola";
            var login = "emanuel";
            var password = "123";
            var description = "Blog teste para validação do projeto";
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
                        Titulo = DateTime.Now.ToLongTimeString() + "_Título"
                    });
            }

            // Validação
            var listaPosts = postCtr.Get(idBlog);

            Assert.IsNotNull(listaPosts);
            Assert.AreEqual(100, listaPosts.Count);
            Assert.AreEqual(idBlog, listaPosts[99].IdBlog);
        }



    }
}
