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

        [BsonElement("DataHora")]
        public DateTime Datahora { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }


    }
}