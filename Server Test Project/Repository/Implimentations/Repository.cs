using Server_Test_Project.FIleStorage.Interfaces;
using Server_Test_Project.JsonConventer.Interfaces;
using Server_Test_Project.Models;
using Server_Test_Project.Repository.Interfaces;
using Server_Test_Project.Services.Interfaces;

namespace Server_Test_Project.Repository.Implimentations
{
    public class Repository : IRepository
    {
        private readonly IImageStorage imageStorage;
        private IJsonConverter jsonConverter;

        public Repository(IJsonConverter jsonConverter, IImageStorage imageStorage)
        {
            this.imageStorage = imageStorage;
            this.jsonConverter = jsonConverter;
        }
        
        public IEnumerable<Card> GetAll()
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            if (cards == null || cards.Count == 0)
                return new List<Card>();
            else
                return cards;
        }

        public List<Card> Create(List<Card> cards)
        {
            return jsonConverter.Serialize(cards);
        }

        public void Delete(int id)
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            if (cards != null || cards.Count == 0)
            {
                var cardToDelete = cards.First(i => i.Id == id);
                cards.Remove(cards.First(i => i.Id == id));
                jsonConverter.Serialize(cards);
                imageStorage.Delete(cardToDelete.ImageName);
            }
        }

        public Card Update(Card cardToEdit)
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            if (cards != null && cards.Count > 0)
            {
                var card = cards.FirstOrDefault(i => i.Id == cardToEdit.Id);
                if (card == null)
                    return null;
                int index = cards.IndexOf(card);
                cards[index] = cardToEdit;
                jsonConverter.Serialize(cards);
                return cardToEdit;
            }
            return null;
        }
    }
}
