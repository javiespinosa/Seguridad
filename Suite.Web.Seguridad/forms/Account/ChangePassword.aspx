<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="Suite.Web.Seguridad.forms.Account.ChangePassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxLoadingPanel" TagPrefix="dx" %>
    <%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxRoundPanel" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxPanel" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <script type="text/javascript">
     var prm = Sys.WebForms.PageRequestManager.getInstance();
     prm.add_initializeRequest(prm_InitializeRequest);
     prm.add_endRequest(prm_EndRequest);
     function prm_InitializeRequest(sender, args) {
         lpanel.Show();
     }
     function prm_EndRequest(sender, args) {
         lpanel.Hide();

     }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="CatalogTitle" style="text-align: center">
        <dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Cambiar contraseña" Font-Bold="True">
        </dx:ASPxLabel>
    </div>
    <div align="center">
        <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="250px" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"
            CssPostfix="Office2010Blue" EnableDefaultAppearance="False" GroupBoxCaptionOffsetX="6px"
            GroupBoxCaptionOffsetY="-19px" HeaderText="Proporcione los datos siguientes:"
            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" DefaultButton="ASPxButtonGuardar">
            <ContentPaddings PaddingBottom="10px" PaddingLeft="9px" PaddingRight="11px" PaddingTop="10px" />
            <ContentPaddings PaddingLeft="9px" PaddingTop="10px" PaddingRight="11px" PaddingBottom="10px">
            </ContentPaddings>
            <HeaderStyle>
                <Paddings PaddingBottom="6px" PaddingLeft="9px" PaddingRight="11px" PaddingTop="3px" />
                <Paddings PaddingLeft="9px" PaddingTop="3px" PaddingRight="11px" PaddingBottom="6px">
                </Paddings>
            </HeaderStyle>
            <PanelCollection>
                <dx:PanelContent ID="PanelContent1" runat="server" SupportsDisabledAttribute="True">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="100%">
                                <tr>
                                    <td width="300px">
                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Contraseña actual:" Font-Bold="True"
                                            Width="130px" Font-Size="12px">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxActual" runat="server" Width="170px" TextMode="Password"
                                            Font-Size="16px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="20px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorActual" runat="server" ErrorMessage="Debe escribir la contraseña actual"
                                            Display="Dynamic" ForeColor="Red" ControlToValidate="TextBoxActual">Este campo es obligatorio</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="300px">
                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Nueva contraseña:" Font-Bold="True"
                                            Width="130px">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxNueva" runat="server" Width="170px" MaxLength="20" TextMode="Password"
                                            Font-Size="16px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" colspan="2" height="20px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorNueva" runat="server" ErrorMessage="Debe escribir la nueva contraseña"
                                            Display="Dynamic" ForeColor="Red" ToolTip="Ver Error al final de la Página" ControlToValidate="TextBoxNueva">Este campo es obligatorio</asp:RequiredFieldValidator>
                                        <asp:PasswordStrength ID="PasswordStrength1" runat="server" TargetControlID="TextBoxNueva"
                                            PreferredPasswordLength="8" MinimumNumericCharacters="1" PrefixText="Seguridad: "
                                            TextStrengthDescriptions="Muy pobre;Debil;Media;Fuerte;Excelente" HelpHandlePosition="AboveLeft"
                                            TextStrengthDescriptionStyles="TextIndicator_TextBox1;TextIndicator_TextBox3;TextIndicator_TextBox3_Handle;TextIndicator_TextBox1_Strength1;TextIndicator_TextBox1_Strength2"
                                            MinimumLowerCaseCharacters="1" MinimumUpperCaseCharacters="1" MinimumSymbolCharacters="1"
                                            RequiresUpperAndLowerCaseCharacters="True">
                                        </asp:PasswordStrength>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="300px">
                                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Repetir contraseña:" Font-Bold="True"
                                            Width="130px">
                                        </dx:ASPxLabel>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="TextBoxRepetir" runat="server" Width="170px" MaxLength="20" TextMode="Password"
                                            Font-Size="16px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="20px">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorRepetir" runat="server" ErrorMessage="Debe repetir la nueva contraseña"
                                            Display="Dynamic" ForeColor="Red" ToolTip="Ver Error al final de la Página" ControlToValidate="TextBoxRepetir">Este campo es obligatorio.</asp:RequiredFieldValidator>
                                        <br />
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ForeColor="Red" ControlToCompare="TextBoxNueva"
                                            ControlToValidate="TextBoxRepetir" Display="Dynamic" ErrorMessage="La contraseña no coincide. Verifique por favor."
                                            SetFocusOnError="True">La contraseña no coincide. Verifique por favor.</asp:CompareValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="center">
                                        <dx:ASPxButton ID="ASPxButtonGuardar" runat="server" ClientInstanceName="btn" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"
                                            CssPostfix="Office2010Blue" HorizontalAlign="Center" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css"
                                            Text="Aceptar" Width="128px" ToolTip="Guardar los cambios e iniciar sesión con la nueva contraseña"
                                            OnClick="ASPxButtonGuardar_Click">
                                            <Image Url="~/App_Themes/Tema1/img/PasswordBoxEditModule.Icon.png">
                                            </Image>
                                        </dx:ASPxButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <dx:ASPxLabel ID="ASPxLabel5" runat="server" Text="Después de guardar la nueva contraseña, se le direccionará al inicio de sesión."
                                            Font-Size="16px" ForeColor="#003366">
                                        </dx:ASPxLabel>
                                        <br />
                                        <dx:ASPxLabel ID="ASPxLabelError" runat="server" Text="" ForeColor="Red" ClientInstanceName="ASPxLabelError">
                                        </dx:ASPxLabel>
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <dx:ASPxLoadingPanel ID="lpanel" runat="server" ClientInstanceName="lpanel" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"
                        CssPostfix="Office2010Blue" ImageSpacing="5px" Modal="True" Text="Cambiando la contraseña&amp;hellip; ">
                        <Image Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                        </Image>
                    </dx:ASPxLoadingPanel>
                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
    </div>
</asp:Content>
