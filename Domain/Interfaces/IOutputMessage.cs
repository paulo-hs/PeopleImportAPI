using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOutputMessage
    {
        string Message { get; }
        bool Error { get; }
    }
}
