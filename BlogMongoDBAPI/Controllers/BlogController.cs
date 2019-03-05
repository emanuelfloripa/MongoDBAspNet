﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BlogMongoDBAPI.Models;
using BlogMongoDBAPI.Services;

namespace BlogMongoDBAPI.Controllers
{
    //[Authorize]
    public class BlogController : ApiController
    {

        private readonly BlogService _blogService;

        public BlogController()
        {
            var conn = System.Configuration.ConfigurationManager.ConnectionStrings[Consts.ConnStringName].ConnectionString;
            _blogService = new BlogService(conn);
        }

        public IEnumerable<BlogViewModels> Get()
        {
            var blog = _blogService.Get();
            //if (blog == null)
            //    return NotFound();
            return blog;
        }

        public BlogViewModels Get(string id)
        {
            return _blogService.Get(id);
        }

        public void Post([FromBody]BlogViewModels value)
        {
            _blogService.Insert(value);
        }

        public int Put(string id, [FromBody]BlogViewModels value)
        {
            var result = _blogService.Update(id, value);
            return (int)result.ModifiedCount;

        }

        public void Delete(string id)
        {
            _blogService.Remove(id);
        }
    }
}

