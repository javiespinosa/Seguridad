using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Seguridad.Clases;

namespace Suite.Web.Seguridad.forms.Account
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                TextBoxActual.Focus();
        }


        /// <summary>
        /// La ruta en el menu deberá estar escrita con "ChangePassword.aspx?UN=1& RE=1" 
        /// el primer argumento ayuda a validar que se ejecute la opción desde el menú.
        /// El segundo argumento indica que se direccione la página a la página de inicio de sesion indicando en la variable RUTA_REGRESO_CAMBIO_PASSWORD el valor "default.aspx"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// 
        protected void ASPxButtonGuardar_Click(object sender, EventArgs e)
        {
            var classSeguridad = new ClassSeguridad();
            try
            {

                if (Request.QueryString["UN"] != null)
                {
                    var loginActual = Master.SuiteUser.SuiteUserName;

                    var passwordActual = TextBoxActual.Text.Trim();
                    var passwordNuevo = TextBoxNueva.Text.Trim();
                    var passwordRepetir = TextBoxRepetir.Text.Trim();


                    ASPxLabelError.Text = string.Empty;
                    if (passwordNuevo != passwordRepetir)
                    {
                        throw new Exception("No coincide la nueva contraseña");
                    }


                    var ex = new Exception();
                    if (!classSeguridad.CambiarPassword(loginActual, passwordActual, passwordNuevo, ref ex))
                    {
                        throw new Exception(ex.Message);

                    }
                    classSeguridad.Dispose();

                    if (Request.QueryString["RE"] != null)
                    {

                        HttpContext.Current.Session.Clear();
                        HttpContext.Current.Session.Abandon();
                        HttpContext.Current.User = null;
                        System.Web.Security.FormsAuthentication.SignOut(); // if forms auth is used
                        Session.Abandon();

                        var regreso = ConfigurationManager.AppSettings["RUTA_REGRESO_CAMBIO_PASSWORD"];
                        Response.Redirect(regreso);
                    }
                }
            }
            catch (Exception ex)
            {
                ASPxLabelError.Text = ex.Message;
                if (ex.Message == "Contraseña incorrecta.")
                {
                    TextBoxActual.Focus();
                }
                else
                {
                    TextBoxNueva.Focus();
                }
            }
        }

    }
}