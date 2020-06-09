<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reestablecer.aspx.cs" Inherits="Suite.Web.Seguridad.forms.Reestablecer" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>



<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate >
            <asp:Panel ID="Panel1" runat="server">
                <table width="98%">
                    <tr>
                        <td>
                             <dx:ASPxLabel ID="ASPxLabel3" runat="server" 
                            Text="Pulse el botón de abajo para reestablecer la contraseña del usuario. La contraseña actual del usuario se reemplazará por la contraseña con la que fue creada inicialmente la cuenta.">
                            </dx:ASPxLabel>
                            <br/><br/>
                            <center>
                                <dx:ASPxButton ID="ASPxButtonReestablecer" runat="server" 
                                    Text="Reestablecer Contraseña" onclick="ASPxButtonReestablecer_Click" 
                                    CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                    CssPostfix="Office2010Blue" 
                                    SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" >
                                </dx:ASPxButton>
                            </center>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="Panel2" runat="server" Visible="False">
                <center>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Contraseña reestablecida">
                    </dx:ASPxLabel>
                </center>
            </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        
        
    </div>
    </form>
</body>
</html>
