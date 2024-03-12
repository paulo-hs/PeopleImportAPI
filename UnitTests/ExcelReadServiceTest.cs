using Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace UnitTests
{
    public class ExcelReadServiceTest
    {
        private ExcelReadService _excelReadService;

        public ExcelReadServiceTest()
        {
            _excelReadService = new ExcelReadService();
        }

        private byte[] mockFile() { 
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("nome;cpf;endereço;cidade;estado;ddd;telefone");
            for(int i = 1; i <= 100; i++)
            {
                sb.AppendLine("nome;cpf;endereço;cidade;estado;ddd;telefone");
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private byte[] mockFileInvalid()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("nome;cpf;endereço;cidade;estado");
            for (int i = 1; i <= 100; i++)
            {
                sb.AppendLine("nome;cpf;endereço;cidade;estado;ddd;telefone");
            }
            return Encoding.UTF8.GetBytes(sb.ToString());
        }

        private byte[] mockFileEmpty()
        {            
            return Encoding.UTF8.GetBytes("");
        }

        [Fact]
        public void ValidFile_LoadFile_ShouldFillLines()
        {
            string fileName = "Test";
            _excelReadService.Load(mockFile(), fileName);

            Assert.Equal(101, _excelReadService.Lines.Count());
            Assert.Equal(fileName, _excelReadService.FileName);
        }

        [Fact]
        public void EmptyFile_LoadFile_ShouldHaveEmptyLines()
        {
            string fileName = "Test";
            _excelReadService.Load(mockFileEmpty(), fileName);

            Assert.Equal(0, _excelReadService.Lines.Count());
            Assert.Equal(fileName, _excelReadService.FileName);
        }

        [Fact]
        public void ValidFile_CountLines_ShouldReturn101()
        {
            string fileName = "Test";
            _excelReadService.Load(mockFile(), fileName);

            Assert.Equal(101, _excelReadService.CountLines());
            Assert.Equal(fileName, _excelReadService.FileName);
        }

        [Fact]
        public void ValidFile_ValidateSchema_ShouldReturnTrue()
        {
            string fileName = "Test";
            _excelReadService.Load(mockFile(), fileName);

            Assert.True(_excelReadService.ValidateSchema());
        }

        [Fact]
        public void EmptyFile_ValidateSchema_ShouldReturnFalse()
        {
            string fileName = "Test";
            _excelReadService.Load(mockFileInvalid(), fileName);

            Assert.False(_excelReadService.ValidateSchema());
        }
    }
}
