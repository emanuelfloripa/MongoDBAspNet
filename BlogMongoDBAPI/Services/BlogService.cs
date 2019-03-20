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
            ObjectId oid = new ObjectId(id);
            return _blogs.Find<BlogModel>(blog => blog._Id == oid).FirstOrDefault();
        }

        public BlogModel Insert(BlogModel blog)
        {
            _blogs.InsertOne(blog);
            return blog;
        }

        public int Update(string id, BlogModel blogIn)
        {
            ObjectId oid = new ObjectId(id);
            blogIn._Id = oid;
            var result = _blogs.ReplaceOne(blog => blog._Id == oid, blogIn);
            return (int)result.ModifiedCount;
        }

        public void Remove(BlogModel blogIn)
        {
            _blogs.DeleteOne(blog => blog._Id == blogIn._Id);
        }

        public void Remove(string id)
        {
            ObjectId oid = new ObjectId(id);
            _blogs.DeleteOne(blog => blog._Id == oid);
        }
    }
}