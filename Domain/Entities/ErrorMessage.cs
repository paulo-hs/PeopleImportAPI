using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ErrorMessage : IOutputMessage
    {
        private readonly string _message;
        public string Message => _message;
        public bool Error => true;

        public ErrorMessage(string message)
        {
            _message = message;
        }
    }
}
