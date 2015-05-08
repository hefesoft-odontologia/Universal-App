using Hefesoft.Standard.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace Hefesoft.Standard.Util
{
	internal static class Comun
	{
		internal static string[] partes;

		public static string Aplicativo
		{
			get
			{
				string fullName = Assembly.GetExecutingAssembly().FullName;
				return "";
			}
		}

		public static string IPMaquina
		{
			get
			{
				return "127.0.0.1";
			}
		}

		public static bool IsDesignTime
		{
			get
			{
				try
				{
				}
				catch
				{
				}
				return true;
			}
		}

		public static string Usuario
		{
			get
			{
				return string.Empty;
			}
		}

		static Comun()
		{
			string[] strArrays = new string[] { "01", "X", "A0", "8", "9F", "I", "95", "3", "15", "2", "41", "1", "61", "5", "2B", "B" };
			Comun.partes = strArrays;
		}

		public static void ActualizarAuditoria(IEntidadBase entidad)
		{
			if (entidad == null)
			{
				throw new ArgumentNullException("entidad");
			}
            //entidad.Modificado = DateTime.Now;
            //entidad.IPOrigen = Comun.IPMaquina;
            //entidad.Usuario = Comun.Usuario;
		}

		public static string AddSpacesToSentence(string text)
		{
			string str;
			if (!string.IsNullOrEmpty(text))
			{
				StringBuilder stringBuilder = new StringBuilder(text.Length * 2);
				stringBuilder.Append(text[0]);
				for (int i = 1; i < text.Length; i++)
				{
					if (char.IsUpper(text[i]))
					{
						stringBuilder.Append(' ');
						stringBuilder.Append(char.ToLower(text[i], CultureInfo.InvariantCulture));
					}
					else
					{
						stringBuilder.Append(text[i]);
					}
				}
				str = stringBuilder.ToString();
			}
			else
			{
				str = "";
			}
			return str;
		}

		public static void AjustarFormatoFecha(string cultura)
		{
			if (cultura == null)
			{
				throw new ArgumentNullException("cultura");
			}
			CultureInfo cultureInfo = (new CultureInfo(cultura)).Clone() as CultureInfo;
			cultureInfo.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
			cultureInfo.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
			cultureInfo.DateTimeFormat.LongDatePattern = "dd-MM-yyyy";
			Thread.CurrentThread.CurrentCulture = cultureInfo;
			Thread.CurrentThread.CurrentUICulture = cultureInfo;
		}

		public static void CopiarPropiedades<TOrigen, TDestino>(Type interfaz, TOrigen origen, TDestino destino)
		{
			if (interfaz == null)
			{
				throw new ArgumentNullException("interfaz");
			}
			if (origen == null)
			{
				throw new ArgumentNullException("origen");
			}
			if (destino == null)
			{
				throw new ArgumentNullException("destino");
			}
			Type type = typeof(TOrigen);
			Type type1 = typeof(TDestino);
			
            //foreach (PropertyInfo propertyInfo in interfaz.GetProperties().Where<PropertyInfo>(new Func<PropertyInfo, bool>(null, (PropertyInfo t) => (t.CanRead ? t.CanWrite : false))))
            //{
            //    object value = type.GetProperty(propertyInfo.Name).GetValue(origen, null);
            //    type1.GetProperty(propertyInfo.Name).SetValue(destino, value, null);
            //}
		}

		public static void CopiarPropiedades<TOrigen>(TOrigen origen, TOrigen destino)
		{
			if (origen == null)
			{
				throw new ArgumentNullException("origen");
			}
			if (destino == null)
			{
				throw new ArgumentNullException("destino");
			}
			
            //foreach (PropertyInfo propertyInfo in typeof(TOrigen).GetProperties().Where<PropertyInfo>(new Func<PropertyInfo, bool>(null, (PropertyInfo t) => (t.CanRead ? t.CanWrite : false))))
            //{
            //    object value = propertyInfo.GetValue(origen, null);
            //    propertyInfo.SetValue(destino, value, null);
            //}
		}

		internal static string EvaluarData(Exception error)
		{
			string str;
			try
			{
				if ((error.Data == null ? false : error.Data.Count > 0))
				{
					StringBuilder stringBuilder = new StringBuilder();
					foreach (DictionaryEntry datum in error.Data)
					{
						CultureInfo invariantCulture = CultureInfo.InvariantCulture;
						object[] key = new object[] { datum.Key, datum.Value };
						stringBuilder.AppendFormat(invariantCulture, "{0}=[{1}] | ", key);
					}
					str = stringBuilder.ToString();
				}
				else
				{
					str = string.Empty;
				}
			}
			catch
			{
				str = string.Empty;
			}
			return str;
		}

		internal static string EvaluarTarget(Exception error)
		{
			string empty;
			try
			{
				empty = string.Empty;
			}
			catch
			{
				empty = string.Empty;
			}
			return empty;
		}

		public static bool IsEmailValido(string email)
		{
			return (new Regex("^(([^<>()[\\]\\\\.,;:\\s@\\\"]+(\\.[^<>()[\\]\\\\.,;:\\s@\\\"]+)*)|(\\\".+\\\"))@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\])|(([a-zA-Z\\-0-9]+\\.)+[a-zA-Z]{2,}))$")).IsMatch(email);
		}

		public static byte ModuloOnce(long numero)
		{
			byte num;
			string str = numero.ToString(CultureInfo.InvariantCulture);
			byte[] numArray = new byte[] { 3, 7, 13, 17, 19, 23, 29, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97 };
			if ((int)numArray.Length < str.Length)
			{
				CultureInfo invariantCulture = CultureInfo.InvariantCulture;
				object[] length = new object[] { (int)numArray.Length };
				throw new ErrorException(string.Format(invariantCulture, "Algoritomo de módulo 11 sólo es soportado para números con {0} digitos", length));
			}
			int num1 = 0;
			byte num2 = 0;
			for (short i = Convert.ToInt16(str.Length - 1); i > -1; i = (short)(i - 1))
			{
				num1 = num1 + numArray[num2] * Convert.ToByte(str.Substring(i, 1), CultureInfo.InvariantCulture);
				num2 = (byte)(num2 + 1);
			}
			byte num3 = Convert.ToByte(num1 % 11);
			num = (num3 >= 2 ? Convert.ToByte(11 - num3) : num3);
			return num;
		}
	}
}