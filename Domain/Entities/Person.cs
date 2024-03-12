using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Person
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Endereco { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }

        public Person(string nome, string cPF, string endereco, string cidade, string estado, string dDD, string telefone)
        {
            Nome = nome;
            CPF = cPF;
            Endereco = endereco;
            Cidade = cidade;
            Estado = estado;
            DDD = dDD;
            Telefone = telefone;
        }
        public Person(string[] fields)
        {
            Nome = fields[0];
            CPF = fields[1];
            Endereco = fields[2];
            Cidade = fields[3];
            Estado = fields[4];
            DDD = fields[5];
            Telefone = fields[5];
        }
    }
}
