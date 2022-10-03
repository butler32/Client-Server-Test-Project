using Microsoft.AspNetCore.Mvc;
using Server_Test_Project.Models;
using System.Text.Json.Nodes;

namespace Server_Test_Project.JsonConventer.Interfaces
{
    public interface IJsonConverter
    {
        public IEnumerable<Card> Deserialize();

        public List<Card> Serialize(List<Card> cards);
        public IList<Card> SerializeRewrite(IList<Card> cards);
    }
}
