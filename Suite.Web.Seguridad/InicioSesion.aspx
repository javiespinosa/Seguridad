<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InicioSesion.aspx.cs" Inherits="Suite.Web.Seguridad.InicioSesion" %>

<%@ Register Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxMenu" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dxrp" Namespace="DevExpress.Web.ASPxRoundPanel" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dxp" Namespace="DevExpress.Web.ASPxPanel" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
</head>
<body>
	<form id="form1" runat="server">
	<div align="center">
		<div align="center">
			<dxrp:ASPxRoundPanel ID="rpRoundPanel" runat="server" Width="1000px" ClientInstanceName="RoundPanel1"
				ShowHeader="False" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue"
				EnableDefaultAppearance="False" GroupBoxCaptionOffsetX="6px" GroupBoxCaptionOffsetY="-19px"
				SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
				<ContentPaddings PaddingBottom="10px" PaddingLeft="9px" PaddingRight="11px" PaddingTop="10px" />
				<HeaderStyle>
					<Paddings PaddingBottom="6px" PaddingLeft="9px" PaddingRight="11px" PaddingTop="3px" />
				</HeaderStyle>
				<PanelCollection>
					<dxp:PanelContent ID="PanelContent1" runat="server">
						<dxp:ASPxPanel ID="pnlContent" runat="server" Width="100%" ClientInstanceName="ContentPanel">
							<PanelCollection>
								<dxp:PanelContent ID="PanelContent2" runat="server">
									<table width="100%">
										<tr>
											<td>
											</td>
											<td width="700px" align="center">
												<h1>
													Nombre de la Empresa
												</h1>
												<dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Administrador de Seguridad" Font-Bold="True" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css"
													CssPostfix="Office2010Blue" ForeColor="#FF6600">
												</dx:ASPxLabel>
											</td>
											<td>
											</td>
										</tr>
									</table>
								</dxp:PanelContent>
							</PanelCollection>
						</dxp:ASPxPanel>
					</dxp:PanelContent>
				</PanelCollection>
			</dxrp:ASPxRoundPanel>
			<h2>
				Iniciar sesión
			</h2>
			<p>
				<dx:ASPxLabel ID="ASPxLabel4" runat="server" Text="Especifique su nombre de usuario y contraseña."
					CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
				</dx:ASPxLabel>
			</p>
		</div>
		<asp:Login ID="SuiteLogin" runat="server" DisplayRememberMe="False" FailureText="sesión erroneo."
			LoginButtonText="Iniciar" PasswordLabelText="Contraseña:" PasswordRequiredErrorMessage="requerido."
			TitleText="Inicio" UserNameLabelText="Usuario:" UserNameRequiredErrorMessage="requerido."
			Width="40px" BackColor="#EFF3FB" BorderColor="#B5C7DE" BorderStyle="Solid" BorderWidth="1px"
			Font-Names="Verdana" Font-Size="0.8em" BorderPadding="4" ForeColor="#333333">
			<InstructionTextStyle Font-Italic="True" ForeColor="Black" />
			<LoginButtonStyle BackColor="White" BorderColor="#507CD1" BorderStyle="Solid" BorderWidth="1px"
				Font-Names="Verdana" Font-Size="0.8em" ForeColor="#284E98" />
			<TextBoxStyle Width="510"></TextBoxStyle>
			<TextBoxStyle Font-Size="0.8em" />
			<TitleTextStyle BackColor="#507CD1" Font-Bold="True" ForeColor="#FFFFFF" Font-Size="0.9em" />
		</asp:Login>
	</div>
	</form>
</body>
</html>
