using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Data;
using DevExpress.Web.ASPxEditors;
using DevExpress.Web.ASPxGridView;
using DevExpress.Web.Data;
using Seguridad;
using Seguridad.Clases;
using Seguridad.Model;
using DevExpress.Web.ASPxTreeList;

namespace Suite.Web.Seguridad.forms
{
	public partial class usuarios : System.Web.UI.Page
	{
		private static int idUsuarioActual = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			this.Master.IdModulo = 7;
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
			if (Session["PERMISOS"] == null)
			{
				Response.Redirect("~/InicioSesion.aspx");
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

		protected void ASPxComboBoxUnidadesAdministrativas_SelectedIndexChanged(object sender, EventArgs e)
		{
			EntityDataSourceUsuarios.DataBind();
		}

		protected void ASPxGridView1_RowInserting(object sender, ASPxDataInsertingEventArgs e)
		{
			GridViewDataDateColumn a = ASPxGridView1.Columns[10] as GridViewDataDateColumn;

			var usuario = new Usuarios
			{
				Agregado = DateTime.Now,
				Eliminado = false,
				idUnidadAdministrativa = Convert.ToInt32(ASPxComboBoxUnidadesAdministrativas.Value),
				// idAplicacion = Convert.ToInt32(e.NewValues["idAplicacion"]),
				UserName = Convert.ToString(e.NewValues["UserName"]),
				Pwd = Convert.ToString(e.NewValues["UserName"]),
				Nombre = Convert.ToString(e.NewValues["Nombre"]),
				Puesto = Convert.ToString(e.NewValues["Puesto"]),
				Telefono = Convert.ToString(e.NewValues["Telefono"]),
				Email = Convert.ToString(e.NewValues["Email"]),
				Activo = Convert.ToBoolean(e.NewValues["Activo"]),
				Notas = Convert.ToString(e.NewValues["Notas"]),
				UsaBiometricos = Convert.ToBoolean(e.NewValues["UsaBiometricos"]),
				IsApproved = true,
				IsLockedOut = false,
				FailedPasswordAnswerAttemptCount = 0,
				FailedPasswordAttemptCount = 0,
				DiasVigenciaPassword = 0
			};

			if (e.NewValues["idAplicacion"] != null)
			{
				usuario.idAplicacion = Convert.ToInt32(e.NewValues["idAplicacion"]);
			}
			else
			{
				usuario.idAplicacion = -1;
			}

			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			if (!classSeguridad.NuevoUsuario(usuario, ref ex))
			{
				var usuario1 = Master.SuiteUser;
				ClassBitacora.RegistraExcepcion(ex, usuario1.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario1.SuiteIdUsuario);
				throw new Exception(ex.Message + ", " + ex.InnerException, ex);
			}

			idUsuarioActual = 0;

			e.Cancel = true;
			((ASPxGridView)sender).CancelEdit();
		}

		protected void ASPxGridView1_RowUpdating(object sender, ASPxDataUpdatingEventArgs e)
		{
			var usuario = new Usuarios
			{
				idUsuario = Convert.ToInt32(e.Keys[0]),
				Modificado = DateTime.Now,
				Eliminado = false,
				idUnidadAdministrativa = Convert.ToInt32(ASPxComboBoxUnidadesAdministrativas.Value),
				idAplicacion = Convert.ToInt32(e.NewValues["idAplicacion"]),
				UserName = Convert.ToString(e.NewValues["UserName"]),
				Pwd = Convert.ToString(e.NewValues["Pwd"]),
				Nombre = Convert.ToString(e.NewValues["Nombre"]),
				Puesto = Convert.ToString(e.NewValues["Puesto"]),
				Telefono = Convert.ToString(e.NewValues["Telefono"]),
				Email = Convert.ToString(e.NewValues["Email"]),
				Activo = Convert.ToBoolean(e.NewValues["Activo"]),
				Notas = Convert.ToString(e.NewValues["Notas"]),
				UsaBiometricos = Convert.ToBoolean(e.NewValues["UsaBiometricos"])
			};
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			if (!classSeguridad.EditarUsuario(usuario, ref ex))
			{
				var usuario1 = Master.SuiteUser;
				ClassBitacora.RegistraExcepcion(ex, usuario1.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario1.SuiteIdUsuario);
				throw new Exception(ex.Message, ex);
			}

			idUsuarioActual = 0;

			e.Cancel = true;
			((ASPxGridView)sender).CancelEdit();
		}

		protected void ASPxGridView1_RowDeleting(object sender, ASPxDataDeletingEventArgs e)
		{
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			if (!classSeguridad.EliminarUsuario(Convert.ToInt32(e.Keys[0]), ref ex))
			{
				var usuario1 = Master.SuiteUser;
				ClassBitacora.RegistraExcepcion(ex, usuario1.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario1.SuiteIdUsuario);
				throw new Exception(ex.Message, ex);
			}

			e.Cancel = true;
			((ASPxGridView)sender).CancelEdit();
		}

		protected void ASPxGridView1_CellEditorInitialize(object sender, ASPxGridViewEditorEventArgs e)
		{

			//idUsuarioActual = Convert.ToInt32(e.KeyValue);
			//if (e.Column.FieldName == "idAplicacion")
			//{
			//    var combo = (ASPxComboBox)e.Editor;
			//    var classeguridad = new ClassSeguridad();
			//    combo.DataSource = classeguridad.ObtAplicacionesXUniAdmin(Convert.ToInt32(ASPxComboBoxUnidadesAdministrativas.Value));
			//    combo.DataBind();
			//}
		}

		protected void ASPxButtonDesbloquear_Click(object sender, EventArgs e)
		{
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			var usuario = classSeguridad.ObtUsuario(idUsuarioActual);
			if (Convert.ToBoolean(usuario.IsLockedOut))
			{
				if (!classSeguridad.UnlockUser(idUsuarioActual))
				{
					var usuario1 = Master.SuiteUser;
					ClassBitacora.RegistraExcepcion(ex, usuario1.SuiteIdUnidadAdministrativa, Master.IdModulo, usuario1.SuiteIdUsuario);
					throw new Exception(ex.Message, ex);
				}
			}
		}

		protected void ASPxGridView1_InitNewRow(object sender, ASPxDataInitNewRowEventArgs e)
		{
			e.NewValues["IsLockedOut"] = false;
		}
	}
}