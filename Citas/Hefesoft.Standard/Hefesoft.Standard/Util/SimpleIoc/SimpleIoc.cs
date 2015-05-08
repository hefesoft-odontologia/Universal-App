using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Util.SimpleIoc
{
    public static class RegistrarClase
    {
        public static void registrarClaseUI<T>() where T : class
        {
            if (!GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.IsRegistered<T>())
            {
                GalaSoft.MvvmLight.Ioc.SimpleIoc.Default.Register<T>(true);
            }
        }

    }    
}
