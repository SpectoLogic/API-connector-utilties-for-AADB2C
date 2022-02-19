using Newtonsoft.Json;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Models
{
    public class ClaimsRequestBase
    {
        [JsonProperty("step")]
        public string Step { get; set; }
        [JsonProperty("ui_locales")]
        public string UILocales { get; set; }
        [JsonProperty("client_id")]
        public string ClientId { get; set; }
        [JsonProperty("identities")]
        public AADIdentity[] Identities { get; set; }
    }
}
