using Newtonsoft.Json;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Models
{
    public class ErrorMessageResponse : MessageResponse
    {
        [JsonProperty(PropertyName = "status")]
        public int Status { get; set; } = 400;
    }
}
