using System;
using System.Collections.Generic;
using BlogMongoDBAPI.Models;
using MongoDB.Driver;

namespace BlogMongoDBAPI.Services
{
    internal class PostBlogService
    {
        //private readonly IMongoCollection<PostModel> _db;
        BlogModel _blog;

        public PostBlogService(BlogModel blog)
        {
            //var client = new MongoClient(conn);
            //var dataBase = client.GetDatabase(Consts.DBName);
            //_db = dataBase.GetCollection<PostModel>("Posts");
            _blog = blog;
        }

        public List<PostModel> Get()
        {
            //return _db.Find(post => true).ToList();
            return _blog.Posts;
        }

        public PostModel Get(string id)
        {
            return null;
            //return _db.Find<PostModel>(post => post.Id == id).FirstOrDefault();
        }

        internal void Post(PostModel p)
        {
            throw new NotImplementedException();
        }

        internal void Put(PostModel post)
        {
            throw new NotImplementedException();
        }

        internal void Delete(string id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Insert de um novo post em um blog
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        public PostModel Insert(PostModel post)
        {
            //_db.InsertOne(post);
            return post;
        }

        /// <summary>
        /// Update de um post de um blog
        /// </summary>
        /// <param name="id"></param>
        /// <param name="postIn"></param>
        /// <returns></returns>
        public ReplaceOneResult Update(string id, PostModel postIn)
        {
            postIn.Id = id;
           // var result = _db.ReplaceOne(blog => blog.Id == id, postIn);
            return null;
        }

        /// <summary>
        /// Remove um registro de Post do Blog
        /// </summary>
        /// <param name="post"></param>
        public void Remove(PostModel post)
        {
            //_db.DeleteOne(blog => blog.Id == post.Id);
        }
        /// <summary>
        /// Remove um registro de Post do blog
        /// </summary>
        /// <param name="id"></param>
        public void Remove(string id)
        {
            //_db.DeleteOne(blog => blog.Id == id);
        }
    }
}