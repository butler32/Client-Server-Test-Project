using Server_Test_Project.JsonConventer.Interfaces;
using Server_Test_Project.Models;
using Server_Test_Project.Repository.Interfaces;

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

        public Card GetById(int id)
        {
            Card card = jsonConverter.Deserialize().ToList().FirstOrDefault(i => i.Id == id);
            return card;
        }

        public Card GetByName(string name)
        {
            Card card = jsonConverter.Deserialize().ToList().FirstOrDefault(n => n.Name == name);
            return card;
        }

        public Card Create(Card card)
        {
            return jsonConverter.Serialize(card);
        }

        public void Delete(Card card)
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            bool succes = cards.Remove(card);
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
