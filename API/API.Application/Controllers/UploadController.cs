using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Text;
using TechMentor.Entities;

namespace TechMentor.Controllers
{   
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]

    public class UploadController : ControllerBase
    {
        private readonly IDistributedCache  _cache;
        private readonly ILogger<UploadController> _logger;

        private readonly string gifsFolderPath;

        public UploadController(IDistributedCache cache, ILogger<UploadController> logger)
        {
            _cache = cache;
            _logger = logger;
            // Determine o caminho relativo dentro do projeto para a pasta "Gifs"
            gifsFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Gifs");

            // Verifique se a pasta "Gifs" existe. Se não existir, você pode criá-la.
            if (!Directory.Exists(gifsFolderPath))
            {
                Directory.CreateDirectory(gifsFolderPath);
            }

            
        }

        [HttpGet("{label}")]
        public IActionResult CheckIfExistsInRedisDB(string label)
        {
            // Verifica se a chave (title) existe no cache Redis
            var gifPath = _cache.GetString(label);

            if (!string.IsNullOrEmpty(gifPath))
            {
                return Ok($"O GIF com título '{label}' existe no Redis. Caminho: {gifPath}");
            }
            else
            {
                return NotFound($"O GIF com título '{label}' não foi encontrado no Redis.");
            }
        }

        [HttpPost("uploadGif")]
        public async Task<IActionResult> UploadGif([FromForm] GifUploadModel model)
        {
            if (model.Gif != null && model.Gif.Length > 0)
            {
                var gifFileName = $"{Guid.NewGuid()}.gif";
                var gifFullPath = Path.Combine(gifsFolderPath, gifFileName);

                using (var stream = new FileStream(gifFullPath, FileMode.Create))
            {
                await model.Gif.CopyToAsync(stream);
            }

            // Armazenar o caminho do GIF no Redis juntamente com o rótulo
            var gifData = new GifData(model.Label, gifFullPath);
            var serializedData = JsonConvert.SerializeObject(gifData);

            // Adicione logs de depuração para registrar as chaves e valores no Redis
            _logger.LogInformation($"Chave no Redis: {"GifKeys"}");
            _logger.LogInformation($"Valor no Redis: {serializedData}");

            await _cache.SetStringAsync(model.Label, serializedData);

            return Ok($"GIF '{model.Label}' carregado com sucesso e armazenado no Redis.");
        }

        return BadRequest("Erro no envio do GIF.");
    }

    

    }

}