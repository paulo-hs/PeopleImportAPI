using Application.Services;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

namespace PeopleImportAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class API : ControllerBase
    {
        private readonly ILogger<API> _logger;
        private readonly IFileReadService _csvReadService;
        private readonly IImportService _importService;
        private readonly IAuthService _authService;

        public API(ILogger<API> logger, IFileReadService fileReadService, IImportService importService, IAuthService authService)
        {
            _logger = logger;
            _csvReadService = fileReadService;
            _importService = importService;
            _authService = authService;
        }

        [HttpGet]
        [Route("token")]
        public IActionResult GetToken()
        {
            return Ok(_authService.GetToken());
        }

        [HttpPost]
        [Route("import")]
        [Authorize]
        public async Task<IActionResult> ImportCSVFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new ErrorMessage("Arquivo inválido."));

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                byte[] fileArray = ms.ToArray();
                _csvReadService.Load(fileArray, file.FileName);
            }

            if (!_csvReadService.ValidateSchema())
                return BadRequest(new ErrorMessage("Formato do arquivo deve conter \"Nome;CPF;Endereço;Cidade;Estado;DDD;Telefone\""));

            if (_csvReadService.CountLines() < 10001)
                return BadRequest(new ErrorMessage("O arquivo deve conter no mínimo 10.000 itens"));

            _importService.ImportFile(_csvReadService.FileName, _csvReadService.Lines);

            return Ok(new SuccessMessage("Arquivo em processamento"));
        }

        [HttpGet]
        [Route("import")]
        [Authorize]
        public IActionResult GetImportEvents()
        {
            return Ok(_importService.ListImportEvents());
        }
    }
}
