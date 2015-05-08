using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Hefesoft.Standard.Util
{	
	public class ElementoListaDatos<TLlave>
	{	
		
		public string Descripcion
		{
			get;
			set;
		}
		
	
		public TLlave Id
		{
			get;
			set;
		}

		public ElementoListaDatos()
		{
		}

		public override string ToString()
		{
			return this.Descripcion;
		}
	}
}