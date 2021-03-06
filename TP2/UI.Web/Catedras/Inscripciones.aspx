﻿<%@ Page Title="Inscripciones" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Inscripciones.aspx.cs" Inherits="UI.Web.Catedras.Inscripciones" %>
<asp:Content ID="ContentPlanes" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:GridView ID="GridView" runat="server" AutoGenerateColumns="False" 
        SelectedRowStyle-BackColor="Black" SelectedRowStyle-ForeColor="White" 
        DataKeyNames="ID" OnSelectedIndexChanged="gridView_SelectedIndexChanged" 
        CaptionAlign="Top" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField HeaderText="ID" DataField="IdInscripcion" />
            <asp:BoundField HeaderText="Materia" DataField="Materia" />
            <asp:BoundField DataField="Comision" HeaderText="Comisión" />
            <asp:BoundField DataField="AnioCalendario" HeaderText="Año" />
            <asp:BoundField DataField="Condicion" HeaderText="Condición" />
            <asp:BoundField DataField="Nota" HeaderText="Nota" />
            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
        </Columns>
        <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />

<SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="True"></SelectedRowStyle>
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
    </asp:GridView>
    <asp:Panel ID="gridActionsPanel" runat="server">
        <asp:LinkButton ID="editarLinkButton" runat="server" 
            OnClick="editarLinkButton_Click" CssClass="btn btnEditar">Editar</asp:LinkButton>
        <asp:LinkButton ID="eliminarLinkButton" runat="server" 
            OnClick="eliminarLinkButton_Click" CssClass="btn btnEliminar">Eliminar</asp:LinkButton>
        <asp:LinkButton ID="nuevoLinkButton" runat="server" 
            OnClick="nuevoLinkButton_Click" CssClass="btn btnNuevo">Nuevo</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formPanel" Visible="false" runat="server" CssClass="wrapForm">
        <asp:Label ID="formError" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblAlumno" runat="server" Text="Alumno: "></asp:Label>
        <asp:Label ID="lblNombreAlumno" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblPlan" runat="server" Text="Plan: "></asp:Label>
        <asp:Label ID="lblNombrePlan" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblMateria" runat="server" Text="Materia: "></asp:Label>
        <asp:DropDownList ID="cmbMateria" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lblCondicion" runat="server" Text="Condicion: "></asp:Label>
        <asp:DropDownList ID="cmbCondicion" runat="server"></asp:DropDownList>
        <br />
        <asp:Label ID="lblNota" runat="server" Text="Nota: "></asp:Label>
        <asp:TextBox ID="txtNota" runat="server"></asp:TextBox>
        <br />
        <asp:Panel ID="formActionsPanel" runat="server">
            <asp:LinkButton ID="aceptarLinkButton" runat="server" 
                OnClick="aceptarLinkButton_Click" CssClass="btn">Aceptar</asp:LinkButton>
            <asp:LinkButton ID="cancelarLinkButton" runat="server" 
                OnClick="cancelarLinkButton_Click" CssClass="btn">Cancelar</asp:LinkButton>
        </asp:Panel>
    </asp:Panel>

</asp:Content>
