using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BlogMongoDBAPI.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace BlogMongoDBAPI.Services
{
    public class BlogService
    {

        private readonly IMongoCollection<BlogViewModels> _blogs;

        //public BlogService(IConfiguration config)
        public BlogService(string conn)
        {
            //var client = new MongoClient(config.GetConnectionString("BlogStoreDB"));
            var client = new MongoClient(conn);
            var dataBase = client.GetDatabase(Consts.DBName);
            _blogs = dataBase.GetCollection<BlogViewModels>("Blog");
        }

        public List<BlogViewModels> Get()
        {
            return _blogs.Find(blog => true).ToList();
        }

        public BlogViewModels Get(string id)
        {
            return _blogs.Find<BlogViewModels>(blog => blog.Id == id).FirstOrDefault();
        }

        public BlogViewModels Insert(BlogViewModels blog)
        {
            _blogs.InsertOne(blog);
            return blog;
        }

        public ReplaceOneResult Update(string id, BlogViewModels blogIn)
        {
            blogIn.Id = id;
            var result = _blogs.ReplaceOne(blog => blog.Id == id, blogIn);
            return result;
        }

        public void Remove(BlogViewModels blogIn)
        {
            _blogs.DeleteOne(blog => blog.Id == blogIn.Id);
        }

        public void Remove(string id)
        {
            _blogs.DeleteOne(blog => blog.Id == id);
        }
    }
}