using System;
using System.Collections.Generic;
using System.Diagnostics;
using BlogMongoDBAPI.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogMongoDBAPI.Services
{
    internal class PostBlogService
    {

        private readonly IMongoCollection<PostModel> _db;

        public PostBlogService(string conn)
        {
            var client = new MongoClient(conn);
            var dataBase = client.GetDatabase(Consts.DBName);
            _db = dataBase.GetCollection<PostModel>("Posts");
        }

        public List<PostModel> Get(ObjectId idBlog)
        {
            return _db.Find<PostModel>(post => post.idBlog == idBlog).ToList();
        }

        public PostModel Get(MongoDB.Bson.ObjectId idBlog, string id)
        {
            var lista = _db.Find<PostModel>(post => (post.idBlog == idBlog) && (post.Id == id));
            return lista.First();
        }

        /// <summary>
        /// Insert de um novo post em um blog
        /// </summary>
        /// <param name="post"></param>
        /// <returns>O ID do Post inserido</returns>
        internal string Insert(string idBlog, PostModel p)
        {
            ObjectId oid = new ObjectId(idBlog);
            p.idBlog = oid;
            _db.InsertOne(p);
            return p.Id;
        }

        /// <summary>
        /// Update de um post de um blog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postIn"></param>
        /// <returns></returns>
        internal bool Update(string idBlog, PostModel p)
        {
            ObjectId oid = new ObjectId(idBlog);
            return Update(oid, p);
        }

        internal bool Update(ObjectId idBlog, PostModel p)
        {
            var result = _db.ReplaceOne(post => post.Id == p.Id, p);
            return result.ModifiedCount == 1;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>TRUE se achou e conseguiu deletar o registro</returns>
        public bool Remove(string id)
        {
            var result = _db.DeleteOne(post => post.Id == id);
            return result.DeletedCount == 1;
        }

        //public string AddSecao(PostModel post, SecaoModel secao)
        //{
        //    secao.Id = ObjectId.GenerateNewId().ToString();
        //    post.Secoes.Add(secao);
        //    Update(post.idBlog, post);
        //    return secao.Id;
        //}

        //public string AddSecao(SecaoModel secaoPai, SecaoModel secaoFilho)
        //{
        //    secao.Id = ObjectId.GenerateNewId().ToString();
        //    post.Secoes.Add(secao);
        //    Update(post.idBlog, post);
        //    return secao.Id;
        //}

        //public SecaoModel GetSecao(PostModel post, string id)
        //{
        //    var secao = post.Secoes.Find(p => p.Id == id);
        //    return secao;
        //}

        //public int IndexOfSecaoID(PostModel post, string idSecao)
        //{
        //    var index = post.Secoes.FindIndex(secao => secao.Id == idSecao);
        //    return index;
        //}

        //public void UpdateSecao(PostModel post, SecaoModel secao, string idSecao)
        //{
        //    var index = IndexOfSecaoID(post, idSecao);
        //    post.Secoes[index] = secao;
        //    Update(post.idBlog, post);
        //}

        //public void RemoveSecao(PostModel post, string idSecao)
        //{
        //    var index = IndexOfSecaoID(post, idSecao);
        //    post.Secoes.RemoveAt(index);
        //    Update(post.idBlog, post);
        //}

    }
}