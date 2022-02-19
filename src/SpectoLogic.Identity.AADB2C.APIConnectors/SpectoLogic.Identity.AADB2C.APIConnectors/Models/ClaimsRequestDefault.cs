namespace SpectoLogic.Identity.AADB2C.APIConnectors.Models
{
    public class ClaimsRequestDefault : ClaimsRequestBase
    {
        public string city { get; set; }
        public string streetAddress { get; set; }
        public string postalCode { get; set; }
        public string country { get; set; }
        public string email { get; set; }
        public string surname { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
    }
}
