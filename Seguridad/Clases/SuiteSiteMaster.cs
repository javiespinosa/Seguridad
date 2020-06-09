using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Configuration;
using System.Net;
using Seguridad.Model;
using System.Web.Configuration;
using DevExpress.Web.ASPxMenu;

namespace Seguridad.Clases
{
	public class SuiteSiteMaster : System.Web.UI.MasterPage
	{
		#region Propiedades
		public virtual SuiteMembershipUser SuiteUser
		{
			get { return GetUser(); }
		}
		public virtual int AplicacionId
		{
			get { return GetAplicacionId(); }
		}

		public virtual void AsignarModulo(int idModulo)
		{
			AsignarViewState("ID_MODULO", idModulo.ToString());
		}
		public virtual void AsignarModulo2(string codigoModulo)
		{
			AsignarViewState("ID_CODIGO_MODULO", codigoModulo);
		}
		public virtual int AccPersonaId
		{
			get { return GetAccPersonaId(); }
		}
		public virtual string ReportServerUrl
		{
			get { return GetReporServerUrl(); }
		}

		#endregion


		#region Métodos internos
		private void AsignarViewState(string nombre, string valor)
		{
			if (ViewState[nombre] == null)
				ViewState.Add(nombre, valor);
			else
				ViewState[nombre] = valor;
		}
		protected SuiteMembershipUser GetUser()
		{
			return Membership.GetUser() as SuiteMembershipUser;
		}
		protected int GetAplicacionId()
		{
			var aplicacionId = Convert.ToInt32(ConfigurationManager.AppSettings["APLICACION_ID"]);
			Session["APLICACION_ID"] = aplicacionId;
			return aplicacionId;
		}
		protected int GetAccPersonaId()
		{
			var accPersonaId = 0;
			var siteUser = GetUser();
			if (siteUser != null)
			{
				accPersonaId = siteUser.SuiteIdUsuario;
			}
			return accPersonaId;
		}

		protected string GetReporServerUrl()
		{
			var reportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"];
			Session["ReportServerUrl"] = reportServerUrl;
			return reportServerUrl;
		}
		protected string GetPage(bool raw = false)
		{
			var resultado = Page.Request.FilePath.Trim().ToLower();
			if (raw) resultado = Page.Request.RawUrl.Trim().ToLower();
			var raiz = Request.ApplicationPath;
			if (raiz == "/")
			{
				resultado = "~" + resultado;
			}
			else
			{
				resultado = resultado.Replace(raiz.Trim().ToLower(), "~");
			}
			return resultado;
		}


		public bool ValidaItem(int idItem)
		{
			bool resultado = false;
			//using (var persona = new Suite.Comun.BLL.AccPersona())
			//{
			//    resultado = persona.ItemValidar(AccPersonaId, idItem);
			//}
			return resultado;
		}

		#endregion


		protected void PageLoad(ASPxMenu navigationMenu, TipoArmadoMenu tipoArmadoMenu)
		{
			var usuario = GetUser();
			if (!IsPostBack)
			{
				if (usuario.SuiteDiasVigenciaPassword != 0)// Si es cero, no tiene fecha de caducidad.
				{
					var d = usuario.LastPasswordChangedDate.AddDays(usuario.SuiteDiasVigenciaPassword).Date;
					if (usuario.LastPasswordChangedDate.AddDays(usuario.SuiteDiasVigenciaPassword).Date < DateTime.Now.Date)
					{
						if (Request.QueryString.Count == 0)
						{
							var rutaCambioPassword =
							ConfigurationManager.AppSettings["RUTA_CAMBIO_PASSWORD"];
							Response.Redirect(rutaCambioPassword + "?UN=" + usuario.SuiteUserName + "&RE=1");
						}
					}
				}

			}

			if (ValidaAcceso(usuario))
			{
				navigationMenu.Items.Clear();
				int idAplicacion = 0;
				if (tipoArmadoMenu == TipoArmadoMenu.Aplicaciones)
				{
					CargaMenuAplicaciones(usuario.SuiteIdUnidadAdministrativa, navigationMenu, usuario);
				}
				else
				{
					if (WebConfigurationManager.AppSettings["APLICACION_ID"] != null)
					{
						idAplicacion = Convert.ToInt32(WebConfigurationManager.AppSettings["APLICACION_ID"]);
					}
					CargaMenuModulos(idAplicacion, navigationMenu, usuario);
				}
				if (!IsPostBack)
				{


					var modulo = Convert.ToInt32(ViewState["ID_MODULO"]);
					if (Session["ENTRADA"] == null)
					{
						RegistrarEvento(usuario.SuiteIdUnidadAdministrativa, Convert.ToInt32(usuario.SuiteIdAplicacion), modulo,
							usuario.SuiteIdUsuario, string.Empty, string.Empty, true, (int)PistaColor.Verde, (int)PistaTipo.Acceso, string.Empty);
						Session.Add("ENTRADA", "1");
					}
					else
					{
						RegistrarEvento(usuario.SuiteIdUnidadAdministrativa, Convert.ToInt32(usuario.SuiteIdAplicacion), modulo,
							usuario.SuiteIdUsuario, string.Empty, string.Empty, true, (int)PistaColor.Verde, (int)PistaTipo.Consulta, string.Empty);
					}
				}

			}
			else
			{
				//navigationMenu.Items.Clear();
			}
		}

