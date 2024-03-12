using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.MongoDB
{
    public class DataBaseConnService : Domain.Interfaces.IDataBaseConnService
    {           
        private readonly IMongoDatabase _database;

        public dynamic Database => _database;

        public DataBaseConnService()
        {
            try
            {
                var client = new MongoClient(Environment.GetEnvironmentVariable("MONGODBCONN"));                
                _database = client.GetDatabase(Environment.GetEnvironmentVariable("MONGODB"));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao MongoDB: {ex.Message}");
                throw new Exception("Erro ao conectar ao Banco de Dados");
            }
        }        
    }
}
