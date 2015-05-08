using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Hefesoft.Standard.Util
{
	[DataContract]
	public class ServerErrorWrapper
	{
		[DataMember]
		public string Aplicativo
		{
			get;
			set;
		}

		[DataMember]
		public string Data
		{
			get;
			set;
		}

		[DataMember]
		public ServerErrorWrapper InnerServerError
		{
			get;
			set;
		}

		[DataMember]
		public string IPMaquina
		{
			get;
			set;
		}

		[DataMember]
		public Collection<string> MensajesSql
		{
			get;
			set;
		}

		[DataMember]
		public string Message
		{
			get;
			set;
		}

		[DataMember]
		public string Solucion
		{
			get;
			set;
		}

		[DataMember]
		public string Source
		{
			get;
			set;
		}

		[DataMember]
		public string StackTrace
		{
			get;
			set;
		}

		[DataMember]
		public string Target
		{
			get;
			set;
		}

		[DataMember]
		public string Tipo
		{
			get;
			set;
		}

		[DataMember]
		public string Usuario
		{
			get;
			set;
		}

		public ServerErrorWrapper(Exception innerException)
		{
			this.Solucion = "Revise el detalle y comuniquese con su soporte técnico";			
			this.MensajesSql = new Collection<string>();
			this.GenerarPropiedades(innerException);
		}

		public ServerErrorWrapper(string mensaje, Exception innerException)
		{
			this.Solucion = "Revise el detalle y comuniquese con su soporte técnico";			
			this.MensajesSql = new Collection<string>();
			this.GenerarPropiedades(innerException);
			this.Message = mensaje;
		}

		public ServerErrorWrapper(string mensaje, string solucion, Exception innerException)
		{
			this.Solucion = "Revise el detalle y comuniquese con su soporte técnico";			
			this.MensajesSql = new Collection<string>();			
			this.GenerarPropiedades(innerException);
			this.Message = mensaje;
			this.Solucion = solucion;
		}

		public void Copy(ServerErrorWrapper origen)
		{
			if (origen == null)
			{
				throw new ArgumentNullException("origen");
			}
			this.Solucion = origen.Solucion;
			this.Usuario = origen.Usuario;
			this.Aplicativo = origen.Aplicativo;
			this.Message = origen.Message;
			this.Tipo = origen.Tipo;
			this.Data = origen.Data;
			this.IPMaquina = origen.IPMaquina;
			this.Source = origen.Source;
			this.StackTrace = origen.StackTrace;
			this.Target = origen.Target;
			this.MensajesSql = origen.MensajesSql;
			if (origen.InnerServerError != null)
			{
				this.InnerServerError = (ServerErrorWrapper)origen.InnerServerError.MemberwiseClone();
			}
		}

		private void GenerarPropiedades(Exception error)
		{
			this.Message = error.Message;
			this.Tipo = error.GetType().ToString();			
			this.StackTrace = error.StackTrace;			
			if (error.InnerException != null)
			{
				this.InnerServerError = new ServerErrorWrapper(error.InnerException);
			}
			FaultException<ServerErrorWrapper> faultException = error as FaultException<ServerErrorWrapper>;
			if ((faultException == null ? true : faultException.Detail == null))
			{
				ErrorException errorException = error as ErrorException;
				if (errorException != null)
				{
					this.Usuario = errorException.Usuario;
					this.Aplicativo = errorException.Aplicativo;
					this.Solucion = errorException.Solucion;
					this.IPMaquina = errorException.IPMaquina;
				}
			}
			else
			{
				this.Copy(faultException.Detail);
			}
		}

		public string ToHtml()
		{
			StringBuilder stringBuilder = new StringBuilder();
			ServerErrorWrapper innerServerError = this;
			stringBuilder.Append("<TABLE cellSpacing='0' cellPadding='0' border='0'>");
			while (innerServerError != null)
			{
				object[] message = new object[] { innerServerError.Message, innerServerError.Tipo, innerServerError.Usuario, innerServerError.IPMaquina, innerServerError.Aplicativo, innerServerError.Source, innerServerError.Target, innerServerError.Data, innerServerError.Solucion, innerServerError.StackTrace };
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "<TR><TD colspan='3'><b>{0}</b></TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Clase:</TD><TD>{1}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Usuario:</TD><TD>{2}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>IP Origen:</TD><TD>{3}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Aplicacion:</TD><TD>{4}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Origen:</TD><TD>{5}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Método:</TD><TD>{6}</TD></TR>zzzz<TR><TD>&nbsp;&nbsp;</TD><TD>Complementos:</TD><TD>{7}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD>Solucion:</TD><TD>{8}</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD colspan='2'>Cola:</TD></TR><TR><TD>&nbsp;&nbsp;</TD><TD colspan='2'>{9}</TD></TR>", message);
				if (innerServerError.MensajesSql.Count > 0)
				{
					StringBuilder stringBuilder1 = new StringBuilder();
					bool flag = true;
					foreach (string mensajesSql in innerServerError.MensajesSql)
					{
						if (flag)
						{
							flag = false;
							CultureInfo invariantCulture = CultureInfo.InvariantCulture;
							message = new object[] { mensajesSql };
							stringBuilder1.AppendFormat(invariantCulture, "<TR><TD>&nbsp;&nbsp;</TD><TD>Mensajes SQL:</TD><TD>{0}</TD></TR>", message);
						}
						else
						{
							CultureInfo cultureInfo = CultureInfo.InvariantCulture;
							message = new object[] { mensajesSql };
							stringBuilder1.AppendFormat(cultureInfo, "<TR><TD>&nbsp;&nbsp;</TD><TD></TD>&nbsp;<TD>{0}</TD></TR>", message);
						}
					}
					stringBuilder.Replace("zzzz", stringBuilder1.ToString());
				}
				else
				{
					stringBuilder.Replace("zzzz", string.Empty);
				}
				innerServerError = innerServerError.InnerServerError;
			}
			stringBuilder.Append("</TABLE>");
			stringBuilder.Replace(Environment.NewLine, string.Empty);
			return stringBuilder.ToString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (ServerErrorWrapper i = this; i != null; i = i.InnerServerError)
			{
				object[] message = new object[] { i.Message, i.Tipo, i.Usuario, i.IPMaquina, i.Aplicativo, i.Source, i.Target, i.Data, i.Solucion, i.StackTrace };
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}\r\n  Clase:        {1}\r\n  Usuario:      {2}\r\n  IP Origen:    {3}\r\n  Aplicacion:   {4}\r\n  Origen:       {5}\r\n  Método:       {6}zzzz\r\n  Complementos: {7}\r\n  Solucion:     {8}\r\n  Cola:\r\n{9}\r\n\t------------------ // ------------------\r\n", message);
				if (i.MensajesSql.Count > 0)
				{
					StringBuilder stringBuilder1 = new StringBuilder();
					bool flag = true;
					foreach (string mensajesSql in i.MensajesSql)
					{
						if (flag)
						{
							flag = false;
							CultureInfo invariantCulture = CultureInfo.InvariantCulture;
							message = new object[] { mensajesSql };
							stringBuilder1.AppendFormat(invariantCulture, "\r\n  Mensajes SQL: {0}", message);
						}
						else
						{
							CultureInfo cultureInfo = CultureInfo.InvariantCulture;
							message = new object[] { mensajesSql };
							stringBuilder1.AppendFormat(cultureInfo, "\r\n                {0}", message);
						}
					}
					stringBuilder.Replace("zzzz", stringBuilder1.ToString());
				}
				else
				{
					stringBuilder.Replace("zzzz", string.Empty);
				}
			}
			return stringBuilder.ToString();
		}
	}
}