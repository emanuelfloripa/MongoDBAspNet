using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogMongoDBAPI.Models
{
    public class PostModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        [BsonElement("idBlog")]
        [BsonRequired]
        public ObjectId idBlog { get; set; }

        [BsonElement("Titulo")]
        [BsonRequired]
        public string Titulo { get; set; }

        [BsonElement("DataHora")]
        [BsonRequired]
        public DateTime Datahora { get; set; }

        [BsonElement("Secoes")]
        public List<SecaoModel> Secoes { get; set; }        
    }
}