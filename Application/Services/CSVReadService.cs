using Microsoft.AspNetCore.Routing.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CSVReadService : Domain.Interfaces.IFileReadService
    {
        private List<string> _lines = new List<string>();
        private string _fileName = string.Empty;

        public List<string> Lines => _lines;
        public string FileName => _fileName;

        public void Load(byte[] file, string fileName)
        {
            _fileName = fileName;
            using (var ms = new MemoryStream(file))
            {
                using (var reader = new StreamReader(ms, Encoding.UTF8))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        _lines.Add(line);
                    }
                }
            }
        }

        public int CountLines()
        {
            return _lines.Count;
        }

        public bool ValidateSchema()
        {
            if (CountLines() == 0)
                return false;

            string firstLine = Lines.First();
            return firstLine.ToLower().Equals("nome;cpf;endereço;cidade;estado;ddd;telefone");
        }
    }
}
