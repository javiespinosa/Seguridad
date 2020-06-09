using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web.ASPxTreeList;
using Seguridad.Clases;
using Seguridad.Model;

namespace Suite.Web.Seguridad.forms
{
    public partial class PermisosModulosRoles : System.Web.UI.Page
    {  
        private int idRol
        {
            get
            {
                if (Session["PermisoIdRol"] == null)
                {
                    Session["PermisoIdRol"] = new int();
                }

                return (int)Session["PermisoIdRol"];
            }
            set { Session["PermisoIdRol"] = value; }
        }
        private Modulos tModulo
        {
            get
            {
                return null;
            }
            set { Session["PermisoModuloRol"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.IdModulo = 8;

            if (!IsPostBack)
            {
                tModulo = null;
                if (Request.QueryString["id"] != null)
                    idRol = Convert.ToInt32(Request.QueryString["id"]);

                EntityDataSourceAplicaciones.DataBind();
                EntityDataSourcePermisosRoles.CommandParameters.Add(new Parameter() { Name = "idRol", DbType = System.Data.DbType.Int32, DefaultValue = idRol.ToString() }); 
            }

            ClassSeguridad classSeguridad = new ClassSeguridad();
            Roles tRol = classSeguridad.ObtRol(idRol);
            if (tRol != null)
                //lblTitulo.Text = tRol.Descripcion;
                TreeListPermisosModulos.Caption = tRol.Descripcion;
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
            var RolPermiso = new RolesPermisos
            {
                idRolPermisos = Convert.ToInt32(((TextBox)TreeListPermisosModulos.FindEditFormTemplateControl("txtIdRolPermisos")).Text),
                idRol = idRol,
                idModulo = Convert.ToInt32(e.Keys["idModulo"]),
                Visualizar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkVisualizar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkVisualizar")).Checked : false,
                Agregar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkAgregar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkAgregar")).Checked : false,
                Modificar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkModificar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkModificar")).Checked : false,
                Eliminar = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEliminar")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEliminar")).Checked : false,
                Especial = ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEspecial")).Visible ? ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkEspecial")).Checked : false
            };

            GuardarPermisos(RolPermiso, ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkForzarPermisos")).Checked);

			//if (((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkAplicarSubniveles")).Checked)
			//{
			//    Exception ex = new Exception();
			//    var classseguridad = new Suite.Seguridad.BLL.ClassSeguridad();
			//    List<ObtPermisosRolesPadre_Result> tLista = classseguridad.ObtenerPermisosRolesPadre(RolPermiso.idRol, RolPermiso.idModulo, ref ex);
			//    foreach (var item in tLista)
			//    { 
			//        var tRolPermiso = new Suite.Seguridad.DAL.RolesPermisos
			//        {
			//            idRolPermisos = Convert.ToInt32(item.idRolPermisos),
			//            idModulo = item.idModulo,
			//            idRol = RolPermiso.idRol,
			//            Visualizar = RolPermiso.Visualizar,
			//            Agregar = RolPermiso.Agregar,
			//            Modificar = RolPermiso.Modificar,
			//            Eliminar = RolPermiso.Eliminar,
			//            Especial = RolPermiso.Especial
			//        };

			//        GuardarPermisos(tRolPermiso, ((CheckBox)TreeListPermisosModulos.FindEditFormTemplateControl("chkForzarPermisos")).Checked);
			//    }
			//}

            tModulo = null;

            EntityDataSourcePermisosRoles.DataBind();
            TreeListPermisosModulos.DataBind(); 

            e.Cancel = true;
            TreeListPermisosModulos.CancelEdit(); 
        }

