using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImportService : Domain.Interfaces.IImportService
    {
        private readonly IDataBasePersonService _dataBasePersonService;
        private readonly IDataBaseImportEventService _dataBaseImportEventService;

        public ImportService(IDataBasePersonService dataBasePersonService, IDataBaseImportEventService dataBaseImportEventService)
        {
            _dataBasePersonService = dataBasePersonService;
            _dataBaseImportEventService = dataBaseImportEventService;
        }

        public Task ImportFile(string fileName, List<string> fileStringLines)
        {            
            return Task.Run(() => {
                var insertEvent = _dataBaseImportEventService.InsertImportEvent(new ImportEvent(fileName));

                string[] fields = [];
                foreach(string line in  fileStringLines)
                {
                    if (line == fileStringLines.First())
                        continue;

                    fields = line.Split(";");
                    if(fields.Length == 7)
                    {
                        _dataBasePersonService.InsertPerson(new Person(fields));
                    }                    
                }

                insertEvent.Status = EventStatus.done;
                _dataBaseImportEventService.UpdateImportEventStatus(insertEvent);
            });
        }

        public IEnumerable<ImportEvent> ListImportEvents()
        {
            return _dataBaseImportEventService.GetImportEvents();
        }
    }
}
