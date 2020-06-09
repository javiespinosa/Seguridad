using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace Seguridad
{
	public class LogError
	{
		private string sPath = @"C:/LogErrorSeguridad/";
		public LogError()
		{
			if (!Directory.Exists(sPath))
				Directory.CreateDirectory(sPath);
		}
		public void AsignarError(string Mensaje)
		{

			string MensajeOriginal = "";
			if (File.Exists(sPath + "LogErrorSeguridad.txt"))
				MensajeOriginal = File.ReadAllText(sPath + "LogErrorSeguridad.txt");
			StreamWriter sw = new StreamWriter(sPath + "LogErrorSeguridad.txt");
			sw.WriteLine(MensajeOriginal + Environment.NewLine + Mensaje);
			sw.Flush();
			sw.Close();
		}
	}
}