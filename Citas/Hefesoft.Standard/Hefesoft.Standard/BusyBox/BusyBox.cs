using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.BusyBox
{
    public static class BusyBox
    {
        public static void UserControlCargando(bool cargando = true, string mensaje = "Cargando")
        {
            try
            {
                var busyVM = ServiceLocator.Current.GetInstance<Hefesoft.Standard.BusyBox.Busy>();
                busyVM.IsBusy = cargando;
                busyVM.Texto = mensaje;
            }
            catch
            { }
        }
    }
}
