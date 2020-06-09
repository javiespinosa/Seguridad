using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seguridad.Model;
using System.Web.Configuration;
using System.Transactions;
using System.Data;
using System.Data.Objects;

namespace Seguridad.Clases
{
	public class ClassSeguridad : IDisposable
	{
        private Seguridad2017Entities context = new Seguridad2017Entities();
		//private ClassBitacora bitacora;
		private int idAplicacion;
		private const int Intentos = 3;

		public ClassSeguridad()
		{
			if (WebConfigurationManager.AppSettings["APLICACION_ID"] != null)
			{
				idAplicacion = Convert.ToInt32(WebConfigurationManager.AppSettings["APLICACION_ID"]);
			}
		}

		#region CRUD Unidades Administrativas

		public bool NuevaUnidadAdministrativa(UnidadesAdministrativas unidadAdministrativa, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.UnidadesAdministrativas.Any())
							id = context.UnidadesAdministrativas.Select(x => x.idUnidadAdministrativa).Max();
						unidadAdministrativa.idUnidadAdministrativa = id + 1;

						context.AddToUnidadesAdministrativas(unidadAdministrativa);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							//ClassBitacora.RegistraExcepcion(ex, unidadAdministrativa.idUnidadAdministrativa , -1, -1,-1);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EditarUnidadAdministrativa(UnidadesAdministrativas unidadAdministrativa, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal =
							context.UnidadesAdministrativas.Single(
								c => c.idUnidadAdministrativa == unidadAdministrativa.idUnidadAdministrativa);
						unidadAdministrativa.Agregado = registroOriginal.Agregado;

						context.UnidadesAdministrativas.Attach(registroOriginal);
						context.UnidadesAdministrativas.ApplyCurrentValues(unidadAdministrativa);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							//ClassBitacora.RegistraExcepcion(ex, unidadAdministrativa.idUnidadAdministrativa , -1, -1,-1);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EliminarUnidadAdministrativa(int idUnidadAdministrativa, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal =
							context.UnidadesAdministrativas.Single(
								c => c.idUnidadAdministrativa == idUnidadAdministrativa);
						var unidadAdministrativa = new UnidadesAdministrativas
						{
							idUnidadAdministrativa = registroOriginal.idUnidadAdministrativa,
							Agregado = registroOriginal.Agregado,
							Modificado = DateTime.Now,
							Eliminado = true,
							Clave = registroOriginal.Clave,
							Nombre = registroOriginal.Nombre,
							RFC = registroOriginal.RFC,
							Telefono = registroOriginal.Telefono,
							idSectorPublico = registroOriginal.idSectorPublico,
							idSecPubFin = registroOriginal.idSecPubFin,
							idSectorEconomico = registroOriginal.idSectorEconomico,
							idEntePublico = registroOriginal.idEntePublico,
							Notas = registroOriginal.Notas,
							Padre = registroOriginal.Padre
						};

						context.UnidadesAdministrativas.Attach(registroOriginal);
						context.UnidadesAdministrativas.ApplyCurrentValues(unidadAdministrativa);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// ClassBitacora.RegistraExcepcion(ex, idUnidadAdministrativa, -1, -1, -1);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		#endregion

		#region Aplicaciones - Unidades administrativas

		public bool NuevaAplicacionUa(AplicacionesUA aplicacionUa, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.AplicacionesUA.Any())
							id = context.AplicacionesUA.Select(x => x.idAplicacionUA).Max();
						aplicacionUa.idAplicacionUA = id + 1;

						context.AddToAplicacionesUA(aplicacionUa);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EditarAplicacionUa(AplicacionesUA aplicacionUa, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal =
							context.AplicacionesUA.Single(c => c.idAplicacionUA == aplicacionUa.idAplicacionUA);

						context.AplicacionesUA.Attach(registroOriginal);
						context.AplicacionesUA.ApplyCurrentValues(aplicacionUa);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EliminarAplicacionUa(int idAplicacionUa, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.AplicacionesUA.Single(c => c.idAplicacionUA == idAplicacionUa);
						context.DeleteObject(registroOriginal);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}

			return success;
		}

		#endregion

		#region CRUD Usuarios

