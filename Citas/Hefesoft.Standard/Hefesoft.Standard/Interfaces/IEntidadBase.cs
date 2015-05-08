using Hefesoft.Standard.Enumeradores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Interfaces
{
    public interface IEntidadBase
    {
        string PartitionKey { get; set; }
        string RowKey { get; set; }
        string nombreTabla { get; set; }
        bool generarIdentificador { get; set; }
        bool? Activo { get; set; }
        Estado_Entidad Estado_Entidad { get; set; }

    }
}
