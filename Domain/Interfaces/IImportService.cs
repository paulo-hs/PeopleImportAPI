using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IImportService
    {
        Task ImportFile(string fileName, List<string> fileStringLines);
        IEnumerable<ImportEvent> ListImportEvents();
    }
}
