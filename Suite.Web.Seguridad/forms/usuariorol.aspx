<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuariorol.aspx.cs" Inherits="Suite.Web.Seguridad.forms.usuariorol" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Rol:">
                    </dx:ASPxLabel>
                </td>
                <td>
                    <dx:ASPxComboBox ID="ASPxComboBoxRoles" runat="server" 
                        DataSourceID="EntityDataSourceRoles" TextField="Descripcion" ValueField="idRol" 
                        ValueType="System.String" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" Spacing="0" 
                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                        <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                        </LoadingPanelImage>
                        <LoadingPanelStyle ImageSpacing="5px">
                        </LoadingPanelStyle>
                        <ButtonStyle Width="13px">
                        </ButtonStyle>
                    </dx:ASPxComboBox>
                   
                    <asp:EntityDataSource ID="EntityDataSourceRoles" runat="server" 
                        ConnectionString="name=Seguridad2017Entities" 
                        DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                        EntitySetName="Roles" EntityTypeFilter="Roles" 
                        Select="it.[idRol], it.[Descripcion]" 
                        ContextTypeName="Seguridad.Model.Seguridad2017Entities" 
                        Where="it.[Eliminado]= false">
                    </asp:EntityDataSource>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <dx:ASPxButton ID="ASPxButtonGuardar" runat="server" Text="Asignar" 
                        onclick="ASPxButtonGuardar_Click" Height="25px" Width="100px" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" 
                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    </dx:ASPxButton>
                </td>
                <td align="right">
                    <dx:ASPxButton ID="ASPxButtonQuitar" runat="server" Text="Quitar" 
                        onclick="ASPxButtonQuitar_Click" Width="100px" 
                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                        CssPostfix="Office2010Blue" 
                        SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                    </dx:ASPxButton>
                </td>
               
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
