<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="usuarios.aspx.cs" Inherits="Suite.Web.Seguridad.forms.usuarios" MasterPageFile="../MasterPage.Master" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<%@ Register assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web.ASPxEditors" tagprefix="dx" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
         function ShowDetailPopup(idUsuario) {
//             var e = document.getElementById("ASPxComboBoxUnidadesAdministrativas").value ;
//             var str = e.options[e.selectedIndex].value;
             popup.SetSize(380, 300); 
             popup.SetContentUrl('usuariorol.aspx?id=' + idUsuario  );
             popup.Show();
         }
         function ShowHuellaPopup(idUsuario) {
             
             popup.SetSize(650, 450);
             popup.SetContentUrl('usuariohuella.aspx?id=' + idUsuario);
             popup.Show();
         }
         function ShowRestPopup(idUsuario) {

             popup.SetSize(610, 150);
             popup.SetContentUrl('Reestablecer.aspx?id=' + idUsuario);

             popup.Show();
         }
         function MostrarPermisos(idUsuario) {
             window.location.href = 'PermisosModulos.aspx?id=' + idUsuario; 
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
                     AutoGenerateColumns="False" DataSourceID="EntityDataSourceUsuarios" 
                     KeyFieldName="idUsuario" onrowdeleting="ASPxGridView1_RowDeleting" 
                     onrowinserting="ASPxGridView1_RowInserting" 
                     onrowupdating="ASPxGridView1_RowUpdating" 
                     oncelleditorinitialize="ASPxGridView1_CellEditorInitialize" 
                     oncommandbuttoninitialize="ASPxGridView1_CommandButtonInitialize"
                     Caption="Usuarios"  
                     CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                     CssPostfix="Office2010Blue" oninitnewrow="ASPxGridView1_InitNewRow" >  
                    <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" 
                         PopupEditFormHorizontalAlign="Center" PopupEditFormModal="True" 
                         PopupEditFormVerticalAlign="WindowCenter" />
                    <SettingsText ConfirmDelete="¿Esta seguro de eliminar el registro?" 
                        PopupEditFormCaption="Información de Usuarios" />
                    <Columns>
                        <dx:GridViewCommandColumn ButtonType="Image" ShowInCustomizationForm="True" 
                             VisibleIndex="0" Width="70px">
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
                        <dx:GridViewDataHyperLinkColumn FieldName="idUsuario" ReadOnly="True" 
                             VisibleIndex="0" Caption="Rol" >
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:ShowDetailPopup('{0}');" 
                                  ImageUrl="~/App_Themes/Tema1/img/rol.png">
                            </PropertiesHyperLinkEdit>
                              <Settings AllowAutoFilter="False" />
                              <Settings AllowAutoFilter="False"></Settings>
                              <EditFormSettings Visible="False" />
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataHyperLinkColumn FieldName="idUsuario" ReadOnly="True" 
                             VisibleIndex="0" Caption="Rest" ToolTip="Reestablecer contraseña" >
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:ShowRestPopup('{0}');" 
                                  ImageUrl="~/App_Themes/Tema1/img/Refresh.png">
                            </PropertiesHyperLinkEdit>
                              <Settings AllowAutoFilter="False" />
                              <Settings AllowAutoFilter="False"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataHyperLinkColumn>
                        <dx:GridViewDataHyperLinkColumn FieldName="idUsuario" ReadOnly="True" 
                             VisibleIndex="0" Caption="Permisos" ToolTip="Asignar Permisos" >
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:MostrarPermisos('{0}')"
                                  ImageUrl="~/App_Themes/Tema1/img/key_01.png" >
                            </PropertiesHyperLinkEdit>
                              <Settings AllowAutoFilter="False" />
                              <Settings AllowAutoFilter="False"></Settings>
                            <CellStyle HorizontalAlign="Center">
                            </CellStyle>
                            <EditFormSettings Visible="False" />
                        </dx:GridViewDataHyperLinkColumn> 
                        <dx:GridViewDataTextColumn FieldName="idUsuario" ReadOnly="True" 
                             ShowInCustomizationForm="True" Visible="False" VisibleIndex="0">
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn Caption="Aplicación" FieldName="idAplicacion" 
                             ShowInCustomizationForm="True" VisibleIndex="1" Name="Aplicacion" 
                             Visible="False">
                             <PropertiesComboBox 
                                 TextField="Nombre" ValueField="idAplicacion" ValueType="System.String" 
                                 Spacing="0">
                             </PropertiesComboBox>
                         </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="UserName" ShowInCustomizationForm="True" 
                             VisibleIndex="3" Caption="Usuario">
                             <PropertiesTextEdit>
                                 <ValidationSettings>
                                     <RequiredField IsRequired="True" />
                                 </ValidationSettings>
                             </PropertiesTextEdit>
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Nombre" ShowInCustomizationForm="True" 
                             VisibleIndex="4">
                             <PropertiesTextEdit Width="200px">
                                 <ValidationSettings>
                                     <RequiredField IsRequired="True" />
                                 </ValidationSettings>
                             </PropertiesTextEdit>
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Puesto" ShowInCustomizationForm="True" 
                             VisibleIndex="5" Visible="False">
                             <EditFormSettings Visible="True" />
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="Teléfono" FieldName="Telefono" 
                             ShowInCustomizationForm="True" VisibleIndex="6">
                             <Settings AllowAutoFilter="False" />
                        <Settings AllowAutoFilter="False"></Settings>
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Email" ShowInCustomizationForm="True" 
                             VisibleIndex="7">
                             <Settings AllowAutoFilter="False" />
                        <Settings AllowAutoFilter="False"></Settings>
                         </dx:GridViewDataTextColumn>
                        <dx:GridViewDataCheckColumn FieldName="Activo" Caption="Activa/Autorizada" ShowInCustomizationForm="True" 
                             VisibleIndex="8">
                             <Settings AllowAutoFilter="False" />
                        <Settings AllowAutoFilter="False"></Settings>
                         </dx:GridViewDataCheckColumn>
                         <dx:GridViewDataCheckColumn Caption="Usa Biométricos" 
                             FieldName="UsaBiometricos" ShowInCustomizationForm="True" 
                             VisibleIndex="9" Visible="False">
                             <Settings AllowAutoFilter="False" />
                        <Settings AllowAutoFilter="False"></Settings>
                             <EditFormSettings Visible="True" />
                         </dx:GridViewDataCheckColumn>
                         <dx:GridViewDataSpinEditColumn Caption="Días de vigencia del Password" 
                            FieldName="DiasVigenciaPassword" Visible="False" VisibleIndex="10">
                             <PropertiesSpinEdit DisplayFormatString="g" Spacing="0">
                             </PropertiesSpinEdit>
                             <Settings AllowAutoFilter="False" />
                             <EditFormSettings Visible="True" />
                        </dx:GridViewDataSpinEditColumn>
                         
                          <dx:GridViewDataCheckColumn Caption="Bloqueada" 
                             FieldName="IsLockedOut" ShowInCustomizationForm="True" 
                             VisibleIndex="10" Visible="False" ReadOnly="true">
                             <Settings AllowAutoFilter="False" />
                        <Settings AllowAutoFilter="False"></Settings>
                             <EditFormSettings Visible="True" />
                         </dx:GridViewDataCheckColumn>

                        <dx:GridViewDataCheckColumn Caption="." 
                             FieldName="IsLockedOut" ShowInCustomizationForm="True" 
                             VisibleIndex="11" Visible="False">
                             <Settings AllowAutoFilter="False" />
                        <Settings AllowAutoFilter="False"></Settings>
                             <EditFormSettings Visible="True" />
                             <EditItemTemplate>
                                <table>
                                    <tr>    
                                        <td>
                                           <%-- <asp:CheckBox ID="chkBloqueado" runat = "server" Checked='<%# Eval("IsLockedOut") %>' />--%>
                                            <%--<asp:CheckBox ID="CheckBox1" runat = "server" Checked='<%# Eval("IsLockedOut") %>' />--%>
                                        </td>
                                        <td align="right" style="width:200px">
                                            <dx:ASPxButton ID="ASPxButtonDesbloquear" runat="server" Text="Desbloquear" 
                                                onclick="ASPxButtonDesbloquear_Click" AutoPostBack="true" >
                                             </dx:ASPxButton>
                                             
                                        </td>
                                    </tr>
                                </table>
                             </EditItemTemplate>
                         </dx:GridViewDataCheckColumn>

                        <dx:GridViewDataMemoColumn FieldName="Notas" VisibleIndex="17" Visible="False">
                            <PropertiesMemoEdit Height="50px" Width="400px">
                            </PropertiesMemoEdit>
                            <Settings AllowAutoFilter="False" />
                        <Settings AllowAutoFilter="False"></Settings>
                             <EditFormSettings Visible="True" />
                         </dx:GridViewDataMemoColumn> 
                        <dx:GridViewDataHyperLinkColumn FieldName="idUsuario" ReadOnly="True" 
                                VisibleIndex="19" Caption="Huella" >
                            <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:ShowHuellaPopup('{0}');" 
                                    ImageUrl="~/App_Themes/Tema1/img/fingerprint.png">
                            </PropertiesHyperLinkEdit>
                                <Settings AllowAutoFilter="False" /> 
                        <Settings AllowAutoFilter="False"></Settings>
                             <EditFormSettings Visible="False" />
                        </dx:GridViewDataHyperLinkColumn> 
                        <dx:GridViewDataDateColumn  Caption="Última fecha de inicio de sesión" 
                            FieldName="LastLoginDate" Visible="False" VisibleIndex="12" ReadOnly="true" >
                            <PropertiesDateEdit Spacing="0" ReadOnlyStyle-BackColor="ButtonShadow">
                            <ReadOnlyStyle BackColor="ControlDark"></ReadOnlyStyle>
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="True"  />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="Fecha del último cambio de Password" 
                            FieldName="LastPasswordChangedDate" Visible="False" VisibleIndex="13" ReadOnly="true">
                            <PropertiesDateEdit Spacing="0" ReadOnlyStyle-BackColor="ButtonShadow">
                            <ReadOnlyStyle BackColor="ControlDark"></ReadOnlyStyle>
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataDateColumn>
                        <dx:GridViewDataDateColumn Caption="Fecha del último bloqueo de cuenta" 
                            FieldName="LastLockoutDate" Visible="False" VisibleIndex="15" ReadOnly="true">
                            <PropertiesDateEdit Spacing="0" ReadOnlyStyle-BackColor="ButtonShadow">
                            <ReadOnlyStyle BackColor="ControlDark"></ReadOnlyStyle>
                            </PropertiesDateEdit>
                            <EditFormSettings Visible="True" />
                        </dx:GridViewDataDateColumn>
                     </Columns>
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
                <asp:EntityDataSource ID="EntityDataSourceUsuarios" runat="server" 
                     ConnectionString="name=Seguridad2017Entities" 
                     DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                     EnableInsert="True" EnableUpdate="True" EntitySetName="Usuarios" 
                     EnableDelete="True" EntityTypeFilter="Usuarios" 
                     
                     Where="it.Eliminado=false" 
                     Select="" AutoGenerateOrderByClause="True" 
                     ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                     <WhereParameters>
                         <asp:ControlParameter ControlID="ASPxComboBoxUnidadesAdministrativas" 
                             Name="idUnidadAdministrativa" PropertyName="Value" DbType="Int32" />
                     </WhereParameters>
                 </asp:EntityDataSource> 
             </td>
        </tr>
        <tr>
            <td colspan="2" >
                <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" 
                     ClientInstanceName="popup" Width="380px" Height="200px" 
                     HeaderText="Información" Modal="True" PopupHorizontalAlign="WindowCenter" 
                     PopupVerticalAlign="WindowCenter">
                    <HeaderTemplate>
                        <span >Información</span>
                        <div style="float: right">
                            <dx:ASPxImage ID="img" runat="server" ImageUrl="~/App_Themes/Tema1/img/delete.png"  Cursor="pointer">
                                <ClientSideEvents Click="function(s, e){popup.Hide();}" />
                            </dx:ASPxImage>
                        </div>
                    </HeaderTemplate>
                    <ContentCollection>
                        <dx:PopupControlContentControl runat="server">
                            
                        </dx:PopupControlContentControl>
                    </ContentCollection>
                </dx:ASPxPopupControl>
            </td>
        </tr> 
     </table>
      
</asp:Content>
