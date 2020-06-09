using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Seguridad.Clases
{
	public class SuiteMembershipUser : MembershipUser
	{

		public int SuiteIdUsuario { get; set; }
		public DateTime SuiteAgregado { get; set; }
		public DateTime? SuiteModificado { get; set; }
		public bool SuiteEliminado { get; set; }
		public int SuiteIdUnidadAdministrativa { get; set; }
		public int? SuiteIdAplicacion { get; set; }
		public string SuiteUserName { get; set; }
		public string SuiteNombre { get; set; }
		public string SuitePuesto { get; set; }
		public string SuiteTelefono { get; set; }
		public string SuiteEmail { get; set; }
		public bool SuiteActivo { get; set; }
		public string SuiteNotas { get; set; }
		public bool SuiteUsaBiometricos { get; set; }
		public int SuiteDiasVigenciaPassword { get; set; }

		public SuiteMembershipUser(
		   string providername,
		   string username,
		   object providerUserKey,
		   string email,
		   string passwordQuestion,
		   string comment,
		   bool isApproved,
		   bool isLockedOut,
		   DateTime creationDate,
		   DateTime lastLoginDate,
		   DateTime lastActivityDate,
		   DateTime lastPasswordChangedDate,
		   DateTime lastLockedOutDate,

		   int _IdUsuario,
		   DateTime _Agregado,
		   DateTime? _Modificado,
		   bool _Eliminado,
		   int _IdUnidadAdministrativa,
		   int? _IdAplicacion,
		   string _UserName,
		   string _Nombre,
		   string _Puesto,
		   string _Telefono,
		   string _Email,
		   bool _Activo,
		   string _Notas,
		   bool _UsaBiometricos,
		   int? _DiasVigenciaPassword

			) :
			base(
		   providername,
		   username,
		   providerUserKey,
		   email,
		   passwordQuestion,
		   comment,
		   isApproved,
		   isLockedOut,
		   creationDate,
		   lastLoginDate,
		   lastActivityDate,
		   lastPasswordChangedDate,
		   lastLockedOutDate
		   )
		{
			SuiteIdUsuario = _IdUsuario;
			SuiteAgregado = _Agregado;
			SuiteModificado = _Modificado;
			SuiteEliminado = _Eliminado;
			SuiteIdUnidadAdministrativa = _IdUnidadAdministrativa;
			SuiteIdAplicacion = _IdAplicacion;
			SuiteUserName = _UserName;
			SuiteNombre = _Nombre;
			SuitePuesto = _Puesto;
			SuiteTelefono = _Telefono;
			SuiteEmail = _Email;
			SuiteActivo = _Activo;
			SuiteNotas = _Notas;
			SuiteUsaBiometricos = _UsaBiometricos;
			SuiteDiasVigenciaPassword = Convert.ToInt32(_DiasVigenciaPassword);
		}
	}
}