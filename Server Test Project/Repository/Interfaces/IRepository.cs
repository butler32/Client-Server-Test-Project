using Server_Test_Project.Models;

namespace Server_Test_Project.Repository.Interfaces
{
    public interface IRepository
    {
        public IEnumerable<Card> GetAll();
        public List<Card> Create(List<Card> cards);
        public Card Update(Card card);
        public void Delete(int id);
    }
}
