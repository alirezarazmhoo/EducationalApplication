#pragma checksum "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "52b3c95d9f6f8ad7245411a977b33679d13e8638"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Grades_Index), @"mvc.1.0.view", @"/Views/Grades/Index.cshtml")]
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
#line 1 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\_ViewImports.cshtml"
using EducationalApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\_ViewImports.cshtml"
using EducationalApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"52b3c95d9f6f8ad7245411a977b33679d13e8638", @"/Views/Grades/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8f32439c51fa860ff323bb9b937bc88b9f15c88", @"/Views/_ViewImports.cshtml")]
    public class Views_Grades_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EducationalApplication.Utility.PaginatedList<EducationalApplication.Models.Grade>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("register"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<section class=""content-header"">
    <h1 style=""float:right"">
        مدیریت مقاطع تحصیلی
    </h1>
    <ol class=""breadcrumb"" >
        <li><a href=""#""><i class=""fa fa-home""></i> خانه</a></li>
        <li class=""active"">مقطع تحصیلی</li>
    </ol>
</section>
<div style=""margin-top: 50px;margin-bottom:3px"">
    <a class=""btn btn-success openmodal"" href=""#""  data-toggle=""modal"" data-target=""#myModal"">ثبت جدید</a>
</div>

<div class=""row"">
    <div class=""col-sm-12"">
        <div class=""text-center"" style=""border-style: solid;border-color:dimgray"">
            <h2 style=""font-size:medium"">جستجو</h2>
        </div>
");
#nullable restore
#line 25 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
         using (Html.BeginForm("Index",
         "Grades",
         FormMethod.Post))
        {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""box zmdi-border-color"" style=""height:auto"" >
                <div class=""box-body table-responsive no-padding row "">
                    <div style=""width:90%;margin-right:40px;margin-top:10px"" >
                        <div class=""row"" >
                            <label for=""exampleInputEmail1"">عنوان جستجو:</label>
                            <input name=""searchString"" placeholder=""متن را وارد کنید""");
            BeginWriteAttribute("class", " class=\"", 1308, "\"", 1316, 0);
            EndWriteAttribute();
            WriteLiteral(@" style=""width:260px"">
                        </div>
                    </div>
                </div>
                <div style=""margin-top:20px"" dir=""rtl"">
                    <button style=""margin-right:20px;"" type=""submit"" class=""btn btn-primary"">جتسجو یا بارگزاری مجدد</button>
                </div>
            </div>
");
#nullable restore
#line 43 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    </div>
</div>

<div class=""row"">
    <div class=""col-xs-12"">
        <div class=""box"">
            <div class=""box-body table-responsive no-padding"">
                <table class=""table table-hover text-center"">
                    <tr>
                        <th>
                            ردیف
                        </th>
                        <th>
                            نام
                        </th>
                        <th>
                            کد
                        </th>
                        <th>
                            عملیات
                        </th>
                    </tr>
");
#nullable restore
#line 66 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                       int rowNo = 0; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 67 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
#nullable restore
#line 71 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                    Write(rowNo += 1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 74 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                   Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 77 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                   Write(item.Code);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        <button type=\"button\" class=\"btn btn-danger\" data-toggle=\"modal\" data-target=\"#QuestionModal\" onclick=\"AssignButtonClicked(this)\" data-assigned-id=\"");
#nullable restore
#line 80 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                                                                                                                                                                       Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"> حدف</button> |\r\n                        <button type=\"button\" class=\"btn btn-warning\" data-toggle=\"modal\" data-target=\"#EditModal\"");
            BeginWriteAttribute("id", " id=\"", 3063, "\"", 3076, 1);
#nullable restore
#line 81 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
WriteAttributeValue("", 3068, item.Id, 3068, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("> ویرایش</button>\r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 84 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </table>\r\n            </div>\r\n        </div>\r\n\r\n");
#nullable restore
#line 89 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
          
            var prevDisabled = !Model.HasPreviousPage ? "disabled" : "";
            var nextDisabled = !Model.HasNextPage ? "disabled" : "";
        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "52b3c95d9f6f8ad7245411a977b33679d13e863810625", async() => {
                WriteLiteral("\r\n            صفحه قبل\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pageNumber", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 95 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                      WriteLiteral(Model.PageIndex - 1);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3509, "btn", 3509, 3, true);
            AddHtmlAttributeValue(" ", 3512, "btn-default", 3513, 12, true);
#nullable restore
#line 96 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
AddHtmlAttributeValue(" ", 3524, prevDisabled, 3525, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "52b3c95d9f6f8ad7245411a977b33679d13e863813485", async() => {
                WriteLiteral("\r\n            صفحه بعد\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-pageNumber", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 100 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
                      WriteLiteral(Model.PageIndex + 1);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 3685, "btn", 3685, 3, true);
            AddHtmlAttributeValue(" ", 3688, "btn-default", 3689, 12, true);
#nullable restore
#line 101 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\Grades\Index.cshtml"
AddHtmlAttributeValue(" ", 3700, nextDisabled, 3701, 13, false);

#line default
#line hidden
#nullable disable
            EndAddHtmlAttributeValues(__tagHelperExecutionContext);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

    </div>
</div>



<div class=""modal fade"" id=""myModal"" role=""dialog"" style=""height:auto;overflow-y:visible"">
    <div class=""modal-dialog modal-sm"" style=""width:400px"">
        <div class=""modal-content"">
            <div class=""modal-header text-center"">
                <button type=""button"" class=""close"" data-dismiss=""modal"">&times;</button>
                <h4 class=""modal-title"">ثبت جدید</h4>
            </div>
            <div class=""modal-body"" style=""height:auto"">

                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "52b3c95d9f6f8ad7245411a977b33679d13e863816878", async() => {
                WriteLiteral(@"
                    <input id=""GradeId"" name=""GradeId"" hidden />
                    <div class=""form-group"" style=""margin-right:5px"">
                        <label for=""exampleInputEmail1""> نام :</label>
                        <input display=""نام"" id=""Name"" name=""Name"" placeholder=""نام مقطع را وارد کنید"" class=""form-control"" style=""width:70%"" required>
                    </div>
                    <div class=""form-group"" style=""margin-right:5px"">
                        <label for=""exampleInputEmail1""> کد :</label>
                        <input display=""کد"" id=""Code"" name=""Code"" placeholder=""کد مقطع را وارد کنید"" class=""form-control"" style=""width:70%"" required>
                    </div>
                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

            </div>
            <div class=""modal-footer"">
                <button type=""button"" class=""btn btn-danger"" data-dismiss=""modal"">انصراف</button>
                <button type=""button"" class=""btn btn-success"" onclick=""Save();"">ثبت</button>
            </div>
        </div>
    </div>
</div>
<div id=""Error"">

</div>
<div id=""Success"">

</div>
<div id=""Question"">

</div>
<script>
    window.onload = Load;
    function Load() {
        CreateAllModals();
        $("".openmodal"").click(function () {
            clear('register', [""input""]);
        });
        $("".btn-warning"").click(function () {
            EditAjax(""../Grades/Deatails"", this.id);
            var bodyStyle = document.body.style;
            bodyStyle.removeProperty('padding-right');
        });
    }
    function Save() {
        if (checkvalidity('register') === 0) {
            return;
        }
        else {
            $(""#myModal"").modal('toggle');
            var Parameters = [{ id: ""Name"",");
            WriteLiteral(@" htmlname: ""Name"", special: """" }, { id: ""Code"", htmlname: ""Code"", special: """" }  , { id: ""GradeId"", htmlname: ""GradeId"", special: """" }];
            PostAjax('../Grades/Register', Parameters, ""../Grades/Index"");
        }
    }
    function AssignButtonClicked(elem) {
        $('#GradeId').val($(elem).data('assigned-id'));
    }
    function Remove() {
        var Parameters = [{ id: ""GradeId"", htmlname: ""GradeId"", special: """" }];
        PostAjax('../Grades/Remove', Parameters, ""../Grades/Index"");
    }


</script>

");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EducationalApplication.Utility.PaginatedList<EducationalApplication.Models.Grade>> Html { get; private set; }
    }
}
#pragma warning restore 1591
