using dbTechMaker.Model;


using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dbTechMaker.Interface
{
    internal interface IListado_proyecto : IBase<Listado_proyecto>
    {
       Listado_proyecto Get(int id);

        int UpdateStatus (Listado_proyecto t, string status, string observation);
    }
}
