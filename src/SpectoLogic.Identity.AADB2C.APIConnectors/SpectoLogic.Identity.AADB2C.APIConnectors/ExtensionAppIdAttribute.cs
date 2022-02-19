using System;

namespace SpectoLogic.Identity.AADB2C.APIConnectors
{
    public class ExtensionAppIdAttribute : Attribute
    {
        public ExtensionAppIdAttribute()
        {
        }
        public ExtensionAppIdAttribute(string extensionID)
        {
            ExtensionID = extensionID;
        }

        /// <summary>
        /// Extension GUID
        /// </summary>
        public string ExtensionID { get; set; } = String.Empty;
    }
}
