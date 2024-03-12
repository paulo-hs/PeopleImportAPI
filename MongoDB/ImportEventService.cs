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
    public class ImportEventService : IDataBaseImportEventService
    {
        private readonly IDataBaseConnService _connService;
        private readonly string _collectionString = "Events";

        private readonly IMongoCollection<ImportEvent> _collection;

        public ImportEventService(IDataBaseConnService connService)
        {
            _connService = connService;
            _collection = connService.Database.GetCollection<ImportEvent>(_collectionString);
        }

        public IEnumerable<ImportEvent> GetImportEvents()
        {
            var currentDate = DateTime.Now.Date;
            var filter = Builders<ImportEvent>.Filter.Gte(e => e.EventDate, currentDate) &
                                  Builders<ImportEvent>.Filter.Lt(e => e.EventDate, currentDate.AddDays(1));

            return _collection.Find(filter).ToList();
        }

        public ImportEvent InsertImportEvent(ImportEvent importEvent)
        {
            _collection.InsertOne(importEvent);
            return importEvent;
        }

        public void UpdateImportEventStatus(ImportEvent importEvent)
        {
            var filter = Builders<ImportEvent>.Filter.Eq(e => e.Id, importEvent.Id); // Filtro para encontrar o documento a ser atualizado
            var update = Builders<ImportEvent>.Update.Set(e => e.Status, importEvent.Status);
            _collection.UpdateOne(filter, update);
        }
    }
}
