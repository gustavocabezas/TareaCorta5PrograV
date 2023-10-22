<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="TareaCorta5PrograV._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <link href="CSS/Inicio.css" rel="stylesheet" />

    <main>

        <h1>Menú</h1>


<div class="row">
    <div class ="col-md-10 col-md-offset-1">
        <div class="table-responsive">
            <asp:GridView ID="Dato" Width="100%" runat="server"  CellPadding="4" ForeColor="Black" Height="292px" 
                 CssClass="table table-bordered tab-condensed table-responsive table-hover" BackColor="White" BorderStyle="Solid" BorderColor="#999999" BorderWidth="3px" CellSpacing="2">
                <AlternatingRowStyle BackColor="White" ForeColor="black" />
                        <HeaderStyle BackColor="#6b696b" Font-Bold="true" Font-Size="Larger" ForeColor="White"/>
                        <RowStyle BackColor="#f5f5f5"/>
                        <SelectedRowStyle BackColor="#669999" Font-Bold="true" ForeColor="White"/>

            </asp:GridView>
        </div>
    </div>
</div>

       
    </main>

</asp:Content>
