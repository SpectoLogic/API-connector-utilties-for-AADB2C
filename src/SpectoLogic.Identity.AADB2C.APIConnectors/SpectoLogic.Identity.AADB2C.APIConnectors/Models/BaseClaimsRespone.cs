using Newtonsoft.Json;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Models
{
    public class BaseClaimsRespone
    {
        [JsonProperty(PropertyName = "version")]
        public string Version { get; set; } = "1.0.0";
        [JsonProperty(PropertyName = "action")]
        public virtual string Action { get; set; } = string.Empty;
    }
}