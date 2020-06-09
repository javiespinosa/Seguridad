<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="modulos.aspx.cs" Inherits="Suite.Web.Seguridad.forms.modulos" MasterPageFile="../MasterPage.Master" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript"> 
                function Subir(s, e) 
                {
                    var items = ListBoxIndex.GetSelectedItems();
	                if (items == null) return;
	                for (i = 0; i < items.length; i++) 
                    {
                        if (items[i].index > 0) 
                        {
		                    ListBoxIndex.RemoveItem(items[i].index);
		                    ListBoxIndex.InsertItem(items[i].index - 1, items[i].text, items[i].value)
		                    ListBoxIndex.SetSelectedIndex(items[i].index - 1);
			                //grid.PerformCallback(s.GetText());
		                }
	                }
                }
                function Bajar(s, e) 
                {
                    var item = ListBoxIndex.GetSelectedItem();
                    if (item == null) return;
                    if (item.index < ListBoxIndex.GetItemCount() - 1) 
                    {
                        ListBoxIndex.RemoveItem(item.index);
                        ListBoxIndex.InsertItem(item.index + 1, item.text, item.value)
                        ListBoxIndex.SetSelectedIndex(item.index + 1);
                        //grid.PerformCallback(s.GetText());
                    }
                }
            </script>
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
                            onselectedindexchanged="ASPxComboBoxEmpresas_SelectedIndexChanged" 
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
                        <asp:EntityDataSource ID="EntityDataSourceAplicaciones" runat="server" 
                            ConnectionString="name=Seguridad2017Entities" 
                            DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                            EntitySetName="Aplicaciones" EntityTypeFilter="Aplicaciones" 
                            Select="it.[idAplicacion], it.[Nombre]" Where="it.Eliminado=false" 
                            ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                        </asp:EntityDataSource>
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
                    <td colspan="5" >
                        <asp:Panel ID="Panel1" runat="server" ScrollBars="Horizontal" Width="995px">
                            <dx:ASPxTreeList ID="ASPxTreeList1" runat="server" AutoGenerateColumns="False" 
                                DataSourceID="EntityDataSourceModulos" KeyFieldName="idModulo" 
                                Width="995px" Caption="Módulos" ParentFieldName="Padre"  
                                oncommandcolumnbuttoninitialize="ASPxTreeList1_CommandColumnButtonInitialize" 
                                CssFilePath="~/App_Themes/Office2010Blue/{0}/styles.css" 
                                CssPostfix="Office2010Blue" >
                                <Columns>   
                                    <dx:TreeListDataColumn>    
                                        <HeaderCaptionTemplate >  
                                            <dx:ASPxButton ID="btnPruebaAdd" runat="server" onclick="btnPruebaAdd_Click" BackColor="Transparent" ImagePosition="Left" EnableDefaultAppearance="false">
                                                <Image ToolTip="Agregar" Url="~/App_Themes/Tema1/img/Add.png"></Image>
                                                <Border BorderStyle="None" />
                                            </dx:ASPxButton> 
                                        </HeaderCaptionTemplate>                        
                                        <DataCellTemplate>                                
                                            <asp:ImageButton ID="btnEditar" runat="server" CommandName="EditarReg" ImageUrl="~/App_Themes/Tema1/img/edit.png" ToolTip="Editar" OnCommand ="btnEditarAddDelete_onCommand" CommandArgument='<%# Bind("idModulo") %>'/>
                                            <asp:ImageButton ID="btnAgregar" runat="server" CommandName="AgregarReg" ImageUrl="~/App_Themes/Tema1/img/Add.png" ToolTip="Agregar" OnCommand ="btnEditarAddDelete_onCommand"  CommandArgument='<%# Bind("idModulo") %>'/>
                                            <asp:ImageButton ID="btnEliminar" runat="server" CommandName="EliminarReg" ImageUrl="~/App_Themes/Tema1/img/delete.png" ToolTip="Eliminar" OnCommand="btnEditarAddDelete_onCommand" CommandArgument='<%# Bind("idModulo") %>'/>
                                        </DataCellTemplate>
                                    </dx:TreeListDataColumn>
                                    <dx:TreeListTextColumn Caption="ID" FieldName="idModulo" VisibleIndex="1">
                                        <EditFormSettings Visible="False" />
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn FieldName="Codigo" VisibleIndex="3" 
                                        ShowInCustomizationForm="True" Caption="Código" >
                                        <EditFormSettings VisibleIndex="0" />
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn FieldName="Descripcion" VisibleIndex="4" 
                                        ShowInCustomizationForm="True" Caption="Descripción">
                                        <PropertiesTextEdit Width="200px">
                                        </PropertiesTextEdit>
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn FieldName="MenuNavigateURL" VisibleIndex="5" 
                                        ShowInCustomizationForm="True">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn FieldName="MenuToolTip" VisibleIndex="6" 
                                        ShowInCustomizationForm="True">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn FieldName="MenuVisibleIndex" VisibleIndex="7" 
                                        ShowInCustomizationForm="True">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListTextColumn FieldName="MenuImage" VisibleIndex="8" 
                                        ShowInCustomizationForm="True">
                                    </dx:TreeListTextColumn>
                                    <dx:TreeListComboBoxColumn FieldName="Padre" VisibleIndex="10">
                                        <PropertiesComboBox DataSourceID="EntityDataSourceModulosPadre" 
                                            TextField="Descripcion" ValueField="idModulo" ValueType="System.String" 
                                            Spacing="0">
                                        </PropertiesComboBox>
                                    </dx:TreeListComboBoxColumn> 
                                </Columns>
                                <Settings SuppressOuterGridLines="True" />
                                <SettingsPager Mode="ShowPager" NumericButtonCount="20" PageSize="20">
                                    <Summary AllPagesText="Páginas: {0} - {1} ({2} registros)" Position="Right" Text="Página {0} de {1} ({2} registros)" />
                                </SettingsPager>
                                <SettingsLoadingPanel Text="Procesando&amp;hellip;" />
                                <SettingsEditing EditFormColumnCount="1" Mode="PopupEditForm" />
                                <SettingsPopupEditForm Caption="Información del Módulo"  
                                HorizontalAlign="Center" Modal="True" VerticalAlign="WindowCenter" Width="350" />
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
                            </dx:ASPxTreeList>
                            <br/>
                        </asp:Panel> 
                        <asp:EntityDataSource ID="EntityDataSourceModulos" runat="server" 
                            ConnectionString="name=Seguridad2017Entities" 
                            DefaultContainerName="Seguridad2017Entities" EnableDelete="True" 
                            EnableFlattening="False" EnableInsert="True" EnableUpdate="True" 
                            EntitySetName="Modulos" EntityTypeFilter="Modulos" 
                            Where="it.Eliminado=false and it.idAplicacion=@ParametroIdAplicacion
                            AND (@ParametroCodigoDesc IS NULL OR (it.Codigo LIKE '%' + @ParametroCodigoDesc + '%' OR it.Descripcion LIKE '%' + @ParametroCodigoDesc + '%'))"                            
                            ContextTypeName="Seguridad.Model.Seguridad2017Entities">
                            <WhereParameters>
                                <asp:ControlParameter ControlID="ASPxComboBoxAplicaciones" Name="ParametroIdAplicacion" PropertyName="Value" DbType="Int32" />
                                <asp:ControlParameter ControlID="txtCodigoDescripcion" Name="ParametroCodigoDesc" PropertyName="Text" DbType="String" />
                            </WhereParameters>
                        </asp:EntityDataSource>
                        <asp:EntityDataSource ID="EntityDataSourceModulosPadre" runat="server" 
                            ConnectionString="name=Seguridad2017Entities" 
                            DefaultContainerName="Seguridad2017Entities" EnableFlattening="False"  
                            Select="it.[idModulo], it.[Codigo], it.[Descripcion], it.[idAplicacion], it.[Eliminado]"  
                            CommandText = "SELECT A.idModulo, A.Codigo, '[' + A.Codigo + '] ' + A.Descripcion AS Descripcion, A.idAplicacion, A.Eliminado FROM Modulos AS A 
                                WHERE A.Eliminado = false AND A.idAplicacion = @ParametroIdAplicacion" 
                            ContextTypeName="Seguridad.Model.Seguridad2017Entities"
                            OrderBy="it.[Codigo] ASC">  
                            <CommandParameters>
                                <asp:ControlParameter ControlID="ASPxComboBoxAplicaciones" Name="ParametroIdAplicacion" 
                                    PropertyName="Value" DbType="Int32"/>
                            </CommandParameters>
                        </asp:EntityDataSource>
                    <br />
                    </td>
                </tr>
            </table>
            <dx:ASPxPopupControl ID="ASPxPopUpControlEditar" runat="server" 
                ClientInstanceName="popup2" Width="350px" Height="310px"
                HeaderText="Detalle" Modal="True"  AllowDragging="true" 
                PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" 
                ShowPageScrollbarWhenModal="True" CloseAction="CloseButton">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">   
                        <table style="width:100%">                              
                            <tr>
                                <td colspan="2" align="left">
                                    <dx:ASPxCheckBox runat="server" ID="chkMostrarEnMenu" AutoPostBack="true"
                                        Text="Mostrar en menú" OnValueChanged="chkMostrarEnMenu_ValueChanged"></dx:ASPxCheckBox>
                                </td>
                            </tr> 
                            <tr>
                                <td colspan="2"><hr/></td>
                            </tr>
                            <tr class="FormRowView">
                                <td class="FormLabelView" align="left"  style="width:180px; height:25px" >Padre:</td> 
                                <asp:TextBox ID="txtModuloKey" runat="server" Visible="false"></asp:TextBox>
                                <td >                                         
                                    <dx:ASPxComboBox ID="cmbPadre" runat="server" 
                                        DataSourceID="EntityDataSourceModulosPadre"  
                                        TextField="Descripcion" ValueField="idModulo" ValueType="System.String" 
                                        Width="200px" Height="20px" Spacing="0" AutoPostBack="true" 
                                        OnSelectedIndexChanged="cmbPadre_SelectedIndexChanged">
                                    </dx:ASPxComboBox> 
                                </td>
                            </tr>   
                            <tr class="FormRowView">
                                <td class="FormLabelView" align="left" style="width:180px; height:25px">Código:</td>
                                <td >                                             
                                    <asp:TextBox Font-Size="8" ID="txtCodigo" runat="server" Visible="True" Width="200px" Height="20px" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="FormRowView" >
                                <td class="FormLabelView" align="left" style="width:180px; height:25px">Descripción:</td>
                                <td >
                                    <asp:TextBox Font-Size="8" ID="txtDescripcion" runat="server" Visible="True" 
                                        Width="200px" Height="20px" OnTextChanged="txtDescripcion_TextChanged" AutoPostBack = "true" ></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="FormRowView">
                                <td class="FormLabelView" align="left" style="width:180px; height:25px">Menu Navigate URL:</td>
                                <td >
                                    <asp:TextBox Font-Size="8" ID="txtMenuNaURL" runat="server" Visible="True" Width="200px" Height="20px"  ></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="FormRowView">
                                <td class="FormLabelView" align="left" style="width:180px; height:25px">Menu Tool Tip:</td>
                                <td >
                                    <asp:TextBox Font-Size="8" ID="txtMenuToolTip" runat="server" Visible="True" Width="200px" Height="20px"  ></asp:TextBox>
                                </td>
                            </tr> 
                            <tr class="FormRowView">
                                <td class="FormLabelView" align="left" style="width:100px; height:25px">Menu Visible Index:</td>
                                <td >
                                    <table>
                                        <tr>
                                            <td colspan="2">                                                             
                                                <dx:ASPxListBox ID="ASPxListVisibleIndex" runat="server" 
                                                    DataSourceID="EntityDataSourceModulosHijos" ClientInstanceName="ListBoxIndex"
                                                    SelectionMode="Single" TextField="Descripcion" ValueField="idModulo" 
                                                    ValueType="System.Int32" Width="200px">
                                                </dx:ASPxListBox>  
                                            </td>                                                        
                                        </tr>
                                        <tr>
                                            <td style="width:50%" align="center" >
                                                <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" Text="UP" Width="20px">
                                                <ClientSideEvents Click="function(s, e) { Subir(); }">
                                                    </ClientSideEvents>
                                                </dx:ASPxButton> 
                                            </td>
                                            <td style="width:50%" align="center">
                                                <dx:ASPxButton ID="ASPxButton2" runat="server" AutoPostBack="False" Text="DOWN" Width="20px">
                                                <ClientSideEvents Click="function(s, e) { Bajar(); }">
                                                    </ClientSideEvents>
                                                </dx:ASPxButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="FormRowView">
                                <td class="FormLabelView" align="left" style="width:180px; height:25px">Menu Image:</td>
                                <td >                                         
                                    <asp:TextBox Font-Size="8" ID="txtImage" runat="server" Visible="True" Width="200px" Height="20px" ></asp:TextBox>
                                </td>
                            </tr> 
                            <tr>
                                <td class="FormLabelView" align="left" style="width:180px; height:25px" >Permisos:</td> 
                                <td style="width:200px">
                                    <table  width="100%">
                                        <tr>
                                            <td align="left">
                                                <dx:ASPxCheckBox ID="chkVisualizar" runat="server" CheckState="Unchecked" 
                                                    Text="Visualizar">
                                                </dx:ASPxCheckBox>
                                                <dx:ASPxCheckBox ID="chkModificar" runat="server" CheckState="Unchecked" 
                                                    Text="Modificar">
                                                </dx:ASPxCheckBox>
                                                <dx:ASPxCheckBox ID="chkEspecial" runat="server" CheckState="Unchecked" 
                                                    Text="Especial">
                                                </dx:ASPxCheckBox>
                                            </td>
                                            <td align="left" valign="top">
                                                <dx:ASPxCheckBox ID="chkAgregar" runat="server" CheckState="Unchecked" 
                                                    Text="Agregar">
                                                </dx:ASPxCheckBox>
                                                <dx:ASPxCheckBox ID="chkEliminar" runat="server" CheckState="Unchecked" 
                                                    Text="Eliminar">
                                                </dx:ASPxCheckBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:center; width:100%" colspan="2">
                                    <asp:label runat="server" ID="lblDetalleMsgError" ForeColor="Red"></asp:label>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align:right; width:100%" colspan="2"> 
                                    <asp:ImageButton ID="btnGuardar" runat="server" 
                                    ImageUrl="~/App_Themes/Tema1/img/save.png" AlternateText="Guardar" 
                                    ToolTip="Guardar" 
                                    ValidationGroup="GuardarDetalle" OnClick="btnGuardar_Click" />
                                    <asp:ImageButton ID="btnCancelar" runat="server" 
                                        ImageUrl="~/App_Themes/Tema1/img/cancelar.png" AlternateText="Cancelar" 
                                        ToolTip="Cancelar" style="width: 32px" OnClick="btnCancelar_Click" />
                        
                                </td> 
                            </tr>           
                            <tr>
                                <td colspan="2">
                                    <asp:EntityDataSource ID="EntityDataSourceModulosHijos" runat="server" 
                                        ConnectionString="name=Seguridad2017Entities" 
                                        DefaultContainerName="Seguridad2017Entities" EnableFlattening="False" 
                                        EntitySetName="Modulos" EntityTypeFilter="Modulos" 
                                        Select="it.[idModulo], it.[Descripcion], it.[MenuVisibleIndex]" 
                                        Where="(it.Eliminado=false and it.MenuVisibleIndex > 0) and (it.Padre=@ParametroIdPadre or (@ParametroIdPadre = -1 AND it.Padre IS NULL AND it.idAplicacion = @idAplicacion)) " 
                                        ContextTypeName="Seguridad.Model.Seguridad2017Entities"
                                        OrderBy="it.[MenuVisibleIndex] ASC">
                                        <WhereParameters>
                                            <asp:ControlParameter ControlID="cmbPadre" Name="ParametroIdPadre" 
                                                PropertyName="Value" DbType="Int32"/>
                                            <asp:ControlParameter ControlID="ASPxComboBoxAplicaciones" Name="idAplicacion" 
                                                PropertyName="Value" DbType="Int32"/>
                                        </WhereParameters>
                                    </asp:EntityDataSource>
                                </td>
                            </tr>      
                        </table> 
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
            <dx:ASPxPopupControl ID="ASPxPopUpEliminar" runat="server" 
                    ClientInstanceName="objPopUpEliminar" Width="380px" Height="100px" 
                    HeaderText="Eliminar" Modal="True"  AllowDragging="true" 
                    PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                        <asp:TextBox ID="txtEliminarKey" runat="server" Visible="False"></asp:TextBox>
                        <table style="width:100%">
                            <tr>
                                <td colspan="4"><p>¿Está seguro que desea eliminar el elemento seleccionado?</p></td>
                            </tr>
                            <tr>
                                <td style="width:50px">&nbsp;</td>
                                <td style="width:60px">
                                    <dx:ASPxButton ID="btnEliminarSi" runat="server" Text="Sí" Width="60px" 
                                        OnClick="btnEliminarSi_Click">
                                    </dx:ASPxButton>                                        
                                </td>
                                <td style="width:60px"><dx:ASPxButton ID="btnEliminarNo" runat="server" Text="No" Width="60px" 
                                        OnClick="btnEliminarNo_Click">
                                    </dx:ASPxButton></td>
                                <td style="width:50px">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>  
