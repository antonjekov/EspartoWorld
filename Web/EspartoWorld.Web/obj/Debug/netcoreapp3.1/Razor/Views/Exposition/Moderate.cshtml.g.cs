#pragma checksum "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\Moderate.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eadeaa75b565fc63fafd8d28a6d4bfa2647f821a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Exposition_Moderate), @"mvc.1.0.view", @"/Views/Exposition/Moderate.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\_ViewImports.cshtml"
using EspartoWorld.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\_ViewImports.cshtml"
using EspartoWorld.Web.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eadeaa75b565fc63fafd8d28a6d4bfa2647f821a", @"/Views/Exposition/Moderate.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0750521cbf3218c0a499ab16ef79f0abc154d0ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Exposition_Moderate : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<EspartoWorld.Web.ViewModels.ExposicionItems.ExpositionItemViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<table class=\"table table-hover\">\r\n    <thead>\r\n        <tr>\r\n            <th scope=\"col\">Title</th>\r\n            <th scope=\"col\">Author</th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 11 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\Moderate.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>");
#nullable restore
#line 14 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\Moderate.cshtml"
               Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>");
#nullable restore
#line 15 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\Moderate.cshtml"
               Write(item.Author.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                <td>\r\n                    <a class=\"btn btn-success\"");
            BeginWriteAttribute("href", " href=\"", 494, "\"", 529, 2);
            WriteAttributeValue("", 501, "/Exposition/Details/", 501, 20, true);
#nullable restore
#line 17 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\Moderate.cshtml"
WriteAttributeValue("", 521, item.Id, 521, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">Details</a>\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 20 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\Moderate.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<EspartoWorld.Web.ViewModels.ExposicionItems.ExpositionItemViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
