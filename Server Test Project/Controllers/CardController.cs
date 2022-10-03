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
            return cardService.GetAll();
        }

        [HttpPost("Create")]
        public List<Card> Create ([FromBody]string jsonContent)
        {
            CardImportExport card = JsonConvert.DeserializeObject<CardImportExport>(jsonContent);
            return cardService.Create(card);
        }

        [HttpPut("Update")]
        public Card? Update (Card card)
        {
            bool success = true;
            try
            {
                cardService.Update(card);
            }
            catch (Exception)
            {
                success = false;
            }
            return success ? card : null;
        }

        [HttpDelete("Delete")]
        public void Delete(int id)
        {
            bool success = true;
            try
            {
                cardService.Delete(id);
            }
            catch (Exception)
            {
                success = false;
            }
        }
    }
}
