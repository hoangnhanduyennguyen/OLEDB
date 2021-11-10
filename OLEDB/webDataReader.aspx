<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="webDataReader.aspx.cs" Inherits="OLEDB.webDataReader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>WEB DATA READER</title>
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T" crossorigin="anonymous"/>
    <style type="text/css">
        .auto-style1 {
        text-decoration: underline;
        text-align: center;
        }
        .auto-style4 {
        width: 258px;
        }
        .auto-style6 {
        width: 99px;
        }
        .auto-style7 {
        height: 23px;
        width: 99px;
        }
        .auto-style5 {
        height: 23px;
        }
        .auto-style8 {
        width: 99px;
        height: 26px;
        }
        .auto-style9 {
        height: 26px;
        }
        .auto-style10 {
        width: 700px;
        }
        .auto-style11 {
        text-align: center;
        }
    </style>
</head>
<body>
    <h1 align="center"><u>THE DATAREADER OBJECT</u></h1>
    <form id="form1" runat="server">
        <div>
            <table align="center" class="auto-style10">
                <tr>
                    <td>
                        <asp:Label ID="lblName" runat="server">Select a Course</asp:Label>
                    <br/>
                        <asp:ListBox ID="lstCourse" runat="server" AutoPostBack="true" OnSelectedIndexChanged="lstCourse_SelectedIndexChanged">

                        </asp:ListBox>
                    </td>
                    <td>
                        <table class="auto-style4" align="center">
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="lblNumber" runat="server" Text="Number:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNumber" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="lblCourse1" runat="server" Text="Title:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTitle" runat="server" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="lblCourse2" runat="server" Text="Duration:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDuration" runat="server" Width="25px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="txtCourse3" runat="server" Text="Teacher:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtTeacher" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style8">
                                    <asp:Label ID="lblCourse4" runat="server" Text="Description:"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" style="resize:none"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" class="btn btn-success"/>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" class="auto-style1">
                        <!--Styling in ASP will run at server, not in the browser
                            <AlternatingRowStyle BackColor="#FFADAD" />-->
                         <asp:GridView ID="gridResult" runat="server" BackColor="White" BorderColor="#FFDAC7" BorderStyle="None" BorderWidth="1px" CellPadding="5" GridLines="Vertical">            
                             
                             <FooterStyle BackColor="#FEF1E6" ForeColor="Black"/>
                             <HeaderStyle BackColor="#FFADAD" ForeColor="White" Font-Bold ="false" Font-Underline="false"/>
                             <PagerStyle BackColor="#9999999" ForeColor="Black" HorizontalAlign="Center" />
                             <SelectedRowStyle BackColor="#FF5733" ForeColor="White" Font-Bold="true" />
                             <SortedAscendingCellStyle BackColor="#DAF7A6" />
                             <SortedAscendingHeaderStyle BackColor="Yellow" />
                             <SortedDescendingCellStyle BackColor="#3160CC"/>
                             <SortedDescendingHeaderStyle BackColor="#17F9F2"/>
                         </asp:GridView>
                    </td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style11" colspan="2">&nbsp;</td>
                    <td class="auto-style11">&nbsp;</td>
                </tr>
            </table>
        </div>
        <br />
        <br />
        <br />
        <br />
        <asp:GridView ID="gridTest" runat="server"></asp:GridView>
        <br />
        <br />
        <br />
        <br />
    </form>
</body>
</html>
