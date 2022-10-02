using Server_Test_Project.Models;
using Server_Test_Project.Repository.Interfaces;
using Server_Test_Project.Services.Interfaces;

namespace Server_Test_Project.Services.Implimentations
{
    public class CardService : ICardService
    {
        IRepository repository;

        public CardService(IRepository repository)
        {
            this.repository = repository;
        }

        public Card Create(Card card)
        {
            return repository.Create(card);
        }

        public void Delete(Card card)
        {
            repository.Delete(card);
        }

        public IList<Card> GetAll()
        {
            var cards = repository.GetAll().ToList();
            return cards;
        }

        public Card GetById(int id)
        {
            var card = repository.GetById(id);
            return card;
        }

        public Card GetByName(string name)
        {
            var card = repository.GetByName(name);
            return card;
        }

        public Card Update(Card card)
        {
            repository.Update(card);
            return card;
        }
    }
}
