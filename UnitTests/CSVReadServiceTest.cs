using Application.Services;
using System.Text;

namespace UnitTests
{
    public class CSVReadServiceTest
    {
        private CSVReadService _csvReadService;

        public CSVReadServiceTest()
        {
            _csvReadService = new CSVReadService();
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
            _csvReadService.Load(mockFile(), fileName);

            Assert.Equal(101, _csvReadService.Lines.Count());
            Assert.Equal(fileName, _csvReadService.FileName);
        }

        [Fact]
        public void EmptyFile_LoadFile_ShouldHaveEmptyLines()
        {
            string fileName = "Test";
            _csvReadService.Load(mockFileEmpty(), fileName);

            Assert.Equal(0, _csvReadService.Lines.Count());
            Assert.Equal(fileName, _csvReadService.FileName);
        }

        [Fact]
        public void ValidFile_CountLines_ShouldReturn101()
        {
            string fileName = "Test";
            _csvReadService.Load(mockFile(), fileName);

            Assert.Equal(101, _csvReadService.CountLines());
            Assert.Equal(fileName, _csvReadService.FileName);
        }

        [Fact]
        public void ValidFile_ValidateSchema_ShouldReturnTrue()
        {
            string fileName = "Test";
            _csvReadService.Load(mockFile(), fileName);

            Assert.True(_csvReadService.ValidateSchema());
        }

        [Fact]
        public void EmptyFile_ValidateSchema_ShouldReturnFalse()
        {
            string fileName = "Test";
            _csvReadService.Load(mockFileInvalid(), fileName);

            Assert.False(_csvReadService.ValidateSchema());
        }
    }
}
