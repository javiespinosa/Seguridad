using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.ASPxTreeList;
using DevExpress.Web.Data;
using Seguridad.Clases;
using Seguridad.Model;
using DevExpress.Web.ASPxEditors;

namespace Suite.Web.Seguridad.forms
{
    public partial class modulos : System.Web.UI.Page
    {
        private int _idAplicacion = -1;
        private static bool Nuevo = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.IdModulo = 6;
        }

        protected void Page_LoadComplete(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var permisos = Master.ObtenerPermisos(); 
                
                if (Session["PERMISOS"] == null)
                {
                    Session.Add("PERMISOS", permisos);
                }
                else
                {
                    Session["PERMISOS"] = permisos;
                } 
            } 
        }

        protected void ASPxTreeList1_CommandColumnButtonInitialize(object sender, TreeListCommandColumnButtonEventArgs e)
        {
            e.Visible = DevExpress.Utils.DefaultBoolean.False;

            var permisos = (UsuariosPermisos)Session["PERMISOS"];
            
            if (e.ButtonType == TreeListCommandColumnButtonType.New)
            {
                if (Convert.ToBoolean(permisos.Agregar))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;

                }
                else
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }

            if (e.ButtonType == TreeListCommandColumnButtonType.Edit)
            {
                if (Convert.ToBoolean(permisos.Modificar))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
                else
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            }


            if (e.ButtonType == TreeListCommandColumnButtonType.Delete)
            {
                if (Convert.ToBoolean(permisos.Eliminar))
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.True;
                }
                else
                {
                    e.Visible = DevExpress.Utils.DefaultBoolean.False;
                }
            } 
        }

        protected void ASPxComboBoxEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceModulos.DataBind();
            ASPxTreeList1.DataBind();
        }   

        protected void txtDescripcion_TextChanged(object sender, EventArgs e)
        { 
            int idModulo = txtModuloKey.Text.Trim() != "" ? Convert.ToInt32(txtModuloKey.Text.Trim()) : -1;
            TextBox txt = (TextBox)sender;
            ListEditItem tActual = ASPxListVisibleIndex.Items.FindByValue(idModulo);
            if (tActual != null)
                tActual.Text = txt.Text;
            else
            {
                tActual = new ListEditItem() { Value = -1, Text = txt.Text };
                if(chkMostrarEnMenu.Checked)
                    ASPxListVisibleIndex.Items.Add(tActual);
            }

            tActual.Selected = true; 
        } 

        protected void btnPruebaAdd_Click(object sender, EventArgs e)
        {
            LimpiarPantalla();
            ASPxPopUpControlEditar.ShowPageScrollbarWhenModal = true;
            ASPxPopUpControlEditar.ShowOnPageLoad = true;

            Nuevo = true;

            cmbPadre.DataBind();
            cmbPadre.Items.Insert(0, new ListEditItem() { Index = 0, Value = -1, Text = "Ninguno" });
            cmbPadre.SelectedIndex = 0;

            chkVisualizar.Checked = true;
            chkAgregar.Checked = true;
            chkModificar.Checked = true;
            chkEliminar.Checked = true;
            chkEspecial.Checked = false;
        } 

        protected void btnEditarAddDelete_onCommand(object sender, CommandEventArgs e)
        {
            LimpiarPantalla();
            if (e.CommandName == "EditarReg" || e.CommandName == "AgregarReg")
            {
                Nuevo = true;

                ASPxTreeList1.CancelEdit();
                cmbPadre.DataBind();
                cmbPadre.Items.Insert(0, new ListEditItem() { Index = 0, Value = -1, Text = "Ninguno" });
                cmbPadre.SelectedIndex = 0;
                
                if (e.CommandName == "EditarReg")
                {
                    Nuevo = false;

                    int idModulo = Convert.ToInt32(e.CommandArgument);
                    var classseguridad = new ClassSeguridad();
                    var Modulo = classseguridad.ObtenerModulo(idModulo);
                    if(Modulo.Padre != null)
                        cmbPadre.SelectedItem = cmbPadre.Items.FindByValue(Modulo.Padre.ToString()); 
                    if (cmbPadre.SelectedItem == null)
                        cmbPadre.SelectedIndex = 0; 
                    txtModuloKey.Text = idModulo.ToString();
                    txtCodigo.Text = Modulo.Codigo.ToString();
                    txtDescripcion.Text = Modulo.Descripcion;
                    txtMenuNaURL.Text = Modulo.MenuNavigateURL;
                    txtMenuToolTip.Text = Modulo.MenuToolTip;
                    txtImage.Text = Modulo.MenuImage;

                    if (Modulo.MenuVisibleIndex == 0)
                    {
                        ListEditItem tActual = ASPxListVisibleIndex.Items.FindByValue(Modulo.idModulo);
                        ASPxListVisibleIndex.Items.Remove(tActual);
                        chkMostrarEnMenu.Checked = false;
                    }
                    else
                        chkMostrarEnMenu.Checked = true;

                    if (Modulo.Permisos != null)
                    {
                        string[] sPermisos = Modulo.Permisos.Split(',');
                        chkVisualizar.Checked = sPermisos[0].ToString() == "1" ? true : false;
                        chkAgregar.Checked = sPermisos[1].ToString() == "1" ? true : false;
                        chkModificar.Checked = sPermisos[2].ToString() == "1" ? true : false;
                        chkEliminar.Checked = sPermisos[3].ToString() == "1" ? true : false;
                        chkEspecial.Checked = sPermisos[4].ToString() == "1" ? true : false;
                    }
                    else
                    {
                        chkVisualizar.Checked = true;
                        chkAgregar.Checked = true;
                        chkModificar.Checked = true;
                        chkEliminar.Checked = true;
                        chkEspecial.Checked = false;
                    }
                }
                else
                {
                    if (Convert.ToInt32(e.CommandArgument) > 0) 
                        cmbPadre.SelectedItem = (ListEditItem)cmbPadre.Items.FindByValue(e.CommandArgument); 

                    chkVisualizar.Checked = true;
                    chkAgregar.Checked = true;
                    chkModificar.Checked = true;
                    chkEliminar.Checked = true;
                    chkEspecial.Checked = false;
                }
                
                
                ASPxPopUpControlEditar.ShowPageScrollbarWhenModal = true;
                ASPxPopUpControlEditar.ShowOnPageLoad = true;
            }
            else if (e.CommandName == "EliminarReg")
            {
                int idModulo = Convert.ToInt32(e.CommandArgument);
                txtEliminarKey.Text = idModulo.ToString();
                ASPxPopUpEliminar.ShowPageScrollbarWhenModal = true;
                ASPxPopUpEliminar.ShowOnPageLoad = true;
            }
        }

        private void LimpiarPantalla()
        {
            cmbPadre.SelectedIndex = -1;
            txtCodigo.Text = "";
            txtDescripcion.Text = "";
            txtMenuNaURL.Text = "";
            txtMenuToolTip.Text = "";
            txtImage.Text = "";
            ASPxListVisibleIndex.DataSource = null;
            chkVisualizar.Checked = false;
            chkAgregar.Checked = false;
            chkModificar.Checked = false;
            chkEliminar.Checked = false;
            chkEspecial.Checked = false;
            chkMostrarEnMenu.Checked = false;
        }

        protected void btnEliminarSi_Click(object sender, EventArgs e)
        {
            var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            int idModulo = Convert.ToInt32(txtEliminarKey.Text.Trim());
            if (!classseguridad.EliminarModulo(idModulo, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }

            EntityDataSourceModulosPadre.DataBind();
            EntityDataSourceModulos.DataBind();
            ASPxTreeList1.DataBind(); 
            txtEliminarKey.Text = "";
            ASPxPopUpEliminar.ShowOnPageLoad = false;
        }

        protected void btnEliminarNo_Click(object sender, EventArgs e)
        {
            txtEliminarKey.Text = "";
            ASPxPopUpEliminar.ShowOnPageLoad = false;
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)
        {
            clearMessage();
            if (Validar())
                GuardarModulo();
        }

        private bool Validar()
        {
            if (txtCodigo.Text.Trim() == "")
            { 
                lblDetalleMsgError.Text = "Indique Código para el Módulo";
                return false;
            }

            if (txtDescripcion.Text.Trim() == "")
            {
                lblDetalleMsgError.Text = "Indique Descripción para el Módulo";
                return false;
            }

            return true;
        }

        private void clearMessage()
        {
            lblDetalleMsgError.Text = "";
        }

        private void GuardarModulo()
        { 
            _idAplicacion = Convert.ToInt32(ASPxComboBoxAplicaciones.Value);
            var modulo = new Modulos
            { 
                Eliminado = false,
                idAplicacion = _idAplicacion,
                Codigo = txtCodigo.Text.Trim(),
                Descripcion = txtDescripcion.Text.Trim(),
                MenuNavigateURL = txtMenuNaURL.Text.Trim(),
                MenuToolTip = txtMenuToolTip.Text.Trim(),
                MenuImage = txtImage.Text.Trim()
            };

            if (!chkMostrarEnMenu.Checked)
                modulo.MenuVisibleIndex = 0;
            else
            {
                int idModulo = txtModuloKey.Text.Trim() != "" ? Convert.ToInt32(txtModuloKey.Text.Trim()) : -1;
                modulo.MenuVisibleIndex = ASPxListVisibleIndex.Items.Count > 0 ? ASPxListVisibleIndex.Items.FindByValue(idModulo).Index + 1 : 1;
            }

            string sPermisos = "";
            sPermisos += chkVisualizar.Checked ? "1," : "0,";
            sPermisos += chkAgregar.Checked ? "1," : "0,";
            sPermisos += chkModificar.Checked ? "1," : "0,";
            sPermisos += chkEliminar.Checked ? "1," : "0,";
            sPermisos += chkEspecial.Checked ? "1" : "0";
            modulo.Permisos = sPermisos;
            
            if(cmbPadre.SelectedItem == null)
                modulo.Padre = null; 
            else if(Convert.ToInt32(cmbPadre.SelectedItem.Value) > 0)
                modulo.Padre = Convert.ToInt32(cmbPadre.SelectedItem.Value);

            var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            bool bGuardar = false;

            if (Nuevo)
            {
                modulo.Agregado = DateTime.Now;
                modulo.Eliminado = false;
                bGuardar = classseguridad.NuevoModulo(modulo, ref ex);
            }
            else
            {
                modulo.idModulo = getNumberValue(txtModuloKey.Text);
                modulo.Modificado = DateTime.Now.Date;
                bGuardar = classseguridad.EditarModulo(modulo, ref ex);
            }

            if (!bGuardar)
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }
            else //if(modulo.Padre != null)
            {   
                foreach (ListEditItem item in ASPxListVisibleIndex.Items)
                {
                    if (item.Text == modulo.Descripcion)
                        continue;
                    if (!classseguridad.ActualizarIndexModulo(Convert.ToInt32(item.Value), item.Index + 1, ref ex))
                        throw new Exception(ex.Message, ex);
                }
            }

            EntityDataSourceModulosPadre.DataBind();
            EntityDataSourceModulos.DataBind();
            ASPxTreeList1.DataBind();
            txtModuloKey.Text = "";
            ASPxPopUpControlEditar.ShowOnPageLoad = false;
        }

        protected void btnCancelar_Click(object sender, ImageClickEventArgs e)
        {
            txtModuloKey.Text = "";
            ASPxPopUpControlEditar.ShowOnPageLoad = false;
        }

        private int getNumberValue(string v)
        {
            try
            {
                return Convert.ToInt32(v);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        protected void chkMostrarEnMenu_ValueChanged(object sender, EventArgs e)
        {
            if (cmbPadre.SelectedItem != null )
            {
                //if (Convert.ToInt32(cmbPadre.SelectedItem.Value) > 0)
                //{
                    ASPxCheckBox chk = (ASPxCheckBox)sender;

                    int idModulo = txtModuloKey.Text.Trim() != "" ? Convert.ToInt32(txtModuloKey.Text.Trim()) : -1;
                    ListEditItem tActual = ASPxListVisibleIndex.Items.FindByValue(idModulo);
                    if (tActual != null)
                    {
                        if (!chk.Checked)
                            ASPxListVisibleIndex.Items.Remove(tActual);
                    }
                    else
                    {
                        if (chk.Checked)
                        {
                            tActual = new ListEditItem() { Value = idModulo, Text = txtDescripcion.Text.Trim() };
                            ASPxListVisibleIndex.Items.Add(tActual);
                            tActual.Selected = true;
                        }
                    }
                //}
            }
        }

        protected void cmbPadre_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceModulosHijos.DataBind();
            ASPxListVisibleIndex.DataBind();

            if (ASPxPopUpControlEditar.ShowPageScrollbarWhenModal && ASPxPopUpControlEditar.ShowOnPageLoad && chkMostrarEnMenu.Checked && Convert.ToInt32(cmbPadre.SelectedItem.Value) > 0)
            {
                ListEditItem tActual = null;
                if (Nuevo)
                {
                    tActual = new ListEditItem() { Value = -1, Text = txtDescripcion.Text.Trim() };
                    ASPxListVisibleIndex.Items.Add(tActual);
                }
                else
                {
                    tActual = ASPxListVisibleIndex.Items.FindByValue(Convert.ToInt32(txtModuloKey.Text.Trim()));
                    if (tActual != null)
                        tActual.Text = txtDescripcion.Text;
                    else
                    {
                        tActual = new ListEditItem() { Value = Convert.ToInt32(txtModuloKey.Text.Trim()), Text = txtDescripcion.Text.Trim() };
                        ASPxListVisibleIndex.Items.Add(tActual);
                    }
                }
            }
        } 
    }
}