using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BlogMongoDBAPI.Models
{
    public class SecaoModel
    {
        public SecaoModel()
        {
            Secoes = new List<SecaoModel>();
        }

        public SecaoModel getSecao(string id)
        {
            var secao = Secoes.Find(s => s.Id == id);
            return secao;
        }

        public void addSecao(SecaoModel secao)
        {
            if (secao.Id == null)
                secao.Id = ObjectId.GenerateNewId().ToString();
            Secoes.Add(secao);
        }

        public bool removeSecao(string id)
        {
            var secao = getSecao(id);
            if (secao == null)
                return false;

            return Secoes.Remove(secao);
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("SubTitulo")]
        [BsonRequired]
        public string SubTitulo { get; set; }

        [BsonElement("Conteudo")]
        public string Conteudo { get; set; }

        [BsonElement("Secoes")]
        public List<SecaoModel> Secoes { get; set; }
    }
}