        private void GuardarPermisos(RolesPermisos RolPermiso, bool ForzarPermisos)
        {
            try
            {
                var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            RolesPermisos tPermiso = null;
            RolesPermisos tPermisoTempRol = null;

            if (RolPermiso.idRolPermisos != -1)
            {
                tPermiso = classseguridad.ObtRolesPermisos(RolPermiso.idRol).Where(b => b.idModulo == RolPermiso.idModulo).FirstOrDefault();

                tPermisoTempRol = new RolesPermisos()
                {
                    Agregar = tPermiso.Agregar,
                    Eliminar = tPermiso.Eliminar,
                    Especial = tPermiso.Especial,
                    idModulo = tPermiso.idModulo,
                    idRol = tPermiso.idRol,
                    idRolPermisos = tPermiso.idRolPermisos,
                    Modificar = tPermiso.Modificar,
                    Visualizar = tPermiso.Visualizar
                };

            }

            if (RolPermiso.idRolPermisos != -1 &&
                (!RolPermiso.Visualizar &&
                (bool)!RolPermiso.Agregar &&
                (bool)!RolPermiso.Modificar &&
                (bool)!RolPermiso.Eliminar &&
                (bool)!RolPermiso.Especial))
            {
                if (!classseguridad.EliminarRolPermiso(RolPermiso.idRolPermisos, ref ex))
                {
                    var usuario = Master.SuiteUser;
                    ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                    throw new Exception(ex.Message, ex);
                }
                else
                    ActualizarUsuariosPermisos(classseguridad, RolPermiso, tPermisoTempRol, false, ForzarPermisos);
            }
            else if (RolPermiso.idRolPermisos == -1)
            {
                if (!classseguridad.NuevoRolPermiso(RolPermiso, ref ex))
                {
                    var usuario = Master.SuiteUser;
                    ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                    throw new Exception(ex.Message, ex);
                }
                else 
                    ActualizarUsuariosPermisos(classseguridad, RolPermiso, tPermisoTempRol, true, ForzarPermisos);
            }
            else
            { 
                if (!classseguridad.EditarRolPermiso(RolPermiso, ref ex))
                {
                    var usuario = Master.SuiteUser;
                    ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                    throw new Exception(ex.Message, ex);
                }
                else
                    ActualizarUsuariosPermisos(classseguridad, RolPermiso, tPermisoTempRol, false, ForzarPermisos);
            }
            }
            catch (Exception ex)
            {
                
            }
        }

        private bool ActualizarUsuariosPermisos(ClassSeguridad clsSeguridad, RolesPermisos _RolPermiso, RolesPermisos _tPermisoTempRol, bool Nuevo, bool ForzarPermisos)
        {
            bool Resultado = false;
            var ex = new Exception();

            List<UsuariosRoles> tListaUsuarios = clsSeguridad.ObtUsuariosRol(_RolPermiso.idRol);
            if (tListaUsuarios.Count > 0)
            {
                foreach (UsuariosRoles tUsuario in tListaUsuarios)
                {
                    UsuariosPermisos tPermisoTemp = clsSeguridad.ObtUsuariosPermisos(tUsuario.idUsuario, _RolPermiso.idModulo, ref ex).Where(b => b.idModulo == _RolPermiso.idModulo).FirstOrDefault();
                       
                    if (tPermisoTemp != null)
                    {
                        UsuariosPermisos tPermisoTempUsuario = new UsuariosPermisos()
                        {
                            Agregar = tPermisoTemp.Agregar,
                            Eliminar = tPermisoTemp.Eliminar,
                            idModulo = tPermisoTemp.idModulo,
                            idUsuario = tPermisoTemp.idUsuario,
                            idUsuarioPermiso = tPermisoTemp.idUsuarioPermiso,
                            Modificar = tPermisoTemp.Modificar,
                            Visualizar = tPermisoTemp.Visualizar,
                            Especial = tPermisoTemp.Especial
                        };

                        bool Actualizar = false;

                        if (ForzarPermisos)
                            Actualizar = true;
                        else if (!Nuevo)
                        {
                            if (tPermisoTempUsuario.Visualizar == _tPermisoTempRol.Visualizar &&
                                tPermisoTempUsuario.Agregar == _tPermisoTempRol.Agregar &&
                                tPermisoTempUsuario.Modificar == _tPermisoTempRol.Modificar &&
                                tPermisoTempUsuario.Eliminar == _tPermisoTempRol.Eliminar &&
                                tPermisoTempUsuario.Especial == _tPermisoTempRol.Especial)
                                Actualizar = true; 
                        }

                        if (Actualizar)
                        {
                            tPermisoTempUsuario.idUsuarioPermiso = tPermisoTemp.idUsuarioPermiso;
                            tPermisoTempUsuario.Visualizar = _RolPermiso.Visualizar;
                            tPermisoTempUsuario.Agregar = _RolPermiso.Agregar;
                            tPermisoTempUsuario.Modificar = _RolPermiso.Modificar;
                            tPermisoTempUsuario.Eliminar = _RolPermiso.Eliminar;
                            tPermisoTempUsuario.Especial = _RolPermiso.Especial; 

                            if (!tPermisoTempUsuario.Visualizar && (bool)!tPermisoTempUsuario.Agregar && (bool)!tPermisoTempUsuario.Modificar && (bool)!tPermisoTempUsuario.Eliminar && (bool)!tPermisoTempUsuario.Especial)
                                clsSeguridad.EliminarUsuarioPermiso(tPermisoTempUsuario.idUsuarioPermiso, ref ex);
                            else
                                clsSeguridad.EditarUsuarioPermiso(tPermisoTempUsuario, ref ex);
                        }
                    }
                    else
                    {
                        if (Nuevo || ForzarPermisos)
                        {
                            UsuariosPermisos tNuevoPermiso = new UsuariosPermisos()
                            {
                                idUsuario = tUsuario.idUsuario,
                                idModulo = _RolPermiso.idModulo,
                                Visualizar = _RolPermiso.Visualizar,
                                Agregar = _RolPermiso.Agregar,
                                Modificar = _RolPermiso.Modificar,
                                Eliminar = _RolPermiso.Eliminar,
                                Especial = _RolPermiso.Especial
                            };

                            clsSeguridad.NuevoUsuarioPermiso(tNuevoPermiso, ref ex);
                        }
                    } 
                }
            }

            return Resultado;
        }

