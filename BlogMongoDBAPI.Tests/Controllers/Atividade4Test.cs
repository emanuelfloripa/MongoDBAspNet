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

        [TestMethod]
        public void CadastrarBlogComUmUsuario()
        {
            BlogController blogCtr = new BlogController();

            BlogModel blog = new BlogModel
            {
                Owner = "Emanuel Espíndola",
                Password = "123",
                Description = "Blog teste para validação do projeto",
            };

            //blogCtr.Post(blog);

            //Validação
            

        }






    }
}
