using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Seguridad.Model;
using System.Net;
using System.Web.Configuration;
using System.Transactions;
using System.Data;
using System.IO;
using System.Xml.Serialization;

namespace Seguridad.Clases
{
	public class ClassBitacora : IDisposable
	{

		private static Seguridad2017Entities context = new Seguridad2017Entities();
		private const int Intentos = 3;

		public static string Serialize(object dataToSerialize)
		{
			if (dataToSerialize == null) return null;

			using (StringWriter stringwriter = new System.IO.StringWriter())
			{
				var serializer = new XmlSerializer(dataToSerialize.GetType());
				serializer.Serialize(stringwriter, dataToSerialize);
				return stringwriter.ToString();
			}
		}

		public static T Deserialize<T>(string xmlText)
		{
			if (String.IsNullOrWhiteSpace(xmlText)) return default(T);

			using (StringReader stringReader = new System.IO.StringReader(xmlText))
			{
				var serializer = new XmlSerializer(typeof(T));
				return (T)serializer.Deserialize(stringReader);
			}
		}

		#region CRUD Excepciones LOG


		public static bool RegistraExcepcion(Exception ex, int idUnidadAdministrativa_, int idModulo_, int idUsuario_)
		{
			var success = false;
			var hostName = Dns.GetHostName();
			IPHostEntry ipHostEntry = Dns.GetHostEntry(hostName);
			string usuarioIp_ = "HOST=" + hostName + "; IP=" + Convert.ToString(ipHostEntry.AddressList[ipHostEntry.AddressList.Length - 1]);

			int idAplicacion1 = -1;
			if (WebConfigurationManager.AppSettings["APLICACION_ID"] != null)
			{
				idAplicacion1 = Convert.ToInt32(WebConfigurationManager.AppSettings["APLICACION_ID"]);
			}

			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.ExcepcionesLog.Any())
							id = context.ExcepcionesLog.Select(x => x.idExcepcion).Max();
						var excepcion = new ExcepcionesLog
						{
							idExcepcion = id + 1,
							idUnidadAdministrativa = idUnidadAdministrativa_,
							idAplicacion = idAplicacion1,
							idModulo = idModulo_,
							idUsuario = idUsuario_,
							UsuarioIP = usuarioIp_,
							Mensaje = ex.Message,
							StackTrace = ex.StackTrace,
							TargetSite = ex.TargetSite.ToString(),
							HelpLink = ex.HelpLink,
							FechaHora = DateTime.Now
						};

						context.AddToExcepcionesLog(excepcion);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception inex)
					{
						// resultExcepcion = inex;
						if (inex.GetType() != typeof(UpdateException))
						{
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


		public static bool RegistraExcepcion(Exception ex, int idAplicacion)
		{
			var success = false;


			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.ExcepcionesLog.Any())
							id = context.ExcepcionesLog.Select(x => x.idExcepcion).Max();
						var excepcion = new ExcepcionesLog
						{
							idExcepcion = id + 1,
							idUnidadAdministrativa = -1,
							idAplicacion = idAplicacion,
							idModulo = -1,
							idUsuario = -1,
							UsuarioIP = string.Empty,
							Mensaje = ex.Message,
							StackTrace = ex.StackTrace,
							TargetSite = ex.TargetSite.ToString(),
							HelpLink = ex.HelpLink,
							FechaHora = DateTime.Now
						};

						context.AddToExcepcionesLog(excepcion);
						context.SaveChanges();

						transaction.Complete();
						success = true;
						break;
					}
					catch (Exception inex)
					{
						//  resultExcepcion = inex;
						if (inex.GetType() != typeof(UpdateException))
						{
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

		#region disposable
		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					context.Dispose();
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

		#region CRUD Registro eventos
		public static bool RegistraEvento(RegistroEventos registroEventos, ref Exception excepcion)
		{
			var success = false;
			for (int i = 0; i < Intentos; i++)
			{
				using (var transaction = new TransactionScope())
				{
					try
					{
						int id = 0;
						if (context.RegistroEventos.Any())
							id = context.RegistroEventos.Select(x => x.idRegistroEvento).Max();
						registroEventos.idRegistroEvento = id + 1;

						context.AddToRegistroEventos(registroEventos);
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
	}
}