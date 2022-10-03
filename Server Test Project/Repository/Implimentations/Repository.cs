using Server_Test_Project.FIleStorage.Interfaces;
using Server_Test_Project.JsonConventer.Interfaces;
using Server_Test_Project.Models;
using Server_Test_Project.Repository.Interfaces;
using Server_Test_Project.Services.Interfaces;

namespace Server_Test_Project.Repository.Implimentations
{
    public class Repository : IRepository
    {
        private IJsonConverter jsonConverter;

        public Repository(IJsonConverter jsonConverter)
        {
            this.jsonConverter = jsonConverter;
        }
        
        public IEnumerable<Card> GetAll()
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            return cards;
        }

        public List<Card> Create(List<Card> cards)
        {
            return jsonConverter.Serialize(cards);
        }

        public void Delete(int id)
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            cards.Remove(cards.First(i => i.Id == id));
            jsonConverter.SerializeRewrite(cards);
        }

        public Card Update(Card card)
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            int index = cards.IndexOf(card);
            cards[index] = card;
            jsonConverter.SerializeRewrite(cards);
            return card;
        }
    }
}
