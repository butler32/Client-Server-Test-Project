using Server_Test_Project.Models;

namespace Server_Test_Project.Services.Interfaces
{
    public interface ICardService
    {
        public IList<CardImportExport> GetAll();
        public Card Update(CardImportExport card);
        public void Delete(int id);
        public List<Card> Create(CardImportExport card);
        public int UniqueId(List<Card> cards);
    }
}
