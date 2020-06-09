<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Excepciones.aspx.cs" Inherits="Suite.Web.Seguridad.forms.Excepciones"
	MasterPageFile="../MasterPage.Master" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
	Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxEditors" Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
	<asp:ScriptManager ID="ScriptManager1" runat="server">
	</asp:ScriptManager>
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		<ContentTemplate>
			<table width="1000px">
				<tr>
					<td width="150px">
						<dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Unidad administrativa:" Font-Bold="True"
							CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
						</dx:ASPxLabel>
					</td>
					<td>
						<dx:ASPxComboBox ID="ASPxComboBoxUnidadesAdministrativas" runat="server" DataSourceID="EntityDataSourceUnidadesAdministrativas"
							TextField="Nombre" ValueField="idUnidadAdministrativa" ValueType="System.String"
							SelectedIndex="0" AutoPostBack="True" OnSelectedIndexChanged="ASPxComboBoxUnidadesAdministrativas_SelectedIndexChanged"
							CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue"
							Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
							<LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
							</LoadingPanelImage>
							<LoadingPanelStyle ImageSpacing="5px">
							</LoadingPanelStyle>
							<ButtonStyle Width="13px">
							</ButtonStyle>
						</dx:ASPxComboBox>
						<asp:EntityDataSource ID="EntityDataSourceUnidadesAdministrativas" runat="server"
							ConnectionString="name=Seguridad2017Entities" DefaultContainerName="Seguridad2017Entities"
							EnableFlattening="False" EntitySetName="UnidadesAdministrativas" EntityTypeFilter="UnidadesAdministrativas"
							Select="it.[idUnidadAdministrativa], it.[Nombre]" Where="it.Eliminado=false"
							ContextTypeName="Seguridad.Model.Seguridad2017Entities">
						</asp:EntityDataSource>
					</td>
					<td>
						<dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Aplicación:" Font-Bold="True"
							CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
						</dx:ASPxLabel>
					</td>
					<td>
						<dx:ASPxComboBox ID="ASPxComboBoxAplicaciones" runat="server" DataSourceID="EntityDataSourceAplicaciones"
							TextField="Nombre" ValueField="idAplicacion" ValueType="System.String" AutoPostBack="True"
							OnSelectedIndexChanged="ASPxComboBoxAplicaciones_SelectedIndexChanged" SelectedIndex="0"
							CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue"
							Spacing="0" SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
							<LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
							</LoadingPanelImage>
							<LoadingPanelStyle ImageSpacing="5px">
							</LoadingPanelStyle>
							<ButtonStyle Width="13px">
							</ButtonStyle>
						</dx:ASPxComboBox>
						<asp:EntityDataSource ID="EntityDataSourceAplicaciones" runat="server" ConnectionString="name=Seguridad2017Entities"
							DefaultContainerName="Seguridad2017Entities" CommandText="Select distinct A.idAplicacion,A.Nombre from Aplicaciones as A
                                    inner join AplicacionesUA AS B on B.idAplicacion=B.idAplicacion
                                    where B.idUnidadAdministrativa=@idUnidadAdministrativa" ContextTypeName="Seguridad.Model.Seguridad2017Entities">
							<CommandParameters>
								<asp:ControlParameter ControlID="ASPxComboBoxUnidadesAdministrativas" DbType="Int32"
									Name="idUnidadAdministrativa" PropertyName="Value" />
							</CommandParameters>
						</asp:EntityDataSource>
					</td>
				</tr>
				<tr>
					<td width="1000px" colspan="4">
						<asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Height="400px">
							<dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="1000px" AutoGenerateColumns="False"
								Caption="Excepciones" DataSourceID="EntityDataSourceExcepcionesLog" KeyFieldName="idExcepcion"
								CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
								<Columns>
									<dx:GridViewCommandColumn VisibleIndex="0">
										<ClearFilterButton Visible="True">
										</ClearFilterButton>
									</dx:GridViewCommandColumn>
									<dx:GridViewDataTextColumn Caption="ID" FieldName="idExcepcion" ReadOnly="True" VisibleIndex="1">
										<Settings AllowAutoFilter="False" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataComboBoxColumn Caption="Módulo" FieldName="idModulo" VisibleIndex="4">
										<PropertiesComboBox DataSourceID="EntityDataSourceModulos" TextField="Descripcion"
											ValueField="idModulo" ValueType="System.String" Spacing="0">
										</PropertiesComboBox>
									</dx:GridViewDataComboBoxColumn>
									<dx:GridViewDataComboBoxColumn Caption="Usuario" FieldName="idUsuario" VisibleIndex="5">
										<PropertiesComboBox DataSourceID="EntityDataSourceUsuarios" TextField="UserName"
											ValueField="idUsuario" ValueType="System.String" Spacing="0">
										</PropertiesComboBox>
									</dx:GridViewDataComboBoxColumn>
									<dx:GridViewDataTextColumn Caption="IP" FieldName="UsuarioIP" VisibleIndex="7">
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="Mensaje" VisibleIndex="8">
										<Settings AllowAutoFilter="False" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="StackTrace" VisibleIndex="9">
										<Settings AllowAutoFilter="False" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="TargetSite" VisibleIndex="10">
										<Settings AllowAutoFilter="False" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataTextColumn FieldName="HelpLink" VisibleIndex="11">
										<Settings AllowAutoFilter="False" />
									</dx:GridViewDataTextColumn>
									<dx:GridViewDataDateColumn Caption="Fecha" FieldName="FechaHora" VisibleIndex="6">
										<PropertiesDateEdit DisplayFormatString="G" Spacing="0">
										</PropertiesDateEdit>
									</dx:GridViewDataDateColumn>
								</Columns>
								<SettingsPager Mode="ShowAllRecords">
								</SettingsPager>
								<Settings ShowFilterRow="True" />
								<SettingsText EmptyDataRow="No se encontraron registros" />
								<Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
									<LoadingPanelOnStatusBar Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
									</LoadingPanelOnStatusBar>
									<LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
									</LoadingPanel>
								</Images>
								<ImagesFilterControl>
									<LoadingPanel Url="~/App_Themes/Office2010Blue/GridView/Loading.gif">
									</LoadingPanel>
								</ImagesFilterControl>
								<Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
									<Header ImageSpacing="5px" SortingImageSpacing="5px">
									</Header>
									<LoadingPanel ImageSpacing="5px">
									</LoadingPanel>
								</Styles>
								<StylesPager>
									<PageNumber ForeColor="#3E4846">
									</PageNumber>
									<Summary ForeColor="#1E395B">
									</Summary>
								</StylesPager>
								<StylesEditors ButtonEditCellSpacing="0">
									<ProgressBar Height="21px">
									</ProgressBar>
								</StylesEditors>
							</dx:ASPxGridView>
							<asp:EntityDataSource ID="EntityDataSourceExcepcionesLog" runat="server" ConnectionString="name=Seguridad2017Entities"
								DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" EntitySetName="ExcepcionesLog"
								EntityTypeFilter="ExcepcionesLog" OrderBy="it.idExcepcion desc" ContextTypeName="Seguridad.Model.Seguridad2017Entities">
							</asp:EntityDataSource>
							<asp:EntityDataSource ID="EntityDataSourceModulos" runat="server" ConnectionString="name=Seguridad2017Entities"
								DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" EntitySetName="Modulos"
								EntityTypeFilter="" Select="it.[idModulo], it.[Descripcion]" Where="" ContextTypeName="Seguridad.Model.Seguridad2017Entities">
							</asp:EntityDataSource>
							<asp:EntityDataSource ID="EntityDataSourceUsuarios" runat="server" ConnectionString="name=Seguridad2017Entities"
								DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" EntitySetName="Usuarios"
								EntityTypeFilter="Usuarios" Select="it.[idUsuario], it.[UserName]" Where="" ContextTypeName="Seguridad.Model.Seguridad2017Entities">
							</asp:EntityDataSource>
							<br />
							<br />
						</asp:Panel>
					</td>
				</tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