		public bool NuevoUsuario(Usuarios usuario, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.Usuarios.Any())
							id = context.Usuarios.Select(x => x.idUsuario).Max();
						usuario.idUsuario = id + 1;
						if (EncriptarPassword() && usuario.Pwd != null)
						{
							var passwordEncriptado = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(usuario.Pwd, FormsAuthPasswordFormat.SHA1.ToString());
							usuario.Pwd = passwordEncriptado;
						}

						context.AddToUsuarios(usuario);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public static bool EncriptarPassword()
		{
			var resultado = false;
			if (WebConfigurationManager.AppSettings["ENCRIPTAR_PASSWORD"] != null)
			{
				if (WebConfigurationManager.AppSettings["ENCRIPTAR_PASSWORD"] == "1")
				{
					resultado = true;
				}
			}
			return resultado;
		}

		public bool EditarUsuario(Usuarios usuario, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Usuarios.Single(c => c.idUsuario == usuario.idUsuario);
						context.Usuarios.Attach(registroOriginal);
                        registroOriginal.UserName = usuario.UserName;
						registroOriginal.Modificado = DateTime.Now;
						registroOriginal.Nombre = usuario.Nombre;
						registroOriginal.Puesto = usuario.Puesto;
						registroOriginal.Telefono = usuario.Telefono;
						registroOriginal.Email = usuario.Email;
						registroOriginal.Activo = usuario.Activo;
						registroOriginal.Notas = usuario.Notas;
						registroOriginal.UsaBiometricos = usuario.UsaBiometricos;
						registroOriginal.IsApproved = true;
						registroOriginal.DiasVigenciaPassword = usuario.DiasVigenciaPassword;


						//usuario.Agregado = registroOriginal.Agregado;
						//usuario.idUnidadAdministrativa = registroOriginal.idUnidadAdministrativa;
						//usuario.idAplicacion = registroOriginal.idAplicacion;   
						//usuario.Pwd = registroOriginal.Pwd;
						//if (EncriptarPassword() && usuario.Pwd != null)
						//{
						//    var passwordEncriptado = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(usuario.Pwd, FormsAuthPasswordFormat.SHA1.ToString());
						//    usuario.Pwd = passwordEncriptado;
						//}


						context.Usuarios.ApplyCurrentValues(registroOriginal);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool SetLastLoginDate(int idUsuario, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Usuarios.Single(c => c.idUsuario == idUsuario);
						context.Usuarios.Attach(registroOriginal);
						registroOriginal.LastLoginDate = DateTime.Now;
						context.Usuarios.ApplyCurrentValues(registroOriginal);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public void UpdateFailureCount(int idUsuario, int passwordAttemptWindow, int maxInvalidPasswordAttempts, string failureType)
		{
			Usuarios usuario;
			var windowStart = new DateTime();
			int failureCount = 0;
			using (var transaction = new TransactionScope())
			{
				try
				{
					usuario = context.Usuarios.FirstOrDefault(e => e.idUsuario == idUsuario);
					if (usuario != null)
					{
						if (failureType == "password")
						{
							failureCount = Convert.ToInt32(usuario.FailedPasswordAttemptCount);
							windowStart = Convert.ToDateTime(usuario.FailedPasswordAttemptWindowStart);
						}
						if (failureType == "passwordAnswer")
						{
							failureCount = Convert.ToInt32(usuario.FailedPasswordAnswerAttemptCount);
							windowStart = Convert.ToDateTime(usuario.FailedPasswordAnswerAttemptWindowStart);
						}
						var windowEnd = windowStart.AddMinutes(passwordAttemptWindow);
						context.Usuarios.Attach(usuario);
						if (failureCount == 0 || DateTime.Now > windowEnd)
						{
							if (failureType == "password")
							{
								usuario.FailedPasswordAttemptCount = 1;
								usuario.FailedPasswordAttemptWindowStart = DateTime.Now;
							}
							if (failureType == "passwordAnswer")
							{
								usuario.FailedPasswordAnswerAttemptCount = 1;
								usuario.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
							}
						}
						else
						{
							if (failureCount++ >= maxInvalidPasswordAttempts)
							{
								usuario.IsLockedOut = true;
								usuario.LastLockoutDate = DateTime.Now;
							}
							else
							{
								if (failureType == "password")
								{
									usuario.FailedPasswordAttemptCount = failureCount;

								}
								if (failureType == "passwordAnswer")
								{
									usuario.FailedPasswordAnswerAttemptCount = failureCount;
								}
							}
						}
						context.Usuarios.ApplyCurrentValues(usuario);
						context.SaveChanges();

						transaction.Complete();
					}
				}
				catch (Exception )
				{

					throw;
				}
			}
		}

		public bool EliminarUsuario(int idUsuario, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Usuarios.Single(c => c.idUsuario == idUsuario);
						context.Usuarios.Attach(registroOriginal);
						registroOriginal.Eliminado = true;
						registroOriginal.Modificado = DateTime.Now;
						context.Usuarios.ApplyCurrentValues(registroOriginal);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;

		}

		private bool CheckPassword(string password, string dbpassword)
		{
			string pass1 = password;
			string pass2 = dbpassword;

			//switch (PasswordFormat)
			//{
			//    case MembershipPasswordFormat.Encrypted:
			//        pass2 = UnEncodePassword(dbpassword);
			//        break;
			//    case MembershipPasswordFormat.Hashed:
			//        pass1 = EncodePassword(password);
			//        break;
			//    default:
			//        break;
			//}

			if (pass1 == pass2)
			{
				return true;
			}

			return false;
		}

		public bool UnlockUser(int idUsuario)
		{
			var resultado = false;
			using (var transaction = new TransactionScope())
			{
				try
				{
					var usuario = context.Usuarios.First(e => e.idUsuario == idUsuario);
					context.Usuarios.Attach(usuario);
					usuario.IsLockedOut = false;
					usuario.FailedPasswordAttemptCount = 0;
					usuario.Modificado = DateTime.UtcNow;
					context.Usuarios.ApplyCurrentValues(usuario);
					context.SaveChanges();

					transaction.Complete();
					resultado = true;
				}
				catch (Exception )
				{
					transaction.Dispose();
				}
			}
			context.AcceptAllChanges();
			return resultado;
		}

		public Usuarios ValidarUsuario(string userName, string userPwd, int passwordAttemptWindow, int maxInvalidPasswordAttempts, ref bool isValid)
		{
			Usuarios usuario;
			isValid = false;
			var exepcion = new Exception();
			try
			{
				//aqui la validación es con el primero, se deberia validar si hay mas de uno?
				var passwordEncriptado = userPwd;
				if (EncriptarPassword() && userPwd != null)
				{
					passwordEncriptado = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userPwd, FormsAuthPasswordFormat.SHA1.ToString());
				}

				usuario = context.Usuarios.FirstOrDefault(e => e.UserName == userName && e.Eliminado == false && e.IsLockedOut == false);
				var pwd = string.Empty;
				if (usuario != null)
				{
					pwd = usuario.Pwd;
					if (CheckPassword(passwordEncriptado, pwd))
					{
						if (Convert.ToBoolean(usuario.IsApproved))
						{
							isValid = true;
							if (SetLastLoginDate(usuario.idUsuario, ref exepcion))
							{

							}
						}
					}
					else
					{
						UpdateFailureCount(usuario.idUsuario, passwordAttemptWindow, maxInvalidPasswordAttempts,
							"password");
					}
				}

				//LogError tLog = new LogError();
				//tLog.AsignarError("Validar Usuario: OK");

			}
			catch (Exception ex)
			{
				usuario = new Usuarios();
				usuario.idUsuario = 0;
				//ClassBitacora.RegistraExcepcion(ex);


			}
			return usuario;
		}

		public Usuarios UsuarioPorLogin(string userName)
		{
			Usuarios resultado;
			try
			{
				resultado = context.Usuarios.FirstOrDefault(e => e.UserName == userName && e.Eliminado == false);
			}
			catch (Exception )
			{
				resultado = new Usuarios();
				//ClassBitacora.RegistraExcepcion(ex);
			}
			return resultado;
		}

		public bool CambiarPassword(string userName, string userPwdAnterior, string userPwdNuevo, ref Exception ex)
		{
			Usuarios usuarioOriginal;
			bool resultado = false;
			try
			{
				var passwordEncriptadoAnterior = string.Empty;
				var passwordEncriptadoNuevo = string.Empty;
				if (EncriptarPassword() && userPwdAnterior != null)
				{
					passwordEncriptadoAnterior = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userPwdAnterior, FormsAuthPasswordFormat.SHA1.ToString());
					passwordEncriptadoNuevo = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(userPwdNuevo, FormsAuthPasswordFormat.SHA1.ToString());
				}
				else
				{
					throw new Exception("Error al encripar la información");
				}
				usuarioOriginal = context.Usuarios.FirstOrDefault(e => e.UserName == userName && e.Pwd == passwordEncriptadoAnterior);
				if (usuarioOriginal != null)
				{
					var success = false;
					for (int i = 0; i < Intentos; i++)
					{
						using (var transaction = new TransactionScope())
						{
							try
							{
								var registroOriginal = context.Usuarios.Single(c => c.idUsuario == usuarioOriginal.idUsuario);
								context.Usuarios.Attach(registroOriginal);
								registroOriginal.Modificado = DateTime.Now;
								registroOriginal.Pwd = passwordEncriptadoNuevo;
								registroOriginal.LastPasswordChangedDate = DateTime.Now;

								context.Usuarios.ApplyCurrentValues(registroOriginal);
								context.SaveChanges();

								transaction.Complete();
								success = true;
								break;
							}
							catch (Exception exe)
							{
								ex = exe;
								if (ex.GetType() != typeof(UpdateException))
								{
									// Bitacora.RegistraExcepcion(ex);
									break;
								}
							}
						}
					}
					if (success)
					{
						// Reset the context since the operation succeeded.
						context.AcceptAllChanges();
						resultado = true;
					}
				}
				else
				{
					throw new Exception("Contraseña incorrecta.");
				}
			}
			catch (Exception exepcion)
			{
				ex = exepcion;
			}
			return resultado;
		}

