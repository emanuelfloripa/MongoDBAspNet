using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BlogMongoDBAPI.Models
{
    public class SecaoModel
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SubTitulo")]
        public string SubTitulo { get; set; }

        [BsonElement("Conteudo")]
        public string Datahora { get; set; }

        [BsonElement("Secoes")]
        public List<SecaoModel> Secoes { get; set; }
    }
}