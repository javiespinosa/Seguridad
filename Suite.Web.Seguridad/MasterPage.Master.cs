using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using Seguridad;
using Seguridad.Clases;
using Seguridad.Model;

namespace Suite.Web.Seguridad
{
	public partial class MasterPage : SuiteSiteMaster
	{
		public int IdAplicacion_;
		private int _idModulo;
		public int IdModulo
		{
			get
			{
				return _idModulo;
			}
			set
			{
				_idModulo = value;
			}
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if (IdAplicacion_ == 0)
				IdAplicacion_ = 6;

			if (IdModulo == 0)
				IdModulo = 12; //Modulo del formulario default

			base.AsignarModulo(IdModulo);
			base.PageLoad(ASPxMenu1, TipoArmadoMenu.Modulo);
		}



	}
}