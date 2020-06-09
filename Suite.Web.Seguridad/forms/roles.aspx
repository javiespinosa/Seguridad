<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="roles.aspx.cs" Inherits="Suite.Web.Seguridad.forms.roles" MasterPageFile="../MasterPage.Master" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function MostrarPermisos(idRol, Descripcion) 
        {
            window.location.href = 'PermisosModulosRoles.aspx?id=' + idRol;
        }
    </script>
     <table width="100%">
         <tr>
             <td width="150px">
                 <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Unidad Administrativa:" 
                     Font-Bold="True" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                     CssPostfix="Office2010Blue">
                 </dx:ASPxLabel>
             </td>
             <td>
                 <dx:ASPxComboBox ID="ASPxComboBoxUnidadesAdministrativas" runat="server" 
                     DataSourceID="EntityDataSourceUnidadesAdministrativas" TextField="Nombre" 
                     ValueField="idUnidadAdministrativa" ValueType="System.String" 
                     AutoPostBack="True" SelectedIndex="0"
                     onselectedindexchanged="ASPxComboBoxUnidadesAdministrativas_SelectedIndexChanged" 
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
         </tr> 
         <tr>
             <td colspan="2" >
                 <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" 
                     AutoGenerateColumns="False" DataSourceID="EntityDataSourceRoles" 
                     KeyFieldName="idRol" onrowdeleting="ASPxGridView1_RowDeleting" 
                     onrowinserting="ASPxGridView1_RowInserting" 
                     onrowupdating="ASPxGridView1_RowUpdating" 
                     oncelleditorinitialize="ASPxGridView1_CellEditorInitialize" 
                     oncommandbuttoninitialize="ASPxGridView1_CommandButtonInitialize"
                     Caption="Usuarios" 
                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                     CssPostfix="Office2010Blue" >  
                     <Columns>
                         <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" 
                             VisibleIndex="0" Width="150px">
                             <EditButton Visible="True">
                                 <Image Url="~/App_Themes/Tema1/img/edit.png">
                                 </Image>
                             </EditButton>
                             <NewButton Visible="True">
                                 <Image Url="~/App_Themes/Tema1/img/Add.png">
                                 </Image>
                             </NewButton>
                             <DeleteButton Visible="True">
                                 <Image Url="~/App_Themes/Tema1/img/delete.png">
                                 </Image>
                             </DeleteButton>
                             <CancelButton>
                                 <Image Url="~/App_Themes/Tema1/img/cancelar.png">
                                 </Image>
                             </CancelButton>
                             <UpdateButton>
                                 <Image Url="~/App_Themes/Tema1/img/save.png">
                                 </Image>
                             </UpdateButton>
                         </dx:GridViewCommandColumn>
                         <dx:GridViewDataHyperLinkColumn FieldName="idRol" ReadOnly="True" 
                             VisibleIndex="0" Caption="Permisos" ToolTip="Asignar Permisos" >
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:MostrarPermisos('{0}')"
                                  ImageUrl="~/App_Themes/Tema1/img/key_01.png" >
                            </PropertiesHyperLinkEdit>
                              <Settings AllowAutoFilter="False" />
                              <Settings AllowAutoFilter="False"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                        </dx:GridViewDataHyperLinkColumn> 
                         <dx:GridViewDataTextColumn FieldName="idRol" ReadOnly="True" 
                             ShowInCustomizationForm="True" Visible="False" VisibleIndex="1">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn Caption="Código" FieldName="Codigo" 
                             ShowInCustomizationForm="True" VisibleIndex="5" Width="100px">
                             <PropertiesTextEdit Width="100px">
                             </PropertiesTextEdit>
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataMemoColumn Caption="Descripción" FieldName="Descripcion" 
                             ShowInCustomizationForm="True" VisibleIndex="6" Width="70%">
                             <PropertiesMemoEdit Height="50px" Width="300px">
                             </PropertiesMemoEdit>
                         </dx:GridViewDataMemoColumn>
                     </Columns>
                    <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" 
                         PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True" 
                         PopupEditFormVerticalAlign="WindowCenter" />
                    <SettingsText ConfirmDelete="¿Esta seguro de eliminar el registro?" 
                        PopupEditFormCaption="Información de Usuarios" /> 
                    <SettingsBehavior ConfirmDelete="True" />
                    <SettingsPager>
                         <AllButton Text="All">
                         </AllButton>
                         <NextPageButton Text="Next &gt;">
                         </NextPageButton>
                         <PrevPageButton Text="&lt; Prev">
                         </PrevPageButton>
                         <Summary AllPagesText="Página: {0} - {1} ({2} registros)" 
                             Text="Página {0} de {1} ({2} registros)" />
                     </SettingsPager> 
                    <SettingsEditing Mode="PopupEditForm" PopupEditFormHorizontalAlign="Center" 
                         PopupEditFormVerticalAlign="WindowCenter" PopupEditFormModal="True" 
                         EditFormColumnCount="1"></SettingsEditing> 
                    <Settings ShowFilterRow="True" /> 
                    <SettingsText ConfirmDelete="&#191;Esta seguro de eliminar el registro?" 
                         PopupEditFormCaption="Informaci&#243;n de Usuarios" 
                         EmptyDataRow="No se encontraron registros"></SettingsText> 
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
                 <asp:EntityDataSource ID="EntityDataSourceRoles" runat="server" 
                     ConnectionString="name=Seguridad2017Entities" 
                     DefaultContainerName="Seguridad2017Entities" EnableDelete="True" 
                     EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
                     EntitySetName="Roles" 
                     
                     Where="it.Eliminado=false and it.idUnidadAdministrativa=@idUnidadAdministrativa" 
                     ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                     <WhereParameters>
                         <asp:ControlParameter ControlID="ASPxComboBoxUnidadesAdministrativas" 
                             DbType="Int32" Name="idUnidadAdministrativa" PropertyName="Value" />
                     </WhereParameters>
                 </asp:EntityDataSource>
             </td>
         </tr>  
     </table> 
</asp:Content>  

