using Microsoft.AspNetCore.Mvc;
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
        public IEnumerable<Card> GetAll()
        {
            return cardService.GetAll();
        }

        [HttpGet("GetById")]
        public Card GetById(int Id)
        {
            return cardService.GetById(Id);
        }

        [HttpGet("GetByName")]
        public Card GetByName(string name)
        {
            return cardService.GetByName(name);
        }

        
        [HttpPost("Create")]
        public Card Create (string jsonContent)
        {
            Card card = jsonConverter.Des(jsonContent);
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
        public Card? Delete(Card card)
        {
            bool success = true;
            try
            {
                cardService.Delete(card);
            }
            catch (Exception)
            {
                success = false;
            }
            return success ? card : null;
        }
    }
}
