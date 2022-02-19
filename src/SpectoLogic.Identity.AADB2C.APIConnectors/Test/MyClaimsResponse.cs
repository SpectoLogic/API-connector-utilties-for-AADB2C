using SpectoLogic.Identity.AADB2C.APIConnectors.Models;

namespace Test
{
    [SpectoLogic.Identity.AADB2C.APIConnectors.ExtensionAppId("00491DC6-827B-47E9-a2fD-E2F19772C006")]
    [SpectoLogic.Identity.AADB2C.APIConnectors.CustomClaim("PhoneNumber", typeof(string))]
    [SpectoLogic.Identity.AADB2C.APIConnectors.CustomClaim("LoyaltyId", typeof(int))]
    public partial class MyClaimsResponse : CreateUserResponse
    {
    }
}
