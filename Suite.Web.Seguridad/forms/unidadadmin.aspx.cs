using System;
using System.Data;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Seguridad.Clases;
using Seguridad.Model;

namespace Suite.Web.Seguridad.forms
{
    public partial class unidadadmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.IdModulo = 4;
           
            
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
            var permisos = (UsuariosPermisos) Session["PERMISOS"];

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
            ASPxGridView1.SettingsDetail.ShowDetailRow = Convert.ToBoolean(permisos.Especial);
            
        }
        protected void ASPxGridView2_CommandButtonInitialize(object sender, ASPxGridViewCommandButtonEventArgs e)
        {
            var permisos = Master.ObtenerPermisos();

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


        protected void ASPxGridView1_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            var unidadAdministrativa = new UnidadesAdministrativas  
            {
                Agregado = DateTime.Now,
                Eliminado = false,
                Clave = Convert.ToString(e.NewValues["Clave"]),
                Nombre = Convert.ToString(e.NewValues["Nombre"]),
                RFC = Convert.ToString(e.NewValues["RFC"]),
                Telefono = Convert.ToString(e.NewValues["Telefono"]),
                idSectorPublico = Convert.ToInt32(e.NewValues["idSectorPublico"]),
                idSecPubFin = Convert.ToInt32(e.NewValues["idSecPubFin"]),
                idSectorEconomico = Convert.ToInt32(e.NewValues["idSectorEconomico"]),
                idEntePublico = Convert.ToInt32(e.NewValues["idEntePublico"]),
                Notas = Convert.ToString(e.NewValues["Notas"])
            };
            if (e.NewValues["Padre"] == null)
                unidadAdministrativa.Padre = null;
            else
            {
                unidadAdministrativa.Padre = Convert.ToInt32(e.NewValues["Padre"]);
            }
            var ex = new Exception();
            var classEmpresa = new ClassSeguridad();
            if (!classEmpresa.NuevaUnidadAdministrativa(unidadAdministrativa, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }


            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }
        protected void ASPxGridView1_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            var unidadAdministrativa = new UnidadesAdministrativas
            {
                idUnidadAdministrativa = Convert.ToInt32(e.Keys[0]),
                Modificado = DateTime.Now.Date,
                Eliminado = false,
                Clave = Convert.ToString(e.NewValues["Clave"]),
                Nombre = Convert.ToString(e.NewValues["Nombre"]),
                RFC = Convert.ToString(e.NewValues["RFC"]),
                Telefono = Convert.ToString(e.NewValues["Telefono"]),
                idSectorPublico = Convert.ToInt32(e.NewValues["idSectorPublico"]),
                idSecPubFin = Convert.ToInt32(e.NewValues["idSecPubFin"]),
                idSectorEconomico = Convert.ToInt32(e.NewValues["idSectorEconomico"]),
                idEntePublico = Convert.ToInt32(e.NewValues["idEntePublico"]),
                Notas = Convert.ToString(e.NewValues["Notas"])
            };

            if (e.NewValues["Padre"] == null)
                unidadAdministrativa.Padre = null;
            else
            {
                unidadAdministrativa.Padre = Convert.ToInt32(e.NewValues["Padre"]);
            }
            var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            if (!classseguridad.EditarUnidadAdministrativa(unidadAdministrativa, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }
        protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            if (!classseguridad.EliminarUnidadAdministrativa( Convert.ToInt32(e.Keys[0]), ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }

            e.Cancel = true;
            ASPxGridView1.CancelEdit();
        }

        protected void detailGrid_DataSelect(object sender, EventArgs e)
        {
            Session["ID_UNIDADADMINISTRATIVA"] = (sender as ASPxGridView).GetMasterRowKeyValue();
        }
        protected void ASPxGridView1_DetailRowExpandedChanged(object sender, ASPxGridViewDetailRowEventArgs e)
        {
            Session["DetailRowVisibleIndex"] = e.VisibleIndex;
        }

        protected void ASPxGridView2_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            var aplicacioneUa=new AplicacionesUA
            {
                idUnidadAdministrativa = Convert.ToInt32(Session["ID_UNIDADADMINISTRATIVA"]),
                idAplicacion = Convert.ToInt32(e.NewValues["idAplicacion"])
            };
            var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            if (!classseguridad.NuevaAplicacionUa(aplicacioneUa, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }
            e.Cancel = true;
            var detail = (ASPxGridView)sender;
            detail.CancelEdit();
           
        }
        protected void ASPxGridView2_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            var aplicacioneUa = new AplicacionesUA
            {
                idAplicacionUA  = Convert.ToInt32(e.Keys[0]),
                idUnidadAdministrativa = Convert.ToInt32(Session["ID_UNIDADADMINISTRATIVA"]),
                idAplicacion = Convert.ToInt32(e.NewValues["idAplicacion"])
            };

            var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            if (!classseguridad.EditarAplicacionUa(aplicacioneUa, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }

            e.Cancel = true;
            var detail = (ASPxGridView)sender;
            detail.CancelEdit();
        }
        protected void ASPxGridView2_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        {
            var ex = new Exception();
            var classseguridad = new ClassSeguridad();
            if (!classseguridad.EliminarAplicacionUa( Convert.ToInt32(e.Keys[0]), ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                throw new Exception(ex.Message, ex);
            }

            e.Cancel = true;
            var detail = (ASPxGridView)sender;
            detail.CancelEdit();
        }

    }
}