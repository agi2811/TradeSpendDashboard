#pragma checksum "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\ReportMovement\MTDActualMovementFC.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ae1af5b081ad17865b23f644b5a5a2275eb06a6a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ReportMovement_MTDActualMovementFC), @"mvc.1.0.view", @"/Views/ReportMovement/MTDActualMovementFC.cshtml")]
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
#line 1 "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\_ViewImports.cshtml"
using TradeSpendDashboard;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\_ViewImports.cshtml"
using TradeSpendDashboard.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ae1af5b081ad17865b23f644b5a5a2275eb06a6a", @"/Views/ReportMovement/MTDActualMovementFC.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2cca2e5113c8e9920b198c6ee1a6fc91d77b505a", @"/Views/_ViewImports.cshtml")]
    public class Views_ReportMovement_MTDActualMovementFC : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form-horizontal"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("formUpload"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("data-toggle", new global::Microsoft.AspNetCore.Html.HtmlString("validator"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("autocomplete", new global::Microsoft.AspNetCore.Html.HtmlString("off"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("text/javascript"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Excel/exceljs.min.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Excel/FileSaver.min.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/Report/MTDActualMovementFC.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral(@"<style>
    .selectbox-field {
        margin-bottom: -7px !important;
        margin-top: -7px !important;
    }

    .right-side {
        position: absolute;
        right: 1px;
        top: 6px;
    }

    #tblReport, #tblHistory {
        max-height: 840px;
    }

     #gridContainer {
        max-height: 640px;
        max-width: 100%;
    }

    .dx-row > td, .dx-row > tr > td{

        border : 1px solid #e3e3e3;
    }

    .dx-treelist-container{
        color : rgba(0,0,0,.87) !important;
    }

     .dx_treeList_header {
         height: 32px; 
         padding-top:5px !important;
    }

    .dx-data-row td {
        font-family: Poppins,-apple-system,BlinkMacSystemFont,Segoe UI,Helvetica Neue,Arial,sans-serif;
        font-size: .725rem;
        font-weight: 400;
        line-height: 1.525;
        color: #101010 !important;
        /*color: rgba(0,0,0,.87);*/
    }
</style>

