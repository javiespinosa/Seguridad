using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Suite.Web.Seguridad.forms
{
    public partial class Excepciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected override void OnSaveStateComplete(EventArgs e)
        {
            EntityDataSourceExcepcionesLog.DataBind();
            ASPxGridView1.DataBind();
        }
        protected void ASPxComboBoxUnidadesAdministrativas_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceAplicaciones.DataBind(); 
            ASPxComboBoxAplicaciones.DataBind();

            EntityDataSourceExcepcionesLog.DataBind();
            ASPxGridView1.DataBind();
        }

        protected void ASPxComboBoxAplicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceExcepcionesLog.DataBind();
            ASPxGridView1.DataBind();
        }
    }
}