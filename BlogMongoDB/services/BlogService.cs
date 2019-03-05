using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogMongoDB.model;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BlogMongoDB.services
{
    public class BlogService
    {

        private readonly IMongoCollection<Blog> _blogs;

        public BlogService(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("BlogStoreDB"));
            var dataBase = client.GetDatabase("BlogStoreDB");
            _blogs = dataBase.GetCollection<Blog>("Blog");
        }

        public List<Blog> Get()
        {
            return _blogs.Find(blog => true).ToList();
        }

        public Blog Get(string id)
        {
            return _blogs.Find<Blog>(blog => blog.Id == id).FirstOrDefault();
        }

        public Blog Create(Blog blog)
        {
            _blogs.InsertOne(blog);
            return blog;
        }

        public void Update(string id, Blog blogIn)
        {
            _blogs.ReplaceOne(blog => blog.Id == id, blogIn);
        }

        public void Remove(Blog blogIn)
        {
            _blogs.DeleteOne(blog => blog.Id == blogIn.Id);
        }

        public void Remove(string id)
        {
            _blogs.DeleteOne(blog => blog.Id == id);
        }
    }
}