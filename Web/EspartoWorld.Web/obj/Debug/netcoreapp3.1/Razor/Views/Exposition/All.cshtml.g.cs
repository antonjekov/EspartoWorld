#pragma checksum "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b86c3edc06421a1d9f6a443cdd51b334b73032fb"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Exposition_All), @"mvc.1.0.view", @"/Views/Exposition/All.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b86c3edc06421a1d9f6a443cdd51b334b73032fb", @"/Views/Exposition/All.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0750521cbf3218c0a499ab16ef79f0abc154d0ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Exposition_All : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<EspartoWorld.Web.ViewModels.ExposicionItems.ExpositionItemViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/flipCards.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"container\">\r\n    <div class=\"row text-center\">\r\n");
#nullable restore
#line 5 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <div class=\"col-md-3 card-container\">\r\n                <div class=\"card card-flip border border-dark  m-2\">\r\n                    <div class=\"front card-block\">\r\n                        <img");
            BeginWriteAttribute("src", " src=", 401, "", 420, 1);
#nullable restore
#line 10 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
WriteAttributeValue("", 406, item.ImageUrl, 406, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"card-img-top\" />\r\n                        <h4 class=\"card-title\">");
#nullable restore
#line 11 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                                          Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                    </div>\r\n                    <div class=\"back card-block card-body d-flex flex-column\">\r\n                        <h4 class=\"card-title m-2\">");
#nullable restore
#line 14 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                                              Write(item.Title);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h4>\r\n                        <p class=\"card-text m-1\"><b>Author:</b> ");
#nullable restore
#line 15 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                                                           Write(item.Author.UserName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p class=\"card-text m-1\">");
#nullable restore
#line 16 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                                            Write(item.Description);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 17 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                         if (item.IsSold)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <h5 class=\"card-text text-muted m-1 mt-auto\"><b>SOLD</b></h5>\r\n");
#nullable restore
#line 20 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <h5 class=\"card-text text-danger m-1 mt-auto\"><b>");
#nullable restore
#line 23 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                                                                        Write(item.Price);

#line default
#line hidden
#nullable disable
            WriteLiteral(" &euro;</b></h5>\r\n                            <a href=\"#\" class=\"btn btn-primary mt-auto\">Buy</a>\r\n");
#nullable restore
#line 25 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
                        }

#line default
#line hidden
#nullable disable
            WriteLiteral("                    </div>\r\n                </div>\r\n            </div>\r\n");
#nullable restore
#line 29 "C:\gitHub\Software-University\C# .NET Core\FinalProject\EspartoWorld\Web\EspartoWorld.Web\Views\Exposition\All.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral(@"
    <script>
        $(document).ready(function () {
            var front = document.getElementsByClassName(""front"");
            var back = document.getElementsByClassName(""back"");

            var highest = 0;
            var absoluteSide = """";

            for (var i = 0; i < front.length; i++) {
                if (front[i].offsetHeight > back[i].offsetHeight) {
                    if (front[i].offsetHeight > highest) {
                        highest = front[i].offsetHeight;
                        absoluteSide = "".front"";
                    }
                } else if (back[i].offsetHeight > highest) {
                    highest = back[i].offsetHeight;
                    absoluteSide = "".back"";
                }
            }
            $("".front"").css(""height"", highest);
            $("".back"").css(""height"", highest);
            $(absoluteSide).css(""position"", ""absolute"");
        });
    </script>
");
            }
            );
            WriteLiteral("\r\n");
            DefineSection("Styles", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "b86c3edc06421a1d9f6a443cdd51b334b73032fb9613", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
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