        protected void TreeListPermisosModulos_HtmlDataCellPrepared(object sender, TreeListHtmlDataCellEventArgs e)
        {
            ClassSeguridad cSeguridad = new ClassSeguridad();
            Modulos tModuloPermisos = cSeguridad.ObtenerModulo(Convert.ToInt32(e.NodeKey));

            string sPermisos = "0,0,0,0,0";
            if (tModuloPermisos.Permisos != "" && tModuloPermisos.Permisos != null)
                sPermisos = tModuloPermisos.Permisos;

            string[] Permisos = sPermisos.Split(',');
            bool bVisualiar = Permisos[0] == "1" ? true : false;
            bool bAgregar = Permisos[1] == "1" ? true : false;
            bool bModificar = Permisos[2] == "1" ? true : false;
            bool bEliminar = Permisos[3] == "1" ? true : false;
            bool bEspecial = Permisos[4] == "1" ? true : false;

            if (e.Column.Index == 4 && !bVisualiar)
                e.Cell.Controls[0].Visible = false;
            else if (e.Column.Index == 5 && !bAgregar)
                e.Cell.Controls[0].Visible = false;
            else if (e.Column.Index == 6 && !bModificar)
                e.Cell.Controls[0].Visible = false;
            else if (e.Column.Index == 7 && !bEliminar)
                e.Cell.Controls[0].Visible = false;
            else if (e.Column.Index == 8 && !bEspecial)
                e.Cell.Controls[0].Visible = false;
        }

        protected void TreeListPermisosModulos_CellEditorInitialize(object sender, TreeListColumnEditorEventArgs e)
        {
            ClassSeguridad cSeguridad = new ClassSeguridad();
            tModulo = cSeguridad.ObtenerModulo(Convert.ToInt32(e.NodeKey));

            string sPermisos = "0,0,0,0,0";
            if (tModulo.Permisos != "" && tModulo.Permisos != null)
                sPermisos = tModulo.Permisos;

            string[] Permisos = sPermisos.Split(',');
            bool bVisualiar = Permisos[0] == "1" ? true : false;
            bool bAgregar = Permisos[1] == "1" ? true : false;
            bool bModificar = Permisos[2] == "1" ? true : false;
            bool bEliminar = Permisos[3] == "1" ? true : false;
            bool bEspecial = Permisos[4] == "1" ? true : false;

            if (e.Column.Index == 4 && !bVisualiar)
                e.Editor.Visible = false;
            else if (e.Column.Index == 5 && !bAgregar)
                e.Editor.Visible = false;
            else if (e.Column.Index == 6 && !bModificar)
                e.Editor.Visible = false;
            else if (e.Column.Index == 7 && !bEliminar)
                e.Editor.Visible = false;
            else if (e.Column.Index == 8 && !bEspecial)
                e.Editor.Visible = false;
        }

        protected void btnCancelarTodo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("roles.aspx");
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