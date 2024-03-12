using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SuccessMessage : IOutputMessage
    {
        private readonly string _message;
        public string Message => _message;

        public bool Error => false;

        public SuccessMessage(string message)
        {
            _message = message;
        }      
    }
}
