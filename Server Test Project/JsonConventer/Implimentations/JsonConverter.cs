using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Server_Test_Project.JsonConventer.Interfaces;
using Server_Test_Project.Models;
using Server_Test_Project.Services.Implimentations;
using Server_Test_Project.Services.Interfaces;
using System.Text.Json.Nodes;

namespace Server_Test_Project.JsonConventer.Implimentations
{
    public class JsonConverter : IJsonConverter
    {
        private string fileName = Path.Combine("Data", "Json", "Cards.json");


        public IEnumerable<Card> Deserialize()
        {
            List<Card>? entities = File.Exists(fileName) ? 
                JsonConvert.DeserializeObject<List<Card>>(File.ReadAllText(fileName)) : null;
            return entities;
        }

        public List<Card> Serialize(List<Card> cards)
        {
            File.WriteAllText(fileName, JsonConvert.SerializeObject(cards));
            return cards;
        }
    }
}
