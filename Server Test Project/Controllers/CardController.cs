using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server_Test_Project.JsonConventer.Interfaces;
using Server_Test_Project.Models;
using Server_Test_Project.Services.Interfaces;
using System.Text.Json.Nodes;

namespace Server_Test_Project.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly ICardService cardService;
        private readonly IJsonConverter jsonConverter;
        private readonly ILogger<CardController> _logger;

        public CardController(ILogger<CardController> logger, ICardService cardService, IJsonConverter jsonConverter)
        {
            _logger = logger;
            this.cardService = cardService;
            this.jsonConverter = jsonConverter;
        }

        [HttpGet("GetAll")]
        public IEnumerable<CardImportExport> GetAll()
        {
            var result = cardService.GetAll();
            if (result == null || result.Count == 0)
                return null;
            return cardService.GetAll();
        }

        [HttpPost("Create")]
        public List<Card> Create ([FromBody]string jsonContent)
        {
            if (jsonContent != null && jsonContent.Length > 0)
            {
                CardImportExport card = JsonConvert.DeserializeObject<CardImportExport>(jsonContent);
                return cardService.Create(card);
            }
            else
                return null;
        }

        [HttpPut("Update")]
        public bool Update([FromBody]string jsonContent)
        {
            bool success = true;
            CardImportExport card = new CardImportExport();
            if (jsonContent != null && jsonContent.Length > 0)
                card = JsonConvert.DeserializeObject<CardImportExport>(jsonContent);

            if (card != null)
            {
                try
                {
                    cardService.Update(card);
                }
                catch (Exception)
                {
                    success = false;
                }
                return success;
            }
            else
            {
                success = false;
                return success;
            }
        }

        [HttpDelete("Delete")]
        public void Delete(int id)
        {
            cardService.Delete(id);
        }
    }
}
