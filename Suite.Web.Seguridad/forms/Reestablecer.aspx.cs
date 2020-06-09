using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad.Clases;
using Seguridad.Model;

namespace Suite.Web.Seguridad.forms
{
    public partial class Reestablecer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ASPxButtonReestablecer_Click(object sender, EventArgs e)
        {
            ClassSeguridad classSeguridad = new ClassSeguridad();
            if (Request.QueryString.Count > 0)
            {
                int idUsuario = Convert.ToInt32( Request.QueryString[0]);
                var ex = new Exception();
                if (!classSeguridad.ReestablecerPassword(idUsuario, ref ex))
                {
                    var usuario = Master.SuiteUser;
                    ClassBitacora.RegistraExcepcion(ex, usuario.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario.SuiteIdUsuario);
                    throw new Exception(ex.Message, ex);
                }
                Panel1.Visible = false;
                Panel2.Visible = true;
            }

            
        }
    }
}