<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="aplicaciones.aspx.cs" Inherits="Suite.Web.Seguridad.forms.aplicaciones" MasterPageFile="../MasterPage.Master" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
     <table width="100%">
         <tr>
             <td colspan="2" >
                 <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                     Caption="Aplicaciones" DataSourceID="EntityDataSourceAplicaciones" 
                     KeyFieldName="idAplicacion" Width="100%" 
                     onrowinserting="ASPxGridView1_RowInserting" 
                     onrowdeleting="ASPxGridView1_RowDeleting" 
                     oncommandbuttoninitialize="ASPxGridView1_CommandButtonInitialize" 
                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                     CssPostfix="Office2010Blue">
                     <Columns>
                         <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="0" Width="100px">
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
                             <ClearFilterButton Visible="True">
                             </ClearFilterButton>
                         </dx:GridViewCommandColumn>
                         <dx:GridViewDataTextColumn FieldName="idAplicacion" ReadOnly="True" 
                             VisibleIndex="1" Caption="ID" 
                             ToolTip="Identificación Unica de la Aplicación">
                             <Settings AllowAutoFilter="False" />
                             <EditFormSettings Visible="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Clave" VisibleIndex="3">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Nombre" VisibleIndex="4">
                             <PropertiesTextEdit>
                                 <ValidationSettings>
                                     <RequiredField IsRequired="True" />
                                 </ValidationSettings>
                             </PropertiesTextEdit>
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataMemoColumn Caption="Descripción" FieldName="Descripcion" 
                             VisibleIndex="5">
                             <PropertiesMemoEdit Height="50px" Width="400px">
                             </PropertiesMemoEdit>
                         </dx:GridViewDataMemoColumn>
                         <dx:GridViewDataCheckColumn FieldName="Activa" VisibleIndex="9">
                         </dx:GridViewDataCheckColumn>
                         <dx:GridViewDataTextColumn FieldName="URL" VisibleIndex="6">
                             <PropertiesTextEdit>
                                 <ValidationSettings>
                                     <RequiredField IsRequired="True" />
                                 </ValidationSettings>
                             </PropertiesTextEdit>
                             <Settings AllowAutoFilter="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Version" VisibleIndex="7" 
                             Caption="Versión" Visible="False">
                             <Settings AllowAutoFilter="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Icono" VisibleIndex="8" Visible="False">
                             <Settings AllowAutoFilter="False" />
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataSpinEditColumn FieldName="Intentos" VisibleIndex="11">
                             <PropertiesSpinEdit AllowNull="False" DisplayFormatString="g" Spacing="0">
                                 <ValidationSettings>
                                     <RequiredField IsRequired="True" />
                                 </ValidationSettings>
                             </PropertiesSpinEdit>
                             <Settings AllowAutoFilter="False" />
                         </dx:GridViewDataSpinEditColumn>
                     </Columns>
                     <SettingsPager AlwaysShowPager="True">
                         <Summary AllPagesText="Página: {0} - {1} ({2} registros)" 
                             Text="Página {0} de {1} ({2} registros)" />
                     </SettingsPager>
                     <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" 
                         PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True" 
                         PopupEditFormVerticalAlign="WindowCenter" />
                     <Settings ShowFilterRow="True" />
                     <SettingsText ConfirmDelete="¿Esta seguro de eliminar el registro?" 
                         EmptyDataRow="No se encontraron registros" 
                         PopupEditFormCaption="Datos de la Aplicación" CommandNew="ok" />
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
                 <asp:EntityDataSource ID="EntityDataSourceAplicaciones" runat="server" 
                     ConnectionString="name=Seguridad2017Entities" 
                     DefaultContainerName="Seguridad2017Entities" EnableDelete="True" 
                     EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
                     EntitySetName="Aplicaciones" EntityTypeFilter="Aplicaciones" 
                     Where="it.Eliminado=false" 
                     ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                 </asp:EntityDataSource>
             </td>
         </tr>
     </table>
      
</asp:Content>
