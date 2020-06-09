<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="unidadadmin.aspx.cs" Inherits="Suite.Web.Seguridad.forms.unidadadmin" MasterPageFile="~/MasterPage.Master"%>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.v11.1.Linq, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Data.Linq" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <table width="100%">
         <tr>
             <td>
                 <dx:ASPxGridView ID="ASPxGridView1" runat="server" AutoGenerateColumns="False" 
                     Caption="Unidades Administrativas" DataSourceID="EntityDataSourceUniAdmin" 
                     KeyFieldName="idUnidadAdministrativa" Width="100%" 
                     onrowdeleting="ASPxGridView1_RowDeleting" 
                     onrowinserting="ASPxGridView1_RowInserting" 
                     onrowupdating="ASPxGridView1_RowUpdating" 
                     oncommandbuttoninitialize="ASPxGridView1_CommandButtonInitialize" 
                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                     CssPostfix="Office2010Blue">
                     <Columns>
                         <dx:GridViewCommandColumn ButtonType="Image" VisibleIndex="0" Width="100px">
                             <EditButton Visible="True">
                                 <Image AlternateText="Modificar" Url="~/App_Themes/Tema1/img/edit.png">
                                 </Image>
                             </EditButton>
                             <NewButton Visible="True">
                                 <Image AlternateText="Agregar" Url="~/App_Themes/Tema1/img/Add.png">
                                 </Image>
                             </NewButton>
                             <DeleteButton Visible="True">
                                 <Image AlternateText="Eliminar" Url="~/App_Themes/Tema1/img/delete.png">
                                 </Image>
                             </DeleteButton>
                             <CancelButton>
                                 <Image AlternateText="Cancelar" Url="~/App_Themes/Tema1/img/cancelar.png">
                                 </Image>
                             </CancelButton>
                             <UpdateButton>
                                 <Image AlternateText="Guardar" Url="~/App_Themes/Tema1/img/save.png">
                                 </Image>
                             </UpdateButton>
                         </dx:GridViewCommandColumn>
                         <dx:GridViewDataTextColumn FieldName="idUnidadAdministrativa" ReadOnly="True" 
                             ShowInCustomizationForm="True" VisibleIndex="1" Visible="False">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Clave" ShowInCustomizationForm="True" 
                             VisibleIndex="5">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" 
                             VisibleIndex="6">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="RFC" ShowInCustomizationForm="True" 
                             VisibleIndex="7">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="Telefono" ShowInCustomizationForm="True" 
                             VisibleIndex="8">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="idSectorPublico" 
                             ShowInCustomizationForm="True" VisibleIndex="9">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="idSecPubFin" 
                             ShowInCustomizationForm="True" VisibleIndex="10">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="idSectorEconomico" 
                             ShowInCustomizationForm="True" VisibleIndex="11">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataTextColumn FieldName="idEntePublico" 
                             ShowInCustomizationForm="True" VisibleIndex="12">
                         </dx:GridViewDataTextColumn>
                         <dx:GridViewDataMemoColumn FieldName="Notas" VisibleIndex="13">
                             <PropertiesMemoEdit Height="50px" Width="200px">
                             </PropertiesMemoEdit>
                         </dx:GridViewDataMemoColumn>
                         <dx:GridViewDataTextColumn FieldName="Padre" ShowInCustomizationForm="True" 
                             VisibleIndex="15">
                         </dx:GridViewDataTextColumn>
                     </Columns>
                     <SettingsBehavior ConfirmDelete="True" />
                     <SettingsPager>
                         <AllButton Text="All">
                         </AllButton>
                         <NextPageButton Text="Next &gt;">
                         </NextPageButton>
                         <PrevPageButton Text="&lt; Prev">
                         </PrevPageButton>
                     </SettingsPager>
                     <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" 
                         PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True" 
                         PopupEditFormVerticalAlign="WindowCenter" />
                     <SettingsText ConfirmDelete="¿Esta seguro de eliminar el registro?" 
                         PopupEditFormCaption="Información de la Unidad Administrativa" />
                     <SettingsDetail ShowDetailRow="True" AllowOnlyOneMasterRowExpanded="True" />
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
                      <Templates>
                           <DetailRow >
                               <dx:ASPxGridView ID="ASPxGridView2" runat="server" AutoGenerateColumns="False" 
                                    OnBeforePerformDataSelect="detailGrid_DataSelect"
                                    DataSourceID="EntityDataSourceAplicacionesUA" 
                                    KeyFieldName="idAplicacionUA" 
                                    onrowinserting="ASPxGridView2_RowInserting"
                                    OnRowUpdating="ASPxGridView2_RowUpdating"
                                    OnRowDeleting="ASPxGridView2_RowDeleting"
                                    oncommandbuttoninitialize="ASPxGridView2_CommandButtonInitialize" 
                                   DataSourceForceStandardPaging="True" >
                                    <Columns>
                                        <dx:GridViewCommandColumn VisibleIndex="0" ButtonType="Image" Width="70px">
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
                                        <dx:GridViewDataTextColumn FieldName="idAplicacionUA" ReadOnly="True" 
                                            VisibleIndex="1" Visible="False">
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataTextColumn FieldName="idUnidadAdministrativa" VisibleIndex="2" Visible="false" >
                                        </dx:GridViewDataTextColumn>
                                        <dx:GridViewDataComboBoxColumn FieldName="idAplicacion" VisibleIndex="3" 
                                            Caption="Aplicación" Width="200px">
                                            <PropertiesComboBox DataSourceID="EntityDataSourceAplicaciones" 
                                                TextField="Nombre" ValueField="idAplicacion" ValueType="System.String">
                                            </PropertiesComboBox>
                                        </dx:GridViewDataComboBoxColumn>
                                    </Columns>
                                    <SettingsBehavior ConfirmDelete="True" />
                                    <SettingsEditing Mode="Inline" />
                                    <SettingsText ConfirmDelete="¿Esta seguro de eliminar el registro?" />
                                </dx:ASPxGridView>
                           </DetailRow>
                      </Templates>
                 </dx:ASPxGridView>
                 

                 <asp:EntityDataSource ID="EntityDataSourceUniAdmin" runat="server" 
                     ConnectionString="name=Seguridad2017Entities" 
                     DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                     EntitySetName="UnidadesAdministrativas" 
                     EntityTypeFilter="UnidadesAdministrativas" EnableInsert="True" Select="" 
                     Where="it.Eliminado=false" 
                     ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                 </asp:EntityDataSource>
             </td>
         </tr>
     </table>
      
   
    <asp:EntityDataSource ID="EntityDataSourceAplicacionesUA" runat="server" 
         ConnectionString="name=Seguridad2017Entities" 
         DefaultContainerName="Seguridad2017Entities" EnableDelete="True" 
         EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
         EntitySetName="AplicacionesUA" EntityTypeFilter="AplicacionesUA" Select="" 
         Where="it.idUnidadAdministrativa=@idUnidadAdministrativa" 
         ContextTypeName="Seguridad.Model.Seguridad2017Entities">
        <WhereParameters>
            <asp:SessionParameter Name="idUnidadAdministrativa" 
                SessionField="ID_UNIDADADMINISTRATIVA" DbType="Int32" />
        </WhereParameters>
    </asp:EntityDataSource>
    <asp:EntityDataSource ID="EntityDataSourceAplicaciones" runat="server" 
         ConnectionString="name=Seguridad2017Entities" 
         DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
         EntitySetName="Aplicaciones" EntityTypeFilter="Aplicaciones" 
         Select="it.[idAplicacion], it.[Nombre]" 
         ContextTypeName="Seguridad.Model.Seguridad2017Entities">
    </asp:EntityDataSource>
</asp:Content>
