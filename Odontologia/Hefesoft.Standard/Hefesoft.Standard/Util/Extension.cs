using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Hefesoft.Standard.Util
{
	public static class Extension
	{
		public static IEnumerable<T> Aplanar<T>(this IEnumerable<T> fuente, Func<T, IEnumerable<T>> subColeccion)
		{
			foreach (T t in fuente)
			{
				yield return t;
				foreach (T t1 in subColeccion.Invoke(t).Aplanar<T>(subColeccion))
				{
					yield return t1;
				}
			}
		}

		private static string BuscarDescripcion(EnumDescriptionAttribute attribute)
		{
			string empty = string.Empty;
			if ((attribute == null ? false : !string.IsNullOrEmpty(attribute.Description)))
			{
				empty = attribute.Description;
				if (attribute.TipoRecurso != null)
				{
					PropertyInfo property = attribute.TipoRecurso.GetProperty(empty, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
					if (property != null)
					{
						empty = property.GetValue(null, null) as string;
					}
				}
			}
			return empty;
		}

		public static string GetDescription(this Enum value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			string str = value.ToString();
			EnumDescriptionAttribute enumDescriptionAttribute = value.GetEnumDescriptionAttribute();
			if (!string.IsNullOrEmpty(Extension.BuscarDescripcion(enumDescriptionAttribute)))
			{
				str = enumDescriptionAttribute.Description;
			}
			return str;
		}

		public static EnumDescriptionAttribute GetEnumDescriptionAttribute(this Enum value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			EnumDescriptionAttribute enumDescriptionAttribute = null;
			string str = value.ToString();
			FieldInfo field = value.GetType().GetField(str);
			EnumDescriptionAttribute[] customAttributes = field.GetCustomAttributes(typeof(EnumDescriptionAttribute), false) as EnumDescriptionAttribute[];
			if ((customAttributes == null ? false : (int)customAttributes.Length > 0))
			{
				enumDescriptionAttribute = customAttributes[0];
			}
			return enumDescriptionAttribute;
		}

		public static Array GetValues(this Enum e)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			return Extension.GetValues(e.GetType());
		}

		private static Array GetValues(Type tipo)
		{
			if (tipo == null)
			{
				throw new ArgumentNullException("tipo");
			}
			if (!tipo.IsEnum)
			{
				throw new ArgumentException("EL tipo de dato no es Enumerador");
			}
			List<Enum> enums = new List<Enum>();
			FieldInfo[] fields = tipo.GetFields(BindingFlags.Static | BindingFlags.Public);
			for (int i = 0; i < (int)fields.Length; i++)
			{
				FieldInfo fieldInfo = fields[i];
				enums.Add((Enum)Enum.Parse(tipo, fieldInfo.Name, false));
			}
			return enums.ToArray();
		}

		public static bool Has<T>(this Enum type, T value)
		{
			try
			{
			}
			catch
			{
			}
			return true;
		}

		public static bool Is<T>(this Enum type, T value)
		{
			try
			{
			}
			catch
			{
			}
			return true;
		}

		public static bool IsNumeric(this object valor)
		{
			bool flag;
			if (valor != null)
			{
				decimal num = new decimal(0);
				flag = decimal.TryParse(valor.ToString(), out num);
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static List<KeyValueTriplet<Enum, TNumericKey, string>> ToExtendedList<TNumericKey>(Type type)
		{
			return type.ToExtendedList<TNumericKey>(true);
		}

		public static List<KeyValueTriplet<Enum, TNumericKey, string>> ToExtendedList<TNumericKey>(this Type type, bool incluirOcultos)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsEnum)
			{
				throw new InvalidCastException("El tipo no es un enumerador");
			}
			List<KeyValueTriplet<Enum, TNumericKey, string>> keyValueTriplets = new List<KeyValueTriplet<Enum, TNumericKey, string>>();
			foreach (Enum value in Extension.GetValues(type))
			{
				EnumDescriptionAttribute enumDescriptionAttribute = value.GetEnumDescriptionAttribute();
				string str = value.ToString();
				bool flag = true;
				KeyValueTriplet<Enum, TNumericKey, string> keyValueTriplet = new KeyValueTriplet<Enum, TNumericKey, string>()
				{
					Key = value,
					NumericKey = (TNumericKey)Convert.ChangeType(value, typeof(TNumericKey), CultureInfo.InvariantCulture),
					Value = str
				};
				KeyValueTriplet<Enum, TNumericKey, string> keyValueTriplet1 = keyValueTriplet;
				if (enumDescriptionAttribute != null)
				{
					flag = (!enumDescriptionAttribute.Ocultar ? true : incluirOcultos);
					string str1 = Extension.BuscarDescripcion(enumDescriptionAttribute);
					if (!string.IsNullOrEmpty(str1))
					{
						keyValueTriplet1.Value = str1;
					}
				}
				if (flag)
				{
					keyValueTriplets.Add(keyValueTriplet1);
				}
			}
			return keyValueTriplets;
		}

		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static List<KeyValueTriplet<TKey, TNumericKey, string>> ToExtendedList<TKey, TNumericKey>(this Type type)
		where TKey : struct
		{
			return type.ToExtendedList<TKey, TNumericKey>(true);
		}

		public static List<KeyValueTriplet<TKey, TNumericKey, string>> ToExtendedList<TKey, TNumericKey>(this Type type, bool incluirOcultos)
		where TKey : struct
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsEnum)
			{
				throw new InvalidCastException("El tipo no es un enumerador");
			}
			if (typeof(TKey) != type)
			{
				throw new InvalidCastException("Debe coincidir el Typo con el generic de TKey");
			}
			List<KeyValueTriplet<TKey, TNumericKey, string>> keyValueTriplets = new List<KeyValueTriplet<TKey, TNumericKey, string>>();
			foreach (Enum value in Extension.GetValues(type))
			{
				EnumDescriptionAttribute enumDescriptionAttribute = value.GetEnumDescriptionAttribute();
				string str = value.ToString();
				bool flag = true;
				KeyValueTriplet<TKey, TNumericKey, string> keyValueTriplet = new KeyValueTriplet<TKey, TNumericKey, string>()
				{
					Key = (TKey)Enum.Parse(type, str, true),
					NumericKey = (TNumericKey)Convert.ChangeType(value, typeof(TNumericKey), CultureInfo.InvariantCulture),
					Value = str
				};
				KeyValueTriplet<TKey, TNumericKey, string> keyValueTriplet1 = keyValueTriplet;
				if (enumDescriptionAttribute != null)
				{
					flag = (!enumDescriptionAttribute.Ocultar ? true : incluirOcultos);
					string str1 = Extension.BuscarDescripcion(enumDescriptionAttribute);
					if (!string.IsNullOrEmpty(str1))
					{
						keyValueTriplet1.Value = str1;
					}
				}
				if (flag)
				{
					keyValueTriplets.Add(keyValueTriplet1);
				}
			}
			return keyValueTriplets;
		}

		public static string ToFormatString(this Exception valor)
		{
			string str;
			if (valor != null)
			{
				str = (new ServerErrorWrapper(valor)).ToString();
			}
			else
			{
				str = null;
			}
			return str;
		}

		public static string ToHtmlTable(this Exception valor)
		{
			string html;
			if (valor != null)
			{
				html = (new ServerErrorWrapper(valor)).ToHtml();
			}
			else
			{
				html = null;
			}
			return html;
		}

		public static IList ToList(this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			List<KeyValuePair<Enum, string>> keyValuePairs = new List<KeyValuePair<Enum, string>>();
			foreach (Enum value in Extension.GetValues(type))
			{
				keyValuePairs.Add(new KeyValuePair<Enum, string>(value, value.GetDescription()));
			}
			return keyValuePairs;
		}

        public static int enumToValue(object odontograma, Type enumerator)
        {
            int x = (int)Enum.Parse(enumerator, Enum.GetName(enumerator, odontograma), true);
            return x;
        }
	}
}