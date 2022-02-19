using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;
using SpectoLogic.Identity.AADB2C.APIConnectors.Generators.Extensions;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Generators
{
    public class ClassToAugmentInfo
    {
        public ClassDeclarationSyntax ClassDeclaration { get; internal set; }
        public string ExtensionID { get; set; }
        public List<AttributeSyntax> CustomClaimAttributes { get; set; } = new List<AttributeSyntax>();
    }

    public class ExtensionAppIdSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassToAugmentInfo> ClassesToAugment { get; set; } = new List<ClassToAugmentInfo>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            // Investigate all Class Declarations
            if (syntaxNode is ClassDeclarationSyntax cds)
            {
#if DEBUG
                //if (!Debugger.IsAttached)
                //{
                //    Debugger.Launch();
                //}
#endif
                ClassToAugmentInfo augmentInfo = null;
                
                // Find the ExtensionAppId Attribute
                var extAppIdAttr = cds.FindAttribute("SpectoLogic.Identity.AADB2C.APIConnectors", "ExtensionAppId");
                if (extAppIdAttr != null)
                {
                    if (extAppIdAttr != null && extAppIdAttr.ArgumentList.Arguments.Count > 0)
                    {
                        var argExpression = extAppIdAttr.ArgumentList.Arguments.First().Expression;
                        var token = argExpression.ChildTokens().FirstOrDefault();
                        if (token != null)
                        {
                            augmentInfo = new ClassToAugmentInfo()
                            {
                                // Remove - of the Client-ID as the Flow ignores your result
                                // not throwing any error!!!
                                ExtensionID = token.ValueText.Replace("-", "").ToLower(),
                                ClassDeclaration = cds
                            };
                            ClassesToAugment.Add(augmentInfo);
                        }
                    }
                    // Since we have found a class with the ExtensionAppID Attribute, now iterate through all Custom CLaim Definitions
                    if (augmentInfo!=null)
                    {
                        var customClaimAttrs = cds.FindAttributes("SpectoLogic.Identity.AADB2C.APIConnectors", "CustomClaim");
                        foreach (var customClaimAttr in customClaimAttrs)
                        {
                            if (customClaimAttr != null && customClaimAttr.ArgumentList.Arguments.Count > 0)
                            {
                                augmentInfo.CustomClaimAttributes.Add(customClaimAttr);
                            }
                        }
                    }
                }
            }
        }
    }
}
