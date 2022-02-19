using SpectoLogic.Identity.AADB2C.APIConnectors;
using SpectoLogic.Identity.AADB2C.APIConnectors.Models;

namespace ClaimsProviderFunc.Models
{
    // Client-ID of AAD-App "b2c-extensions-app. Do not modify. Used by AADB2C for storing user data."
    [ExtensionAppId("eff3e2b4-6308-437e-953f-95fec3dc1573")]
    [CustomClaim("ADACId", typeof(string))]
    [CustomClaim("PhoneNumber", typeof(string))]
    public partial class MyClaimsResponse : CreateUserResponse
    {
    }
}
