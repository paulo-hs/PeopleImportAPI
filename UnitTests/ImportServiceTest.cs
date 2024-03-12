using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace UnitTests
{
    public class ImportServiceTest
    {
        private ImportService _importService;
        private Mock<IDataBasePersonService> _databasePersonService;
        private Mock<IDataBaseImportEventService> _databaseImportEventService;

        public ImportServiceTest()
        {
            _databasePersonService = new Mock<IDataBasePersonService>();
            _databaseImportEventService = new Mock<IDataBaseImportEventService>();
            _importService = new ImportService(_databasePersonService.Object, _databaseImportEventService.Object);
        }

        private List<string> mockLines() {
            List<string> list = new List<string>();
            list.Add("nome;cpf;endereço;cidade;estado;ddd;telefone");
            for(int i = 1; i <= 100; i++)
            {
                list.Add($"nome{i};cpf{i};endereço{i};cidade{i};estado{i};ddd{i};telefone{i}");
            }
            return list;
        }

        private List<string> mockLinesInvalid()
        {
            List<string> list = new List<string>();
            list.Add("nome;cpf;endereço;cidade;estado");
            list.Add($"nome0;cpf0;endereço0;cidade0;estado0;ddd0;telefone0");
            for (int i = 1; i <= 100; i++)
            {
                list.Add("nome{i};cpf{i};endereço{i};cidade{i};estado{i}");
            }
            return list;
        }

        private List<string> mockLinesEmpty()
        {
            return new List<string>();
        }

        private List<ImportEvent> mockListImportEvent()
        {
            List<ImportEvent> list = new List<ImportEvent>();
            int status = 0;
            for (int i = 1; i <= 100; i++)
            {
                list.Add(new ImportEvent($"Test{i}") { Status = (EventStatus)status });

                if (status == 0)
                    status++;
                else
                    status--;
            }
            return list;
        }

        [Fact]
        public void HundredLines_ImportFile_ShouldCall_Insert_Update_Events_Once_And_Insert_Person_HundredTimes()
        {
            _databaseImportEventService.Setup(e => e.InsertImportEvent(It.IsAny<ImportEvent>())).Returns(new ImportEvent("Teste"));

            _importService.ImportFile("Test", mockLines()).Wait();            

            _databaseImportEventService.Verify(e => e.InsertImportEvent(It.IsAny<ImportEvent>()), Times.Exactly(1));
            _databaseImportEventService.Verify(e => e.UpdateImportEventStatus(It.IsAny<ImportEvent>()), Times.Exactly(1));
            _databasePersonService.Verify(e => e.InsertPerson(It.IsAny<Person>()), Times.Exactly(100));
        }

        [Fact]
        public void OneValidLine_ImportFile_ShouldCall_Insert_Update_Events_Once_And_Insert_Person_Once()
        {
            _databaseImportEventService.Setup(e => e.InsertImportEvent(It.IsAny<ImportEvent>())).Returns(new ImportEvent("Teste"));

            _importService.ImportFile("Test", mockLinesInvalid()).Wait();

            _databaseImportEventService.Verify(e => e.InsertImportEvent(It.IsAny<ImportEvent>()), Times.Exactly(1));
            _databaseImportEventService.Verify(e => e.UpdateImportEventStatus(It.IsAny<ImportEvent>()), Times.Exactly(1));
            _databasePersonService.Verify(e => e.InsertPerson(It.IsAny<Person>()), Times.Exactly(1));
        }

        [Fact]
        public void ZeroLines_ImportFile_ShouldCall_Insert_Update_Events_Once_And_Insert_Person_Zero()
        {
            _databaseImportEventService.Setup(e => e.InsertImportEvent(It.IsAny<ImportEvent>())).Returns(new ImportEvent("Teste"));

            _importService.ImportFile("Test", mockLinesEmpty()).Wait();

            _databaseImportEventService.Verify(e => e.InsertImportEvent(It.IsAny<ImportEvent>()), Times.Exactly(1));
            _databaseImportEventService.Verify(e => e.UpdateImportEventStatus(It.IsAny<ImportEvent>()), Times.Exactly(1));
            _databasePersonService.Verify(e => e.InsertPerson(It.IsAny<Person>()), Times.Exactly(0));
        }

        [Fact]
        public void HundredEvents_ListImportEvents_ShouldReturn_50_ProcessingStatus()
        {
            _databaseImportEventService.Setup(e => e.GetImportEvents()).Returns(mockListImportEvent());

            var listEvents = _importService.ListImportEvents();

            Assert.Equal(50, listEvents.Where(p => p.Status == EventStatus.processing).Count());
            Assert.Equal(50, listEvents.Where(p => p.Status == EventStatus.done).Count());
        }
    }
}
