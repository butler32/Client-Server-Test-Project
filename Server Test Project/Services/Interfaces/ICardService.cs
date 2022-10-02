using Server_Test_Project.Models;

namespace Server_Test_Project.Services.Interfaces
{
    public interface ICardService
    {
        public IList<Card> GetAll();
        public Card GetById(int id);
        public Card GetByName(string name);
        public Card Update(Card card);
        public void Delete(Card card);
        public Card Create(Card card);

    }
}
