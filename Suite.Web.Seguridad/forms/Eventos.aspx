<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Eventos.aspx.cs" Inherits="Suite.Web.Seguridad.forms.Eventos" MasterPageFile="../MasterPage.Master"%>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxLoadingPanel" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
             <table width="100%">
                 <tr>
                     <td width="150px">
                         <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Unidad administrativa:" 
                             Font-Bold="True" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue">
                         </dx:ASPxLabel>
                     </td>
                     <td>
                         <dx:ASPxComboBox ID="ASPxComboBoxUnidadesAdministrativas" runat="server" 
                             DataSourceID="EntityDataSourceUnidadesAdministrativas" TextField="Nombre" 
                             ValueField="idUnidadAdministrativa" ValueType="System.String" 
                             SelectedIndex="0" AutoPostBack="True" 
                             
                             onselectedindexchanged="ASPxComboBoxUnidadesAdministrativas_SelectedIndexChanged" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Spacing="0" 
                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                             <ClientSideEvents SelectedIndexChanged="function(s, e) {
	lpanel.Show();
	e.processOnServer = true;
}" />
                             <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                             </LoadingPanelImage>
                             <LoadingPanelStyle ImageSpacing="5px">
                             </LoadingPanelStyle>
                             <ButtonStyle Width="13px">
                             </ButtonStyle>
                         </dx:ASPxComboBox>
                         <asp:EntityDataSource ID="EntityDataSourceUnidadesAdministrativas" 
                             runat="server" ConnectionString="name=Seguridad2017Entities" 
                             DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                             EntitySetName="UnidadesAdministrativas" 
                             EntityTypeFilter="UnidadesAdministrativas" 
                             Select="it.[idUnidadAdministrativa], it.[Nombre]" 
                             Where="it.Eliminado=false" 
                             ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                            </asp:EntityDataSource>
                     </td>
                     <td>
                         <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Aplicación:" 
                             Font-Bold="True" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue">
                         </dx:ASPxLabel>
                     </td>
                     <td>
                         <dx:ASPxComboBox ID="ASPxComboBoxAplicaciones" runat="server" 
                             DataSourceID="EntityDataSourceAplicaciones" TextField="Nombre" 
                             ValueField="idAplicacion" ValueType="System.String" AutoPostBack="True" 
                             onselectedindexchanged="ASPxComboBoxAplicaciones_SelectedIndexChanged" 
                             SelectedIndex="0" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Spacing="0" 
                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                             <ClientSideEvents SelectedIndexChanged="function(s, e) {
	lpanel.Show();
	e.processOnServer = true;
}" />
                             <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                             </LoadingPanelImage>
                             <LoadingPanelStyle ImageSpacing="5px">
                             </LoadingPanelStyle>
                             <ButtonStyle Width="13px">
                             </ButtonStyle>
                         </dx:ASPxComboBox>
                         <asp:EntityDataSource ID="EntityDataSourceAplicaciones" runat="server" 
                             ConnectionString="name=Seguridad2017Entities" 
                             DefaultContainerName="Seguridad2017Entities" 
                             CommandText="Select distinct A.idAplicacion,A.Nombre from Aplicaciones as A
                                    inner join AplicacionesUA AS B on B.idAplicacion=B.idAplicacion
                                    where B.idUnidadAdministrativa=@idUnidadAdministrativa" ContextTypeName="Seguridad.Model.Seguridad2017Entities" 
                             
                            >
                             <CommandParameters>
                                 <asp:ControlParameter ControlID="ASPxComboBoxUnidadesAdministrativas" 
                                     DbType="Int32" Name="idUnidadAdministrativa" PropertyName="Value" />
                             </CommandParameters>
                         </asp:EntityDataSource>
                     </td>
                     <td>
                         <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Usuario:" Font-Bold="True" 
                             CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue">
                         </dx:ASPxLabel>
                     </td>
                     <td>
                         <dx:ASPxComboBox ID="ASPxComboBoxUsuarios" runat="server" 
                             DataSourceID="EntityDataSourceUsuarios" TextField="UserName" 
                             ValueField="idUsuario" ValueType="System.String" AutoPostBack="True" 
                             onselectedindexchanged="ASPxComboBoxUsuarios_SelectedIndexChanged" 
                             SelectedIndex="0" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                             CssPostfix="Office2010Blue" Spacing="0" 
                             SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
                             <ClientSideEvents SelectedIndexChanged="function(s, e) {
	lpanel.Show();
	e.processOnServer = true;
}" />
                             <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                             </LoadingPanelImage>
                             <LoadingPanelStyle ImageSpacing="5px">
                             </LoadingPanelStyle>
                             <ButtonStyle Width="13px">
                             </ButtonStyle>
                         </dx:ASPxComboBox>
                         <asp:EntityDataSource ID="EntityDataSourceUsuarios" runat="server" 
                             ConnectionString="name=Seguridad2017Entities" 
                             DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                             EntitySetName="Usuarios" EntityTypeFilter="Usuarios" 
                             Select="it.[idUsuario], it.[UserName], it.[Nombre]" 
                             Where="it.idUnidadAdministrativa=@idUnidadAdministrativa" 
                             ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                             <WhereParameters>
                                 <asp:ControlParameter ControlID="ASPxComboBoxUnidadesAdministrativas" 
                                     DbType="Int32" Name="idUnidadAdministrativa" PropertyName="Value" />
                             </WhereParameters>
                         </asp:EntityDataSource>
                     </td>
                 </tr>
                
                 <tr>
                     <td colspan ="6">
                         <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="400px" 
                             Width="1000px">
                             <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" 
                                 AutoGenerateColumns="False" DataSourceID="EntityDataSourceEventos" 
                                 KeyFieldName="idRegistroEvento" Caption="Registro de Eventos" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue">
                                 <Columns>
                                     <dx:GridViewDataTextColumn FieldName="Aplicacion" VisibleIndex="1">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn FieldName="idRegistroEvento" ReadOnly="True" 
                                         VisibleIndex="0">
                                         <Settings AllowAutoFilter="False" />
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataComboBoxColumn Caption="Módulo" FieldName="modulo" 
                                         VisibleIndex="2">
                                         <PropertiesComboBox DataSourceID="EntityDataSourceModulos" 
                                             TextField="Descripcion" ValueField="idModulo" ValueType="System.String" 
                                             Spacing="0">
                                         </PropertiesComboBox>
                                         <Settings AllowAutoFilter="False" />
                                     </dx:GridViewDataComboBoxColumn>
                                     <dx:GridViewDataTextColumn FieldName="usuario" VisibleIndex="5" Visible="False">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn FieldName="UsuarioIP" ShowInCustomizationForm="True" 
                                         VisibleIndex="4">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn FieldName="UsuarioAg" VisibleIndex="6">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataDateColumn FieldName="FechaHora" VisibleIndex="7">
                                         <PropertiesDateEdit DisplayFormatString="g" EditFormat="DateTime" Spacing="0">
                                         </PropertiesDateEdit>
                                     </dx:GridViewDataDateColumn>
                                     <dx:GridViewDataTextColumn FieldName="PistaColor" VisibleIndex="9">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn FieldName="PistaTipo" VisibleIndex="10">
                                     </dx:GridViewDataTextColumn>
                                     <dx:GridViewDataTextColumn FieldName="PistaDetalle" VisibleIndex="11">
                                         <Settings AllowAutoFilter="False" />
                                     </dx:GridViewDataTextColumn>
                                 </Columns>
                                 <SettingsPager Visible="False" Mode="ShowAllRecords">
                                 </SettingsPager>
                                 <Settings ShowFilterRow="True" />
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
                                 <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                     CssPostfix="Office2010Blue">
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
                             <asp:EntityDataSource ID="EntityDataSourceEventos" runat="server" 
                                 ConnectionString="name=Seguridad2017Entities" 
                                 DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                                 Where="" OrderBy="it.idRegistroEvento Desc" Select="it.[idRegistroEvento],
