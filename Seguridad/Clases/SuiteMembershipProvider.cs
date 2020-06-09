using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Seguridad.Clases
{
	public class SuiteMembershipProvider : MembershipProvider
	{
		private Seguridad.Clases.ClassSeguridad _classSeguridad;
		//public int IdUnidadAdministrativa;
		private int pMaxInvalidPasswordAttempts;
		private int pPasswordAttemptWindow;
		#region MembershipProvider Implementados
		public override bool RequiresQuestionAndAnswer
		{
			get { return false; }
		}

		/// <summary>
		/// Inicializa la instancia
		/// </summary>
		/// <param name="name"></param>
		/// <param name="config"></param>
		public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
		{
			base.Initialize(name, config);
			//IdUnidadAdministrativa = Convert.ToInt32(name); 
			pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
			pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
			_classSeguridad = new ClassSeguridad();
			//LogError tLog = new LogError();
			//tLog.AsignarError("Initialize: OK");
		}
		private string GetConfigValue(string configValue, string defaultValue)
		{
			if (String.IsNullOrEmpty(configValue))
				return defaultValue;

			return configValue;
		}
		/// <summary>
		/// Validar Usuario
		/// </summary>
		/// <param name="correo">Correo electronico del usuario</param>
		/// <param name="password">password del usuario</param>
		/// <returns>Verdadero en caso de correspondencia, falso en caso de no correspondencia o error</returns>
		public override bool ValidateUser(string userName, string userPwd)
		{
			var idUsuario = 0;
			bool isValid = false;
			//Para establecer la salt del password
			var passwordSalt = string.Empty;
			var passwordHash = userPwd.Trim();
			//Deshabilitamos de momento la validacion via hashig de pass+salt
			//passwordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(passwordHash+passwordSalt, "sha1");
			var ex = new Exception();

			//var user = _classSeguridad.ValidarUsuario(userName.Trim(), passwordHash, PasswordAttemptWindow, MaxInvalidPasswordAttempts, ref isValid);

			var classSeguridad1 = new ClassSeguridad();
			var user = classSeguridad1.ValidarUsuario(userName.Trim(), passwordHash, PasswordAttemptWindow, MaxInvalidPasswordAttempts, ref isValid);
			classSeguridad1.Dispose();
			if (user != null)
			{
				idUsuario = user.idUsuario;
			}
			else
			{
				return false;
			}

			//LogError tLog = new LogError();
			//tLog.AsignarError("Validate User: OK");
			return isValid;
		}

		/// <summary>
		/// Crea un nuevo usuario
		/// </summary>
		/// <param name="username">IdPersonal del cual se creará el Usuario</param>
		/// <param name="password">Contraseña</param>
		/// <param name="email">correo electronico del usuario</param>
		/// <param name="passwordQuestion">pregunta secreta</param>
		/// <param name="passwordAnswer">respuesta secreta</param>
		/// <param name="isApproved"></param>
		/// <param name="providerUserKey"></param>
		/// <param name="status">Estatus de creación del usuario</param>
		/// <returns>Un usuario de Sucop</returns>
		public override MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out MembershipCreateStatus status)
		{
			throw new Exception("The method or operation is not implemented.");
			//return this.CreateUser(int.Parse(username), password, email, passwordQuestion, passwordAnswer, out status);
		}
		/// <summary>
		/// Crea un nuevo usuario de Sucop
		/// </summary>
		/// <param name="idPersonal">IdPersonal del cual se crea el usuario</param>
		/// <param name="password">contraseña</param>
		/// <param name="correo">correo electronico, se usara como nombre de usuario</param>
		/// <param name="preguntaSecreta">pregunta secreta para recuperacion de contraseña</param>
		/// <param name="respuestaSecreta">respuesta secreta para recuperacion de contraseña</param>
		/// <param name="createStatus">estatus de creacion del usuario</param>
		/// <returns>Un usuario de Sucop</returns>
		public MembershipUser CreateUser()
		{
			throw new Exception("The method or operation is not implemented.");
			////Para establecer la salt del password
			//var passwordSalt = string.Empty;
			//var passwordHash = password.Trim();
			////Deshabilitamos de momento la validacion via hashig de pass+salt
			////passwordHash = FormsAuthentication.HashPasswordForStoringInConfigFile(passwordHash+passwordSalt, "sha1");            


			//SuiteMembershipUser user = null;
			//try
			//{
			//    var personalTableAdapter = new PersonalTableAdapter();
			//    var persona = personalTableAdapter.GetDataByIdPersonal(idPersonal);
			//    if (persona.Rows.Count == 1)
			//    {
			//        string nombrec = persona.Rows[0]["Nombre"].ToString().Trim() + " " + persona.Rows[0]["APaterno"].ToString().Trim() + " " + persona.Rows[0]["AMaterno"].ToString().Trim();
			//        _usuarioTableAdapter.Insert(correo, idPersonal, hash, preguntaSecreta, respuestaSecreta, idEstatus);
			//        user = user = new SuiteMembershipUser("SuiteMembershipProvider",
			//                                                nombrec,
			//                                                null,
			//                                                correo,
			//                                                preguntaSecreta,
			//                                                String.Empty,
			//                                                true,
			//                                                false,
			//                                                DateTime.Now,
			//                                                DateTime.Now,
			//                                                DateTime.Now,
			//                                                DateTime.Now,
			//                                                DateTime.Now,
			//                                                0,
			//                                                idPersonal,
			//                                                idEstatus,
			//                                                nombrec);
			//        createStatus = MembershipCreateStatus.Success;
			//    }
			//    else
			//    {
			//        createStatus = MembershipCreateStatus.ProviderError;
			//        user = null;
			//    }
			//}
			//catch (Exception ex)
			//{
			//    createStatus = MembershipCreateStatus.ProviderError;
			//    user = null;
			//    throw ex;
			//}
			//return user;
		}

		public override bool ChangePassword(string correo, string oldPassword, string newPassword)
		{
			throw new Exception("The method or operation is not implemented.");
			//string hashNuevo = FormsAuthentication.HashPasswordForStoringInConfigFile(newPassword, "sha1");

		}

		public override MembershipUser GetUser(string userName, bool userIsOnline)
		{
			// var usuario = _classSeguridad.UsuarioPorLogin(userName);
			var classSeguridad1 = new ClassSeguridad();
			var usuario = classSeguridad1.UsuarioPorLogin(userName);
			classSeguridad1.Dispose();
			var suiteMembershipUser = UsuarioToSuiteMembershipUser(usuario);
			if (suiteMembershipUser.IsLockedOut)
			{
				suiteMembershipUser = null;
			}
			return suiteMembershipUser;
		}

		public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override void UpdateUser(MembershipUser user)
		{
			this.UpdateUser((user as SuiteMembershipUser));
		}

		public void UpdateUser(SuiteMembershipUser user)
		{
			//var _user = SuiteMembershipUserToAccPersona(user);
			//var _userO = _accPersona.PorId(user.AccPersonaId);
			//_accPersona.Editar(_user, _userO);
		}

		public override string GetUserNameByEmail(string correo)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override int MinRequiredNonAlphanumericCharacters
		{
			get { return 0; }
		}

		public override int MinRequiredPasswordLength
		{
			get { return 4; }
		}

		#endregion MembershipProvider Implementados

		#region MembershipProvider NO_Implementados

		public override string ApplicationName
		{
			get
			{
				throw new Exception("The method or operation is not implemented.");
			}
			set
			{
				throw new Exception("The method or operation is not implemented.");
			}
		}


		public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override bool DeleteUser(string username, bool deleteAllRelatedData)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override bool EnablePasswordReset
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public override bool EnablePasswordRetrieval
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override int GetNumberOfUsersOnline()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override string GetPassword(string username, string answer)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override int MaxInvalidPasswordAttempts
		{
			get { return pMaxInvalidPasswordAttempts; }
		}

		public override int PasswordAttemptWindow
		{
			get { return pPasswordAttemptWindow; }
		}

		public override MembershipPasswordFormat PasswordFormat
		{
			get { return MembershipPasswordFormat.Hashed; }
		}

		public override string PasswordStrengthRegularExpression
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public override bool RequiresUniqueEmail
		{
			get { throw new Exception("The method or operation is not implemented."); }
		}

		public override string ResetPassword(string username, string answer)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public override bool UnlockUser(string userName)
		{
			var classSeguridad = new ClassSeguridad();
			var resultado = classSeguridad.UnlockUser(Convert.ToInt32(userName));
			return resultado;
		}

		private void UpdateFailureCount(string username, string failureType)
		{
			var classSeguridad = new ClassSeguridad();
			classSeguridad.UpdateFailureCount(Convert.ToInt32(username), PasswordAttemptWindow, MaxInvalidPasswordAttempts, failureType);

		}



		#endregion MembershipProvider NO_Implementados

		#region MembershipProvider Propios
		private Seguridad.Model.Usuarios SuiteMembershipUserToUsuario(SuiteMembershipUser suiteMembershipUser)
		{
			var usuario = new Seguridad.Model.Usuarios()
			{
				idUsuario = suiteMembershipUser.SuiteIdUsuario,
				Agregado = suiteMembershipUser.SuiteAgregado,
				Modificado = suiteMembershipUser.SuiteModificado,
				Eliminado = suiteMembershipUser.SuiteEliminado,
				idUnidadAdministrativa = suiteMembershipUser.SuiteIdUnidadAdministrativa,
				idAplicacion = suiteMembershipUser.SuiteIdAplicacion,
				UserName = suiteMembershipUser.SuiteUserName,
				Pwd = string.Empty,
				Nombre = suiteMembershipUser.SuiteNombre,
				Puesto = suiteMembershipUser.SuitePuesto,
				Telefono = suiteMembershipUser.SuiteTelefono,
				Email = suiteMembershipUser.SuiteEmail,
				Activo = suiteMembershipUser.SuiteActivo,
				Notas = suiteMembershipUser.SuiteNotas,
				UsaBiometricos = suiteMembershipUser.SuiteUsaBiometricos,
				DiasVigenciaPassword = suiteMembershipUser.SuiteDiasVigenciaPassword
			};

			return usuario;
		}
		//private SuiteMembershipUser UsuarioToSuiteMembershipUser(Suite.Comun.DAL.AccPersona accPersona)
		private SuiteMembershipUser UsuarioToSuiteMembershipUser(Seguridad.Model.Usuarios usuario)
		{
			var suiteMembershipUser = new SuiteMembershipUser("SuiteMembershipProvider",
				usuario.UserName, usuario.idUsuario, usuario.Email, string.Empty, string.Empty, true, false, usuario.Agregado,
				DateTime.Now,
				DateTime.Now,
				Convert.ToDateTime(usuario.LastPasswordChangedDate),
				DateTime.Now,
				usuario.idUsuario,
				usuario.Agregado,
				usuario.Modificado,
				usuario.Eliminado,
				usuario.idUnidadAdministrativa,
				usuario.idAplicacion,
				usuario.UserName,
				usuario.Nombre,
				usuario.Puesto,
				usuario.Telefono,
				usuario.Email,
				usuario.Activo,
				usuario.Notas,
				usuario.UsaBiometricos,
				usuario.DiasVigenciaPassword
			   );
			return suiteMembershipUser;
		}
		#endregion
	}
}