		public bool ReestablecerPassword(int idUsuario, ref Exception ex)
		{
			Usuarios registroOriginal;
			bool resultado = false;
			try
			{
				registroOriginal = context.Usuarios.FirstOrDefault(e => e.idUsuario == idUsuario);
				if (registroOriginal != null)
				{
					var success = false;
					var nuevopassword = registroOriginal.UserName;
					var passwordEncripatado = nuevopassword;
					for (int i = 0; i < Intentos; i++)
					{
						using (var transaction = new TransactionScope())
						{
							if (EncriptarPassword())
							{
								passwordEncripatado = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(
										nuevopassword, FormsAuthPasswordFormat.SHA1.ToString());
							}
							context.Usuarios.Attach(registroOriginal);
							registroOriginal.Modificado = DateTime.Now;
							registroOriginal.Pwd = passwordEncripatado;
							registroOriginal.LastPasswordChangedDate = DateTime.Now;
							context.Usuarios.ApplyCurrentValues(registroOriginal);
							context.SaveChanges();

							transaction.Complete();
							success = true;
							break;
						}
					}

					if (success)
					{
						// Reset the context since the operation succeeded.
						context.AcceptAllChanges();
						resultado = true;
					}
				}
			}
			catch (Exception exepcion)
			{
				ex = exepcion;
			}
			return resultado;
		}

