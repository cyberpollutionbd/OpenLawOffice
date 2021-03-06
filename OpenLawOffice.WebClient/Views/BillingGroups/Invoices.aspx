﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/SiteNoRightBar.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<OpenLawOffice.WebClient.ViewModels.Billing.InvoiceViewModel>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Billing Group Invoices
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Billing Group Invoices</h2>
    
    <table class="listing_table">
        <tr>
            <th style="text-align: center;">
                Date
            </th>
            <th style="text-align: center;">
                Due
            </th>
            <th style="text-align: center;">
                Ext. Id
            </th>
            <th style="text-align: center;">
                Total
            </th>
            <th style="text-align: center; width: 25px;">
                
            </th>
        </tr>
        <% bool altRow = true; 
           foreach (var item in Model)
           { 
               altRow = !altRow;
               if (altRow)
               { %> <tr class="tr_alternate"> <% }
               else
               { %> <tr> <% } %>
            <td>
                <%: item.Date.ToShortDateString() %>
            </td>
            <td>
                <%: item.Due.ToShortDateString() %>
            </td>
            <td>
                <%: item.ExternalInvoiceId %>
            </td>
            <td>
                <%: item.Total.ToString("C") %>
            </td>
            <td>
                <%: Html.ActionLink("Go", "GroupDetails", "Invoices", new { id = item.Id.Value }, new { @class = "btn-money-arrow", title = "Bill" })%>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>
