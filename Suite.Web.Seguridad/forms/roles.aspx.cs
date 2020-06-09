using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Seguridad.Clases;
using Seguridad.Model;

namespace Suite.Web.Seguridad.forms
{
    public partial class roles : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.IdModulo = 8; 
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

        protected void ASPxGridView1_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            var permisos = (UsuariosPermisos)Session["PERMISOS"];

            if (e.ButtonType == ColumnCommandButtonType.New)
            {
                e.Visible = Convert.ToBoolean(permisos.Agregar);
            }
            if (e.ButtonType == ColumnCommandButtonType.Edit)
            {
                e.Visible = Convert.ToBoolean(permisos.Modificar);
            }
            if (e.ButtonType == ColumnCommandButtonType.Delete)
            {
                e.Visible = Convert.ToBoolean(permisos.Eliminar);
            } 
        }
        
        protected void ASPxComboBoxUnidadesAdministrativas_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceRoles.DataBind();
        } 

        protected void ASPxGridView1_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            var rol = new Roles
            {
                Agregado = DateTime.Now,
                Eliminado = false,
                idUnidadAdministrativa = Convert.ToInt32(ASPxComboBoxUnidadesAdministrativas.Value),
                idAplicacion = -1,
                Codigo = Convert.ToString(e.NewValues["Codigo"]),
                Descripcion = Convert.ToString(e.NewValues["Descripcion"])
            };
            var ex = new Exception();
            var classSeguridad = new ClassSeguridad();
            if (!classSeguridad.NuevoRol(rol, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }

            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        }

        protected void ASPxGridView1_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            var rol = new Roles
            {
                idRol  = Convert.ToInt32(e.Keys[0]),
                Modificado = DateTime.Now,
                Eliminado = false,
                idUnidadAdministrativa = Convert.ToInt32(ASPxComboBoxUnidadesAdministrativas.Value),
                idAplicacion = -1,
                Codigo = Convert.ToString(e.NewValues["Codigo"]),
                Descripcion = Convert.ToString(e.NewValues["Descripcion"])
            };
            var ex = new Exception();
            var classSeguridad = new ClassSeguridad();
            if (!classSeguridad.EditarRol(rol, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            } 
            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        } 

        protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            var ex = new Exception();
            var classSeguridad = new ClassSeguridad();
            if (!classSeguridad.EliminarRol( Convert.ToInt32(e.Keys[0]), ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }

            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        } 

        protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
        {
            if (e.Column.FieldName == "idAplicacion")
            {
                var combo = (ASPxComboBox)e.Editor;
                var classeguridad = new ClassSeguridad();
                combo.DataSource = classeguridad.ObtAplicacionesXUniAdmin(Convert.ToInt32(ASPxComboBoxUnidadesAdministrativas.Value));
                combo.DataBind();
            }
           
        }

        protected void ASPxGridView1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
        {
            var ex = new Exception();
            var classSeguridad = new ClassSeguridad();
            var codigo = classSeguridad.ObtCodigoRol(ref ex);
            e.NewValues["Codigo"] = codigo;
        }
    }
}
