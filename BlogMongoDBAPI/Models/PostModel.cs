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

        public PostModel()
        {
            Secoes = new List<SecaoModel>();
        }

        public SecaoModel getSecao(string id)
        {
            var secao = this.Secoes.Find(s => s.Id == id);
            return secao;
        }

        public string addSecao(SecaoModel secao)
        {
            if (secao.Id == null)
                secao.Id = ObjectId.GenerateNewId().ToString();
            Secoes.Add(secao);
            return secao.Id;
        }

        public bool removeSecao(string id)
        {
            var secao = this.Secoes.Find(s => s.Id == id);
            if (secao == null)
                return false;

            return Secoes.Remove(secao);
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("IdBlog")]
        [BsonRequired]
        public string IdBlog { get; set; }

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