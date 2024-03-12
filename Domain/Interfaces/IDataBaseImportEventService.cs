using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IDataBaseImportEventService
    {
        IEnumerable<ImportEvent> GetImportEvents();
        ImportEvent InsertImportEvent(ImportEvent importEvent);
        void UpdateImportEventStatus(ImportEvent importEvent);
    }
}