it.[Aplicacion],
it.[modulo],
it.[usuario],
it.[UsuarioIP],
it.[UsuarioAg],
it.[FechaHora],
it.[PistaColor],
it.[PistaTipo],
it.[PistaDetalle] " 
                                 ContextTypeName="Seguridad.Model.Seguridad2017Entities" 
                                 AutoGenerateWhereClause="True" 
                                 CommandText="select A.idRegistroEvento,B.Nombre as Aplicacion,C.Descripcion as modulo,D.Nombre as usuario,
A.UsuarioIP,A.UsuarioAg,A.FechaHora,A.PistaColor,A.PistaTipo,A.PistaDetalle
from RegistroEventos as A
inner join Aplicaciones as B on B.idAplicacion=A.idAplicacion
inner join modulos as C on C.idModulo=A.idModulo
inner join usuarios as D on D.idUsuario=A.idUsuario
where A.idUnidadAdministrativa==@idUnidadAdministrativa and A.idAplicacion=@idAplicacion and A.idUsuario=@idUsuario">
                                 <CommandParameters>
                                     <asp:ControlParameter ControlID="ASPxComboBoxUnidadesAdministrativas" 
                                         DbType="Int32" Name="idUnidadAdministrativa" PropertyName="Value" />
                                     <asp:ControlParameter ControlID="ASPxComboBoxAplicaciones" DbType="Int32" 
                                         Name="idAplicacion" PropertyName="Value" />
                                     <asp:ControlParameter ControlID="ASPxComboBoxUsuarios" DbType="Int32" 
                                         Name="idUsuario" PropertyName="Value" />
                                 </CommandParameters>
                             </asp:EntityDataSource>
                             <asp:EntityDataSource ID="EntityDataSourceModulos" runat="server" 
                                 ConnectionString="name=Seguridad2017Entities" 
                                 DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                                 EntitySetName="Modulos" EntityTypeFilter="Modulos" 
                                 Select="it.[idModulo], it.[Descripcion]" 
                                 Where="it.idAplicacion=@idAplicacion" 
                                 ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                                 <WhereParameters>
                                     <asp:ControlParameter ControlID="ASPxComboBoxAplicaciones" DbType="Int32" 
                                         Name="idAplicacion" PropertyName="Value" />
                                 </WhereParameters>
                             </asp:EntityDataSource>
                             <dx:ASPxLoadingPanel ID="ASPxLoadingPanel1" runat="server" 
                                 ClientInstanceName="lpanel" 
                                 CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                 CssPostfix="Office2010Blue" ImageSpacing="5px" Modal="True" 
                                 Text="Procesando&amp;hellip;">
                                 <Image Url="~/App_Themes/Office2010Blue/Web/Loading.gif">
                                 </Image>
                             </dx:ASPxLoadingPanel>
                             <br />
                         </asp:Panel>
                 
                     </td>
                 </tr>
             </table>
         </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>
