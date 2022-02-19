using Newtonsoft.Json;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Models
{
    public class AADIdentity
    {
        [JsonProperty("signInType")]
        public string SignInType { get; set; } = string.Empty;
        [JsonProperty("issuer")]
        public string Issuer { get; set; } = string.Empty;
        [JsonProperty("issuerAssignedId")]
        public string IssuerAssignedId { get; set; } = string.Empty;
    }
}
