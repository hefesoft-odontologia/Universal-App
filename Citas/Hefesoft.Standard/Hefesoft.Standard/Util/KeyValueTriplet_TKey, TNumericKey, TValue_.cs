using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Hefesoft.Standard.Util
{
	public class KeyValueTriplet<TKey, TNumericKey, TValue>
	{
		public TKey Key
		{
			get;
			internal set;
		}

		public TNumericKey NumericKey
		{
			get;
			internal set;
		}

		public TValue Value
		{
			get;
			internal set;
		}

		public KeyValueTriplet()
		{
		}

		public KeyValueTriplet(TKey key, TNumericKey numericKey, TValue value)
		{
			this.Key = key;
			this.NumericKey = numericKey;
			this.Value = value;
		}

		public override bool Equals(object obj)
		{
			bool flag;
			flag = (!object.ReferenceEquals(obj, null) ? this.Equals((KeyValueTriplet<TKey, TNumericKey, TValue>)obj) : false);
			return flag;
		}

		public bool Equals(KeyValueTriplet<TKey, TNumericKey, TValue> value)
		{
			bool flag;
			flag = (!object.ReferenceEquals(value, null) ? this.Key.Equals(value.Key) : false);
			return flag;
		}

		public override int GetHashCode()
		{
			int hashCode;
			try
			{
				hashCode = this.Key.GetHashCode();
			}
			catch
			{
				hashCode = 0;
			}
			return hashCode;
		}

		public static bool operator ==(KeyValueTriplet<TKey, TNumericKey, TValue> r1, KeyValueTriplet<TKey, TNumericKey, TValue> r2)
		{
			bool flag;
			flag = (!object.ReferenceEquals(r1, null) ? r1.Equals(r2) : object.ReferenceEquals(r2, null));
			return flag;
		}

		public static bool operator !=(KeyValueTriplet<TKey, TNumericKey, TValue> r1, KeyValueTriplet<TKey, TNumericKey, TValue> r2)
		{
			return !(r1 == r2);
		}

		public override string ToString()
		{
			CultureInfo invariantCulture = CultureInfo.InvariantCulture;
			object[] key = new object[] { this.Key, this.NumericKey, this.Value };
			return string.Format(invariantCulture, "[{0}, {1}, {2}]", key);
		}
	}
}