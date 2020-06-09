using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Suite.Web.Seguridad.forms
{
    public partial class Eventos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected override void OnSaveStateComplete(EventArgs e)
        {
            //EntityDataSourceEventos.DataBind();
            //ASPxGridView1.DataBind();
        }

        protected void ASPxComboBoxUnidadesAdministrativas_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceAplicaciones.DataBind();
            ASPxComboBoxAplicaciones.DataBind();

            //EntityDataSourceUsuarios.DataBind();
            //ASPxComboBoxUsuarios.DataBind();

            //EntityDataSourceEventos.DataBind();
            //ASPxGridView1.DataBind();
        }

        protected void ASPxComboBoxAplicaciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            //EntityDataSourceUsuarios.DataBind(); 
            //ASPxComboBoxUsuarios.DataBind();

            EntityDataSourceEventos.DataBind();
            ASPxGridView1.DataBind();

            //EntityDataSourceModulos.DataBind();
        }

        protected void ASPxComboBoxUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            EntityDataSourceEventos.DataBind();
            ASPxGridView1.DataBind();
        }
    }
}