using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Util.Common
{
    public class Colors
    {
        public static int byteArray2Int(byte[] bytes)
        {
            return BitConverter.ToInt32(bytes, 0);
        }

        public static int ConvertLittleEndian(byte[] array)
        {
            int pos = 0;
            int result = 0;
            foreach (byte by in array)
            {
                result |= (int)(by << pos);
                pos += 8;
            }
            return result;
        }

    }
}
