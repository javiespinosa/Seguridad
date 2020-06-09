using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad.Clases;
using Seguridad.Model;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxTreeList;
using System.Drawing;

namespace Suite.Web.Seguridad.forms
{
    public partial class PermisosModulos : System.Web.UI.Page
    {
        private int idUsuario
        {
            get
            {
                if (Session["PermisoIdUsuario"] == null)
                {
                    Session["PermisoIdUsuario"] = new int();
                }

                return (int)Session["PermisoIdUsuario"];
            }
            set { Session["PermisoIdUsuario"] = value; }
        } 
        private Modulos tModulo
        {
            get
            { 
                return null;
            }
            set { Session["PermisoModulo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.IdModulo = 7;

            if (!IsPostBack)
            {
                tModulo = null;
                if (Request.QueryString["id"] != null)
                    idUsuario = Convert.ToInt32(Request.QueryString["id"]);

                EntityDataSourceAplicaciones.DataBind(); 
                EntityDataSourcePermisos.CommandParameters["idUsuario"].DefaultValue = idUsuario.ToString(); 
            }

            ClassSeguridad classSeguridad = new ClassSeguridad();
            Usuarios tUsuario = classSeguridad.ObtUsuario(idUsuario);
            if (tUsuario != null)
                //lblTitulo.Text = tUsuario.Nombre;
                TreeListPermisosModulos.Caption = tUsuario.Nombre;

            tUsuario = null;
            classSeguridad = null;
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

        protected void TreeListPermisosModulos_NodeUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        { 
            var UsuarioPermiso = new UsuariosPermisos 
            { 
                idUsuarioPermiso = Convert.ToInt32(((TextBox)TreeListPermisosModulos.FindEditFormTemplateControl("txtIdUsuarioPermiso")).Text), 
                idUsuario = idUsuario,
                idModulo = Convert.ToInt32(e.Keys["idModulo"]),
                Visualizar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkVisualizar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkVisualizar")).Checked : false,
                Agregar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkAgregar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkAgregar")).Checked : false,
                Modificar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkModificar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkModificar")).Checked : false,
                Eliminar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEliminar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEliminar")).Checked : false,
                Especial = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEspecial")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEspecial")).Checked : false
            }; 
            
            GuardarPermiso(UsuarioPermiso);

			//if (((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkAplicarSubniveles")).Checked) 
			//{
			//    Exception ex = new Exception();
			//    var classseguridad = new Suite.Seguridad.BLL.ClassSeguridad();
			//    List<ObtPermisosUsuariosPadre_Result> tLista = classseguridad.ObtenerPermisosUsuarioPadre(UsuarioPermiso.idUsuario, UsuarioPermiso.idModulo, ref ex);
			//    foreach (var item in tLista)
			//    {
			//        var tUsuarioPermiso = new Suite.Seguridad.DAL.UsuariosPermisos
			//        { 
			//            idUsuarioPermiso = Convert.ToInt32(item.idUsuarioPermiso),
			//            idUsuario = UsuarioPermiso.idUsuario,
			//            idModulo = item.idModulo,
			//            Visualizar = UsuarioPermiso.Visualizar,
			//            Agregar = UsuarioPermiso.Agregar,
			//            Modificar = UsuarioPermiso.Modificar,
			//            Eliminar = UsuarioPermiso.Eliminar,
			//            Especial = UsuarioPermiso.Especial
			//        };

			//        GuardarPermiso(tUsuarioPermiso);
			//    }
			//}

            e.Cancel = true;
            TreeListPermisosModulos.CancelEdit();

            tModulo = null;

            EntityDataSourcePermisos.DataBind();
            TreeListPermisosModulos.DataBind();
        }

        private void GuardarPermiso(UsuariosPermisos UsuarioPermiso)
        {
            try
            {
                var ex = new Exception();
                var classseguridad = new ClassSeguridad();

                if (UsuarioPermiso.idUsuarioPermiso != -1 &&
                    (!UsuarioPermiso.Visualizar &&
                    (bool)!UsuarioPermiso.Agregar &&
                    (bool)!UsuarioPermiso.Modificar &&
                    (bool)!UsuarioPermiso.Eliminar &&
                    (bool)!UsuarioPermiso.Especial))
                {
                    if (!classseguridad.EliminarUsuarioPermiso(UsuarioPermiso.idUsuarioPermiso, ref ex))
                    {
                        var usuario = Master.SuiteUser;
                        ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                        throw new Exception(ex.Message, ex);
                    }
                }
                else if (UsuarioPermiso.idUsuarioPermiso == -1)
                {
                    if (!classseguridad.NuevoUsuarioPermiso(UsuarioPermiso, ref ex))
                    {
                        var usuario = Master.SuiteUser;
                        ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                        throw new Exception(ex.Message, ex);
                    }
                }
                else
                {
                    if (!classseguridad.EditarUsuarioPermiso(UsuarioPermiso, ref ex))
                    {
                        var usuario = Master.SuiteUser;
                        ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                        throw new Exception(ex.Message, ex);
                    }
                } 
            }
            catch (Exception ex)
            {
                
            } 
        }

        protected void TreeListPermisosModulos_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {   
            ClassSeguridad cSeguridad = new ClassSeguridad();
            Modulos tModulo = cSeguridad.ObtenerModulo(Convert.ToInt32(e.NodeKey));
            
            string sPermisos = "0,0,0,0";
            if (tModulo.Permisos != "" && tModulo.Permisos != null)
                sPermisos = tModulo.Permisos;

            string[] Permisos = sPermisos.Split(',');
            bool bVisualiar = Permisos[0] == "1" ? true : false;
            bool bAgregar = Permisos[1] == "1" ? true : false;
            bool bModificar = Permisos[2] == "1" ? true : false;
            bool bEliminar = Permisos[3] == "1" ? true : false;
                
            if (e.Column.Index == 4 && !bVisualiar)
                e.Cell.Controls[0].Visible = false;                    
            else if (e.Column.Index == 5 && !bAgregar)
                e.Cell.Controls[0].Visible = false;
            else if (e.Column.Index == 6 && !bModificar)
                e.Cell.Controls[0].Visible = false;
            else if (e.Column.Index == 7 && !bEliminar)
                e.Cell.Controls[0].Visible = false;  
        }

        protected void TreeListPermisosModulos_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
        {
            ClassSeguridad cSeguridad = new ClassSeguridad();
            tModulo = cSeguridad.ObtenerModulo(Convert.ToInt32(e.NodeKey));

            string sPermisos = "0,0,0,0";
            if (tModulo.Permisos != "" && tModulo.Permisos != null)
                sPermisos = tModulo.Permisos;

            string[] Permisos = sPermisos.Split(',');
            bool bVisualiar = Permisos[0] == "1" ? true : false;
            bool bAgregar = Permisos[1] == "1" ? true : false;
            bool bModificar = Permisos[2] == "1" ? true : false;
            bool bEliminar = Permisos[3] == "1" ? true : false;

            if (e.Column.Index == 4 && !bVisualiar) 
                e.Editor.Visible = false;  
            else if (e.Column.Index == 5 && !bAgregar)
                e.Editor.Visible = false;
            else if (e.Column.Index == 6 && !bModificar)
                e.Editor.Visible = false;
            else if (e.Column.Index == 7 && !bEliminar)
                e.Editor.Visible = false; 
        }

        protected void btnCancelarTodo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        protected void chkVisualizar_Load(object sender, EventArgs e)
        {
            if (tModulo == null)
                return;

            CheckBox chk = (CheckBox)sender;

            string sPermisos = "0,0,0,0,0";
            if (tModulo.Permisos != "" && tModulo.Permisos != null)
                sPermisos = tModulo.Permisos;

            string[] Permisos = sPermisos.Split(',');
            bool bVisualiar = Permisos[0] == "1" ? true : false;
            bool bAgregar = Permisos[1] == "1" ? true : false;
            bool bModificar = Permisos[2] == "1" ? true : false;
            bool bEliminar = Permisos[3] == "1" ? true : false;
            bool bEspecial = Permisos[4] == "1" ? true : false;

            if (chk.ID == "chkVisualizar" && !bVisualiar)                
                chk.Visible = false; 
            else if (chk.ID == "chkAgregar" && !bAgregar) 
                chk.Visible = false; 
            else if (chk.ID == "chkModificar" && !bModificar) 
                chk.Visible = false; 
            else if (chk.ID == "chkEliminar" && !bEliminar) 
                chk.Visible = false; 
            else if (chk.ID == "chkEspecial" && !bEspecial) 
                chk.Visible = false; 
        }

        protected void TreeListPermisosModulos_StartNodeEditing(object sender, TreeListNodeEditingEventArgs e)
        {
            ClassSeguridad cSeguridad = new ClassSeguridad();
            tModulo = cSeguridad.ObtenerModulo(Convert.ToInt32(e.NodeKey));
        }

        protected void ASPxComboBoxAplicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceAplicaciones.DataBind();
            TreeListPermisosModulos.DataBind();
        }
    }
}