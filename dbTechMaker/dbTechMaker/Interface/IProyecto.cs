using dbTechMaker.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace dbTechMaker.Interface
{

    internal interface IProyecto : IBase<Proyecto>
    {
        Proyecto Get(int id);

        DataTable Evento_info(int id);

        DataTable Proyectos_sugeridos(int id);

    }
}

