using Microsoft.AspNetCore.Cors.Infrastructure;
using Server_Test_Project.FIleStorage.Interfaces;
using Server_Test_Project.JsonConventer.Interfaces;
using Server_Test_Project.Models;
using Server_Test_Project.Repository.Interfaces;
using Server_Test_Project.Services.Interfaces;
using System.Collections;

namespace Server_Test_Project.Services.Implimentations
{
    public class CardService : ICardService
    {
        IJsonConverter jsonConverter;
        IImageStorage imageStorage;
        IRepository repository;

        public CardService(IRepository repository, IJsonConverter jsonConverter, IImageStorage imageStorage)
        {
            this.imageStorage = imageStorage;
            this.jsonConverter = jsonConverter;
            this.repository = repository;
        }

        public List<Card> Create(CardImportExport card)
        {
            List<Card> cards = jsonConverter.Deserialize().ToList();
            byte[] imageByte = new byte[card.Image.Length];
            for(int i = 0; i < imageByte.Length; i++)
            {
                imageByte[i] = (byte)card.Image[i];
            }
            Card newCard = new Card();
            newCard.Id = UniqueId(cards);
            newCard.Name = card.Name;
            newCard.ImageName = imageStorage.Create(new MemoryStream(imageByte));
            cards.Add(newCard);
            return repository.Create(cards);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public IList<CardImportExport> GetAll()
        {
            var cardsList = repository.GetAll().ToList();
            List<CardImportExport> cards = new List<CardImportExport>();
            foreach (Card card in cardsList)
            {

                CardImportExport cardToExport = new CardImportExport();
                cardToExport.Id = card.Id;
                cardToExport.Name = card.Name;
                var imageBytes = File.ReadAllBytes($"Data\\Images\\{card.ImageName}.jpg");
                cardToExport.Image = imageBytes.Select(x => (int)x).ToArray();
                cards.Add(cardToExport);
            }
            return cards;
        }

        public int UniqueId(List<Card> cards)
        {
            return cards.Max(x => x.Id) + 1;
        }

        public Card Update(Card card)
        {
            repository.Update(card);
            return card;
        }
    }
}
