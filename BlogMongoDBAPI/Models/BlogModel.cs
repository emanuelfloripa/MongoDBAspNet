using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BlogMongoDBAPI.Models
{

    /*
     * Passo a passo para elaborar a API.
     * https://docs.microsoft.com/pt-br/aspnet/core/tutorials/first-mongo-app?view=aspnetcore-2.2&tabs=visual-studio
     * 
     */

    public class BlogModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _Id { get; set; }  

        [BsonElement("OwnerName")]
        [BsonRequired]
        public string OwnerName { get; set; }

        [BsonElement("Login")]
        [BsonRequired]
        public string Login { get; set; }

        [BsonElement("Password")]
        [BsonRequired]
        public string Password { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

    }
}
 