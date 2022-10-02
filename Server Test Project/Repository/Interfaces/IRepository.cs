using Server_Test_Project.Models;

namespace Server_Test_Project.Repository.Interfaces
{
    public interface IRepository
    {
        public IEnumerable<Card> GetAll();
        public Card GetById(int id);
        public Card GetByName(string name);
        public Card Create(Card card);
        public Card Update(Card card);
        public void Delete(Card card);
    }
}
