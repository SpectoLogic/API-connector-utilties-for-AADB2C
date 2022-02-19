using Newtonsoft.Json;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Models
{
    public class MessageResponse : BaseClaimsRespone
    {
        [JsonProperty(PropertyName = "userMessage")]
        public string UserMessage { get; set; }
    }
}
