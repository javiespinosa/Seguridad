<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PermisosModulos.aspx.cs" Inherits="Suite.Web.Seguridad.forms.PermisosModulos" MasterPageFile="../MasterPage.Master" %>
<%@ MasterType VirtualPath="~/MasterPage.master" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Assembly="DevExpress.Web.ASPxGridView.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxGridView" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.Web.ASPxEditors.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a"
    Namespace="DevExpress.Web.ASPxEditors" TagPrefix="dx" %>

<%@ Register TagPrefix="dx" Namespace="DevExpress.Web.ASPxPopupControl" Assembly="DevExpress.Web.v11.1, Version=11.1.8.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" EnableScriptGlobalization="true" EnableScriptLocalization="true">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <div  class="CatalogTitle" style="text-align: center" >
                 <dx:ASPxLabel ID="lblTitulo" runat="server" Text="Permisos Modulos Usuarios" ></dx:ASPxLabel>
            </div>
            <table width="1000px">
                <tr>
                    <td width="70px">
                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Aplicaciones:" 
                            Font-Bold="True" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                        </dx:ASPxLabel>  
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="ASPxComboBoxAplicaciones" runat="server" 
                            DataSourceID="EntityDataSourceAplicaciones" TextField="Nombre" 
                            ValueField="idAplicacion" ValueType="System.String" SelectedIndex="0"
                            AutoPostBack="True" 
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Spacing="0" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" 
                            onselectedindexchanged="ASPxComboBoxAplicaciones_SelectedIndexChanged" >
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">  
                            </LoadingPanelStyle>
                            <ButtonStyle Width="13px">
                            </ButtonStyle>
                        </dx:ASPxComboBox>
                        <asp:EntityDataSource ID="EntityDataSourceAplicaciones" runat="server" 
                            ConnectionString="name=Seguridad2017Entities" 
                            DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                            EntitySetName="Aplicaciones" EntityTypeFilter="Aplicaciones" 
                            Select="it.[idAplicacion], it.[Nombre]" Where="it.Eliminado=false" 
                            ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                        </asp:EntityDataSource>
                    </td>
                    <td width="70px">
                        <dx:ASPxLabel ID="ASPxLabel3" runat="server" Text="Activos:" 
                            Font-Bold="True" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                        </dx:ASPxLabel>  
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="cmbActivos" runat="server" 
                            AutoPostBack="True" SelectedIndex="0"
                            CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue" Spacing="0" 
                            SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css" >
                            <Items>
                                <dx:ListEditItem Text = "Todos" Value = "-2" />
                                <dx:ListEditItem Text = "Activos" Value = "0" />
                                <dx:ListEditItem Text = "No Activos" Value = "-1" />
                            </Items>
                            <LoadingPanelImage Url="~/App_Themes/Office2010Blue/Editors/Loading.gif">
                            </LoadingPanelImage>
                            <LoadingPanelStyle ImageSpacing="5px">
                            </LoadingPanelStyle>
                            <ButtonStyle Width="13px">
                            </ButtonStyle>
                        </dx:ASPxComboBox>
                    </td>
                    <td width="85px">
                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Código/Descripción:" 
                            Font-Bold="True" CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                            CssPostfix="Office2010Blue">
                        </dx:ASPxLabel>                        
                    </td>
                    <td>
                        <asp:TextBox Font-Size="8" ID="txtCodigoDescripcion" runat="server" 
                            Visible="True" Width="200px" Height="20px" AutoPostBack="true"/>
                    </td>
                    <td>
                        <dx:ASPxButton runat="server" ID="btnBuscar" AutoPostBack="true" Text="Buscar"></dx:ASPxButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="7" >
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="995px">
                             <dx:ASPxTreeList ID="TreeListPermisosModulos" runat="server" AutoGenerateColumns="False"  
                                Caption="Modulos" Width="995px" KeyFieldName="idModulo" ParentFieldName="Padre"
                                DataSourceID="EntityDataSourcePermisos"
		                        CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" EnableCallbacks="False"
                                EnableViewState="False" 
		                        CssPostfix="Office2010Blue" 
                                 onnodeupdating="TreeListPermisosModulos_NodeUpdating" 
                                 onhtmldatacellprepared="TreeListPermisosModulos_HtmlDataCellPrepared" 
                                 oncelleditorinitialize="TreeListPermisosModulos_CellEditorInitialize" 
                                 onstartnodeediting="TreeListPermisosModulos_StartNodeEditing" > 
                                <Templates>
                                    <EditForm>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:TextBox runat="server" ID="txtIdUsuarioPermiso" Visible="false" Text= '<%# Eval("idUsuarioPermiso") %>'>'></asp:TextBox>
                                            <table width="100%"> 
                                                <tr>
                                                    <td align="left"> 
                                                        <asp:CheckBox ID="chkVisualizar" runat="server" Text="  Visualizar" Height="25px"
                                                            Checked='<%# Eval("Visualizar") %>' onload="chkVisualizar_Load" Visible="true" TextAlign="Right" />
                                                    </td>   
                                                </tr>
                                                <tr>
                                                    <td align="left"> 
                                                        <asp:CheckBox ID="chkAgregar" runat="server" Checked='<%# Eval("Agregar") %>' Height="25px" 
                                                            onload="chkVisualizar_Load" TextAlign="Right" Visible="true" Text="  Agregar"/>
                                                    </td> 
                                                </tr>
                                                <tr>
                                                    <td align="left"> 
                                                        <asp:CheckBox ID="chkModificar" runat="server" Text="  Modificar" Height="25px"
                                                            Checked='<%# Eval("Modificar") %>' onload="chkVisualizar_Load" TextAlign="Right" Visible="true" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left"> 
                                                        <asp:CheckBox ID="chkEliminar" runat="server" Checked='<%# Eval("Eliminar") %>' Height="25px" 
                                                            onload="chkVisualizar_Load" TextAlign="Right" Visible="true" Text="  Eliminar" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left">  
                                                        <asp:CheckBox ID="chkEspecial" runat="server" Checked='<%# Eval("Especial") %>' Height="25px"
                                                            onload="chkVisualizar_Load" TextAlign="Right" Visible="true" Text="  Especial" />
                                                    </td>
                                                </tr> 
                                                <tr> 
                                                    <td colspan="6">
                                                        <hr />
                                                    </td> 
                                                </tr>                                               
                                                <tr>
                                                    <td align="left"> 
                                                        <asp:CheckBox ID="chkAplicarSubniveles" runat="server" TextAlign="Right" Visible="true" Text="  Aplicar cambios a todo los subniveles" Height="25px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right">
                                                        <dx:ASPxTreeListTemplateReplacement ID="ASPxTreeListTemplateReplacement1" 
                                                            runat="server" ReplacementType="UpdateButton" />
                                                        <dx:ASPxTreeListTemplateReplacement ID="ASPxTreeListTemplateReplacement2" 
                                                            runat="server" ReplacementType="CancelButton" /> 
                                                    </td>
                                                </tr> 
                                            </table>
                                        </asp:Panel>
                                    </EditForm>
                                 </Templates>
		                        <Columns>   
			                        <dx:TreeListCommandColumn ButtonType="Image" VisibleIndex="0">
			                            <EditButton Visible="True">
				                            <Image AlternateText="Modificar" Url="~/App_Themes/Tema1/img/edit.png">
				                            </Image>
			                            </EditButton>  
			                            <UpdateButton>
				                            <Image AlternateText="Guardar" Url="~/App_Themes/Tema1/img/save.png">
				                            </Image>
			                            </UpdateButton>
			                            <CancelButton>
				                            <Image AlternateText="Cancelar" Url="~/App_Themes/Tema1/img/cancelar.png">
				                            </Image>
			                            </CancelButton>
			                        </dx:TreeListCommandColumn> 
			                        <dx:TreeListTextColumn Caption="ID" FieldName="idModulo" VisibleIndex="0" Visible="false">
				                        <EditFormSettings Visible="False" />
			                        </dx:TreeListTextColumn>  
			                         <dx:TreeListTextColumn Caption="Módulo" FieldName="Descripcion" VisibleIndex="1" Visible="true">
				                        <EditFormSettings Visible="False" />
			                        </dx:TreeListTextColumn>   
                                    <dx:TreeListTextColumn Caption="idusuarioPermiso" FieldName="idUsuarioPermiso" 
                                        VisibleIndex="2" Visible="False" ShowInCustomizationForm="True" 
                                        ReadOnly="True"  >
				                        <EditFormSettings Visible="True" />
			                        </dx:TreeListTextColumn>  
			                         <dx:TreeListCheckColumn Caption="Visualizar" FieldName="Visualizar"
				                         ShowInCustomizationForm="True" VisibleIndex="3" Visible="true" > 
			                         </dx:TreeListCheckColumn> 
			                         <dx:TreeListCheckColumn FieldName="Agregar" ShowInCustomizationForm="True" 
				                         VisibleIndex="4">
			                         </dx:TreeListCheckColumn>
			                         <dx:TreeListCheckColumn FieldName="Modificar" 
				                         ShowInCustomizationForm="True" VisibleIndex="5">
			                         </dx:TreeListCheckColumn>
			                         <dx:TreeListCheckColumn FieldName="Eliminar" ShowInCustomizationForm="True" 
				                         VisibleIndex="6">
			                         </dx:TreeListCheckColumn>
			                         <dx:TreeListCheckColumn FieldName="Especial" ShowInCustomizationForm="True" 
				                         VisibleIndex="7">
			                         </dx:TreeListCheckColumn>  
		                        </Columns>
		                        <Settings SuppressOuterGridLines="True" ShowPreview="false" />
		                        <SettingsPager Mode="ShowPager" NumericButtonCount="20" PageSize="20">
			                        <Summary AllPagesText="Páginas: {0} - {1} ({2} registros)" Position="Right" Text="Página {0} de {1} ({2} registros)" />
		                        </SettingsPager>
		                        <SettingsLoadingPanel Text="Procesando&amp;hellip;" />
		                        <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
		                        <SettingsPopupEditForm Caption="Información del Módulo"  
		                        HorizontalAlign="Center" Modal="True" VerticalAlign="WindowCenter" Width="250px" />
		                        <SettingsText ConfirmDelete="¿Esta seguro de eliminar el registro?" LoadingPanelText="Procesando&amp;hellip;" />
		                        <Images SpriteCssFilePath="~/App_Themes/Office2010Blue/{0}/sprite.css">
			                        <LoadingPanel Url="~/App_Themes/Office2010Blue/TreeList/Loading.gif">
			                        </LoadingPanel>
		                        </Images>
		                        <Styles CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" CssPostfix="Office2010Blue">
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
		                        </StylesEditors>
                                <SettingsBehavior AllowFocusedNode="true" ExpandCollapseAction="NodeClick" ProcessFocusedNodeChangedOnServer="true" ProcessSelectionChangedOnServer="true" />
		                        </dx:ASPxTreeList>   
                        </asp:Panel> 
                    </td>
                </tr> 
            </table>  
            <asp:EntityDataSource ID="EntityDataSourcePermisos" runat="server" 
	             ConnectionString="name=Seguridad2017Entities" 
	             DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
	             Select="it.[Descripcion], it.[Padre], it.[idUsuarioPermiso], it.[idUsuario], it.[idAplicacion], it.[idModulo], 
			            it.[Visualizar], it.[Agregar], it.[Modificar], it.[Eliminar], it.[Especial]" 
	            CommandText=" SELECT T.Descripcion, T.Padre, T.idUsuarioPermiso, T.idUsuario, T.idAplicacion, T.idModulo, 
			            T.Visualizar, T.Agregar, T.Modificar, T.Eliminar, T.Especial FROM (SELECT A.Descripcion,
			              A.Padre,
			              (CASE WHEN B.idUsuarioPermiso IS NULL THEN -1 ELSE B.idUsuarioPermiso END) AS idUsuarioPermiso,
			              B.idUsuario, 
			              A.idAplicacion, 
			              A.idModulo, 
                          (CASE WHEN B.Visualizar IS NULL THEN False ELSE B.Visualizar END) AS Visualizar,
                          (CASE WHEN B.Agregar IS NULL THEN False ELSE B.Agregar END) AS Agregar,
			              (CASE WHEN B.Modificar IS NULL THEN False ELSE B.Modificar END) AS Modificar,
                          (CASE WHEN B.Eliminar IS NULL THEN False ELSE B.Eliminar END) AS Eliminar,
                          (CASE WHEN B.Especial IS NULL THEN False ELSE B.Especial END) AS Especial 
		              FROM Modulos AS A
		              LEFT JOIN UsuariosPermisos AS B ON A.idModulo = B.idModulo AND B.idUsuario = @idUsuario
		              WHERE A.idAplicacion = @idAplicacion AND A.Eliminado = false) AS T 
                      WHERE (@idUsuarioPermiso = -1 AND T.idusuarioPermiso = @idUsuarioPermiso) 
                      OR (@idUsuarioPermiso = 0 AND T.idUsuarioPermiso > @idUsuarioPermiso) OR (@idUsuarioPermiso = -2 AND T.idUsuarioPermiso > @idUsuarioPermiso) AND (@ParametroCodigoDesc IS NULL OR (T.Descripcion LIKE '%' + @ParametroCodigoDesc + '%'))"
	             ContextTypeName="Seguridad.Model.Seguridad2017Entities">  
                 <CommandParameters >
                    <asp:Parameter DbType="Int32" DefaultValue="0" Name="idUsuario" />
                    <asp:ControlParameter ControlID="ASPxComboBoxAplicaciones" DbType="Int32" Name="idAplicacion" PropertyName="Value" />
                    <asp:ControlParameter ControlID="cmbActivos" DbType="Int32" Name="idUsuarioPermiso" PropertyName="Value" />
                    <asp:ControlParameter ControlID="txtCodigoDescripcion" Name="ParametroCodigoDesc" PropertyName="Text" DbType="String" />
                 </CommandParameters>
            </asp:EntityDataSource> 
            <table style="width:100%">
                 <tr>
                    <td>
                        <asp:ImageButton ID="btnCancelarTodo" runat="server" 
                            ImageUrl="~/App_Themes/Tema1/img/cancelar.png" AlternateText="Cancelar" 
                            ToolTip="Cancelar" onclick="btnCancelarTodo_Click"/>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>  
