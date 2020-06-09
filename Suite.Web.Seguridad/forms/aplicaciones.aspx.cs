using System;
using System.Web.UI;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Seguridad.Clases;
using Seguridad.Model;

namespace Suite.Web.Seguridad.forms
{
    public partial class aplicaciones : Page
    {
       
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Master.IdModulo = 5;
            
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

        protected void ASPxGridView1_RowInserting(object sender, ASPxDataInsertingEventArgs e)
        {
            var aplicacion = new Aplicaciones
            {
                Agregado = DateTime.Now,
                Eliminado = false,
                Clave = Convert.ToString(e.NewValues["Clave"]),
                Nombre = Convert.ToString(e.NewValues["Nombre"]),
                Descripcion = Convert.ToString(e.NewValues["Descripcion"]),
                Activa = Convert.ToBoolean(e.NewValues["Activa"]),
                URL = Convert.ToString(e.NewValues["URL"]),
                Version = Convert.ToString(e.NewValues["Version"]),
                Icono = Convert.ToString(e.NewValues["Icono"]),
                Intentos =Convert.ToInt32(e.NewValues["Intentos"]  )
            };
            var ex = new Exception();
            var classSeguridad = new ClassSeguridad();
            if (!classSeguridad.NuevaAplicacion(aplicacion, ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex,usuario.SuiteIdUnidadAdministrativa,Master.IdModulo,usuario.SuiteIdUsuario);  
                throw new Exception(ex.Message, ex);
            }
            
            
            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        }

        protected void ASPxGridView1_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
        {
            var aplicacion = new Aplicaciones
            {
                idAplicacion =Convert.ToInt32( e.Keys[0] ),
                Modificado=DateTime.Now,
                Eliminado =false,
                Clave = Convert.ToString(e.NewValues["Clave"]),
                Nombre = Convert.ToString(e.NewValues["Nombre"]),
                Descripcion = Convert.ToString(e.NewValues["Descripcion"]),
                Activa = Convert.ToBoolean(e.NewValues["Activa"]),
                URL = Convert.ToString(e.NewValues["URL"]),
                Version = Convert.ToString(e.NewValues["Version"]),
                Icono = Convert.ToString(e.NewValues["Icono"]),
                Intentos =Convert.ToInt32(e.NewValues["Intentos"]  )
            };
            var ex = new Exception();
            var classSeguridad = new ClassSeguridad();
            var usuario = Master.SuiteUser;
            if (!classSeguridad.EditarAplicacion(aplicacion, ref ex))
            {
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);  
                throw new Exception(ex.Message, ex);
            }
            else
            {
                //var ree = new RegistroEventos
                //{
                //    idUnidadAdministrativa = usuario.SuiteIdUnidadAdministrativa,
                //    idAplicacion=usuario.SuiteIdAplicacion,
                //    idModulo = this.Master.IdModulo,
                //    idUsuario =usuario.SuiteIdUsuario,
                //    UsuarioIP ="",

                //};
                //ClassBitacora.RegistraEvento(ree, ref ex);
            }

            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        }

        protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
        { 
            var ex = new Exception();
            var classSeguridad = new ClassSeguridad();
            if (!classSeguridad.EliminarAplicacion(Convert.ToInt32(e.Keys[0]), ref ex))
            {
                var usuario = Master.SuiteUser;
                ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);  
                throw new Exception(ex.Message, ex);
            }

            e.Cancel = true;
            ((ASPxGridView)sender).CancelEdit();
        }


    }
}