		public void VerificarIntentosValidos(ref System.Configuration.Configuration config)
		{
			var configSection = (System.Web.Configuration.MembershipSection)config.GetSection("system.web/membership");
			configSection.Providers["SuiteMembershipProvider"].Parameters["maxInvalidPasswordAttempts"] = "1";
			configSection.Providers["SuiteMembershipProvider"].Parameters["passwordAttemptWindow"] = "15";
			config.Save();
		}

		public bool ValidaAcceso(SuiteMembershipUser usuario)
		{
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			var modulo = Convert.ToInt32(ViewState["ID_MODULO"]);
			UsuariosPermisos usuariospermisos = classSeguridad.ObtUsuariosPermisos(usuario.SuiteIdUsuario, modulo, ref ex).FirstOrDefault();
			if (usuariospermisos != null)
			{
				return usuariospermisos.Visualizar;
			}
			else
			{
				return false;
			}
		}

		private void CargaMenuAplicaciones(int idUnidadAdministrativa, ASPxMenu menuItem, SuiteMembershipUser usuario)
		{
			var ex = new Exception();
			var c = new ClassSeguridad();
			var aplicaciones = c.ObtAplicacionesXUniAdmin(idUnidadAdministrativa).OrderBy(e => e.Clave);
			foreach (var a in aplicaciones)
			{
				var nuevoItem = new DevExpress.Web.ASPxMenu.MenuItem
				{
					Name = a.idAplicacion.ToString(),
					Text = a.Nombre,
					ToolTip = a.Descripcion,
					NavigateUrl = a.URL,
					Target = "_blank"
				};
				nuevoItem.Image.Url = a.Icono;
				if (string.IsNullOrEmpty(a.URL))
					CargaMenuModulos(a.idAplicacion, nuevoItem, usuario);
				menuItem.Items.Add(nuevoItem);
			}
		}

		private void CargaMenuModulos(int idAplicacion, DevExpress.Web.ASPxMenu.MenuItem menuItem, SuiteMembershipUser usuario)
		{
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			var modulos = classSeguridad.ObtModulosXAplicacion2(idAplicacion);
			var CodigoOpcionDefault = "000";
			if (WebConfigurationManager.AppSettings["CODIGO_OPCION_MENU_DEFAULT"] != null)
			{
				CodigoOpcionDefault = WebConfigurationManager.AppSettings["CODIGO_OPCION_MENU_DEFAULT"];
			}

			foreach (var m in modulos.OrderBy(b => b.MenuVisibleIndex))
			{
				if (m.Codigo != CodigoOpcionDefault)
				{
					var nuevoItem = new DevExpress.Web.ASPxMenu.MenuItem
					{
						Name = m.idModulo.ToString(),
						Text = m.Descripcion,
						NavigateUrl = m.MenuNavigateURL,
						ToolTip = m.MenuToolTip,
						VisibleIndex = Convert.ToInt32(m.MenuVisibleIndex),
					};

					if (m.MenuVisibleIndex == 0)
					{
						nuevoItem.Visible = false;
						continue;
					}

					nuevoItem.Image.Url = m.MenuImage;
					UsuariosPermisos usuariospermisos =
						classSeguridad.ObtUsuariosPermisos(usuario.SuiteIdUsuario, m.idModulo, ref ex).FirstOrDefault();
					if (usuariospermisos != null)
					{
						nuevoItem.Visible = usuariospermisos.Visualizar;
						if (usuariospermisos.Visualizar)
						{
							GenerarNodosHijos(m.idModulo, nuevoItem, usuario);
						}
					}
					else
					{
						nuevoItem.Visible = false;
					}
					menuItem.Items.Add(nuevoItem);
				}
			}
		}