		public Usuarios ObtUsuario(int idUsuario)
		{
			return context.Usuarios.FirstOrDefault(b => b.idUsuario == idUsuario);
		}

		#endregion

		#region disposable
		private bool disposedValue = false;
		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					context.Dispose();
					context = null;
				}
			}
			disposedValue = true;
		}
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
		#endregion

		#region CRUD Usuarios Permisos
		public List<UsuariosPermisos> ObtUsuariosPermisos(int idUsuario, int idModulo, ref Exception excepcion)
		{
			List<UsuariosPermisos> resultado = null;
			try
			{
				ObjectQuery<UsuariosPermisos> usuariospermisos = context.UsuariosPermisos;
				usuariospermisos.MergeOption = MergeOption.AppendOnly;
				var query = from s in usuariospermisos
							where s.idUsuario == idUsuario && s.idModulo == idModulo
							select s;
				resultado = query.ToList();
			}
			catch (Exception ex)
			{
				excepcion = ex;
			}
			return resultado;
		}

		public bool NuevoUsuarioPermiso(UsuariosPermisos usuariosPermisos, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.UsuariosPermisos.Any())
							id = context.UsuariosPermisos.Select(x => x.idUsuarioPermiso).Max();
						usuariosPermisos.idUsuarioPermiso = id + 1;

						context.AddToUsuariosPermisos(usuariosPermisos);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EditarUsuarioPermiso(UsuariosPermisos usuariosPermisos, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal =
							context.UsuariosPermisos.Single(c => c.idUsuarioPermiso == usuariosPermisos.idUsuarioPermiso);

						context.UsuariosPermisos.Attach(registroOriginal);
						context.UsuariosPermisos.ApplyCurrentValues(usuariosPermisos);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EliminarUsuarioPermiso(int idUsuarioPermiso, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{

						var registroOriginal =
							context.UsuariosPermisos.Single(c => c.idUsuarioPermiso == idUsuarioPermiso);
						context.DeleteObject(registroOriginal);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;

		}
		#endregion

		#region CRUD Aplicaciones

		public List<Aplicaciones> ObtAplicacionesXUniAdmin(int idUnidadAdministrativa)
		{
			ObjectQuery<Aplicaciones> aplicaciones = context.Aplicaciones;
			aplicaciones.MergeOption = MergeOption.AppendOnly;
			var query = from s in aplicaciones
						join sa in context.AplicacionesUA on s.idAplicacion equals sa.idAplicacion
						where sa.idUnidadAdministrativa == idUnidadAdministrativa && s.Eliminado == false
						select s;
			List<Aplicaciones> resultado = query.ToList();
			return resultado;
		}

		public bool NuevaAplicacion(Aplicaciones aplicacion, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.Aplicaciones.Any())
							id = context.Aplicaciones.Select(x => x.idAplicacion).Max();
						aplicacion.idAplicacion = id + 1;

						context.AddToAplicaciones(aplicacion);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EditarAplicacion(Aplicaciones aplicacion, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal =
							context.Aplicaciones.Single(c => c.idAplicacion == aplicacion.idAplicacion);
						aplicacion.Agregado = registroOriginal.Agregado;

						context.Aplicaciones.Attach(registroOriginal);
						context.Aplicaciones.ApplyCurrentValues(aplicacion);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EliminarAplicacion(int idaplicacion, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Aplicaciones.Single(c => c.idAplicacion == idaplicacion);
						var aplicacion = new Aplicaciones
						{
							idAplicacion = registroOriginal.idAplicacion,
							Agregado = registroOriginal.Agregado,
							Modificado = DateTime.Now,
							Eliminado = true,
							Clave = registroOriginal.Clave,
							Nombre = registroOriginal.Nombre,
							Descripcion = registroOriginal.Descripcion,
							Activa = registroOriginal.Activa,
							URL = registroOriginal.URL,
							Version = registroOriginal.Version,
							Icono = registroOriginal.Icono,
							Intentos = registroOriginal.Intentos
						};

						context.Aplicaciones.Attach(registroOriginal);
						context.Aplicaciones.ApplyCurrentValues(aplicacion);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		#endregion

		#region CRUD Modulos

		public List<Modulos> ObtModulosXAplicacion2(int paramidAplicacion)
		{
			ObjectQuery<Modulos> modulos = context.Modulos;
			modulos.MergeOption = MergeOption.AppendOnly;
			var query = from s in modulos
						join sa in context.Aplicaciones on s.idAplicacion equals sa.idAplicacion
						where sa.idAplicacion == paramidAplicacion && s.Eliminado == false && s.Padre == null
						select s;
			List<Modulos> resultado = query.ToList();
			return resultado;
		}

		public List<Modulos> ObtrModulosXPadre(int paramidPadre)
		{
			ObjectQuery<Modulos> modulos = context.Modulos;
			modulos.MergeOption = MergeOption.AppendOnly;
			var query = from s in modulos
						where s.Padre == paramidPadre && s.Eliminado == false
						select s;
			List<Modulos> resultado = query.ToList();
			return resultado;
		}

		public Modulos ObtenerModulo(int idModulo)
		{
			ObjectQuery<Modulos> modulos = context.Modulos;
			modulos.MergeOption = MergeOption.AppendOnly;
			var query = from s in modulos
						where s.idModulo == idModulo && s.Eliminado == false
						select s;
			Modulos resultado = query.FirstOrDefault();
			return resultado;
		}

		public bool EliminarModulo(int idModulo, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Modulos.Single(c => c.idModulo == idModulo);
						var modulo = new Modulos
						{
							idModulo = registroOriginal.idModulo,
							Agregado = registroOriginal.Agregado,
							Modificado = DateTime.Now,
							Eliminado = true,
							idAplicacion = registroOriginal.idAplicacion,
							Codigo = registroOriginal.Codigo,
							Descripcion = registroOriginal.Descripcion,
							MenuNavigateURL = registroOriginal.MenuNavigateURL,
							MenuToolTip = registroOriginal.MenuToolTip,
							MenuVisibleIndex = registroOriginal.MenuVisibleIndex,
							MenuImage = registroOriginal.MenuImage,
							Padre = registroOriginal.Padre
						};

						context.Modulos.Attach(registroOriginal);
						context.Modulos.ApplyCurrentValues(modulo);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool NuevoModulo(Modulos modulo, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.Modulos.Any())
							id = context.Modulos.Select(x => x.idModulo).Max();
						modulo.idModulo = id + 1;

						context.AddToModulos(modulo);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EditarModulo(Modulos modulo, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Modulos.Single(c => c.idModulo == modulo.idModulo);
						modulo.Agregado = registroOriginal.Agregado;

						context.Modulos.Attach(registroOriginal);
						context.Modulos.ApplyCurrentValues(modulo);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}


		public bool ActualizarIndexModulo(int idModulo, int index, ref Exception excepcion)
		{

			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Modulos.Single(c => c.idModulo == idModulo);
						context.Modulos.Attach(registroOriginal);
						registroOriginal.MenuVisibleIndex = index;
						context.Modulos.ApplyCurrentValues(registroOriginal);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		#endregion

		#region CRUD Roles

		public string ObtCodigoRol(ref Exception excepcion)
		{
			var resultado = string.Empty;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					int id = 0;
					try
					{
						if (context.Roles.Any())
							id = context.Roles.Select(x => x.idRol).Max();
						id = id + 1;
						resultado = string.Format("{0:000}", id);
						transaction.Complete();
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							break;
						}
					}
				}
			}
			return resultado;
		}

		public bool NuevoRol(Roles rol, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.Roles.Any())
							id = context.Roles.Select(x => x.idRol).Max();
						rol.idRol = id + 1;

						context.AddToRoles(rol);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EditarRol(Roles rol, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Roles.Single(c => c.idRol == rol.idRol);
						rol.Agregado = registroOriginal.Agregado;

						context.Roles.Attach(registroOriginal);
						context.Roles.ApplyCurrentValues(rol);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EliminarRol(int idRol, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal = context.Roles.Single(c => c.idRol == idRol);
						var rol = new Roles
						{
							idRol = registroOriginal.idRol,
							Agregado = registroOriginal.Agregado,
							Modificado = DateTime.Now,
							Eliminado = true,
							idUnidadAdministrativa = registroOriginal.idUnidadAdministrativa,
							idAplicacion = registroOriginal.idAplicacion,
							Codigo = registroOriginal.Codigo,
							Descripcion = registroOriginal.Descripcion
						};

						context.Roles.Attach(registroOriginal);
						context.Roles.ApplyCurrentValues(rol);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;

		}

		public Roles ObtRol(int idRol)
		{
			return context.Roles.FirstOrDefault(b => b.idRol == idRol);
		}

		#endregion

		#region CRUD Roles Permisos

        /// <summary>
        /// Obtiene el todos los roles disponibles  
        /// </summary>
        /// <returns></returns>
        public List<Roles> UbtRoles()
        {
            ObjectQuery<Roles> Roles = context.Roles;
            Roles.MergeOption = MergeOption.AppendOnly;
            var query = from s in Roles
                        where s.Eliminado == true
                        select s;
            List<Roles> resultado = query.ToList();
            return  resultado;
        }


		public bool NuevoRolPermiso(RolesPermisos rolesPermisos, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.RolesPermisos.Any())
							id = context.RolesPermisos.Select(x => x.idRolPermisos).Max();
						rolesPermisos.idRolPermisos = id + 1;

						context.AddToRolesPermisos(rolesPermisos);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EditarRolPermiso(RolesPermisos rolesPermisos, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						var registroOriginal =
							context.RolesPermisos.Single(c => c.idRolPermisos == rolesPermisos.idRolPermisos);

						context.RolesPermisos.Attach(registroOriginal);
						context.RolesPermisos.ApplyCurrentValues(rolesPermisos);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);

							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;
		}

		public bool EliminarRolPermiso(int idRolPermiso, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{

						var registroOriginal =
							context.RolesPermisos.Single(c => c.idRolPermisos == idRolPermiso);
						context.DeleteObject(registroOriginal);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception ex)
					{
						excepcion = ex;
						if (ex.GetType() != typeof(UpdateException))
						{
							// Bitacora.RegistraExcepcion(ex);
							break;
						}
					}
				}
			}
			if (success)
			{
				// Reset the context since the operation succeeded.
				context.AcceptAllChanges();
			}
			return success;

		}

        public bool AsignarRolAUsuario(int idUsuario, int idRol, ref Exception excepcion)
        {
            var success = false;
            for (int i = 0; i < Intentos; i++)
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        var resultado = context.spAsignarRolAUsuario(idUsuario, idRol);

                        transaction.Complete();
                        success = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        excepcion = ex;
                        if (ex.GetType() != typeof(UpdateException))
                        {
                            // Bitacora.RegistraExcepcion(ex);
                            break;
                        }
                    }
                }
            }
            if (success)
            {
                // Reset the context since the operation succeeded.
                context.AcceptAllChanges();
            }
            return success;
        }

        public bool QuitarRolAUsuario(int idUsuario, ref Exception excepcion)
        {
            var success = false;
            for (int i = 0; i < Intentos; i++)
            {
                using (var transaction = new TransactionScope())
                {
                    try
                    {

                        var resultado = context.spQuitarRolAUsuario(idUsuario);

                        transaction.Complete();
                        success = true;
                        break;
                    }
                    catch (Exception ex)
                    {
                        excepcion = ex;
                        if (ex.GetType() != typeof(UpdateException))
                        {
                            // Bitacora.RegistraExcepcion(ex);
                            break;
                        }
                    }
                }
            }
            if (success)
            {
                // Reset the context since the operation succeeded.
                context.AcceptAllChanges();
            }
            return success;
        }

		/// <summary>
		/// Obtiene el rol al que pertenece el usuario  
		/// </summary>
		/// <param name="idUsuario"></param>
		/// <returns></returns>
		public List<UsuariosRoles> UbtUsuarioRol(int idUsuario)
		{
			ObjectQuery<UsuariosRoles> usuariosRoles = context.UsuariosRoles;
			usuariosRoles.MergeOption = MergeOption.AppendOnly;
			var query = from s in usuariosRoles
						where s.idUsuario == idUsuario
						select s;
			List<UsuariosRoles> resultado = query.ToList();
			return resultado;
		}

		/// <summary>
		/// Obtiene una lista de los usuarios que pertenezca a ese Rol
		/// </summary>
		/// <param name="idRol"></param>
		/// <returns></returns>
		public List<UsuariosRoles> ObtUsuariosRol(int idRol)
		{
			ObjectQuery<UsuariosRoles> usuariosRoles = context.UsuariosRoles;
			usuariosRoles.MergeOption = MergeOption.AppendOnly;
			var query = from s in usuariosRoles
						where s.idRol == idRol
						select s;
			List<UsuariosRoles> resultado = query.ToList();
			return resultado;
		}

		public List<RolesPermisos> ObtRolesPermisos(int idRol)
		{
			ObjectQuery<RolesPermisos> permisosRoles = context.RolesPermisos;
			permisosRoles.MergeOption = MergeOption.AppendOnly;
			var query = from s in permisosRoles
						where s.idRol == idRol
						select s;
			List<RolesPermisos> resultado = query.ToList();
			return resultado;
		}

		#endregion
	}
}