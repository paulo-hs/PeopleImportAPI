using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public enum EventStatus
    {
        processing,
        done
    }

    public class ImportEvent
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public DateTime EventDate { get; set; }
        public string FileName { get; set; }
        public EventStatus Status {  get; set; } 

        public ImportEvent(string fileName)
        {
            FileName = fileName;
            EventDate = DateTime.Now;
            Status = EventStatus.processing;
        }
    }
}