		private void GenerarNodosHijos(int idModuloPadre, DevExpress.Web.ASPxMenu.MenuItem nodoPadre, SuiteMembershipUser usuario)
		{
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			var modulos = classSeguridad.ObtrModulosXPadre(idModuloPadre);
			foreach (var m in modulos)
			{
				var nuevoItem = new DevExpress.Web.ASPxMenu.MenuItem
				{
					Name = m.idModulo.ToString(),
					Text = m.Descripcion,
					NavigateUrl = m.MenuNavigateURL,
					ToolTip = m.MenuToolTip,
					VisibleIndex = Convert.ToInt32(m.MenuVisibleIndex),
				};
				nuevoItem.Image.Url = m.MenuImage;
				UsuariosPermisos usuariospermisos = classSeguridad.ObtUsuariosPermisos(usuario.SuiteIdUsuario, m.idModulo, ref ex).FirstOrDefault();
				if (usuariospermisos != null)
				{
					nuevoItem.Visible = usuariospermisos.Visualizar;
					if (usuariospermisos.Visualizar)
					{
						GenerarNodosHijos(m.idModulo, nuevoItem, usuario);
					}
				}
				else
				{
					nuevoItem.Visible = false;
				}

				if (m.MenuVisibleIndex <= 0) //Oculta los permisos que son externos al menú (botones, procesos, etc.)
					nuevoItem.Visible = false;

				nodoPadre.Items.Add(nuevoItem);
			}
		}

		private void CargaMenuModulos(int idAplicacion, ASPxMenu menuItem, SuiteMembershipUser usuario)
		{
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			var modulos = classSeguridad.ObtModulosXAplicacion2(idAplicacion);
			var CodigoOpcionDefault = "000";
			if (WebConfigurationManager.AppSettings["CODIGO_OPCION_MENU_DEFAULT"] != null)
			{
				CodigoOpcionDefault = WebConfigurationManager.AppSettings["CODIGO_OPCION_MENU_DEFAULT"];
			}

			foreach (var m in modulos)
			{
				if (m.Codigo != CodigoOpcionDefault)
				{
					var nuevoItem = new DevExpress.Web.ASPxMenu.MenuItem
					{
						Name = m.idModulo.ToString(),
						Text = m.Descripcion,
						NavigateUrl = m.MenuNavigateURL,
						ToolTip = m.MenuToolTip,
						VisibleIndex = Convert.ToInt32(m.MenuVisibleIndex),
					};
					nuevoItem.Image.Url = m.MenuImage;
					UsuariosPermisos usuariospermisos = classSeguridad.ObtUsuariosPermisos(usuario.SuiteIdUsuario, m.idModulo, ref ex).FirstOrDefault();
					if (usuariospermisos != null)
					{
						nuevoItem.Visible = usuariospermisos.Visualizar;
						if (usuariospermisos.Visualizar)
						{
							GenerarNodosHijos(m.idModulo, nuevoItem, usuario);
						}
					}
					else
					{
						nuevoItem.Visible = false;
					}

					menuItem.Items.Add(nuevoItem);
				}
			}

		}

		public int RegistrarEvento(int idUnidadAdministrativa_, int idAplicacion_, int idModulo_, int idUsuario_, string usuarioIp_, string usuarioAg_, bool resultadoOperacion_, int pistaColor_, int pistaTipo_, string pistaDetalle_)
		{
			var hostName = Dns.GetHostName();
			IPHostEntry ipHostEntry = Dns.GetHostEntry(hostName);
			usuarioIp_ = "HOST=" + hostName + "; IP=" + Convert.ToString(ipHostEntry.AddressList[ipHostEntry.AddressList.Length - 1]);


			var evento = new RegistroEventos
			{
				idUnidadAdministrativa = idUnidadAdministrativa_,
				idAplicacion = idAplicacion_,
				idModulo = idModulo_,
				idUsuario = idUsuario_,
				UsuarioIP = usuarioIp_,
				UsuarioAg = usuarioAg_,
				ResultadoOperacion = resultadoOperacion_,
				PistaColor = pistaColor_,
				PistaTipo = pistaTipo_,
				PistaDetalle = pistaDetalle_,
				FechaHora = DateTime.Now
			};
			var ex = new Exception();
			int resultado = 1;
			if (!ClassBitacora.RegistraEvento(evento, ref ex))
			{
				resultado = 0;
			}
			return resultado;
		}

		public UsuariosPermisos ObtenerPermisos()
		{
			var ex = new Exception();
			var classSeguridad = new ClassSeguridad();
			var modulo = Convert.ToInt32(ViewState["ID_MODULO"]);
			SuiteMembershipUser usuario = GetUser();
			UsuariosPermisos usuariospermisos = classSeguridad.ObtUsuariosPermisos(usuario.SuiteIdUsuario, modulo, ref ex).FirstOrDefault();
			if (ex.Message != string.Empty)
			{
				ClassBitacora.RegistraExcepcion(ex, Convert.ToInt32(usuario.SuiteIdAplicacion));
			}
			return usuariospermisos;
		}
	}
}