using SpectoLogic.Identity.AADB2C.APIConnectors;
using SpectoLogic.Identity.AADB2C.APIConnectors.Models;

namespace Test
{
    [ExtensionAppId("00491DC6-827B-47E9-a2fD-E2F19772C006")]
    [CustomClaimAttribute("PhoneNumber", typeof(string))]
    [CustomClaim("LoyaltyId", typeof(int))]
    public partial class MyClaimsRequest : ClaimsRequestBase
    {
    }
}
