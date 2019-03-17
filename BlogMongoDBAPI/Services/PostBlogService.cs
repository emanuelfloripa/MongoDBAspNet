using System;
using System.Collections.Generic;
using BlogMongoDBAPI.Models;
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

        public List<PostModel> Get(string idBlog)
        {
            return _db.Find<PostModel>(post => post.idBlog == idBlog).ToList();
        }

        public PostModel Get(string idBlog, string id)
        {
            return _db.Find<PostModel>(post => post.idBlog == idBlog).FirstOrDefault();
        }

        /// <summary>
        /// Insert de um novo post em um blog
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        internal void Insert(string idBlog, PostModel p)
        {
            p.idBlog = idBlog;
            _db.InsertOne(p);
        }

        /// <summary>
        /// Update de um post de um blog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postIn"></param>
        /// <returns></returns>
        internal int Update(string idBlog, PostModel p)
        {
            var result = _db.ReplaceOne(post => post.Id == p.Id, p);
            return (int)result.ModifiedCount;
        }

        public void Remove(string id)
        {
            _db.DeleteOne(post => post.Id == id);
        }

        public void AddSecao(PostModel post, SecaoModel secao)
        {
            post.Secoes.Add(secao);
            Update(post.idBlog, post);
        }

        public SecaoModel GetSecao(PostModel post, string id )
        {
            var secao = post.Secoes.Find(p => p.Id == id);
            return secao;
        }

        public int IndexOfSecaoID(PostModel post, string idSecao)
        {
            var index = post.Secoes.FindIndex(secao => secao.Id == idSecao);
            return index;
        }

        public void UpdateSecao(PostModel post, SecaoModel secao, string idSecao)
        {
            var index = IndexOfSecaoID(post, idSecao);
            post.Secoes[index] = secao;
            Update(post.idBlog, post);
        }

        public void RemoveSecao(PostModel post, string idSecao)
        {
            var index = IndexOfSecaoID(post, idSecao);
            post.Secoes.RemoveAt(index);
            Update(post.idBlog, post);
        }

    }
}