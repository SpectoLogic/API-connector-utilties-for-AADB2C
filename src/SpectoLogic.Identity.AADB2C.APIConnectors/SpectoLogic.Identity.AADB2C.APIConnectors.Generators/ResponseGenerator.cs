using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using SpectoLogic.Identity.AADB2C.APIConnectors.Generators.Extensions;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SpectoLogic.Identity.AADB2C.APIConnectors.Generators
{
    [Generator]
    public class ResponseGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var syntaxReceiver = (ExtensionAppIdSyntaxReceiver)context.SyntaxReceiver;

            // get the recorded user class
            foreach (var foundClass in syntaxReceiver.ClassesToAugment)
            {
                if (foundClass != null)
                {
                    var sourceCode = new StringBuilder();
                    var sourceCodeGenerated = false;

                    var nsName = foundClass.ClassDeclaration.GetNamespace();

                    sourceCode.Append($"namespace {nsName} ");
                    sourceCode.Append("{ public partial class ");
                    sourceCode.Append(foundClass.ClassDeclaration.Identifier.ValueText);
                    sourceCode.Append("{ "); // 

                    foreach (var customClaimAttr in foundClass.CustomClaimAttributes)
                    {
                        var val = customClaimAttr?.ArgumentList?.Arguments.FirstOrDefault()?.Expression?.ChildTokens()?.FirstOrDefault().ValueText;
                        var typ = customClaimAttr.ArgumentList.Arguments[1].Expression.ChildNodes().First().ToString();
                        GenerateJsonProperty(
                                val, typ, foundClass.ExtensionID,
                                sourceCode);
                        sourceCodeGenerated = true;
                    }
                    sourceCode.Append(" } }");
                    if (sourceCodeGenerated)
                    {
                        var sourceText = SourceText.From(sourceCode.ToString(), Encoding.UTF8);
                        context.AddSource($"{foundClass.ClassDeclaration.Identifier.ValueText}.g.cs", sourceText);
                    }
                }
            }
        }

        private void GenerateJsonProperty(
            string val,
            string typ,
            string extensionID,
            StringBuilder sourceCode)
        {
            sourceCode.Append($" [Newtonsoft.Json.JsonProperty(\"extension_{extensionID}_{val}\")]");
            sourceCode.Append($"public {typ} {val} {{ get; set; }} ");
        }

        public void Initialize(GeneratorInitializationContext context)
        {
#if DEBUG
            if (!Debugger.IsAttached)
            {
                //  Debugger.Launch();
            }
#endif
            context.RegisterForSyntaxNotifications(() => new ExtensionAppIdSyntaxReceiver());
        }
    }
}