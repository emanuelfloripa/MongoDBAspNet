using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogMongoDBAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BlogMongoDBAPI.Services
{
    public class BlogService
    {

        private readonly IMongoCollection<BlogModel> _blogs;

        //public BlogService(IConfiguration config)
        public BlogService(string conn)
        {
            //var client = new MongoClient(config.GetConnectionString("BlogStoreDB"));
            var client = new MongoClient(conn);
            var dataBase = client.GetDatabase(Consts.DBName);
            _blogs = dataBase.GetCollection<BlogModel>("Blog");
        }

        public List<BlogModel> Get()
        {
            return _blogs.Find(blog => true).ToList();
        }

        public BlogModel Get(string id)
        {
            return _blogs.Find<BlogModel>(blog => blog._Id == id).FirstOrDefault();
        }

        public string Insert(BlogModel blog)
        {
            _blogs.InsertOne(blog);
            return blog._Id;
        }

        public int Update(string id, BlogModel blogIn)
        {
            blogIn._Id = id;
            var result = _blogs.ReplaceOne(blog => blog._Id == id, blogIn);
            return (int)result.ModifiedCount;
        }

        public void Remove(BlogModel blogIn)
        {
            _blogs.DeleteOne(blog => blog._Id == blogIn._Id);
        }

        public void Remove(string id)
        {
            _blogs.DeleteOne(blog => blog._Id == id);
        }

        internal string Login(string idBlog, string v1, string v2)
        {
            var blog = _blogs.Find(b => (b._Id == idBlog) & (b.Login == v1) & (b.Password == v2)).FirstOrDefault();
            if (blog == null)
                return "";
            else
                return ObjectId.GenerateNewId().ToString();
        }
    }
}