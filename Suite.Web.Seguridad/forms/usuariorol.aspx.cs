using System;
using System.Collections.Generic;
using System.Web.UI;
using Seguridad;
using Seguridad.Clases;
using Seguridad.Model;

namespace Suite.Web.Seguridad.forms
{
    public partial class usuariorol : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //ASPxLabel2.Text = Request.QueryString["ua"].ToString();
            
        }

        protected override  void OnSaveStateComplete(EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarRolAsignado();
            }
        }

        private void CargarRolAsignado()
        {
            if (Request.QueryString.Count > 0)
            {
                int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
                var classSeguridad = new ClassSeguridad();
                var ex = new Exception();
                var resultado = classSeguridad.UbtUsuarioRol(idUsuario);
                if (resultado.Count > 0)
                {
                    UsuariosRoles usuariorol = resultado[0];
                    ASPxComboBoxRoles.SelectedItem = ASPxComboBoxRoles.Items.FindByValue(usuariorol.idRol.ToString());
                    ASPxButtonGuardar.Enabled = false;
                }
                ASPxButtonGuardar.Enabled = resultado.Count == 0;
            }
        }

        protected void ASPxButtonGuardar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                if (ASPxComboBoxRoles.SelectedItem != null)
                {
                    int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
                    var classSeguridad = new ClassSeguridad();
                    var ex = new Exception();
                    if (!classSeguridad.AsignarRolAUsuario(idUsuario, Convert.ToInt32(ASPxComboBoxRoles.SelectedItem.Value), ref ex))
                    {
                        //ClassBitacora.RegistraExcepcion(ex);
                        throw new Exception("No se pudo agregar el rol");
                    }
                }
            }
        }

        protected void ASPxButtonQuitar_Click(object sender, EventArgs e)
        {
            if (Request.QueryString.Count > 0)
            {
                int idUsuario = Convert.ToInt32(Request.QueryString["id"]);
                var classSeguridad = new ClassSeguridad();
                var ex = new Exception();
                if (!classSeguridad.QuitarRolAUsuario(idUsuario, ref ex))
                {
                    //ClassBitacora.RegistraExcepcion(ex);
                    throw new Exception("No se pudo agregar el rol");
                }

            }
        }
    }
}