using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Globalization;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Generators.Extensions
{
    public static class SyntaxExtensions
    {
        public static string GetNamespace(this TypeDeclarationSyntax typeSyntax)
        {
            var ns = typeSyntax.Parent;
            while (ns is not NamespaceDeclarationSyntax) { ns = ns.Parent; }
            var nsp = ns as NamespaceDeclarationSyntax;
            return nsp.Name.ToString();
        }

        public static AttributeSyntax FindAttribute(this TypeDeclarationSyntax typeSyn, string specificNamespace, string specificName)
        {
            foreach (var als in typeSyn.AttributeLists)
            {
                foreach (var at in als.Attributes)
                {
                    if (at.GetNameFromNamespace(specificNamespace) == specificName)
                    {
                        return at;
                    }
                }
            }
            return null;
        }

        public static List<AttributeSyntax> FindAttributes(this TypeDeclarationSyntax typeSyn, string specificNamespace, string specificName)
        {
            var result = new List<AttributeSyntax>();
            foreach (var als in typeSyn.AttributeLists)
            {
                foreach (var at in als.Attributes)
                {
                    if (at.GetNameFromNamespace(specificNamespace) == specificName)
                    {
                        result.Add(at);
                    }
                }
            }
            return result;
        }

        public static string GetNameFromNamespace(this AttributeSyntax atSyn, string specificNamespace = null)
        {
            var name = atSyn.GetNameFromNamespaceIntern(specificNamespace);
            if (name.EndsWith("Attribute"))
            {
                return name.Substring(0, name.Length - "Attribute".Length);
            }
            else
            {
                return name;
            }
        }

        private static string GetNameFromNamespaceIntern(this AttributeSyntax atSyn, string specificNamespace = null)
        {
            if (atSyn.Name is QualifiedNameSyntax qatSyn)
            {
                if (string.IsNullOrEmpty(specificNamespace))
                {
                    return qatSyn.Right.ToFullString();
                }
                // if it must match exactly ==>
                if (string.Compare(specificNamespace, qatSyn.Left.ToFullString(), true, CultureInfo.InvariantCulture) == 0)
                {
                    return qatSyn.Right.ToFullString();
                }
                else
                {
                    return string.Empty;
                }
            }
            else if (atSyn.Name is IdentifierNameSyntax inSyn)
            {
                var name = atSyn.Name.ToFullString();
                if (string.IsNullOrEmpty(specificNamespace))
                {
                    return name;
                }

                var rootSyntaxTree = ((IdentifierNameSyntax)atSyn.Name).GetReference().SyntaxTree.GetRoot();
                foreach (var cn in rootSyntaxTree.ChildNodes())
                {
                    if (cn is UsingDirectiveSyntax usSyn)
                    {
                        foreach (var cn2 in cn.ChildNodes())
                        {
                            if (cn2 is QualifiedNameSyntax)
                            {
                                if (string.Compare(specificNamespace, cn2.ToFullString(), true, CultureInfo.InvariantCulture) == 0)
                                {
                                    return name;
                                }
                            }
                        }
                    }
                }
                return string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