<input type=""text"" id=""CurrentMonth"" class=""d-none"" name=""CurrentMonth""");
            BeginWriteAttribute("value", " value=\"", 1017, "\"", 1046, 1);
#nullable restore
#line 46 "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\ReportMovement\MTDActualMovementFC.cshtml"
WriteAttributeValue("", 1025, ViewBag.CurrentMonth, 1025, 21, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\r\n<input type=\"text\" id=\"CurrentYear\" class=\"d-none\" name=\"CurrentYear\"");
            BeginWriteAttribute("value", " value=\"", 1121, "\"", 1149, 1);
#nullable restore
#line 47 "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\ReportMovement\MTDActualMovementFC.cshtml"
WriteAttributeValue("", 1129, ViewBag.CurrentYear, 1129, 20, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@" />

<div class=""row mb-2 mb-xl-3"">
    <div class=""col-auto d-sm-block"">
        <h4>MTD Actual Movement & Bridging</h4>
    </div>
</div>
 

<div class=""row"">

    <div class=""col-sm-6"">
        <div class=""card"">
            <div class=""card-body"">
");
            WriteLiteral(@"                <div class=""form-group row"">
                    <label for=""dxSelectYear"" class=""col-xs-8 col-sm-8 col-md-4 col-xl-4 col-form-label"">Source 1</label>
                    <div class=""col-xs-9 col-sm-9 col-md-8 col-xl-8"">
                        <div id=""dxSelectSource""></div>
                    </div>
                </div>
                <div class=""form-group row"">
                    <label for=""dxSelectYear"" class=""col-xs-8 col-sm-8 col-md-4 col-xl-4 col-form-label"">Year</label>
                    <div class=""col-xs-9 col-sm-9 col-md-8 col-xl-8"">
                        <div id=""dxSelectYear1""></div>
                    </div>
                </div>
                <div class=""form-group row"">

                    <label for=""dxSelectMonth"" class=""col-xs-8 col-sm-8 col-md-4 col-xl-4 col-form-label"">Month</label>
                    <div class=""col-xs-9 col-sm-9 col-md-8 col-xl-8"">
                        <div id=""dxSelectMonth1""></div>
                    </div>
      ");
            WriteLiteral(@"          </div>
            </div>
        </div>
    </div>
    <div class=""col-sm-6"">
        <div class=""card"">
            <div class=""card-body"">
                <div class=""form-group row"">
                    <label for=""dxSelectYear"" class=""col-xs-8 col-sm-8 col-md-4 col-xl-4 col-form-label"">Source 2</label>
                    <div class=""col-xs-9 col-sm-9 col-md-8 col-xl-8"">
                        <div id=""dxSelectSource2""></div>
                    </div>
                </div>
                <div class=""form-group row"">
                    <label for=""dxSelectYear"" class=""col-xs-8 col-sm-8 col-md-4 col-xl-4 col-form-label"">Year</label>
                    <div class=""col-xs-9 col-sm-9 col-md-8 col-xl-8"">
                        <div id=""dxSelectYear2""></div>
                    </div>

                </div>
                <div class=""form-group row"">

                    <label for=""dxSelectMonth"" class=""col-xs-8 col-sm-8 col-md-4 col-xl-4 col-form-label"">Month</label>
");
            WriteLiteral(@"                    <div class=""col-xs-9 col-sm-9 col-md-8 col-xl-8"">
                        <div id=""dxSelectMonth2""></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
  
</div>
<div class=""row"">
    <div class=""col-sm-12"">
        <div class=""card"">
            <div class=""card-body"">
");
            WriteLiteral(@"
                <div class=""form-group row"" style=""width:99%"">
                    <label for=""dxSelectYear"" class=""col-xs-8 col-sm-8 col-md-2 col-xl-2 col-form-label"">Profit Center</label>
                    <div class=""col-xs-9 col-sm-9 col-md-4 col-xl-4"">
                        <div id=""dxSelectProfitCenter""></div>
                    </div>

                </div>


            </div>
        </div>
    </div>

</div>






<div id=""divButtons"" class=""d-flex bd-highlight mb-3"">
     
    <div class=""p-2 bd-highlight"">
        <button type=""button"" class=""btn btn-primary btn-lg"" style=""margin-right:8px"" onclick=""initTable()"">Apply Filter</button>
    </div>
</div>


<div class=""modal fade"" id=""divModalSnapshot"" data-backdrop=""static"" data-keyboard=""false"" role=""dialog"" aria-hidden=""true"">
    <div>
        <div class=""modal-dialog"" style=""max-width:500px"">
            <div class=""modal-content"">
                <div class=""modal-header"">
                    <h4 class=""m");
            WriteLiteral("odal-title\" id=\"titleModalUpload\"><i class=\"fa fa-list\"></i>&nbsp;</h4>\r\n                </div>\r\n                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae1af5b081ad17865b23f644b5a5a2275eb06a6a12899", async() => {
                WriteLiteral(@"
                    <div class=""modal-body"">
                        <div class=""form-group"">
                            <label class=""col-sm-3 control-label"" style=""width:15%"">Name </label>
                            <div class=""col-lg-9"" style=""width:85%"">
                                <input type=""text"" id=""SnapshotName"" name=""SnapshotName"" class=""form-control"" placeholder=""Please Input Name"">
                            </div>
                        </div>

                    </div>
                    <div class=""modal-footer clearfix"">
                        <button type=""button"" id=""btnSnapshot"" class=""btn btn-success"" onclick=""SnapshotMTDActual()""><i class=""fa fa-save""></i> Snapshot</button>
                        <button type=""button"" id=""btnCancel"" class=""btn btn-danger"" data-dismiss=""modal""><i class=""fa fa-times""></i> Cancel</button>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n<div class=\"row\" style=\"margin-top:10px\">\r\n    <div class=\"col-12 col-lg-12 d-flex\">\r\n\r\n        <div id=\"gridContainer\"></div>\r\n\r\n\r\n    </div>\r\n    \r\n\r\n</div>\r\n \r\n \r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae1af5b081ad17865b23f644b5a5a2275eb06a6a15991", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
#nullable restore
#line 186 "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\ReportMovement\MTDActualMovementFC.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_6.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae1af5b081ad17865b23f644b5a5a2275eb06a6a18112", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
#nullable restore
#line 187 "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\ReportMovement\MTDActualMovementFC.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ae1af5b081ad17865b23f644b5a5a2275eb06a6a20233", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
#nullable restore
#line 188 "C:\~ Data ~\~ Agi Giyanto ~\~ PT. IForce Consulting ~\Application\TradeSpendDashboard\TradeSpendDashboard\Views\ReportMovement\MTDActualMovementFC.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591