using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Extensiones
{
    public static class removerCaracteresEspeciales
    {
        public static string eliminarCaracteresEspeciales(this string s)
        {
            s = s.Replace("_", "");

            return s;
        }
    }
}
