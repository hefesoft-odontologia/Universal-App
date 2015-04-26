using Hefesoft.Standard.Enumeradores;
using Hefesoft.Standard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hefesoft.Standard.Util.Collection.IEnumerable
{
    public class ListadoBase<T> : IEntidadBase
    {
        public string PartitionKey { get; set; }

        public string RowKey { get; set; }

        public string nombreTabla { get; set; }

        public bool generarIdentificador { get; set; }

        public IEnumerable<T> Listado { get; set; }

        public dynamic Adicional { get; set; }

        public bool? Activo { get; set; }
        public Estado_Entidad Estado_Entidad { get; set; }
        
    }
}
