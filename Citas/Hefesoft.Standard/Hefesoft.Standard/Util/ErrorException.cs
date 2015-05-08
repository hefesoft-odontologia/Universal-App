using System;

namespace Hefesoft.Standard.Util
{
	public class ErrorException : Exception
	{
		private string stSolucion = "Revise el detalle y comuniquese con su soporte técnico";

		private string stUsuario = Comun.Usuario;

		private string stAplicativo = Comun.Aplicativo;

		private string stIPMaquina = Comun.IPMaquina;

		public string Aplicativo
		{
			get
			{
				return this.stAplicativo;
			}
		}

		public string IPMaquina
		{
			get
			{
				return this.stIPMaquina;
			}
		}

		public string Solucion
		{
			get
			{
				return this.stSolucion;
			}
		}

		public string Usuario
		{
			get
			{
				return this.stUsuario;
			}
		}

		public ErrorException()
		{
		}

		public ErrorException(string mensaje, Exception innerException) : base(mensaje, innerException)
		{
		}

		public ErrorException(string mensaje) : base(mensaje)
		{
		}

		public ErrorException(string mensaje, string solucion, Exception innerException) : base(mensaje, innerException)
		{
			this.stSolucion = solucion;
		}

		public ErrorException(string mensaje, string solucion) : base(mensaje)
		{
			this.stSolucion = solucion;
		}
	}
}