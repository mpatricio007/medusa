<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Styles/Site.master" CodeBehind="WebForm2.aspx.cs" Inherits="Medusa.WebForm2" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <script type="text/javascript">
        var ajaxRequest;

        $(document).ready(function () {
            $('#btnShowCourses').click(function () {
                GetCoursesData();
            });
        });

        function GetCoursesData() {
            if (ajaxRequest != null) {
                ajaxRequest.abort();
            }
            ajaxRequest = $.getJSON("MedusaService.svc/GetTipoDocEntrada?name='rp'", null,
            function (data) {
                var sb = new Sys.StringBuilder();
                sb.append("<div>");
                $.each(data.d, function (i, item) {
                    CreateDivForOneCourse(sb, item)
                });
                sb.append("</div>");
                $('#divResults').html(sb.toString());
            });
        }

        function CreateDivForOneCourse(stringBuilder, item) {
            stringBuilder.append("<div><span>");
            stringBuilder.append(item.CourseID);
            stringBuilder.append("</span> <span>");
            stringBuilder.append(item.Title);
            stringBuilder.append("</span></div>");
        }
    </script>


      <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <input type="button" id="btnShowCourses" value="Show Courses" /><br />
        <div id="divResults" />
    </asp:Content>