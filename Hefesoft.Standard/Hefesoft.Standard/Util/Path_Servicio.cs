using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public static class Path_Servicio
{
    public static bool modoPruebas { get; set; }

    public static string obtenerUrl()
    {
        if (modoPruebas)
        {
            return "http://localhost:3481/";
        }
        else
        {
            return "http://hefesoftdynamicbackend.azurewebsites.net/";
        }
    }

    public static string obtenerUrlServicio()
    {
        if (modoPruebas)
        {
            return "http://localhost:3481/api/";
        }
        else
        {
            return "http://hefesoftdynamicbackend.azurewebsites.net/api/";
        }
    }

    public static string obtenerUrlServicioPdf()
    {
        if (modoPruebas)
        {
            return "http://localhost:11274/api/";
        }
        else
        {
            return "http://hefesoftpdfendpoint.azurewebsites.net/api/";
        }
    }
}

