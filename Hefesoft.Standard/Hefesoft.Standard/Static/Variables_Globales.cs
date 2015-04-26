using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Static
{
    public static class Variables_Globales
    {
        public static Modo Modo { get; set; }


        public static string Bearer { get; set; }

        public static string PushId { get; set; }

        public static string pushChannelUri { get; set; }
    }

    public enum Modo
    {
        Produccion = 1,
        Pruebas = 2
    }
}
