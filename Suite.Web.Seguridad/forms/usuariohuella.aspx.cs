using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Suite.Web.Seguridad.forms
{
    public partial class usuariohuella : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AppletLiteral.Text = "<APPLET code=\"DPO.AppletRegistro\" archive=\"JavaDPO.jar\" width=550 height=350> <PARAM name=\"Id\" value=\"" + Request.QueryString["id"] + "\"> </APPLET>";
        }
    }
}