#pragma checksum "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d0ed4e13bdcfad584b3400ff19d8cea14e4e56e8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ClassRooms_Index), @"mvc.1.0.view", @"/Views/ClassRooms/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d0ed4e13bdcfad584b3400ff19d8cea14e4e56e8", @"/Views/ClassRooms/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8f32439c51fa860ff323bb9b937bc88b9f15c88", @"/Views/_ViewImports.cshtml")]
    public class Views_ClassRooms_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<EducationalApplication.Utility.PaginatedList<EducationalApplication.Models.ClassRoom>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("value", "0", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("id", new global::Microsoft.AspNetCore.Html.HtmlString("register"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<section class=""content-header"">
    <h1 style=""float:right"">
        مدیریت کلاس های درسی
    </h1>
    <ol class=""breadcrumb"">
        <li><a href=""#""><i class=""fa fa-home""></i> خانه</a></li>
        <li class=""active""></li>
    </ol>
</section>
<div style=""margin-top: 50px;margin-bottom:3px"">
    <a class=""btn btn-success openmodal"" href=""#"" data-toggle=""modal"" data-target=""#myModal"">ثبت جدید</a>
</div>
<div class=""row"">
    <div class=""col-sm-12"">
        <div class=""text-center"" style=""border-style: solid;border-color:dimgray"">
            <h2 style=""font-size:medium"">جستجو</h2>
        </div>
");
#nullable restore
#line 22 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
         using (Html.BeginForm("Index",
    "ClassRooms",
    FormMethod.Post))
        {


#line default
#line hidden
#nullable disable
            WriteLiteral(@"            <div class=""box zmdi-border-color"" style=""height:auto"">
                <div class=""box-body table-responsive no-padding row "">
                    <div style=""width:90%;margin-right:40px;margin-top:10px"">
                        <div class=""row"">
                            <label for=""exampleInputEmail1"">عنوان جستجو:</label>
                            <input name=""searchString"" placeholder=""متن را وارد کنید""");
            BeginWriteAttribute("class", " class=\"", 1285, "\"", 1293, 0);
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
#line 40 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
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
                            مقطع
                        </th>
                        <th>
                            رشته
                        </th>
                        <th>
                            تعداد دانش آموزان
                        </th>
                        <th>
                            تعداد معلمان
                        </th>
                        <th>
                            عملیات
                        <");
            WriteLiteral("/th>\r\n                    </tr>\r\n");
#nullable restore
#line 75 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                       int rowNo = 0; 

#line default
#line hidden
#nullable disable
#nullable restore
#line 76 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                     foreach (var item in Model)
                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <tr>\r\n                            <td>\r\n                                ");
#nullable restore
#line 80 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                            Write(rowNo += 1);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 83 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                           Write(item.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 86 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                           Write(item.Code);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 89 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                           Write(item.Grade.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 92 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                           Write(item.Major.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 95 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                           Write(item.Students.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" نفر\r\n                            </td>\r\n                            <td>\r\n                                ");
#nullable restore
#line 98 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                           Write(item.TeachersToClassRoom.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" نفر\r\n                            </td>\r\n                            <td>\r\n                                <button type=\"button\" class=\"btn btn-danger\" data-toggle=\"modal\" data-target=\"#QuestionModal\" onclick=\"AssignButtonClicked(this)\" data-assigned-id=\"");
#nullable restore
#line 101 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                                                                                                                                                                               Write(item.Id);

#line default
#line hidden
#nullable disable
            WriteLiteral("\"> حدف</button> |\r\n                                <button type=\"button\" class=\"btn btn-warning\" data-toggle=\"modal\" data-target=\"#EditModal\"");
            BeginWriteAttribute("id", " id=\"", 4052, "\"", 4065, 1);
#nullable restore
#line 102 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
WriteAttributeValue("", 4057, item.Id, 4057, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral("> ویرایش</button>\r\n                            </td>\r\n                        </tr>\r\n");
#nullable restore
#line 105 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </table>\r\n            </div>\r\n        </div>\r\n\r\n");
#nullable restore
#line 110 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
          
            var prevDisabled = !Model.HasPreviousPage ? "disabled" : "";
            var nextDisabled = !Model.HasNextPage ? "disabled" : "";
        

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d0ed4e13bdcfad584b3400ff19d8cea14e4e56e813183", async() => {
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
#line 116 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                      WriteLiteral(Model.PageIndex - 1);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4514, "btn", 4514, 3, true);
            AddHtmlAttributeValue(" ", 4517, "btn-default", 4518, 12, true);
#nullable restore
#line 117 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
AddHtmlAttributeValue(" ", 4529, prevDisabled, 4530, 13, false);

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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d0ed4e13bdcfad584b3400ff19d8cea14e4e56e816053", async() => {
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
#line 121 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
                      WriteLiteral(Model.PageIndex + 1);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-pageNumber", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["pageNumber"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginAddHtmlAttributeValues(__tagHelperExecutionContext, "class", 3, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            AddHtmlAttributeValue("", 4690, "btn", 4690, 3, true);
            AddHtmlAttributeValue(" ", 4693, "btn-default", 4694, 12, true);
#nullable restore
#line 122 "D:\Educationalapplication\EducationalApplication\EducationalApplication\Views\ClassRooms\Index.cshtml"
AddHtmlAttributeValue(" ", 4705, nextDisabled, 4706, 13, false);

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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d0ed4e13bdcfad584b3400ff19d8cea14e4e56e819448", async() => {
                WriteLiteral(@"
                    <input id=""GradeId"" name=""GradeId"" hidden />
                    <div class=""form-group"" style=""margin-right:5px"">
                        <label for=""exampleInputEmail1""> نام :</label>
                        <input display=""عنوان کلاس"" id=""Name"" name=""Name"" placeholder=""نام کلاس را وارد کنید"" class=""form-control"" style=""width:70%"" required>
                    </div>
                    <div class=""form-group"" style=""margin-right:5px"">
                        <label for=""exampleInputEmail1""> کد :</label>
                        <input display=""کد"" id=""Code"" name=""Code"" placeholder=""کد کلاس را وارد کنید"" class=""form-control"" style=""width:70%"" required>
                    </div>
                    <div class=""form-group"" style=""margin-right:5px"">
                        <label for=""exampleInputEmail1""> مقطع  :</label>
                        <select class=""form-control"" style=""width:160px"" name=""GradeId"" id=""GradeId"">
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d0ed4e13bdcfad584b3400ff19d8cea14e4e56e820754", async() => {
                    WriteLiteral("انتخاب کنید");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"
                        </select>
                    </div>
                    <div class=""form-group"" style=""margin-right:5px"">
                        <label for=""exampleInputEmail1""> رشته  :</label>
                        <select class=""form-control"" style=""width:160px"" name=""MajorId"" id=""MajorId"">
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d0ed4e13bdcfad584b3400ff19d8cea14e4e56e822335", async() => {
                    WriteLiteral("انتخاب کنید");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n\r\n                        </select>\r\n                    </div>\r\n                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
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
        FillComboBox('../ClassRooms/LoadMajors', 'MajorId');
        FillComboBox('../ClassRooms/LoadGrades', 'GradeId');
        $("".openmodal"").click(function () {
            clear('register', [""input""]);
        });
        $("".btn-warning"").click(function () {
            EditAjax(""../ClassRooms/Deatails"", this.id);
            var bodyStyle = document.body.style;
            bodyStyle.removeProperty('padding-right');
        });
        SetInputFilter([""Code""]);
    }
        function Save() {
        if (checkval");
            WriteLiteral(@"idity('register') === 0) {
            return;
        }
        else {
            $(""#myModal"").modal('toggle');
            var Parameters = [{ id: ""Name"", htmlname: ""Name"", special: """" }, { id: ""Code"", htmlname: ""Code"", special: """" } ,  { id: ""ClassRoomId"", htmlname: ""ClassRoomId"", special: """" },{ id: ""GradeId"", htmlname: ""GradeId"", special: ""combo"" },{ id: ""MajorId"", htmlname: ""MajorId"", special: ""combo"" }];
            PostAjax('../ClassRooms/Register', Parameters, ""../ClassRooms/Index"");
        }
	}
        function AssignButtonClicked(elem) {
        $('#ClassRoomId').val($(elem).data('assigned-id'));
    }
    function Remove() {
        var Parameters = [{ id: ""ClassRoomId"", htmlname: ""ClassRoomId"", special: """" }];
        PostAjax('../ClassRooms/Remove', Parameters, ""../ClassRooms/Index"");
    }
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<EducationalApplication.Utility.PaginatedList<EducationalApplication.Models.ClassRoom>> Html { get; private set; }
    }
}
#pragma warning restore 1591
