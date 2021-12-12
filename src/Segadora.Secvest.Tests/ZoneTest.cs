using System.Text.Json;
using NUnit.Framework;

namespace Segadora.Secvest.Tests
{
    public class ZoneTest
    {
        [Test]
        public void ParseFromJson()
        {
            var json = @"{
  ""id"": ""1"",
  ""name"": ""Home"",
  ""state"": ""unset"",
  ""zones"": [
    ""201"",
    ""202"",
    ""203"",
    ""204"",
    ""206"",
    ""207"",
    ""209"",
    ""210""
  ]
}";
            var zone = JsonSerializer.Deserialize<Zone>(json);

            Assert.AreEqual("1", zone.Id);
            Assert.AreEqual("Home", zone.Name);
            Assert.AreEqual("unset", zone.State);
        }
    }
}
