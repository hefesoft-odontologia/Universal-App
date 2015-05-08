using System;
using System.Runtime.CompilerServices;

namespace Hefesoft.Standard.Util
{
	[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field, AllowMultiple=false)]
	public sealed class EnumDescriptionAttribute : Attribute
	{
		public string Description
		{
			get;
			set;
		}

		public bool Ocultar
		{
			get;
			set;
		}

		public Type TipoRecurso
		{
			get;
			set;
		}

		public EnumDescriptionAttribute()
		{
		}

		public EnumDescriptionAttribute(string description)
		{
			this.Description = description;
		}
	}
}