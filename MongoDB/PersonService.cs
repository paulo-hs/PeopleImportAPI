using Domain.Entities;
using Domain.Interfaces;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapter.MongoDB
{
    public class PersonService : IDataBasePersonService
    {
        private readonly IDataBaseConnService _connService;
        private readonly string _collectionString = "People";

        private readonly IMongoCollection<Person> _collection;

        public PersonService(IDataBaseConnService connService) 
        {
            _connService = connService;
            _collection = connService.Database.GetCollection<Person>(_collectionString);
        }

        public void InsertPerson(Person person)
        {
            _collection.InsertOne(person);
        }
    }
}
