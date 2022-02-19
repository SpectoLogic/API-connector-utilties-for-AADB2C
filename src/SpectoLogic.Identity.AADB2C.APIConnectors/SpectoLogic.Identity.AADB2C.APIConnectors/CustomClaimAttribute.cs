using System;

namespace SpectoLogic.Identity.AADB2C.APIConnectors
{
    [AttributeUsage(validOn: AttributeTargets.Class, AllowMultiple = true)]
    public class CustomClaimAttribute : Attribute
    {
        public CustomClaimAttribute()
        {
        }
        public CustomClaimAttribute(string name, Type claimType)
        {
            Name = name;
            ClaimType = claimType;
        }
        public string Name { get; set; }

        public Type ClaimType { get; set; }
    }
}
