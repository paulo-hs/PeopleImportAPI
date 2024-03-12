using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFileReadService
    {
        List<string> Lines { get; }
        string FileName { get; }
        void Load(byte[] file, string fileName);
        int CountLines();
        bool ValidateSchema();
    }
}
