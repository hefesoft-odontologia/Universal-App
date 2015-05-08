using System;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace Hefesoft.Standard.Util
{
	[CollectionDataContract]
	public class ListaDatosCollection<TLlave> : KeyedCollection<TLlave, ElementoListaDatos<TLlave>>
	{
		public ListaDatosCollection()
		{
		}

		protected override TLlave GetKeyForItem(ElementoListaDatos<TLlave> item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			return item.Id;
		}
	}
}